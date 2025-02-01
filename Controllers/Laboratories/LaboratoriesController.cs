using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hecotoBackend.Services;
using hecotoBackend.DTOs;
using System.Threading.Tasks;
using hecotoBackend.Models;

namespace hecotoBackend.Controllers.Laboratories
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoriesController : ControllerBase
    {
        private readonly LaboratoryServices _laboratoryServices;

        public LaboratoriesController(LaboratoryServices laboratoryServices)
        {
            _laboratoryServices = laboratoryServices ?? throw new ArgumentNullException(nameof(laboratoryServices));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetLaboratories()
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateLaboratory([FromBody] LaboratoryDto laboratory)
        {
            return Ok();
        }

        [HttpGet("{laboratoryId}")]
        [Authorize]
        public async Task<ActionResult> GetLaboratory(string laboratoryId)
        {
            return Ok();
        }

        [HttpPut("{laboratoryId}")]
        [Authorize]
        public async Task<ActionResult> UpdateLaboratory(string laboratoryId, [FromBody] LaboratoryDto laboratory)
        {
            return Ok();
        }

        [HttpDelete("{laboratoryId}")]
        [Authorize]
        public async Task<ActionResult> DeleteLaboratory(string laboratoryId)
        {
            return Ok();
        }
    }
}