namespace AttributesReflectionExercise
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonPropertyAttribute : Attribute
    {
        public string? Name { get; set; }
        public bool Required { get; set; }

        public JsonPropertyAttribute(string? name, bool required = false)
        {
            Name = name;
            Required = required;
        }
    }

}