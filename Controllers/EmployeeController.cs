﻿using Microsoft.AspNetCore.Http;
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
		public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
		{
			var result = await _service.GetEmployeesAsync(cancellationToken);

			if (result is null || result.Count() <= 0)
			{
				return NoContent();
			}

			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetEmployee(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetEmployeeByIdAsync(id, cancellationToken);

			if (result is null) return NotFound();

			return Ok(result);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddEmployee(Employee? item, CancellationToken cancellationToken)
		{
			if (item is null) return BadRequest();

			await _service.CreateEmployeeAsync(item, cancellationToken);
			return Ok();
		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateEmployee(int id, Employee? item, CancellationToken cancellationToken)
		{
			if (item is null || id != item.Id) return BadRequest();

			var toUpdate = await _service.GetEmployeeByIdAsync(id, cancellationToken);
			if (toUpdate != null)
			{
				await _service.UpdateEmployeeAsync(item, cancellationToken);
				return Ok();
			}
			return NotFound();
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
		{
			await _service.DeleteEmployeeByIdAsync(id, cancellationToken);
			return NoContent();
		}
	}
}
