using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class EtapaAutorizacionController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerEtapaAutorizacion()
        {
            string valida = "";
            //valida = validarEmpresaActual();
            //if (valida != "")
            //{
            //    return valida;
            //}

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<EtapaAutorizacionDTO> lstEtapaAutorizacionDTO = oEtapaAutorizacionDAO.ObtenerEtapaAutorizacion(IdSociedad.ToString(),BaseDatos);
            if (lstEtapaAutorizacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEtapaAutorizacionDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertEtapaAutorizacion(EtapaAutorizacionDTO etapaAutorizacionDTO)
        {
            int valida = 0;
            //valida = validarEmpresaActualUpdateInsert();
            //if (valida != 0)
            //{
            //    return valida;
            //}
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oEtapaAutorizacionDAO.UpdateInsertEtapaAutorizacion(etapaAutorizacionDTO, IdSociedad.ToString(),BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdEtapaAutorizacion)
        {
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }


            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<EtapaAutorizacionDTO> lstEtapaAutorizacionDTO = oEtapaAutorizacionDAO.ObtenerDatosxID(IdEtapaAutorizacion,BaseDatos);

            if (lstEtapaAutorizacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEtapaAutorizacionDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarEtapaAutorizacion(int IdEtapaAutorizacion)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            int resultado = oEtapaAutorizacionDAO.Delete(IdEtapaAutorizacion,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public int EliminarDetalleAutorizacion(int IdEtapaAutorizacionDetalle)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            int resultado = oEtapaAutorizacionDAO.EliminarDetalleAutorizacion(IdEtapaAutorizacionDetalle,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
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
