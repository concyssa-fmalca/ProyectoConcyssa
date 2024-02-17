using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PedidoModeloDTO
    {
        public int IdPedidoModelo { get; set; }
        public int IdPedido { get; set; }
        public int IdModelo { get; set; }
        public int IdEtapa { get; set; }
        public int Aprobaciones { get; set; }
        public int Rechazos { get; set; }
        public int IdSociedad { get; set; }

        public IList<PedidoModeloAprobacionesDTO> ListModeloAprobacionesDTO;
    }
    public class PedidoModeloAprobacionesDTO
    {

        public int IdPedido { get; set; }
        public int IdPedidoModelo { get; set; }
        public int IdAutorizador { get; set; }
        public string Autorizador { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public int Accion { get; set; }
        public int Estado { get; set; }

        public string NombEstado { get; set; }
        public string Referencia { get; set; }
    }
}