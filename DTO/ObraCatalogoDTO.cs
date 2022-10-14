using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ObraCatalogoDTO
    {
        public int IdObraCatalogo { get; set; }
        public int IdObra {get;set;}
        public int IdArticulo { get; set; }
        public int IdTipoProducto { get; set; }
        public string DescripcionArticulo { get; set; }
        public string Codigo { get; set; }


    }
}
