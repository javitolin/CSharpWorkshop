using Newtonsoft.Json;

namespace HttpWorkshop
{
    public class WorkerLoginHttpClient : BackgroundService
    {
        private readonly ILogger<WorkerBasicHttpClient> _logger;
        private readonly HttpClient _httpClient;

        public WorkerLoginHttpClient(ILogger<WorkerBasicHttpClient> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("LoginClient");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var payload = JsonConvert.SerializeObject(new
                {
                    username = "someuser",
                    password = "StrongP@ssw0rd!"
                });

                var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/", content);
                _logger.LogInformation(await response.Content.ReadAsStringAsync());

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response status: [{response.StatusCode}]. Response body: [{responseBody}]");
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}
