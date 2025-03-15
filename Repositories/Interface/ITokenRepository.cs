using Microsoft.AspNetCore.Identity;

namespace AppointmentManagement.Repository.Interface
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> list);
    }
}
