using IdentityService.DataAccessLayer.Contexts;
using IdentityService.Interfaces;
using IdentityService.Records;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace IdentityService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly UserContext _userContext;

        public AuthenticationService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public bool AuthenticateUserDetails(LoginDto dto)
        {
            var storedPasswordHash = _userContext.User?.Where(u => u.UserName == dto.Username).Select(u => u.Password).FirstOrDefault();
            
            if (storedPasswordHash == ComputeSha256Hash(dto.Password))
            {
                return true;
            }
            return false;
        }

        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
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
