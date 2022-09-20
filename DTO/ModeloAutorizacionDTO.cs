using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ModeloAutorizacionDTO
    {
        public int IdModeloAutorizacion { get; set; }
        public string NombreModelo { get; set; }
        public string DescripcionModelo { get; set; }
        public bool Estado { get; set; }
        public int IdSociedad { get; set; }

        public List<ModeloAutorizacionAutorDTO> DetalleAutor { get; set; }
        public IList<ModeloAutorizacionAutorDTO> DetallesAutor;

        public List<ModeloAutorizacionCondicionDTO> DetalleCondicion { get; set; }
        public IList<ModeloAutorizacionCondicionDTO> DetallesCondicion;

        public List<ModeloAutorizacionDocumentoDTO> DetalleDocumento { get; set; }
        public IList<ModeloAutorizacionDocumentoDTO> DetallesDocumento;

        public List<ModeloAutorizacionEtapaDTO> DetalleEtapa { get; set; }
        public IList<ModeloAutorizacionEtapaDTO> DetallesEtapa;
    }

    public class ModeloAutorizacionAutorDTO
    {
        public int IdModeloAutorizacionAutor { get; set; }
        public int IdModeloAutorizacion { get; set; }
        public int IdAutor { get; set; }
        public int IdDepartamento { get; set; }
        public int IdSociedad { get; set; }
    }

    public class ModeloAutorizacionCondicionDTO
    {
        public int IdModeloAutorizacionCondicion { get; set; }
        public int IdModeloAutorizacion { get; set; }
        public string Condicion { get; set; }
        public int IdSociedad { get; set; }
    }

    public class ModeloAutorizacionDocumentoDTO
    {
        public int IdModeloAutorizacionDocumento { get; set; }
        public int IdModeloAutorizacion { get; set; }
        public int IdDocumento { get; set; }
        public int IdSociedad { get; set; }
    }

    public class ModeloAutorizacionEtapaDTO
    {
        public int IdModeloAutorizacionEtapa { get; set; }
        public int IdModeloAutorizacion { get; set; }
        public int IdEtapa { get; set; }
        public string DescripcionEtapa { get; set; }
        public int IdSociedad { get; set; }
        public int AutorizacionesRequeridas { get; set; }
        public int RechazosRequeridos { get; set; }
    }
}
