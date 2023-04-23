namespace WorkerDIExamples
{
    public class SecondWorker : BackgroundService
    {
        private readonly ILogger<FirstWorker> _logger;
        IExampleService _exampleService;

        public SecondWorker(ILogger<FirstWorker> logger, IExampleService exampleService)
        {
            _logger = logger;
            _exampleService = exampleService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var number = _exampleService.GetNumber();
                _logger.LogInformation("SecondWorker - Received number: {int}", number);

                _exampleService.RaiseNumber();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}