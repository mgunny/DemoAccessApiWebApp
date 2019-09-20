using DemoAccessApiWebApp.Models;
using System.Threading.Tasks;

namespace DemoAccessApiWebApp.Services
{
    public interface IAPIService
    {
        Task<ApiHttpResponse<T>> GetDataAsync<T>(string Url, string jwt = null);     
        Task<ApiHttpResponse> PostDataAsync(string Url, object content, string jwt = null);
        Task<ApiHttpResponse<T>> PostDataAsync<T>(string Url, object content, string jwt = null);
    }
}