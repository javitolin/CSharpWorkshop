using System.Reflection;
using System.Text;

namespace AttributesReflectionExercise
{
    public static class Serializer
    {
        public static string Serialize<T>(T toSerialize)
        {
            if (toSerialize == null) throw new ArgumentNullException(nameof(toSerialize));

            var properties = toSerialize.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(CustomFormatProperty)));

            var response = new StringBuilder("{");

            foreach (var property in properties)
            {
                var jsonAttr = property.GetCustomAttribute<CustomFormatProperty>();
                var propertyName = jsonAttr?.Name ?? property.Name;
                var propertyValue = property.GetValue(toSerialize);

                if (jsonAttr != null && jsonAttr.Required && propertyValue == null)
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
