namespace ReflectionHelper
{
    public class Person
    {
        [StringMinLength(5)]
        public string FirstName { get; set; }

        [StringMinLength(5)]
        public string LastName { get; set; }

        [StringMinLength(10)]
        public string Email { get; set; }
    }
}
