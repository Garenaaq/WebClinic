﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebClinic.Models;

namespace WebClinic.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ClinicContext _db;
        private readonly IHttpContextAccessor _context;
        private List<string> listWithGender = new List<string>() { "Мужской", "Женский" };
        public AuthorizationController(ClinicContext db, IHttpContextAccessor context) 
        {
            _context = context;
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _context.HttpContext?.Session.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User objUser)
        {
            string loginSHA256 = ComputeHash(objUser.Login);
            string passSHA256 = ComputeHash(objUser.Pass);

            var objUserBd = _db.Users.FirstOrDefault(user => user.Login == loginSHA256 && user.Pass == passSHA256);

            if (objUserBd == null)
            {
                ModelState.AddModelError("Login", "Некорректно записан логин или пароль");
                ModelState.AddModelError("Pass", "Некорректно записан логин или пароль");
                return View();
            }

            _context.HttpContext.Session.SetInt32("idUser", objUserBd.Id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult RegistrationPatient() 
        {
            FillViewBagWithGender();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrationPatient(Patient objPatient, string? Login, string? Pass, string phone)
        {
            FillViewBagWithGender();

            if (!ModelState.IsValid)
            {
                ViewBag.Login = Login;
                ViewBag.Pass = Pass;
                return View(objPatient);
            }

            User objUser = new User();
            objUser.Login = ComputeHash(Login);
            objUser.Pass = ComputeHash(Pass);
            objUser.Role = "Пациент";

            var phoneNumber = new Phonebook { Phone = phone };

            _db.Phonebooks.Add(phoneNumber);

            objUser.Phonebooks.Add(phoneNumber);

            try
            {
                _db.Users.Add(objUser);
                _db.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Login = "Данный логин уже занят!";
                ViewBag.Pass = Pass;
                return View(objPatient);
            }

            var objUserDb = _db.Users.First(u => u.Login == ComputeHash(Login));

            objPatient.FkUsersNavigation = objUserDb;

            _db.Patients.Add(objPatient);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Вы были успешно зарегистрированны";
            return RedirectToAction("Index");
        }

        private void FillViewBagWithGender()
        {
            ViewBag.listWithGender = listWithGender.Select(sex => new SelectListItem()
            {
                Text = sex,
                Value = sex
            });

        }

        private string ComputeHash(string rawData)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
 
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
