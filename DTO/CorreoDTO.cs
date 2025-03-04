using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CorreoDTO
    {
        public string Nombre {get;set;}
        public string Servidor {get;set;}
        public int Puerto {get;set;}
        public string Email {get;set;}
        public string Clave {get;set;}
        public bool SSL { get; set; }
    }
}
