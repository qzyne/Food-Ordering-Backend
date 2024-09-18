using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingFoodFinalTerm.Interface;
using OrderingFoodFinalTerm.Repository;
using System.Linq.Expressions;

namespace OrderingFoodFinalTerm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            
        }

        [HttpGet("id")]
        public IActionResult OrderDetail(int orderId) 
        { 
            try
            {
                var orderDetail =  _orderRepository.GetOrderByID(orderId);
                return Ok(orderDetail);
            }
            catch
            {
                return NotFound("Khong tim thay id");
            }
        }

        [HttpGet]

        public IActionResult GetAllOrder()
        {
            try
            {
                return Ok(_orderRepository.GetAll());
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("UserID")] 
        public IActionResult GetOrderByUserId(int userId)
        {
            var order = _orderRepository.GetOrderByUserID(userId);
            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng");
            }
            try
            {
                return Ok(order);
            } 
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
