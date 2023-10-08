using IdentityService.Records;

namespace IdentityService.Interfaces
{
    public interface IAuthenticationService
    {
        public bool AuthenticateUserDetails(LoginDto dto);
        public string ComputeSha256Hash(string rawData);
    }
}
