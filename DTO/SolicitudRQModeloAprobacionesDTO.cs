using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudRQModeloAprobacionesDTO
    {
        public int IdSolicitudRQModeloAprobaciones { get; set; }

        public int IdSolicitud { get; set; }
        public int IdSolicitudModelo { get; set; }
        public int IdAutorizador { get; set; }
        public string IdArticulo { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public int Accion { get; set; }
        public int IdSociedad { get; set; }
        public decimal CantidadItem { get; set; }
        public decimal PrecioItem { get; set; }
        public int Estado { get; set; }

        public int IdDetalle { get; set; }

        public string NombArticulo { get; set; }
        public string NombEstado { get; set; }
    }
}
