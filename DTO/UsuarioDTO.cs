using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Usuario { get; set; }
        public string? Password { get; set; }
        public int IdPerfil { get; set; }
        public string? NombrePerfil { get; set; }
        public int IdSociedad { get; set; }
        public string? NombreSociedad { get; set; }
        public string? NumeroDocumento { get; set; }

        public bool Estado { get; set; }
        public string? SapUsuario { get; set; }
        public string? SapPassword { get; set; }
        
        public int IdBase { get; set; }
        public bool MovimientoInventario { get; set; }

        public string NombreUsuario { get; set; }
        public int IdDepartamento { get; set; }
        public string Correo { get; set; }
    }
}
