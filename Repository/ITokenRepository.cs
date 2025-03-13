using Microsoft.AspNetCore.Identity;

namespace AppointmentManagement.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> list);
    }
}
