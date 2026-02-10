using Microsoft.AspNetCore.Mvc;
using Montcrest.API.DTOs.Technology;
using Montcrest.BLL.Interfaces;

namespace Montcrest.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TechnologyController : ControllerBase
    {
       private readonly ITechnologyService _technologyService;

        public TechnologyController(ITechnologyService technologyService)
        {
            _technologyService = technologyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var technologies = await _technologyService.GetAllAsync();

            var result = technologies.Select(t => new TechnologyResponseDto
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tech = await _technologyService.GetIdByAsync(id);

            if (tech == null)
                return NotFound("Technology not found");

            return Ok(tech);
        }

    }
}
