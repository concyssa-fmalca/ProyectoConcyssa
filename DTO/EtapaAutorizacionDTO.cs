using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public class EtapaAutorizacionDTO
    {
        public int IdEtapaAutorizacion { get; set; }
        public string NombreEtapa { get; set; }
        public string DescripcionEtapa { get; set; }
        public int AutorizacionesRequeridas { get; set; }
        public int RechazosRequeridos { get; set; }
        public bool Estado { get; set; }

        public List<EtapaAutorizacionDetalleDTO> Detalle { get; set; }
        public IList<EtapaAutorizacionDetalleDTO> Detalles;
    }

    public class EtapaAutorizacionDetalleDTO
    {
        public int IdEtapaAutorizacionDetalle { get; set; }
        public int IdEtapaAutorizacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdDepartamento { get; set; }
        public int IdSociedad { get; set; }
    }
}
