using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VehiculoDTO
    {
        public int IdVehiculo { get; set; }
        public int IdMarca { get; set; }
        public int IdBase { get; set; }
        public string Condicion { get; set; }
        public string CertificadoInscripcion { get; set; }
        public string Placa { get; set; }
        public int IdChofer { get; set; }
        public int IdSociedad { get; set; }

        public bool Estado { get; set; }
        public bool Eliminado { get; set; }

        //Parametros que llenan datos
        public string MarcaDescripcion { get; set; }
        public string BaseDescripcion { get; set; }

        public string ChoferDescripcion { get; set; }
}
}
