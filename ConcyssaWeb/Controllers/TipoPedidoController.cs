using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TipoPedidoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }


        public string ObtenerTipoPedido(int estado = 3)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoPedidoDAO oTipoPedidoDAO = new TipoPedidoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoPedidoDTO> lstTipoPedidoDTO = oTipoPedidoDAO.ObtenerTipoPedido(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstTipoPedidoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoPedidoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
    }
}
