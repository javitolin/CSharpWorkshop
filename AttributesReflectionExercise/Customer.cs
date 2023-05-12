namespace AttributesReflectionExercise
{
    public class Customer
    {
        [CustomFormatProperty("id", required: true)]
        public int Id { get; set; }

        [CustomFormatProperty("name", required: true)]
        public string Name { get; set; }

        [CustomFormatProperty("age", required: false)]
        public int Age { get; set; }

        public Customer(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
