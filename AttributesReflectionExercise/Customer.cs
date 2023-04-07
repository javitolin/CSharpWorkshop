namespace AttributesReflectionExercise
{
    public class Customer : BaseSerializer
    {
        [JsonProperty("id", required: true)]
        public int Id { get; set; }

        [JsonProperty("name", required: true)]
        public string Name { get; set; }

        [JsonProperty("age", required: false)]
        public int Age { get; set; }

        public Customer(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
