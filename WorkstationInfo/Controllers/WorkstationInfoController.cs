using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkstationInfo.Features.GetWorkstationInfo;

namespace WorkstationInfo.Controllers
{
    [Route("api/workstations")]
    [ApiController]
    public class WorkstationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkstationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Belirtilen ID'ye sahip workstation bilgilerini getirir.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetWorkstationInfoDto>> GetWorkstationInfo(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetWorkstationInfoQuery(id));
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Workstation not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }
    }
}