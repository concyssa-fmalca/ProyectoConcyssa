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
#pragma warning disable CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Codigo { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "Descripcion1" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Descripcion1 { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Descripcion1" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "Descripcion2" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Descripcion2 { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Descripcion2" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public int IdUnidadMedida { get; set; }
        public bool ActivoFijo { get; set; }
        public bool ActivoCatalogo { get; set; }
        public int IdCodigoUbso { get; set; }
        public int IdSociedad { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public bool Inventario { get; set; }
        public bool Compra { get; set; }
        public bool Venta { get; set; }

        public string UnidadMedida { get; set; }

        public decimal Stock { get; set; }
        public int IdGrupoUnidadMedida { get; set; }
        public int IdUnidadMedidaInv { get; set; }

        //Opcional
        public decimal PrecioPromedio { get; set; }
        public string NombUnidadMedida { get; set; }

        public decimal UltimoPrecioCompra { get; set; }
        public int IdProveedor { get; set; }
    }
}
