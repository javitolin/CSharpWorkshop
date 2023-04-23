namespace WorkerDIExamples
{
    public class ScopedWorker : BackgroundService
    {
        private readonly ILogger<FirstWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ScopedWorker(ILogger<FirstWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var exampleService = scope.ServiceProvider.GetService<IExampleService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var number = exampleService.GetNumber();
                _logger.LogInformation("ScopedWorker - Received number: {int}", number);

                exampleService.RaiseNumber();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}