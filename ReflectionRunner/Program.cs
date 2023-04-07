using AttributesReflectionExercise;
using Newtonsoft.Json;
using ReflectionHelper;
using System.Reflection;

void PublicClassExample()
{
    Type myType = typeof(MyPublicClass);

    object? myInstance = Activator.CreateInstance(myType);

    PropertyInfo? myProperty = myType.GetProperty("MyProperty");

    int propertyValue = (int)myProperty.GetValue(myInstance);

    Console.WriteLine($"propertyValue - [{propertyValue}]");

    MethodInfo myMethod = myType.GetMethod("MyMethod");

    myMethod.Invoke(myInstance, null);
}

void PrivateClassExample()
{
    Type myType = typeof(MyPrivateClass);

    object? myInstance = Activator.CreateInstance(myType);

    PropertyInfo? myProperty = myType.GetProperty("MyProperty", BindingFlags.NonPublic | BindingFlags.Instance);

    int propertyValue = (int)myProperty.GetValue(myInstance);

    Console.WriteLine($"propertyValue - [{propertyValue}]");

    MethodInfo myMethod = myType.GetMethod("MyMethod", BindingFlags.NonPublic | BindingFlags.Instance);

    myMethod.Invoke(myInstance, null);
}

//Console.WriteLine("Public: ");
//PublicClassExample();

//Console.WriteLine("Private: ");
//PrivateClassExample();

void AttributesExample()
{
    var jsonText = "{ \"username\": \"user\", \"password\": \"superstrongpassword\", \"status\": 0 }";

    var deserialized = JsonConvert.DeserializeObject<UserCredentials>(jsonText);
    Console.WriteLine(deserialized);
}

//AttributesExample();

void CustomAttributeCheck()
{
    Person person = new Person
    {
        FirstName = "John",
        LastName = "Doe",
        Email = "johndoe@example.com"
    };

    Type type = person.GetType();
    var stringProperties = type.GetProperties()
                            .Where(prop => Attribute.IsDefined(prop, typeof(StringMinLengthAttribute)))
                            .Where(prop => prop.PropertyType == typeof(string));

    foreach (var property in stringProperties)
    {
        var value = (string)property.GetValue(person);
        var attribute = (StringMinLengthAttribute)property.GetCustomAttributes(typeof(StringMinLengthAttribute), false).First();
        if (value.Length < attribute.MinimumLength)
        {
            Console.WriteLine($"Error: [{property.Name}] is shorter than the minimum length [{attribute.MinimumLength}]");
        }
        else
        {
            Console.WriteLine($"[{property.Name}] is of correct length");
        }
    }
}

//CustomAttributeCheck();

void ExerciseCheck()
{
    Customer c = new Customer(123, "MyName", 18);
    var serialized = c.Serialize();
    Console.WriteLine(serialized);
}

ExerciseCheck();