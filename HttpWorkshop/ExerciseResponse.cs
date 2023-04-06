using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HttpWorkshop
{
    public class ExerciseResponse : BackgroundService 
    {
        private readonly ILogger<WorkerBasicHttpClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ExerciseResponse(ILogger<WorkerBasicHttpClient> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient("Exercise");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var response = await _httpClient.GetAsync("/");
            _logger.LogInformation(await response.Content.ReadAsStringAsync());

            response = await _httpClient.GetAsync("/start");
            _logger.LogInformation(await response.Content.ReadAsStringAsync());

            var payload = JsonConvert.SerializeObject(new
            {
                example = Path.Combine("C:", "Program Files")
            });

            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

            response = await _httpClient.PutAsync("/Combine", content);
            _logger.LogInformation(await response.Content.ReadAsStringAsync());

            payload = JsonConvert.SerializeObject(new
            {
                input = Path.Combine("C:", "Program Files"),
                output = "C:\\"
            });

            content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

            response = await _httpClient.PostAsync("/GetDirectoryName", content);
            _logger.LogInformation(await response.Content.ReadAsStringAsync());
            
            response = await _httpClient.DeleteAsync("/ReadAllText");
            _logger.LogInformation(await response.Content.ReadAsStringAsync());
            
            response = await _httpClient.GetAsync("/GetAsync");
            _logger.LogInformation(await response.Content.ReadAsStringAsync());
            
            response = await _httpClient.PostAsync("/Answer/3", null);
            var responseText = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<JObject>(responseText);

            var myGuid = responseJson!["GUID"];
            response = await _httpClient.GetAsync($"/Success/{myGuid}");
            _logger.LogInformation(await response.Content.ReadAsStringAsync());
        }

    }
}
