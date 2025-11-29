using Microsoft.AspNetCore.Mvc;
using Entidades.Services.Interfaces;
using Entidades.DTOs;

namespace Entidades.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsApiController : ControllerBase
    {
        private readonly ITeamService _service;
        public TeamsApiController(ITeamService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _service.GetByIdAsync(id);
            return team == null ? NotFound() : Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamDto dto)
        {
            var team = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = team.Id }, team);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateTeamDto dto)
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
