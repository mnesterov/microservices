using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Retry;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using WebHttpAggregator.Serialization.Json;
using WebHttpAggregator.Services;

namespace Teams.API.Services;

public class HttpApiClient : IHttpApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpApiClient> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpApiClient(HttpClient httpClient, 
        ILogger<HttpApiClient> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ActionResult<T>> GetAsync<T>(string url)
    {
        var verb = HttpVerb.GET;
        _logger.LogInformation($"--> Requesting {url} with {verb}");

        var retryPolicy = CreateRetryPolicy(url, verb);

        HttpResponseMessage response = null;
        await retryPolicy.ExecuteAsync(async() =>
        {
            response = await _httpClient.GetAsync(url);
        });

        return await HandleResponseAsync<T>(response);
    }

    public async Task<ActionResult> PutAsync<TResult, TPayload>(string url, TPayload data)
    {
        var verb = HttpVerb.PUT;
        _logger.LogInformation($"--> Requesting {url} with {verb}");

        var content = CreateContent(data);

        var retryPolicy = CreateRetryPolicy(url, verb);

        HttpResponseMessage response = null;
        await retryPolicy.ExecuteAsync(async () =>
        {
            response = await _httpClient.PutAsync(url, content);
        });

        return await HandleResponseAsync<TResult>(response);
    }

    public async Task<ActionResult> PutAsync<TPayload>(string url, TPayload data)
    {
        return await PutAsync<ActionResult, TPayload>(url, data);
    }

    public async Task<ActionResult> PostAsync<TResult, TPayload>(string url, TPayload data)
    {
        var verb = HttpVerb.POST;
        _logger.LogInformation($"--> Requesting {url} with {verb}");

        var content = CreateContent(data);

        var retryPolicy = CreateRetryPolicy(url, verb);

        HttpResponseMessage response = null;
        await retryPolicy.ExecuteAsync(async () =>
        {
            response = await _httpClient.PostAsync(url, content);
        });

        return await HandleResponseAsync<TResult>(response);
    }

    public async Task<ActionResult> PostAsync<TPayload>(string url, TPayload data)
    {
        return await PostAsync<ActionResult, TPayload>(url, data);
    }

    #region Private Methods

    private async Task<ActionResult> HandleResponseAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
            {
                return new OkResult();
            }

            var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
            });

            return new OkObjectResult(result);
        }
        else
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound: 
                    return new NotFoundResult();
                case System.Net.HttpStatusCode.BadRequest: 
                    return new BadRequestResult();
                case System.Net.HttpStatusCode.Unauthorized:
                    return new UnauthorizedResult();
                default:
                    {
                        var e = new ArgumentOutOfRangeException();
                        _logger.LogError($"--> {response.StatusCode} status code result is not handled.");
                        throw e;
                    }
            }
        }
    }

    private StringContent CreateContent<T>(T payload)
    {
        var payloadContent = JsonSerializer.Serialize(payload, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
        });

        var httpContent = new StringContent(payloadContent, Encoding.UTF8, "application/json");

        var requestId = _httpContextAccessor.HttpContext.Request.Headers["x-requestid"].First();
        httpContent.Headers.Add("x-requestid", requestId);
        
        return httpContent;
    }

    private AsyncRetryPolicy CreateRetryPolicy(string url, HttpVerb verb)
    {
        const int retries = 10;
        var policy = Policy.Handle<SocketException>()
            .WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    _logger.LogWarning(exception, $"--> Attempt {retry}/{retries} to execute {verb} {url} fails with error {exception.Message}");
                });

        return policy;
    }

    #endregion

    private enum HttpVerb
    {
        GET,
        PUT,
        POST
    }

}
