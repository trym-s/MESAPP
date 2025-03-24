namespace WorkstationInfo.Features.Queries.GetWorkstationSummary;

public class WorkstationSummaryDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string SerialNumber { get; init; }
    public int? ActiveWorkorderId { get; init; }
    public string Status { get; init; }
    public string StatusColor { get; init; }
}