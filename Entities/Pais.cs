using System.ComponentModel;

namespace InformeClimaPortal.Entities
{
    public class Pais
    {
        [DisplayName("Pais Id")]
        public int PaisId { get; set; }

        [DisplayName("Pais")]
        public string Name { get; set; }

        [DisplayName("Pais External Id")]
        public int ExternalId { get; set; }
    }
}