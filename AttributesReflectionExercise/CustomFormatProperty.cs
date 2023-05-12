namespace AttributesReflectionExercise
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomFormatProperty : Attribute
    {
        public string? Name { get; set; }
        public bool Required { get; set; }

        public CustomFormatProperty(string? name, bool required = false)
        {
            Name = name;
            Required = required;
        }
    }
}