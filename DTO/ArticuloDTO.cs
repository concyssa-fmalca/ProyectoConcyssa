using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ArticuloDTO
    {
        public int IdArticulo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion1 { get; set; }
        public string Descripcion2 { get; set; }
        public int IdUnidadMedida { get; set; }
        public bool ActivoFijo { get; set; }
        public bool ActivoCatalogo { get; set; }
        public int IdCodigoUbso { get; set; }
        public int IdSociedad { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
    }
}
