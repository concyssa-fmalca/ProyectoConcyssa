namespace DTO
{
    public  class PerfilDTO
    {
        public int IdPerfil { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "Perfil" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Perfil { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Perfil" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public bool Estado { get; set; }
        public int IdSociedad { get; set; }
    }
}
