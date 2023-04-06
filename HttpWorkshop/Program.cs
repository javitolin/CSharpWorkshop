using HttpWorkshop;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args);
    })
    .ConfigureServices((hostingContext, services) =>
    {
        //ConfigureBaseServices(hostingContext, services);

        //ConfigureNamedServices(hostingContext, services);

        //ConfigurePollyServices(hostingContext, services);

        //ConfigureExerciseResultServices(hostingContext, services);
    })
    .Build();

void ConfigureExerciseResultServices(HostBuilderContext hostingContext, IServiceCollection services)
{
    services.AddHttpClient("Exercise", client =>
    {
        client.BaseAddress = new Uri(hostingContext.Configuration.GetValue<string>("exercise_url"));
    }).AddPolicyHandler(_ =>
    {
        var serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILogger<Program>>();
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => (int)msg.StatusCode > 399 && (int)msg.StatusCode < 500)
            .WaitAndRetryAsync(6, retryAttempt =>
            {
                logger!.LogInformation($"Polly retrying, Retry attempt: [{retryAttempt}]");
                return TimeSpan.FromSeconds(2 + retryAttempt);
            });
    });

    services.AddHostedService<ExerciseResponse>();
}

void ConfigurePollyServices(HostBuilderContext hostingContext, IServiceCollection services)
{
    services.AddHttpClient("Polly", client =>
    {
        // Configure the HttpClient
        client.BaseAddress = new Uri(hostingContext.Configuration.GetValue<string>("random_url"));
    }).AddPolicyHandler(_ =>
    {
        var serviceProvider = services.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILogger<Program>>();

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(6, retryAttempt =>
            {
                logger!.LogInformation($"Polly retrying, Retry attempt: [{retryAttempt}]");
                return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
            });
    });

    services.AddHostedService<WorkerNamedHttpClientPolly>();

}

void ConfigureNamedServices(HostBuilderContext hostingContext, IServiceCollection services)
{
    services.AddHttpClient("NamedClient", client =>
    {
        client.BaseAddress = new Uri(hostingContext.Configuration.GetValue<string>("check_url"));
    });

    services.AddHostedService<WorkerNamedHttpClient>();
}

void ConfigureBaseServices(HostBuilderContext hostingContext, IServiceCollection services)
{
    services.AddHttpClient();
    services.AddHostedService<WorkerBasicHttpClient>();
}

await host.RunAsync();
