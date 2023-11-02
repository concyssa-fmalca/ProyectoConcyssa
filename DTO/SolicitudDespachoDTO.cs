using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudDespachoDTO
    {
        public int Id { get; set; }
        public int IdSerie { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
        public int IdClaseProducto { get; set; }
        public int IdTipoProducto { get; set; }
        public int IdCuadrilla { get; set; }
        public string DescripcionCuadrilla { get; set; }
        public int IdObra { get; set; }
        public string DescripcionObra { get; set; }
        public int IdBase { get; set; }
        public string DescripcionBase { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string SerieyNum { get; set; }
        public string NombCuadrilla { get; set; }
        public int IdSolicitante { get; set; }
        public int EstadoSolicitud { get; set; }
        public List<SolicitudDespachoDetalleDTO> Detalle { get; set; }
        public IList<SolicitudDespachoDetalleDTO> Detalles { get; set; }

    }


    public class SolicitudDespachoDetalleDTO
    {
        public int Id { get; set; }
        public int IdSolicitudDespacho { get; set; }

        public string CodigoArticulo { get; set; }
        public int IdItem { get; set; }
        public string Descripcion { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdGrupoUnidadMedida { get; set; }
        public int IdDefinicionGrupoUnidad { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string SerieyNum { get; set; }
        public decimal CantidadAtendida { get; set; }
        public int IdTipoProducto { get; set; }
        public int IdObra { get; set; }
        public string NombCuadrilla { get; set; }

    }

}
