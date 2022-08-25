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
#pragma warning disable CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Codigo { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "Descripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Descripcion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Descripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "DescripcionCorta" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string DescripcionCorta { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "DescripcionCorta" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public bool ContratoMantenimiento { get; set; }
        public bool VisibleInternet { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }

        //Campos Opcionales
#pragma warning disable CS8618 // El elemento propiedad "DescripcionBase" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string DescripcionBase { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "DescripcionBase" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.

      
    }
}
