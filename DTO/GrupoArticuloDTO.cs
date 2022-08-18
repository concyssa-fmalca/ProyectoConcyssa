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
#pragma warning disable CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Codigo { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "Descripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Descripcion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Descripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public bool Eliminado { get; set; }
        
        public bool Estado { get; set; }
        

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaUltimaModificacion { get; set; }

        public int UsuarioCreacion { get; set; }

        public int UsuarioUltimaModificacion { get; set; }

        

#pragma warning disable CS8618 // El elemento propiedad "CtaGasto" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaGasto { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaGasto" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaIngresos" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaIngresos { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaIngresos" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaExistencias" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaExistencias { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaExistencias" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaCostoBienes" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaCostoBienes { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaCostoBienes" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaDotacion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaDotacion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaDotacion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaDesviacion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaDesviacion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaDesviacion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaDiferenciaPrecios" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaDiferenciaPrecios { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaDiferenciaPrecios" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaAjusteStockNegativo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaAjusteStockNegativo { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaAjusteStockNegativo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaReduccion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaReduccion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaReduccion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaAumento" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaAumento { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaAumento" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaDevolucionxVentas" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaDevolucionxVentas { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaDevolucionxVentas" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaIngresosExtranjeros" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaIngresosExtranjeros { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaIngresosExtranjeros" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaCostosExtranjeros" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaCostosExtranjeros { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaCostosExtranjeros" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaCompras" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaCompras { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaCompras" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaComprasDevolucion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaComprasDevolucion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaComprasDevolucion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaCostosMercanciasCompradas" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaCostosMercanciasCompradas { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaCostosMercanciasCompradas" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaDiferenciaTipoCambio" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaDiferenciaTipoCambio { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaDiferenciaTipoCambio" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaCompensacionMercancias" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaCompensacionMercancias { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaCompensacionMercancias" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaReduccionLibroMayor" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaReduccionLibroMayor { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaReduccionLibroMayor" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaStockTrabajoEnCurso" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaStockTrabajoEnCurso { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaStockTrabajoEnCurso" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaDesviacionStockWIP" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaDesviacionStockWIP { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaDesviacionStockWIP" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaRevalorizacionStock" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaRevalorizacionStock { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaRevalorizacionStock" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaContrapartidaRevalorizacionInventario" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaContrapartidaRevalorizacionInventario { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaContrapartidaRevalorizacionInventario" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaCompensacionGasto" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaCompensacionGasto { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaCompensacionGasto" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaStockTransito" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaStockTransito { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaStockTransito" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaCreditoVentas" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaCreditoVentas { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaCreditoVentas" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CtaCreditoCompras" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CtaCreditoCompras { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CtaCreditoCompras" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
    }
}
