#nullable disable
using InformeClimaPortal.Entities;
using InformeClimaPortal.Services.InformeClima;
using InformeClimaPortal.Services.OpenWeatherMap;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InformeClimaPortal.Controllers
{
    public class InformesController : Controller
    {
        private readonly IInformeService _informeService;
        private readonly IOpenWeatherMapService _openWeatherMapService;

        public InformesController(IInformeService informeService, IOpenWeatherMapService openWeatherMapService)
        {
            _informeService = informeService;
            _openWeatherMapService = openWeatherMapService;
        }

        // GET: Informes
        public async Task<IActionResult> Index(bool historico, int? externalId)
        {
            var ciudades = await _informeService.Ciudades();

            ViewBag.Ciudades = new SelectList(ciudades, "ExternalId", "Name");

           
            var result = new List<Informe>();

            if (externalId.HasValue)
            {
                if (historico)
                {
                    var informes = _informeService.Informes(externalId.Value).Result;

                    result = informes;
                }
                else
                {
                    var informe = _informeService.UltimoInforme(externalId.Value).Result;
                    result.Add(informe);
                }
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(int externalId, bool historico)
        {
            var resultResponse = await _openWeatherMapService.SearchCity(externalId);
            
            if (resultResponse != null)
            {
                var informe = new Informe();
                informe.Clima = $"{resultResponse.Main.Temp}°";
                informe.SensacionTermica = $"{resultResponse.Main.FeelsLike}°";

                var ciudad = _informeService.CiudadExternalId(resultResponse.Id).Result;

                informe.Ciudad = ciudad;
                informe.FechaHora = DateTime.Now;

                await _informeService.AddInforme(informe);
            }

            return RedirectToAction(nameof(Index), new { historico, externalId});
        }
    }
}
