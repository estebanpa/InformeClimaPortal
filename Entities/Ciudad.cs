using System.ComponentModel;

namespace InformeClimaPortal.Entities
{
    public class Ciudad
    {
        [DisplayName("Ciudad Id")]
        public int CiudadId { get; set; }

        [DisplayName("Ciudad")]
        public string Name { get; set; }

        [DisplayName("Ciudad External Id")]
        public int ExternalId { get; set; }
        public Pais Pais { get; set; }
    }
}