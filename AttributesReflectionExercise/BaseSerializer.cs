using System.Reflection;
using System.Text;

namespace AttributesReflectionExercise
{
    public abstract class BaseSerializer
    {
        public string Serialize()
        {
            var properties = GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(JsonPropertyAttribute)));

            var response = new StringBuilder("{");

            foreach (var property in properties)
            {
                var jsonAttr = property.GetCustomAttribute<JsonPropertyAttribute>();
                var propertyName = jsonAttr.Name ?? property.Name;
                var propertyValue = property.GetValue(this);

                if (jsonAttr.Required && propertyValue == null)
                {
                    throw new Exception($"Required property [{propertyName}] is null.");
                }

                if (propertyValue == null)
                {
                    continue;
                }

                if (propertyValue.GetType() == typeof(string))
                {
                    response.Append($"\"{propertyName}\": \"{propertyValue}\", ");
                }
                else
                {
                    response.Append($"\"{propertyName}\": {propertyValue}, ");
                }

            }

            response = response.Remove(response.Length - 2, 2);

            response.Append('}');

            return response.ToString();
        }
    }
}