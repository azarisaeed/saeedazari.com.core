
using SaeedAzari.Core.Enums.Attributes;

namespace SaeedAzari.Core.Enums
{
    public class BaseEnumDescriptorService<T> : IEnumService<T>
    {


        public Dictionary<T, string> Init()
        {

            var enumType = typeof(T);
            if (!enumType.IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var enumValArray = System.Enum.GetValues(enumType);

            var dic = new Dictionary<T, string>();

            foreach (var val in enumValArray)
            {
                var enumValue = (T)System.Enum.Parse(enumType, val.ToString());
                var enumText = GetEnumDescription(enumValue);
                if (dic.TryGetValue(enumValue, out string? value))
                    throw new Exception($"Enum {enumText} and {value} has same value {enumValue}");
                dic.Add(enumValue, enumText);
            }

            return dic;

        }

        private string GetEnumDescription(object enu)
        {
            var enumType = enu.GetType();
            var enumValue = enumType.GetField(enu.ToString());
            EnumDescriptorAttribute[]? descriptor = enumValue.GetCustomAttributes(typeof(EnumDescriptorAttribute), false) as EnumDescriptorAttribute[]
                ?? throw new Exception($"Description for enum {enumType}.{enumValue} not defined.");
            if (descriptor.Length != 1)
                throw new Exception("Invalid enum descriptor");

            return descriptor[0].Description;

        }


        public Dictionary<T, string> Items => GetItems();


        private Dictionary<T, string> GetItems()
        {
            return Init();

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


        public string GetById(T key)
        {
            if (Items.TryGetValue(key, out string? value))
                return value;
            else
                throw new KeyNotFoundException("The (" + key.ToString + ") key was not present in the EnumService(Of " + typeof(T).Name + ").");
        }
    }
}
