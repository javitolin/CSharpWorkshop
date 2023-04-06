namespace HttpWorkshop
{
    public class WorkerBasicHttpClient : BackgroundService
    {
        private readonly ILogger<WorkerBasicHttpClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WorkerBasicHttpClient(ILogger<WorkerBasicHttpClient> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var urlToCheck = _configuration.GetValue<string>("check_url");

                var response = await _httpClient.GetAsync(urlToCheck);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response status: [{response.StatusCode}]");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}