using Microsoft.AspNetCore.Mvc;
using Entidades.Services.Interfaces;
using Entidades.DTOs;

namespace Entidades.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
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
            var driver = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateDriverDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
