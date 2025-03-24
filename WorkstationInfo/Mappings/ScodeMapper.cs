namespace WorkstationInfo.Mappings;

public class ScodeMapper
{
    public static (string status, string color) Map(string? scode)
    {
        return scode switch
        {
            "3-3" => ("Production", "#90EE90"),
            "4-2" => ("Unplanned Downtime", "#FF7F7F"),
            "2-1" => ("Planned Downtime", "#FFD700"),
            "1-0" => ("Startup Downtime", "#FFE4B5"),
              _   => ("Unknown", "#D3D3D3")
        };
    }
}