namespace SaeedAzari.Core.Enums
{
    public interface IDescriptor<T> where T : struct, Enum
    {
         Dictionary<T, string> Items { get;  set; }
        string GetById(T key);
        Dictionary<T, string> GetAll();
        Dictionary<string, string> AsDic();
        string GetById<TType>(TType key) where TType : struct;
        Dictionary<TType, string> AsDic<TType>() where TType : struct;
    }
}
