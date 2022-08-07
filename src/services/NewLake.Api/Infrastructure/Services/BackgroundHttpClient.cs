public class BackgroundHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BackgroundHttpClient> _logger;

    public BackgroundHttpClient(HttpClient httpClient, ILogger<BackgroundHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> GetResultFromHttpClient()
    {
        //internal k8s service
        var request = new HttpRequestMessage(HttpMethod.Get, "http://new-lake-background-api-service/test");

        var response = await _httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        _logger.LogInformation($"Received {result} from Background Service");

        return result;
    }
}