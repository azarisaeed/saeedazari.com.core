
using Microsoft.AspNetCore.Http;

namespace SaeedAzari.core.Common;
public class ApplicationContext : IApplicationContext
{
    protected static IHttpContextAccessor AcceSSOr;
    protected const string SidHeaderKey = "sid";
    protected const string UidHeaderKey = "uid";

    public ApplicationContext(IHttpContextAccessor acceSSOr)
    {
        AcceSSOr = acceSSOr;

        TraceId = AcceSSOr.HttpContext?.TraceIdentifier;
        UniqueId = AcceSSOr.HttpContext?.Request?.Headers?.FirstOrDefault(_ => _.Key.Equals(UidHeaderKey, StringComparison.OrdinalIgnoreCase)).Value;
        SessionId = AcceSSOr.HttpContext?.Request?.Headers?.FirstOrDefault(_ => _.Key.Equals(SidHeaderKey, StringComparison.OrdinalIgnoreCase)).Value;
        UserName = AcceSSOr.HttpContext?.User?.Identity?.Name;
        UserIp = AcceSSOr.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        Language = AcceSSOr.HttpContext?.Request?.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.Value.Value ?? "en-US";
    }

    public string TraceId { get; set; }
    public string UniqueId { get; set; } // UID
    public string SessionId { get; set; } // SID
    public string UserName { get; set; }
    public string UserIp { get; set; }
    public string Language { get; set; }

    public static bool IsInRole(string role) => AcceSSOr.HttpContext.User.IsInRole(role);
}
