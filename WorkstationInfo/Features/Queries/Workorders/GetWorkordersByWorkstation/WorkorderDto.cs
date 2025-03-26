namespace WorkstationInfo.Features.Queries.GetWorkorders.GetWorkordersByWorkstation;

public class WorkorderDto
{
    public int WorkorderId { get; set; }
    public bool IsActive { get; set; }
    public int CurrentScodeValue { get; set; }
}