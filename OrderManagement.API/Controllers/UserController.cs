using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Business.Services.Abstracts;
using OrderManagement.Core.DTOs.User;
using OrderManagement.Core.Enums;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public UserResponseDTO Login([FromBody] LoginUserRequestDTO request)
        {
            return _userService.LoginUser(request);
        }

        [HttpPost("register")]
        public UserResponseDTO Register([FromBody] RegisterUserRequestDTO request)
        {
            return _userService.RegisterUser(request);
        }
    }
}
