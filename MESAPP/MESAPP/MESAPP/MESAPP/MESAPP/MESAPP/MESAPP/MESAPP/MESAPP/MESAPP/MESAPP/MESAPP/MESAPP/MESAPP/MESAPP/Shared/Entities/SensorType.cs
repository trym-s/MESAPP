namespace Shared.Entities;

public class SensorType
{
    public int Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; } = null!;
    public string? Unit { get; set; }
    public string DataType { get; set; } = "numeric"; // 'numeric', 'boolean', 'categorical'
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public string? Description { get; set; }
}