namespace SaeedAzari.core.Common;
public interface IApplicationContext<TUserNameType> where TUserNameType : class
{
    string TraceId { get; }
    string UniqueId { get; }
    string SessionId { get; }
    TUserNameType UserName { get; }
    string UserIp { get; }
    string Language { get; }
    public List<string> Roles { get; set; }
    bool IsInRole(string role);
}
public interface IApplicationContext : IApplicationContext<string>
{

}