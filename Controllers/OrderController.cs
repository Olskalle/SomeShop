using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAllOrders()
        {
            var result = _service.GetOrders();

            if (result is null || result.Count() <= 0)
            {
                return NoContent();
            }

            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var result = _service.GetOrderById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult AddOrder(Order? item)
        {
			if (item is null) return BadRequest();

			_service.CreateOrder(item);
            return Ok();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateOrder(int id, Order? item)
        {

            if (item is null || id != item.Id) return BadRequest();

            if (_service.GetOrderById(id) != null)
            {
                _service.UpdateOrder(item);
            }
            else
            {
                _service.CreateOrder(item);
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var itemToDelete = _service.GetOrderById(id);
            if (itemToDelete != null)
            {
                _service.DeleteOrder(itemToDelete);
                return Ok();
            }

            return NotFound();
        }
    }
}
