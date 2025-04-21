using MediatR;

namespace OperatorPanel.Features.Commands.ActivateWorkorder;

public class ActivateWorkorderCommand : IRequest<ActivateWorkorderResult>
{
    public int WorkstationId { get; set; }
    public int WorkorderId { get; set; }
    public int InitialScode { get; set; }
    public int OperatorId { get; set; }
    public string? Reason { get; set; }
}