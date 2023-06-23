using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GiroAutorizacionDTO
    {

        public int IdGiro { get; set; }
        public int UsuarioAprobador { get; set; }
        public int IdResponsable { get; set; }

        public int IdSolicitante { get; set; }
        public int IdObra { get; set; }
        public int IdSemana { get; set; }
        public decimal MontoDolares { get; set; }
        public decimal MontoSoles { get; set; }
        public decimal Total { get; set; }

        public DateTime Fecha { get; set; }

        public int IdGiroModelo { get; set; }

        public string NombEtapa { get; set; }
        public int IdEtapa { get; set; }
    }
}
