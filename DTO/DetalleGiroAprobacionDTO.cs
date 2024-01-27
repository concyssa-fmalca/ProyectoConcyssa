using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public class GiroAprobacionDTO
    {
        public int IdGiro { get; set; }
        public string Solicitante { get; set; }
        public string Obra { get; set; }
        public string Semana { get; set; }
        public string Creador { get; set; }

        public DateTime Fecha { get; set; }
        public decimal MontoSoles { get; set; }
        public decimal MontoDolares { get; set; }
        public string SerieNumGiro { get; set; }
    }


    public class DetalleGiroAprobacionDTO
    {
        public int IdGiro { get; set; }
        public int IdGiroDetalle { get; set; }
        public int IdGiroModelo { get; set; }
        public string NumeroDocumento { get; set; }
        public string Moneda { get; set; }
        public decimal Monto { get; set; }
        public string Anexo { get; set; }
        public string Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public int IdDetalle { get; set; }
        public int IdCreador { get; set; }

        public int Accion { get; set; }
        public int IdUsuario { get; set; }
        public string Comentario { get; set; }
        public string SerieNumGiro { get; set; }
    }
}
