using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CuadrillaDTO
    {
        public int IdCuadrilla { get; set; }
        public int IdObra { get; set; }
        public int IdGrupo { get; set; }
        public int IdSubGrupo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion{ get; set; }

        public int IdCapataz { get; set; }
        public int IdSupervisor { get; set; }
        public int IdArea { get; set; }
        public int IdSociedad { get; set; }
        public bool IsActivoFijo { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public bool EsTercero { get; set; }
        public string CuentaServicios { get; set; }
        public string CuentaMateriales { get; set; }
    }
}
