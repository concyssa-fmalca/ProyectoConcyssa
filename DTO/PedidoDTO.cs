using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public class PedidoDTO
    {
        public int DT_RowId { get; set; }
        public int IdPedido { get; set; }
        public int IdAlmacen { get; set; }
        public int IdSociedad { get; set; }
        public int IdProveedor { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public int IdTipoPedido { get; set; }
        public string LugarEntrega { get; set; }
        public int IdCondicionPago { get; set; }
        public int ElaboradoPor { get; set; }
        public int IdUsuario { get; set; }
        public int IdMoneda { get; set; }
        public string Observacion { get; set; }
        public int Serie { get; set; }
        public int Correlativo { get; set; }
        public decimal TipoCambio { get; set; }
        public IList<PedidoDetalleDTO> detalles { get; set; }
        public string NombAlmacen { get; set; }
        public string NombObra { get; set; }
        public string NombBase { get; set; }
        public string NumProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string NombMoneda { get; set; }
        public string NombTipoPedido { get; set; }
        public int IdBase { get; set; }
        public int IdObra { get; set; }


    }
    public class PedidoDetalleDTO
    {
        public int DT_RowId { get; set; }
        public int IdPedidoDetalle {get;set;}
        public int IdPedido {get;set;}
        public string DescripcionArticulo { get; set; } 
        public int IdArticulo { get;set;}
        public int IdGrupoArticulo { get;set;}
        public int IdDefinicion { get;set;}
        public decimal Cantidad {get;set;}
        public decimal valor_unitario {get;set;}
        public decimal precio_unitario {get;set;}
        public string codigo_tipo_afectacion_igv {get;set;}
        public decimal total_base_igv {get;set;}
        public decimal porcentaje_igv {get;set;}
        public decimal total_igv {get;set;}
        public decimal total_impuestos {get;set;}
        public decimal total_valor_item {get;set;}
        public decimal total_item {get;set;}
        public int IdIndicadorImpuesto { get; set; }
        public string CodImpuesto { get; set; }
        public string NombImpuesto { get; set; }
        public int IdGrupoUnidadMedida { get; set; }
    }
}
