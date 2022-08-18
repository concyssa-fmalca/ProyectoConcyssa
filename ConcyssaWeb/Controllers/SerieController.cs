using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class SerieController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerSeries(int estado = 3)
        {
            string mensaje_error = "";
            SerieDAO oSerieDAO = new SerieDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SerieDTO> lstSerieDTO = oSerieDAO.ObtenerSerie(IdSociedad, ref mensaje_error, estado);
            if (lstSerieDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSerieDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



    }
}
