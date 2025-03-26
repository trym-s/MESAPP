using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperatorPanel.Features.Commands;

namespace OperatorPanel.Controllers;

[ApiController]
[Route("api/operator")]
public class OperatorActionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperatorActionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{id}/change-scode")]
    public async Task<IActionResult> ChangeScode(int id, [FromBody] ChangeScodeCommand command)
    {
        if (id != command.WorkstationId)
            return BadRequest("URL workstation ID ile body eşleşmiyor.");

        ChangeScodeResult result = await _mediator.Send(command);

        return Ok(new
        {
            message =  result.Message,
            oldScode = result.OldScode,
            newScode = result.NewScode
        });
    }

}