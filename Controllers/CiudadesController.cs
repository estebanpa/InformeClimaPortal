#nullable disable
using Microsoft.AspNetCore.Mvc;
using InformeClimaPortal.Entities;
using InformeClimaPortal.Services.OpenWeatherMap;
using InformeClimaPortal.Services.InformeClima;

namespace InformeClimaPortal.Controllers
{
    public class CiudadesController : Controller
    {
        private readonly IInformeService _informeService;
        private readonly IOpenWeatherMapService _openWeatherMap;

        public CiudadesController(IInformeService informeService, IOpenWeatherMapService openWeatherMap)
        {
            _informeService = informeService;
            _openWeatherMap = openWeatherMap;
        }

        // GET: Ciudades
        public async Task<IActionResult> Index()
        {
            var ciudades = await _informeService.Ciudades();

            return View(ciudades);
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _informeService.Ciudad(id.Value);

            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string name)
        {
            Ciudad ciudad = null;

            if (!string.IsNullOrEmpty(name))
            {
                var resultResponse = await _openWeatherMap.SearchCity(name);
                if (resultResponse != null)
                {
                    ciudad = new Ciudad();
                    ciudad.ExternalId = resultResponse.Id;
                    ciudad.Name = resultResponse.Name;

                    ciudad.Pais = new Pais();
                    ciudad.Pais.ExternalId = resultResponse.Sys.Id;
                    ciudad.Pais.Name = resultResponse.Sys.Country;
                }
            }

            return View(ciudad);
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                if (!await _informeService.CiudadExists(ciudad.ExternalId))
                {
                    await _informeService.AddCiudad(ciudad);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(ciudad);
        }


        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await _informeService.Ciudad(id.Value);

            if (ciudad == null)
            {
                return NotFound();
            }

            return View(ciudad);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudad = await _informeService.Ciudad(id);

            await _informeService.DeleteCiudad(ciudad);

            return RedirectToAction(nameof(Index));
        }
    }
}
