using BookStore.Common.DTOs.Shipping;
using BookStore.Common.DTOs.ShippingDTO;
using BookStore.Common.DTOs.ShippingDTOs;
using BookStore.Data.Entities;
using BookStore.Service.Services.Loggerservice;
using BookStore.Service.Services.ShippingService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BookStore.API.Controllers
{
	[Route("api/shipping-management")]
	[ApiController]
	public class ShippingController : ControllerBase
	{
		private readonly ILoggerManager _logger;
		private readonly IShippingService _shippingService;

		public ShippingController(ILoggerManager logger, IShippingService shippingService)
		{
			_logger = logger;
			_shippingService = shippingService;
		}

		[EnableQuery]
		[HttpGet("shippings")]
		public async Task<ActionResult<IQueryable<Shipping>>> GetAllShipping()
		{
			var result = await _shippingService.GetAllShippingAsync();
			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[HttpPost("shippings")]
		public async Task<IActionResult> Create([FromBody] CreateShippingRequest createShippingRequest)
		{
			var result = await _shippingService.CreateAsync(createShippingRequest);
			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[HttpDelete("shippings/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var rs = await _shippingService.GetShippingByShippingIdAsync(id);
			if (rs == null) return NotFound();
			var result = await _shippingService.DeleteAsync(id);

			return Ok(result);
		}

		[HttpPut("shippings/{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateShippingStatus updateShippingStatus)
		{
			var rs = await _shippingService.GetShippingByShippingIdAsync(id);
			if (rs == null) return NotFound();
			var result = await _shippingService.UpdateShippingStatus(updateShippingStatus, id);
			if (result == null) return StatusCode(500);

			return Ok(result);
		}

		[HttpGet("shippings/{id}")]
		public async Task<IActionResult> GetShippingDetailById(int id)
		{
			var result = await _shippingService.GetShippingByShippingIdAsync(id);

			if (result == null) return NotFound();

			return Ok(result);
		}

		[HttpPut("shipping-detail/{id}")]
		public async Task<IActionResult> UpdateShippingDetail(UpdateShippingDetailRequest updateShippingDetailRequest, int id)
		{
			var result = await _shippingService.UpdateShippingDetailAsync(updateShippingDetailRequest, id);

			if (result == null) return NotFound();

			return Ok(result);
		}
	}
}
