namespace SaeedAzari.Core.Enums.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class EnumDescriptorAttribute(string description) : Attribute
    {
        private readonly string _description = description;



        public string Description => _description;
    }
}
