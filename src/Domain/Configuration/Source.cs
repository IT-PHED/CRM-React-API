using SharedKernel;

namespace Domain.Configuration;
public sealed class Source : Enumeration<Source>
{
    public static readonly Source CallCenter = new(1, "Call Center");
    public static readonly Source Region = new(2, "Region");
    public static readonly Source Feeder = new(3, "Feeder");
    public static readonly Source Email = new(4, "Email");

    private Source(int id, string name) : base(id, name) { }

    public static IEnumerable<Source> AllSources() => new[] { CallCenter, Region, Feeder, Email };

    public static bool IsValidSourceId(int id) => AllSources().Any(s => s.Id == id);

    public static bool IsValidSourceName(string name) => AllSources().Any(s => s.Name == name);
}
