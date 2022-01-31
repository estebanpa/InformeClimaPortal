using InformeClimaPortal.Services.OpenWeatherMap.Response;
using Newtonsoft.Json;

namespace InformeClimaPortal.Services.OpenWeatherMap
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private readonly string _apikey;

        public OpenWeatherMapService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration.GetValue<string>("OpenWeatherMap_BaseUrl"));
            _apikey = _configuration.GetValue<string>("OpenWeatherMap_ApiKey");
        }

        public Task<ResultResponse> SearchCity(string name)
        {
            try
            {
                var queryString = $"?q={name}&appid={_apikey}&units=metric&lang=sp";
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + queryString).Result;
                if (response.IsSuccessStatusCode)
                {
                    string _data = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ResultResponse>(_data);

                    return Task.FromResult<ResultResponse>(result);
                }

                return Task.FromResult<ResultResponse>(default);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<ResultResponse> SearchCity(int externalId)
        {
            try
            {
                var queryString = $"?id={externalId}&appid={_apikey}&units=metric&lang=sp";
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + queryString).Result;
                if (response.IsSuccessStatusCode)
                {
                    string _data = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ResultResponse>(_data);

                    return Task.FromResult<ResultResponse>(result);
                }

                return Task.FromResult<ResultResponse>(default);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
