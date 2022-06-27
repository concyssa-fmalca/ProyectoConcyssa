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
        public string Perfil { get; set; }
        public int IdMenu { get; set; }
        public string Menu { get; set; }
        public bool Estado { get; set; }
    }
}
