using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SemanaDTO
    {
        public int IdSemana { get; set; }
        public DateTime FechaI { get; set; }
        public DateTime FechaF { get; set; }
        public int NumSemana { get; set; }
        public bool Estado { get; set; }
        public int IdSociedad { get; set; }
        public int Anio { get; set; }
        public int IdTipoRegistro { get; set; }
        public int IdObra { get; set; }
        public string? TipoRegistro { get; set; }
        public string? Obra { get; set; }
        public string? Descripcion { get; set; }
        public decimal Fondo { get; set; }


    }
}
