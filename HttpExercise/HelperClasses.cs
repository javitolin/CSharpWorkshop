using System.Text.RegularExpressions;

namespace HttpExercise
{
    public class Example
    {
        public string? example { get; set; }
    }

    public class InputOutput
    {
        public string? input { get; set; }
        public string? output { get; set; }
    }

    public class UserRetries
    {
        public int RetryNumber { get; set; }
        public DateTime LastRetry { get; set; }
    }

    public class Authentication
    {
        private Regex PasswordRegex = new Regex("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");
        public string? username { get; set; }
        public string? password { get; set; }

        public bool isValid()
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && PasswordRegex.IsMatch(password);
        }
    }

}