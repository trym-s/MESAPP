namespace OperatorPanel.Entities;

public class Workstation
{
    public int WorkstationId { get; set; }
    public string? WorkstationName { get; set; }
    public bool IsActive { get; set; }
    public string? SerialNumber { get; set; }

    public ICollection<Workorder>? Workorders { get; set; }
}