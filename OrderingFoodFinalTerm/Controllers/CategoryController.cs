using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingFoodFinalTerm.Interface;
using OrderingFoodFinalTerm.Repository;
using OrderingFoodFinalTerm.DTO;
using Microsoft.AspNetCore.Authorization;
using CoreApiResponse;
using System.Net;

namespace OrderingFoodFinalTerm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        // Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return CustomResult(_categoryRepository.GetAll(), HttpStatusCode.OK);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        // Get by id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _categoryRepository.GetById(id);
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

        // update
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CategoryDTO category)
        {
            if(category == null)
            {
                return CustomResult(ModelState,HttpStatusCode.BadRequest);
            }
            if (id != category.Id)
            {
                return CustomResult(ModelState,HttpStatusCode.BadRequest);
            }
            try
            {
                _categoryRepository.Update(category);
                return CustomResult("Update thành công",HttpStatusCode.NoContent);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoryRepository.Delete(id);
                return CustomResult("Xóa thành công", HttpStatusCode.OK);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        //Post
        [HttpPost]
        public IActionResult Add(CategoryDTO category)
        {
            try
            {
                var _category = _categoryRepository.GetAll()
                    .Where(c => c.CategoryName.Trim().ToUpper() == category.CategoryName.Trim().ToUpper())
                    .FirstOrDefault();
                // SP tồn tại
                if (_category != null)
                {
                    return CustomResult("Loại hàng đã tồn tại", HttpStatusCode.BadRequest);
                }
                _categoryRepository.Add(category);
                return CustomResult("Thêm thành công", HttpStatusCode.OK);
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
                var res = _categoryRepository.Search(key);
                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
