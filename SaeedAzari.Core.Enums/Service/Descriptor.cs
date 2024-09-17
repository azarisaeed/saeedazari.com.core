
using SaeedAzari.Core.Enums.Attributes;

namespace SaeedAzari.Core.Enums
{
    public class Descriptor<T> : IDescriptor<T> where T : struct, System.Enum
    {
        public Descriptor()
        {
            var enumValArray = System.Enum.GetValues<T>();
            var dic = new Dictionary<T, string>();
            foreach (var val in enumValArray)
            {
                var enumText = GetEnumDescription(val);
                if (dic.TryGetValue(val, out string? value))
                    throw new Exception($"Enum {enumText} and {value} has same value {val}");
                dic.Add(val, enumText);
            }
            Items = dic;
        }

        public  Dictionary<T, string> Items { get; set; }
        private string GetEnumDescription(T enu)
        {
            var enumType = enu.GetType();
            var enumValue = enumType.GetField(enu.ToString());
            EnumDescriptorAttribute[]? descriptor = enumValue.GetCustomAttributes(typeof(EnumDescriptorAttribute), false) as EnumDescriptorAttribute[]
                ?? throw new Exception($"Description for enum {enumType}.{enumValue} not defined.");
            if (descriptor.Length != 1)
                throw new Exception("Invalid enum descriptor");

            return descriptor[0].Description;

        }
        public Dictionary<T, string> GetAll()
        {
            return Items;
        }

        public Dictionary<string, string> AsDic()
        {

            var res = Items.ToDictionary(i => i.Key.ToString(), i => i.Value);
            return res;
        }
        public Dictionary<TType, string> AsDic<TType>() where TType : struct
        {
            var res = Items.ToDictionary(i => (TType)Convert.ChangeType(i.Key, typeof(TType)), i => i.Value);
            return res;
        }

        public string GetById(T key)
        {
            if (Items.TryGetValue(key, out string? value))
                return value;
            else
                throw new KeyNotFoundException("The (" + key.ToString() + ") key was not present in the EnumService(Of " + typeof(T).Name + ").");
        }
        public string GetById<TType>(TType key) where TType : struct
        {


            if (Items.TryGetValue((T)Convert.ChangeType(key, typeof(T)), out string? value))
                return value;
            else
                throw new KeyNotFoundException("The (" + key.ToString() + ") key was not present in the EnumService(Of " + typeof(T).Name + ").");
        }
    }
}
