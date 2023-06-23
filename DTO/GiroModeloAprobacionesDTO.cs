using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GiroModeloAprobacionesDTO
    {
        public int IdGiroModeloAprobaciones { get; set; }

        public int IdGiro { get; set; }
        public int IdGiroModelo { get; set; }
        public int IdAutorizador { get; set; }
        public string IdDetalleGiro { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public int Accion { get; set; }
        public int IdSociedad { get; set; }
        public string NroDocumento { get; set; }
        //public decimal PrecioItem { get; set; }
        public int Estado { get; set; }

        public int IdDetalle { get; set; }
        public string Autorizador { get; set; }
        public string Proveedor { get; set; }
        //public string NombArticulo { get; set; }
        public string NombEstado { get; set; }
    }
}
