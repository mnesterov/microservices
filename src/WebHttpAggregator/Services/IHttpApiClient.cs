using Microsoft.AspNetCore.Mvc;

namespace WebHttpAggregator.Services
{
    public interface IHttpApiClient
    {
        Task<ActionResult<T>> GetAsync<T>(string url);

        Task<ActionResult> PutAsync<TResult, TPayload>(string url, TPayload data);
        Task<ActionResult> PutAsync<TPayload>(string url, TPayload data);

        Task<ActionResult> PostAsync<TResult, TPayload>(string url, TPayload data);
        Task<ActionResult> PostAsync<TPayload>(string url, TPayload data);
    }
}
