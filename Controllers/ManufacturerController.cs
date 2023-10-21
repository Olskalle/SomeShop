using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _service;

        public ManufacturerController(IManufacturerService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAllManufacturers()
        {
            var result = _service.GetManufacturers();

            if (result is null || result.Count() <= 0)
            {
                return NoContent();
            }

            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetManufacturer(int id)
        {
            var result = _service.GetManufacturerById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult AddManufacturer(Manufacturer? item)
        {
			if (item is null) return BadRequest();

			_service.CreateManufacturer(item);
            return Ok();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateManufacturer(int id, Manufacturer? item)
        {

            if (item is null || id != item.Id) return BadRequest();

            if (_service.GetManufacturerById(id) != null)
            {
                _service.UpdateManufacturer(item);
            }
            else
            {
                _service.CreateManufacturer(item);
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteManufacturer(int id)
        {
            var itemToDelete = _service.GetManufacturerById(id);
            if (itemToDelete != null)
            {
                _service.DeleteManufacturer(itemToDelete);
                return Ok();
            }

            return NotFound();
        }
    }
}
