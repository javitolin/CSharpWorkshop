using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ReflectionHelper
{
    public enum UserStatus
    {
        NotConfirmed,
        Active,
        Deleted
    }

    public class UserCredentials
    {
        // Step 1 - Without JsonProperty("...")
        // Step 2 - Add JsonProperty
        [JsonProperty("username")]
        public string MyUsername { get; set; }

        [JsonProperty("password")]
        public string MyPassword { get; set; }

        // Step 3 - Show JsonConverter
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UserStatus Status { get; set; }

        public override string ToString()
        {
            return $"Username: [{MyUsername}]. Password: [{MyPassword}]. Status: [{Status}]";
        }
    }
}
