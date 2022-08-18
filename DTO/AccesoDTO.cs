using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AccesoDTO
    {
        public int IdAceeso { get; set; }
        public int IdPerfil { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "Perfil" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Perfil { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Perfil" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public int IdMenu { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "Menu" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Menu { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Menu" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public bool Estado { get; set; }
    }
}
