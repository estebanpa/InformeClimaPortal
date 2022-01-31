using System.ComponentModel;

namespace InformeClimaPortal.Entities
{
    public class Informe
    {
        public int InformeId { get; set; }

        [DisplayName("Fecha")]
        public DateTime FechaHora { get; set; }
        public Ciudad Ciudad { get; set; }

        [DisplayName("Clima")]
        public string Clima { get; set; }

        [DisplayName("Sensación Térmica")]
        public string SensacionTermica { get; set; }
    }
}
