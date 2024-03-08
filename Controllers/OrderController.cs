using apiTest.Interface;
using apiTest.Model;
using apiTest.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace apiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public IActionResult GetOrderList()
        {
            try
            {
                var orderItems = _orderRepository.GetOrderList();
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateNewOrder([FromBody] Order[] orderItems, [FromQuery] int total)
        {
            {
                try
                {
                    _orderRepository.CreateNewOrder(orderItems, total);
                    return Ok(new { message = "Record updated successfully" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            {
                try
                {
                    _orderRepository.DeleteOrder(id);
                    return Ok(new { message = "Record removed successfully" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            };
        }

    }
}
