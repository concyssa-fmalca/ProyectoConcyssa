using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class CodigoUbsoDTO
    {
        public int IdCodigoUbso { get; set; }
        public int IdSociedad { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Codigo { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "Descripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Descripcion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Descripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public bool Estado { get; set; }
        public bool eliminado { get; set; }
    }
}
