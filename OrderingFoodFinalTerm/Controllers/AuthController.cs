using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingFoodFinalTerm.Interface;
using System.Net;

namespace OrderingFoodFinalTerm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDTO request)
        {
            var checkExists = _userRepository.GetUserByName(request.Username);
            if(checkExists != null)
            {
                return CustomResult("Tài khoản đã tồn tại", HttpStatusCode.BadRequest);
            }
            if(request.Password != request.ConfirmPassword)
            {
                return CustomResult("Mật khẩu không trùng khớp", HttpStatusCode.BadRequest);
            }
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new UserDTO
            {
                Username = request.Username,
                Password = hashPassword,
                Phonenumber = request.Phonenumber

            };
            var result = _userRepository.CreateUser(newUser);
            if (!result)
            {
                return CustomResult("Tạo tài khoản không thành công", HttpStatusCode.BadRequest);
            }
            else
            {
                return CustomResult("Tạo thành công", HttpStatusCode.OK);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO request)
        {
            var _user = _userRepository.GetUserByName(request.Username);
            if(_user == null || !_userRepository.ValidatePassword(_user, request.Password))
            {
                return CustomResult("Tài khoản hoặc mật khẩu không hợp lệ", HttpStatusCode.BadRequest);
            }
            var token = _userRepository.CreateToken(_user);
            return CustomResult(new {Token = token}, HttpStatusCode.OK);
        }
    }
}
