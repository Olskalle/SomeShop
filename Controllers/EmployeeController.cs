using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAllEmployees()
        {
            var result = _service.GetEmployees();

            if (result is null || result.Count() <= 0)
            {
                return NoContent();
            }

            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var result = _service.GetEmployeeById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult AddEmployee(Employee? item)
        {
			if (item is null) return BadRequest();

			_service.CreateEmployee(item);
            return Ok();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateEmployee(int id, Employee? item)
        {

            if (item is null || id != item.Id) return BadRequest();

            if (_service.GetEmployeeById(id) != null)
            {
                _service.UpdateEmployee(item);
            }
            else
            {
                _service.CreateEmployee(item);
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var itemToDelete = _service.GetEmployeeById(id);
            if (itemToDelete != null)
            {
                _service.DeleteEmployee(itemToDelete);
                return Ok();
            }

            return NotFound();
        }
    }
}
