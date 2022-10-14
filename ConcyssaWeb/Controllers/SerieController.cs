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

        public string ObtenerSeries()
        {
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }


            SerieDAO oSerieDAO = new SerieDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SerieDTO> lstSerieDTO = oSerieDAO.ObtenerSeries(IdSociedad.ToString());
            if (lstSerieDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSerieDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerSeriesDT()
        {
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }


            SerieDAO oSerieDAO = new SerieDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SerieDTO> lstSerieDTO = oSerieDAO.ObtenerSeries(IdSociedad.ToString());
            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (lstSerieDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstSerieDTO.Count;
                oDataTableDTO.iTotalRecords = lstSerieDTO.Count;
                oDataTableDTO.aaData = (lstSerieDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
                //return JsonConvert.SerializeObject(lstSerieDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertSerie(SerieDTO SerieDTO)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }


            SerieDAO oSerieDAO = new SerieDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            int resultado = oSerieDAO.UpdateInsertSerie(SerieDTO, IdSociedad.ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }
           
            return resultado;

        }

        public string ObtenerDatosxID(int IdSerie)
        {
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }


            SerieDAO oSerieDAO = new SerieDAO();
            List<SerieDTO> lstSerieDTO = oSerieDAO.ObtenerDatosxID(IdSerie);

            if (lstSerieDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSerieDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarSerie(int IdSerie)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }


            SerieDAO oSerieDAO = new SerieDAO();
            int resultado = oSerieDAO.Delete(IdSerie);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public string ValidarNumeracionSerieSolicitudRQ(int IdSerie)
        {
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }


            SerieDAO oSerieDAO = new SerieDAO();
            List<SerieDTO> lstSerieDTO = oSerieDAO.ValidarNumeracionSerieSolicitudRQ(IdSerie);

            if (lstSerieDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSerieDTO);
            }
            else
            {
                return "sin datos";
            }

        }



        public string validarEmpresaActual()
        {
            string rpta = "";
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            if (IdSociedad == null)
            {
                return "SinBD";
            }
            return rpta;
        }

        public int validarEmpresaActualUpdateInsert()
        {
            int rpta = 0;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            if (IdSociedad == null)
            {
                return -999;
            }
            return rpta;
        }




    }
}
