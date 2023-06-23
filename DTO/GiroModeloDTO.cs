using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GiroModeloDTO
    {

        public int IdGiroModelo { get; set; }
        public int IdGiro { get; set; }
        public int IdModelo { get; set; }
        public int IdEtapa { get; set; }
        public int Aprobaciones { get; set; }
        public int Rechazos { get; set; }
        public int IdSociedad { get; set; }

        public IList<GiroModeloAprobacionesDTO> ListModeloAprobacionesDTO;

    }



    //public class GiroModeloAprobacionesDTO
    //{
    //    public int IdGiroModeloAprobaciones { get; set; }

    //    public int IdGiro { get; set; }
    //    public int IdGiroModelo { get; set; }
    //    public int IdAutorizador { get; set; }
    //    public string IdDetalleGiro { get; set; }
    //    public DateTime FechaAprobacion { get; set; }
    //    public int Accion { get; set; }
    //    public int IdSociedad { get; set; }
    //    public decimal NroDocumento { get; set; }
    //    //public decimal PrecioItem { get; set; }
    //    public int Estado { get; set; }

    //    public int IdDetalle { get; set; }

    //    //public string NombArticulo { get; set; }
    //    //public string NombEstado { get; set; }
    //}

}
