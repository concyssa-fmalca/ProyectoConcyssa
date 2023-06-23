using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudDetalleDTO
    {

        public int IdSolicitudRQDetalle { get; set; }
        public int IdSolicitudCabecera { get; set; }
        public string IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public string IdUnidadMedida { get; set; }
        public string CodUnidadMedida { get; set; }
        public DateTime FechaNecesaria { get; set; }
        public decimal CantidadNecesaria { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal CantidadProcesar { get; set; }
        public decimal PrecioInfo { get; set; }
        public int IdIndicadorImpuesto { get; set; }
        public decimal ItemTotal { get; set; }
        public string IdAlmacen { get; set; }
        public int IdProveedor { get; set; }
        public string NumeroFabricacion { get; set; }
        public string NumeroSerie { get; set; }
        public int IdLineaNegocio { get; set; }
        public string IdCentroCostos { get; set; }
        public string IdProyecto { get; set; }
        public string IdItemMoneda { get; set; }
        public decimal ItemTipoCambio { get; set; }
        public string Referencia { get; set; }
        public string DescripcionItem { get; set; }
        public int EstadoDetalle { get; set; } //Estado de la tabla solicitud detalle siempres va a ser 1 = Pendiente
        public string EstadoDescripcion { get; set; }  //Descripcion del Estado de la tabla solicitud detalle siempres va a ser 1 = Pendiente
        public int Prioridad { get; set; }
        public int EstadoItemAutorizado { get; set; } //Estado de la tabla de aprbaciones de item Pendiete aceptado rechazado
        public int EstadoDisabled { get; set; } // Estado para ver si estan completas las aprobaciones y bloquea la linea o no

        public decimal TotalCompraLocal { get; set; }
        public decimal TotalCompraLima { get; set; }
        public decimal TotalDespacho { get; set; }

        public int IdDetalle { get; set; }

        public int AprobadoAnterior { get; set; }

        public string CodArticulo { get; set; }

        public string NombAlmacen { get; set; } = "";
        public string NombBase { get; set; } = "";
        public string NombObra { get; set; } = "";

        public int IdTipoProducto { get; set; } = 1;


        public string SeriePedido { get; set; }
        public DateTime FechaDocumentoPedido { get; set; }
        public string ConformidadPedido { get; set; }

    }
}
