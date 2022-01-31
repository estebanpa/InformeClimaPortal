using InformeClimaPortal.Services.OpenWeatherMap.Response;

namespace InformeClimaPortal.Services.OpenWeatherMap
{
    public interface IOpenWeatherMapService
    {
        Task<ResultResponse> SearchCity(string name);
        Task<ResultResponse> SearchCity(int externalId);
    }
}
