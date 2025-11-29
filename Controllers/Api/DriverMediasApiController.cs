using Microsoft.AspNetCore.Mvc;
using Entidades.Services.Interfaces;
using Entidades.DTOs;

namespace Entidades.Controllers.Api
{
    [ApiController]
    [Route("api/drivermedias")]
    public class DriverMediasApiController : ControllerBase
    {
        private readonly IDriverMediaService _service;
        public DriverMediasApiController(IDriverMediaService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDriverMediaDto dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required" });

            if (dto.DriverId <= 0)
                return BadRequest(new { error = "DriverId is required and must be a positive integer" });

            if (string.IsNullOrWhiteSpace(dto.Media))
                return BadRequest(new { error = "Media is required" });

            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateDriverMediaDto dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required" });

            if (dto.DriverId <= 0)
                return BadRequest(new { error = "DriverId is required and must be a positive integer" });

            if (string.IsNullOrWhiteSpace(dto.Media))
                return BadRequest(new { error = "Media is required" });

            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
