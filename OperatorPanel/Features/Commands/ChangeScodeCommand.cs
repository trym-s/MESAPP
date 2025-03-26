using MediatR;

namespace OperatorPanel.Features.Commands;

public class ChangeScodeCommand :  IRequest<ChangeScodeResult>
{
    public int WorkstationId { get; set; }
    public int NewScode { get; set; }
    public int OperatorId { get; set; }
    public string? Reason { get; set; }
}