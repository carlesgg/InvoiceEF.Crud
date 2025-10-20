public class GenericProxy
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GenericProxy(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetWeatherAsync(CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient("weatherApi");
        var response = await client.GetAsync("/forecast/today", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(ct);
    }
}
