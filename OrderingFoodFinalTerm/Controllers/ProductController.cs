using Microsoft.AspNetCore.Mvc;
using OrderingFoodFinalTerm.Interface;
using OrderingFoodFinalTerm.Repository;
using OrderingFoodFinalTerm.DTO;
using Microsoft.Extensions.Hosting;
using CoreApiResponse;
using System.Net;
//using OrderingFoodFinalTerm.DTO;

namespace OrderingFoodFinalTerm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            this._hostEnvironment = webHostEnvironment;
        }

        // Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {

                return CustomResult(_productRepository.GetAll(), HttpStatusCode.OK);
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
                var data = _productRepository.GetById(id);
                if (data != null)
                {
                    return CustomResult(data,HttpStatusCode.OK);
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
        public IActionResult Update(int id, [FromForm] ProductDTO product)
        {
            if (id != product.Id)
            {
                return CustomResult(HttpStatusCode.BadRequest);
            }
            try
            {
                _productRepository.Update(product);
                return CustomResult("Update thành công",HttpStatusCode.OK);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        //Update status isActive
        [HttpPut("updateStatus/{id}")]
        public IActionResult UpdateIsActive(int id, int status)
        {
            try
            {
                _productRepository.UpdateIsActive(id, status);
                return CustomResult(HttpStatusCode.OK);
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
                _productRepository.Delete(id);
                return CustomResult(HttpStatusCode.OK);
            }
            catch
            {
                return CustomResult(HttpStatusCode.InternalServerError);
            }
        }

        //Post
        [HttpPost]
        public IActionResult Add([FromForm] ProductDTO product)
        {
            try
            {
                var _product = _productRepository.GetAll()
                    .Where(c => c.ProductName.Trim().ToUpper() == product.ProductName.Trim().ToUpper())
                    .FirstOrDefault();
                // SP tồn tại
                if (_product != null)
                {
                    return CustomResult("Sản phẩm đã tồn tại",HttpStatusCode.BadRequest);
                }
                _productRepository.Add(product);
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
                var res = _productRepository.Search(key);
                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
