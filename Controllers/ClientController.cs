using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAllClients()
        {
            var result = _service.GetClients();

            if (result is null || result.Count() <= 0)
            {
                return NoContent();
            }

            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var result = _service.GetClientById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult AddClient(Client item)
        {
            _service.CreateClient(item);
            return Ok();
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateClient(int id, Client item)
        {

            if (id != item.Id) return BadRequest();

            if (_service.GetClientById(id) != null)
            {
                _service.UpdateClient(item);
            }
            else
            {
                _service.CreateClient(item);
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteClient(int id)
        {
            var itemToDelete = _service.GetClientById(id);
            if (itemToDelete != null)
            {
                _service.DeleteClient(itemToDelete);
                return Ok();
            }

            return NotFound();
        }
    }
}
