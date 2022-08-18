using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DefinicionGrupoUnidadDTO
    {
        public int IdDefinicionGrupo { get; set; }
        public int IdGrupoUnidadMedida { get; set; }
        public int IdUnidadMedidaBase { get; set; }
        public decimal CantidadBase { get; set; }
        public int IdUnidadMedidaAlt { get; set; }
        public decimal CantidadAlt { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "DescUnidadMedidaAlt" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string DescUnidadMedidaAlt { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "DescUnidadMedidaAlt" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "DescUnidadMedidaBase" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string DescUnidadMedidaBase { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "DescUnidadMedidaBase" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
    }
}
