using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GlosaContableDTO
    {
        private string division;

        public int IdGlosaContable { get; set; }
        public int IdDivision { get; set; }
        public string Division { get => division; set => division = value; }
        public int IdSociedad { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? CuentaContable { get; set; }
        public bool Estado { get; set; }
    }
}
