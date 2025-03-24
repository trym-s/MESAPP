using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkstationInfo.Features.Queries.GetAllWorkstationDetails;
using WorkstationInfo.Features.Queries.GetWorkstationDetails;
using WorkstationInfo.Features.Queries.GetWorkstationSummary;

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

        /// Belirtilen ID'ye sahip workstation bilgilerini getirir.
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkstationDetailsDto>> GetWorkstationInfo(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetWorkstationDetailsQuery(id));
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
        [HttpGet("summary")]
        public async Task<ActionResult<List<WorkstationSummaryDto>>> GetAllSummaries()
        {
            var result = await _mediator.Send(new GetWorkstationSummaryQuery());
            return Ok(result);
        }
        
        [HttpGet("details")]
        public async Task<ActionResult<List<WorkstationDetailsListItemDto>>> GetAllDetails()
        {
            var result = await _mediator.Send(new GetAllWorkstationDetailsQuery());
            return Ok(result);
        }
    }
}