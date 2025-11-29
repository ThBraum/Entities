using Microsoft.AspNetCore.Mvc;
using Entidades.Services.Interfaces;
using Entidades.DTOs;

namespace Entidades.Controllers.Api
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversApiController : ControllerBase
    {
        private readonly IDriverService _service;
        public DriversApiController(IDriverService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var driver = await _service.GetByIdAsync(id);
            return driver == null ? NotFound() : Ok(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDriverDto dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required" });

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { error = "Name is required" });

            if (dto.RacingNumber <= 0)
                return BadRequest(new { error = "RacingNumber is required and must be a positive integer" });

            if (dto.TeamId <= 0)
                return BadRequest(new { error = "TeamId is required and must be a positive integer" });

            try
            {
                var driver = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateDriverDto dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required" });

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { error = "Name is required" });

            if (dto.RacingNumber <= 0)
                return BadRequest(new { error = "RacingNumber is required and must be a positive integer" });

            if (dto.TeamId <= 0)
                return BadRequest(new { error = "TeamId is required and must be a positive integer" });

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
