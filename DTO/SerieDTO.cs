using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SerieDTO
    {
        public int IdSerie { get; set; } =0;
#pragma warning disable CS8618 // El elemento propiedad "Serie" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Serie { get; set; } = "";
#pragma warning restore CS8618 // El elemento propiedad "Serie" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public int NumeroInicial { get; set; } = 1;
        public int NumeroFinal { get; set; } = 999999;
        public bool Estado { get; set; }=true;
        public bool Eliminado { get; set; }=false;
        public int IdSociedad { get; set; } = 1;    
        public int Documento { get; set; } = 1; 

        public int IdDocumento { get; set; } = 1;   
        public string NombDocumento { get; set; } = "";
        public string SerieDefecto { get; set; } = "-";
        public int IdPeriodo { get; set; } = 1; 
        public Boolean IsArticulo { get; set; }=false;
        public Boolean IsServicio { get; set; } = false;
        public string NombIndicadorPeriodo { get; set; }
        public DateTime FechaI { get; set; }
        public DateTime FechaF { get; set; }

        public List<PeriodoContableFechaDTO> FechaRelacion { get; set; }



    }
}
