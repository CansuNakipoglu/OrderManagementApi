using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Business.Services.Abstracts;
using OrderManagement.Core.DTOs.User;
using OrderManagement.Core.Entities;
using OrderManagement.Core.Enums;
using OrderManagement.Data.Repositroies.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderManagement.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;

        public UserService(IConfiguration configuration, IGenericRepository<User> repository, IUnitOfWork unitOfWork, IEncryptionService encryptionService)
        {
            _configuration = configuration;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
        }

        public UserResponseDTO RegisterUser(RegisterUserRequestDTO registerUserRequest)
        {
            try
            {
                var encryptedPassword = _encryptionService.Encrypt(registerUserRequest.Password);

                var user = new User
                {
                    Email = registerUserRequest.Email,
                    FullName = registerUserRequest.FullName,
                    Password = encryptedPassword,
                    Role = UserType.Customer
                };

                _repository.Add(user);
                _unitOfWork.Commit();

                return new UserResponseDTO()
                {
                    Token = GenerateToken(user.Id, user.Role),
                    Message = "Başarıyla Kayıt Oldunuz."
                };
            }
            catch (Exception ex)
            {
                return new UserResponseDTO()
                {
                    Token = string.Empty,
                    Message = "Kayıt Esnasında Bir Problemle Karşılaşıldı. Lütfen Tekrar Deneyiniz"
                };
            }
        }

        public UserResponseDTO LoginUser(LoginUserRequestDTO loginUserRequest)
        {
            try
            {
                var user = _repository.FindBy(x => x.Email == loginUserRequest.Email).FirstOrDefault();

                if (user == null || _encryptionService.Decrypt(user.Password) != loginUserRequest.Password)
                {
                    return new UserResponseDTO()
                    {
                        Token = string.Empty,
                        Message = "Hatalı Mail Yada Şifre"
                    };
                }

                return new UserResponseDTO()
                {
                    Token = GenerateToken(user.Id, user.Role),
                    Message = "Başarıyla Giriş Yaptınız."
                };
            }
            catch (Exception ex)
            {
                return new UserResponseDTO()
                {
                    Token = string.Empty,
                    Message = "Giriş İşlemi Esnasında Bir Problemle Karşılaşıldı. Lütfen Tekrar Deneyiniz"
                };
            }
        }

        private string GenerateToken(int userId, UserType userType)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userType.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
