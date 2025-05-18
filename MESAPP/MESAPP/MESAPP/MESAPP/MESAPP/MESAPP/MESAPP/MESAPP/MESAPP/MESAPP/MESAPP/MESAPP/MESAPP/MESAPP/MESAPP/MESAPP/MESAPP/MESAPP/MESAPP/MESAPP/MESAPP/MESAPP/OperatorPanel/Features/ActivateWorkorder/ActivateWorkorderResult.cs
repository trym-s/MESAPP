namespace OperatorPanel.Features.Commands.ActivateWorkorder;

public class ActivateWorkorderResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int PreviousActiveWorkorderId { get; set; }
    public int NewActiveWorkorderId { get; set; }
    public int InitialScode { get; set; }
}