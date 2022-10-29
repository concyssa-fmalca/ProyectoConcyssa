using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PeriodoContableDTO
    {
        public int IdPeriodoContable { get; set; }
        public string CodigoPeriodo { get; set; } = "";
        public string NombrePeriodo { get; set; } = "";
        public int SubPeriodo { get; set; } = 0;
        public int IdIndicadorPeriodo { get; set; } = 0;
        public int StatusPeriodo { get; set; } = 0;
        public DateTime InicioEjercicio { get; set; } = DateTime.Now;
        public string Ejercicio { get; set; } = "";
        public int IdSociedad { get; set; } = 0;
        public int IdUsuario { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Boolean Eliminado { get; set; } = false;

        public Boolean Estado { get; set; } = true;
        public string NombUsuario { get; set; } = "";
        public IList<PeriodoContableFechaDTO> Detalles { get; set; }

        public DateTime FechaContabilizacionI { get; set; } = DateTime.Now;
        public DateTime FechaContabilizacionF { get; set; } = DateTime.Now;

    }
}
