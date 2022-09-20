using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudRQModeloDTO
    {
        public int IdSolicitudRQModelo { get; set; }
        public int IdSolicitud { get; set; }
        public int IdModelo { get; set; }
        public int IdEtapa { get; set; }
        public int Aprobaciones { get; set; }
        public int Rechazos { get; set; }
        public int IdSociedad { get; set; }
    }
}
