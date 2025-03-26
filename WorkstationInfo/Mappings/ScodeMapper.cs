namespace WorkstationInfo.Mappings;

public class ScodeMapper
{
    public static (string status, string color) Map(int? scode)
    {
        return scode switch
        {
            33 => ("Production", "#90EE90"),
            42 => ("Unplanned Downtime", "#FF7F7F"),
            21 => ("Planned Downtime", "#FFD700"),
            10 => ("Startup Downtime", "#FFE4B5"),
             _   => ("Unknown", "#D3D3D3")
        };
    }
}