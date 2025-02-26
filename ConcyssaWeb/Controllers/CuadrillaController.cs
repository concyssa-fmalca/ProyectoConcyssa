using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class CuadrillaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerCuadrilla(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CuadrillaDAO oCuadrillaDAO = new CuadrillaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CuadrillaDTO> lstCuadrillaDTO = oCuadrillaDAO.ObtenerCuadrilla(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstCuadrillaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCuadrillaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string UpdateInsertCuadrilla(CuadrillaDTO oCuadrillaDTO)
        {

            string mensaje_error = "";
            CuadrillaDAO oCuadrillaDAO = new CuadrillaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            oCuadrillaDTO.IdSociedad = IdSociedad;
            int respuesta = oCuadrillaDAO.UpdateInsertCuadrilla(oCuadrillaDTO,BaseDatos,ref mensaje_error, Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")));

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == -1)
                {
                    return "-1";
                }else if(respuesta > 0)
                {
                    return respuesta.ToString();
                }

                else
                {
                    return "error";
                }
            }
        }

        public string ObtenerDatosxID(int IdCuadrilla)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CuadrillaDAO oCuadrillaDAO = new CuadrillaDAO();
            List<CuadrillaDTO> lstCuadrillaDTO = oCuadrillaDAO.ObtenerDatosxID(IdCuadrilla,BaseDatos,ref mensaje_error);

            if (lstCuadrillaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCuadrillaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int EliminarCuadrilla(int IdCuadrilla)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CuadrillaDAO oCuadrillaDAO = new CuadrillaDAO();
            int resultado = oCuadrillaDAO.Delete(IdCuadrilla,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ObtenerCuadrillaxIdObra(int IdObra)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CuadrillaDAO oCuadrillaDAO = new CuadrillaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CuadrillaDTO> lstCuadrillaDTO = oCuadrillaDAO.ObtenerCuadrillaxIdObra(IdObra,BaseDatos,ref mensaje_error);
            if (lstCuadrillaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCuadrillaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerCuadrillaxIdObraSelect2(int IdObra,string Texto="")
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CuadrillaDAO oCuadrillaDAO = new CuadrillaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CuadrillaDTO> lstCuadrillaDTO = oCuadrillaDAO.ObtenerCuadrillaxIdObraSelect2(IdObra, Texto, BaseDatos, ref mensaje_error);
            if (lstCuadrillaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCuadrillaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerCuadrillaxIdBase(int IdBase)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CuadrillaDAO oCuadrillaDAO = new CuadrillaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CuadrillaDTO> lstCuadrillaDTO = oCuadrillaDAO.ObtenerCuadrillaxIdBase(IdBase, BaseDatos, ref mensaje_error);
            if (lstCuadrillaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCuadrillaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerNuevoCodigo(int CodigoUsar,int IdObra)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            CuadrillaDAO oCuadrillaDAO = new CuadrillaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<CuadrillaDTO> lstCuadrillaDTO = oCuadrillaDAO.ObtenerNuevoCodigo(CodigoUsar,IdObra,BaseDatos,ref mensaje_error);
            if (lstCuadrillaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCuadrillaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



    }

    
}
