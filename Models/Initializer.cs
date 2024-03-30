using System.Security.Cryptography;
using System.Text;

namespace WebClinic.Models
{
    public class Initializer
    {
        public async static Task Initialize(ClinicContext context)
        {
            try
            {
                var md5 = MD5.Create();
                User user = new User()
                {
                    Login = ComputeHash("admin"),
                    Pass = ComputeHash("admin"),
                    Role = "админ",
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex) { }
        }

        private static string ComputeHash(string rawData)
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
