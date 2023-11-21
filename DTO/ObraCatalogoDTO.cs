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

    public class ObraCatalogoServicioDTO
    {
        public int IdObraCatalogoServicios { get; set; }
        public int IdObra { get; set; }
        public int IdArticulo { get; set; }
        public bool Estado { get; set; }
        public string DescripcionArticulo { get; set; }
        public string CuentaContable { get; set; }
        public string Codigo { get; set; }


    }
}
