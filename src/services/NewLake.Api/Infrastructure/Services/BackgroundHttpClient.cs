public class BackgroundHttpClient
{
    private readonly HttpClient _httpClient;

    public BackgroundHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetResultFromHttpClient()
    {
        //internal k8s service
        var request = new HttpRequestMessage(HttpMethod.Get, "http://new-lake-background-api-service/test");

        var response = await _httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        Log.Information($"Received {result} from Background Service");

        return result;
    }
}