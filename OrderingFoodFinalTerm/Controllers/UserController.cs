using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingFoodFinalTerm.DTO;
using OrderingFoodFinalTerm.Interface;
using OrderingFoodFinalTerm.Repository;
using System.Net;

namespace OrderingFoodFinalTerm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {

                return CustomResult(_userRepository.GetAll(), HttpStatusCode.OK);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var res = _userRepository.GetUserById(id);
                if(res == null)
                {
                    return CustomResult("Không tìm thấy người dùng",HttpStatusCode.NotFound);
                }
                return CustomResult(res,HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message,HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                _userRepository.DeleteUser(id);
                return CustomResult(HttpStatusCode.OK);
            }catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("Search")]
        public IActionResult Search(string? key)
        {
            try
            {
                var res = _userRepository.Search(key);
                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
