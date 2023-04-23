namespace WorkerDIExamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    // services.AddSingleton<IExampleService, ExampleService>(); // Singleton
                    // services.AddTransient<IExampleService, ExampleService>(); // A different one for each requirement
                    services.AddScoped<IExampleService, ExampleService>(); // A different one for each requirement
                    // services.AddHostedService<FirstWorker>();
                    // services.AddHostedService<SecondWorker>();
                    services.AddHostedService<ScopedWorker>();
                })
                .Build();

            host.Run();
        }
    }
}