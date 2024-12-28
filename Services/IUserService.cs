using LavantellAPIS.Models.Request;
using LavantellAPIS.Models.Response;

namespace LavantellAPIS.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);

    }
}
