using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DevolucionAdmDTO
    {
        public int Id { get; set; }
        public string Correlativo { get; set; }
        public DateTime FechaDocumento { get; set; }
        public int EstadoDevolucion { get; set; }
        public int UsuarioCreador { get; set; }
        public int IdObra { get; set; }
        public string NombObra { get; set; }
        public int IdCuadrilla { get; set; }
        public string NombCuadrilla { get; set; }
        public int IdTipoProducto { get; set; }
        public string Comentario { get; set; }
        public IList<DevolucionAdmDetalle> Detalles { get; set; }

        ///////// WEB
        public int IdSerie { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
        public int IdClaseProducto { get; set; }
        public int IdBase { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public string SerieyNum { get; set; }
        public int IdSolicitante { get; set; }


    }
    public class DevolucionAdmDetalle
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

        //web
        public int IdDevolucionAdm { get; set; }
    }
}
