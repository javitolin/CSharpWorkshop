namespace HttpWorkshop
{
    public class WorkerNamedHttpClientPolly : BackgroundService 
    {
        private readonly ILogger<WorkerBasicHttpClient> _logger;
        private readonly HttpClient _httpClient;

        public WorkerNamedHttpClientPolly(ILogger<WorkerBasicHttpClient> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("Polly");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _httpClient.GetAsync("");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response: [{responseBody}]");
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}
