using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudRQAutorizacionDTO
    {
        public int IdSolicitud { get; set; }
        public int UsuarioAprobador { get; set; }
        public string NumeroSolicitud { get; set; }
        public string Solicitante { get; set; }
        public string Area { get; set; }
        public string TipoArticulo { get; set; }
        public int IdClaseArticulo { get; set; }
        public string Moneda { get; set; }
        public string Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal Neto { get; set; }
        public decimal TotalIgv { get; set; }
        public string Prioridad { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string FechaAprobacion { get; set; }
        public DateTime FechaValidoHasta { get; set; }
        public int IdSociedad { get; set; }

        public int IdSolicitudModelo { get; set; }
        public int IdEtapa { get; set; }

    }
}
