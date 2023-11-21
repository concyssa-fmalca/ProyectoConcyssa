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


    public class SolcitudDespachoMovil
    {
        public int Id { get; set; }
        public string Correlativo { get; set; }
        public DateTime FechaDocumento { get; set; }
        public int EstadoSolicitud { get; set; }
        public int UsuarioCreador { get; set; }
        public int IdObra { get; set; }
        public string NombObra { get; set; }
        public int IdCuadrilla { get; set; }
        public string NombCuadrilla { get; set; }
        public int IdTipoProducto { get; set; }
        public string Comentario { get; set; }
        public IList<SolicitudDetalleMovil> Detalles { get; set; }
    }

    public class SolicitudDetalleMovil
    {
        public int Id { get; set; }
        public int IdItem { get; set; }
        public string CodigoArticulo { get; set; }

        public string Descripcion { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdGrupoUnidadMedida { get; set; }
        public int IdDefinicionGrupoUnidad { get; set; }
        public decimal Cantidad { get; set; }

        public decimal CantidadAtendida { get; set; }
    }

    public class SolicitudDetalleMovilAprobacion
    {
        public int IdArticulo { get; set; }
        public int IdSolicitud { get; set; }
        public string CodigoArticulo { get; set; }
        public string Descripcion1 { get; set; }
        public int Numero { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal CantidadAprobada { get; set; }
        public int Accion { get; set; }
        public int IdSolicitudDespachoModelo { get; set; }
        public int IdSolicitudDespachoDetalle { get; set; }
        public DateTime FechaDocumento { get; set; }
        public int EstadoSolicitud { get; set; }
        public int IdUsuario { get; set; }
        public int IdEtapa { get; set; }
        public string Serie { get; set; }
        public string Referencia { get; set; }
        public string NombUsuario { get; set; }





    }

}
