namespace EmployeeSystem.Application.Options;

public class DbConnectionsOptions
{
    public const string SectionName = "ConnectionStrings";

    public string SystemDb { get; set; } = string.Empty;
}