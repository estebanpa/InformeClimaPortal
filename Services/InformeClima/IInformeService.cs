using InformeClimaPortal.Entities;

namespace InformeClimaPortal.Services.InformeClima
{
    public interface IInformeService
    {
        Task AddCiudad(Ciudad ciudad);
        Task DeleteCiudad(Ciudad ciudad);
        Task<List<Ciudad>> Ciudades();
        Task<Ciudad> Ciudad(int id);
        Task<Ciudad> CiudadExternalId(int externalId);
        Task<bool> CiudadExists(int externalId);

        Task AddInforme(Informe informe);
        Task<List<Informe>> Informes(int externalId);
        Task<Informe> UltimoInforme(int externalId);
    }
}
