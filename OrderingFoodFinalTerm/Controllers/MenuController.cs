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
    public class MenuController : BaseController
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        // Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return CustomResult(_menuRepository.GetAll(), HttpStatusCode.OK);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        //Get By Id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var data = _menuRepository.GetById(id);
                if (data != null)
                {
                    return CustomResult(data, HttpStatusCode.OK);
                }
                return CustomResult(HttpStatusCode.NotFound);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }
        //Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _menuRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Add(MenuDTO menu)
        {
            try 
            {
                _menuRepository.Add(menu);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost("Product")]
        public IActionResult AddProduct(MenuProductDTO request)
        {
            try
            {
                // Nếu không có menu id 
                if (_menuRepository.CheckExistMenu(request.MenuId) == false)
                {
                    return CustomResult("Id menu không tìm thấy", HttpStatusCode.NotFound);
                }
                // lấy id của menu mình tìm, tham chiếu đến list product trong menu,
                // nếu id truyền vào = id product => true
                var product = _menuRepository.GetById(request.MenuId)
                              .Products.Any(c => c.Id == request.ProductId);
                if (product)
                {
                    return CustomResult("Sản phẩm này đã tồn tại", HttpStatusCode.BadRequest);
                }

                _menuRepository.AddProduct(request);

                return CustomResult("Thêm thành công",HttpStatusCode.OK);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);

            }
        }

        [HttpDelete("Product")]
        public IActionResult DeleteProduct(MenuProductDTO request)
        {
            try
            {
                // Nếu không có menu id 
                if (_menuRepository.CheckExistMenu(request.MenuId) == false)
                {
                    return CustomResult("Id menu không tìm thấy", HttpStatusCode.NotFound);
                }
                // lấy id của menu mình tìm, tham chiếu đến list product trong menu,                
                // nếu id truyền vào = id product => true
                var product = _menuRepository.GetById(request.MenuId)
                              .Products.Any(c => c.Id == request.ProductId);
                if (product)
                {
                    _menuRepository.RemoveProduct(request);
                    return CustomResult("Xóa thành công", HttpStatusCode.OK);
                }
                return CustomResult("Không tìm thấy sản phẩm", HttpStatusCode.BadRequest);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(MenuDTO menu, int id)
        {

            if (id != menu.Id)
            {
                return CustomResult("Id menu không tìm thấy", HttpStatusCode.NotFound);
            }
            try
            {
                _menuRepository.Update(menu);
                return CustomResult("Update thành công", HttpStatusCode.OK);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError); 
            }
        }

        [HttpGet("Search")]
        public IActionResult Search(string? key)
        {
            try
            {
                var res = _menuRepository.Search(key);
                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
