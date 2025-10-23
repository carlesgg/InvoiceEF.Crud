using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Infrastructure.Proxies.Contracts;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace InvoiceEF.Crud.Infrastructure.Proxies.Implementations
{
    public class ForbesProxy(IHttpClientFactory httpClientFactory, ILogger<ForbesProxy> logger) : IForbesProxy
    {
        private readonly ILogger<ForbesProxy> _logger = logger;
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ForbesProxy");
        public async Task<OperationResult<IEnumerable<ForbesPersonDto>>> GetListAsync(CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<IEnumerable<ForbesPersonDto>>();

            try
            {
                _logger.LogInformation("Calling Forbes API to get the list...");

                using var response = await _httpClient.GetAsync("list", cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Forbes API returned HTTP {StatusCode}", response.StatusCode);
                    return operationResult
                        .AddError((int)response.StatusCode, $"Forbes API returned error: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync(cancellationToken);

                try
                {
                    var list = JsonSerializer.Deserialize<IEnumerable<ForbesPersonDto>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (list is null)
                    {
                        _logger.LogWarning("Deserialized list was null");
                        return operationResult
                            .AddError(1001, "Deserialized list was null")
                            .AddResult([]);
                    }

                    _logger.LogInformation("Successfully deserialized {Count} Forbes entries", list.Count());
                    return operationResult.AddResult(list);
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "Error deserializing JSON from Forbes API");
                    return operationResult
                        .AddError(1002, "Error deserializing JSON response.")
                        .AddException(jsonEx);
                }
            }
            catch (TaskCanceledException tcex)
            {
                _logger.LogWarning(tcex, "Forbes API request was canceled");
                return operationResult
                    .AddError(1004, "Forbes API request was canceled.")
                    .AddException(tcex);
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error calling Forbes API");
                return operationResult
                    .AddError(1003, "Network error calling Forbes API.")
                    .AddException(httpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in ForbesProxy");
                return operationResult
                    .AddError(9999, "Unexpected error in ForbesProxy.")
                    .AddException(ex);
            }
        }
    }
}
