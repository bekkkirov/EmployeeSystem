namespace EmployeeSystem.Application.Options;

public class DbConnectionsOptions
{
    public const string SectionName = "ConnectionStrings";

    public string SystemDb { get; set; } = string.Empty;

    public string IdentityDb { get; set; } = string.Empty;
}