using OrderManagement.Core.DTOs.User;
using OrderManagement.Core.Enums;

namespace OrderManagement.Business.Services.Abstracts
{
    public interface IUserService
    {
        UserResponseDTO LoginUser(LoginUserRequestDTO loginUserRequest);
        UserResponseDTO RegisterUser(RegisterUserRequestDTO registerUserRequest);
    }
}
