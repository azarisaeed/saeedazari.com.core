namespace SaeedAzari.Core.Enums
{
    public interface IEnumService
    {
        Dictionary<string, string> AsDic();
    }
    public interface IEnumService<T> : IEnumService
    {

        string GetById(T key);
        Dictionary<T, string> GetAll();

    }
}
