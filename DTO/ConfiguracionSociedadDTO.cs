using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ConfiguracionSociedadDTO
    {
        public int Id { get; set; }
        public string Ruc { get; set; } = "";
        public string RazonSocial { get; set; }="";
        public string Direccion { get; set; }="";
        public string NombreBDSAP { get; set; }="";
        public string Alias { get; set; }="";
        public string ctaAsocFT { get; set; }="";
        public string ctaAsocNC { get; set; }="";
    }
}
