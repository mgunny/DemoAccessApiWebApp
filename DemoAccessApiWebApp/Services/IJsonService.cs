using System.Net.Http;

namespace DemoAccessApiWebApp.Services
{
    public interface IJsonService
    {
        T FromJson<T>(string json);
        string ToJson<T>(T objectToSerialize);
        StringContent ToJsonStringContent<T>(T objectToSerialize);
    }
}