using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ObraDTO
    {
        public int IdObra { get; set; }
        public int IdBase { get; set; }
        public int IdTipoObra { get; set; }
        public int IdDivision { get; set; }
        public int IdSociedad { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public bool ContratoMantenimiento { get; set; }
        public bool VisibleInternet { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }

        //Campos Opcionales
        public string DescripcionBase { get; set; }
    }
}
