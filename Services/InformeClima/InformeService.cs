using InformeClimaPortal.DataContext;
using InformeClimaPortal.Entities;
using Microsoft.EntityFrameworkCore;

namespace InformeClimaPortal.Services.InformeClima
{
    public class InformeService : IInformeService
    {
        private readonly InformeClimaDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly int _cantidadMaxima;

        public InformeService(InformeClimaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _cantidadMaxima = _configuration.GetValue<int>("Informes_CantidadMaxima");
        }

        public async Task AddCiudad(Ciudad ciudad)
        {
            _context.Add(ciudad);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCiudad(Ciudad ciudad)
        {
            _context.Remove(ciudad);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ciudad>> Ciudades()
        {
            return await _context.Ciudades
                .Include(x => x.Pais)
                .ToListAsync();
        }

        public async Task<Ciudad> Ciudad(int id)
        {
            return await _context.Ciudades
                .Include(x => x.Pais)
                .FirstOrDefaultAsync(m => m.CiudadId == id);
        }

        public async Task<Ciudad> CiudadExternalId(int externalId)
        {
            return await _context.Ciudades
                .Include(x => x.Pais)
                .FirstOrDefaultAsync(m => m.ExternalId == externalId);
        }

        public Task<bool> CiudadExists(int externalId)
        {
            return Task.FromResult(_context.Ciudades.Include(x => x.Pais).Any(e => e.ExternalId == externalId));
        }

        public async Task AddInforme(Informe informe)
        {
            _context.Add(informe);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Informe>> Informes(int externalId)
        {
            return await _context.Informes
                .Include(x => x.Ciudad)
                .Include(x => x.Ciudad.Pais)
                .Where(x => x.Ciudad.ExternalId == externalId)
                .OrderByDescending(x => x.FechaHora)
                .Take(_cantidadMaxima)
                .ToListAsync();
        }

        public async Task<Informe> UltimoInforme(int externalId)
        {
            return await _context.Informes
                .Include(x => x.Ciudad)
                .Include(x => x.Ciudad.Pais)
                .Where(x => x.Ciudad.ExternalId == externalId)
                .OrderByDescending(x => x.FechaHora)
                .FirstOrDefaultAsync();
        }
    }
}
