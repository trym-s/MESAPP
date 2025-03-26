using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperatorPanel.Features.Commands;
using OperatorPanel.Features.Commands.ActivateWorkorder;
using OperatorPanel.Features.Commands.ChangeScode;

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
            message = "Scode updated successfully.",
            oldScode = result.OldScode,
            newScode = result.NewScode
        });
    }
    
    [HttpPost("{id}/activate-workorder")]
    public async Task<IActionResult> ActivateWorkorder(int id, [FromBody] ActivateWorkorderCommand command)
    {
        if (id != command.WorkstationId)
            return BadRequest("URL workstation ID ile body workstation ID eşleşmiyor.");

        var result = await _mediator.Send(command);

        return Ok(new
        {
            succes = result.Success,
            message = result.Message,
            previousWorkorderId = result.PreviousActiveWorkorderId,
            newWorkorderId = result.NewActiveWorkorderId,
            initialScode = result.InitialScode
        });
    }

}