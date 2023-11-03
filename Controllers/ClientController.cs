using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Controllers
{
	[Authorize]
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
		public async Task<IActionResult> GetAllClients(CancellationToken cancellationToken)
		{
			var result = await _service.GetClientsAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetClient(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetClientByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddClient(Client? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreateClientAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateClient(int id, Client? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetClientByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdateClientAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteClient(int id, CancellationToken cancellationToken)
		{
			await _service.DeleteClientByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
