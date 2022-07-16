using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GrupoArticuloDTO
    {
        public int IdGrupoArticulo { get; set; }
        public int IdSociedad { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        
        public bool Estado { get; set; }
        

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaUltimaModificacion { get; set; }

        public int UsuarioCreacion { get; set; }

        public int UsuarioUltimaModificacion { get; set; }

        

        public string CtaGasto { get; set; }
        public string CtaIngresos { get; set; }
        public string CtaExistencias { get; set; }
        public string CtaCostoBienes { get; set; }
        public string CtaDotacion { get; set; }
        public string CtaDesviacion { get; set; }
        public string CtaDiferenciaPrecios { get; set; }
        public string CtaAjusteStockNegativo { get; set; }
        public string CtaReduccion { get; set; }
        public string CtaAumento { get; set; }
        public string CtaDevolucionxVentas { get; set; }
        public string CtaIngresosExtranjeros { get; set; }
        public string CtaCostosExtranjeros { get; set; }
        public string CtaCompras { get; set; }
        public string CtaComprasDevolucion { get; set; }
        public string CtaCostosMercanciasCompradas { get; set; }
        public string CtaDiferenciaTipoCambio { get; set; }
        public string CtaCompensacionMercancias { get; set; }
        public string CtaReduccionLibroMayor { get; set; }
        public string CtaStockTrabajoEnCurso { get; set; }
        public string CtaDesviacionStockWIP { get; set; }
        public string CtaRevalorizacionStock { get; set; }
        public string CtaContrapartidaRevalorizacionInventario { get; set; }
        public string CtaCompensacionGasto { get; set; }
        public string CtaStockTransito { get; set; }
        public string CtaCreditoVentas { get; set; }
        public string CtaCreditoCompras { get; set; }
    }
}
