using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PeriodoContableFechaDTO
    {
        public int IdPeriodoContableFecha { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int IdPeriodoContable { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public int StatusPeriodo { get; set; } = 0;
    }
}
