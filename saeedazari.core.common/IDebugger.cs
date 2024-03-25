namespace SaeedAzari.core.Common;
public interface IDebugger
{
    void Add(object value);
    List<object> GetValues();
}
