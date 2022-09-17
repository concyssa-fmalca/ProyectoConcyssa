using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace ConcyssaWeb.Controllers
{
    public class SalidaMercanciaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string UpdateInsertMovimiento(MovimientoDTO oMovimientoDTO)
        {
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oMovimientoDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oMovimientoDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oMovimientoDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oMovimientoDTO.IdUsuario);

            string mensaje_error = "";
          
            //int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            oMovimientoDTO.IdSociedad = IdSociedad;
            oMovimientoDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            int respuesta = oMovimimientoDAO.InsertUpdateMovimiento(oMovimientoDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                {
                    oMovimientoDTO.detalles[i].IdMovimiento = respuesta;
                    int respuesta1 = oMovimimientoDAO.InsertUpdateMovimientoDetalle(oMovimientoDTO.detalles[i], ref mensaje_error);
                }

            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return "1";
                }
                else
                {
                    return mensaje_error;
                }
            }
        }

        public string GenerarSalidaExtorno(int IdMovimiento)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            KardexDAO oKardexDAO = new KardexDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento, ref mensaje_error);
            ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();

            if (mensaje_error.ToString().Length == 0)
            {
                //int validadStock = 0;
                //for (int i = 0; i < oMovimientoDTO.detalles.Count(); i++)
                //{
                //    oArticuloStockDTO = oKardexDAO.ObtenerArticuloxIdArticuloxIdAlm(oMovimientoDTO.detalles[i].IdArticulo, oMovimientoDTO.detalles[i].IdAlmacen, ref mensaje_error);
                //    if (oArticuloStockDTO.Stock < oMovimientoDTO.detalles[i].CantidadBase)
                //    {
                //        validadStock = 1;
                //    }
                //}
                //if (validadStock == 1)
                //{
                //    return "No hay suficiente Stock";
                //}
                EntradaMercanciaController oEntradaMercanciaController = new EntradaMercanciaController();
                oMovimientoDTO.IdTipoDocumento = 335;
                oMovimientoDTO.Comentario = "EXTORNO DEL SALIDA " + oMovimientoDTO.NombSerie + "-" + +oMovimientoDTO.Correlativo;
                oMovimientoDTO.IdMovimiento = 0;
                oEntradaMercanciaController.UpdateInsertMovimiento(oMovimientoDTO);


                return "ddd";

            }
            else
            {
                return mensaje_error;

            }

        }


    }
}
