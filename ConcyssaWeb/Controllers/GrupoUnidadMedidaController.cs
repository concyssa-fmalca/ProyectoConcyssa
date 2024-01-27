using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class GrupoUnidadMedidaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }


        public string ObtenerDefinicionUnidadMedidaxGrupo(int IdGrupoUnidadMedida)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();
            List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO = oDefinicionGrupoUnidadDAO.ObtenerDefinicionesxIdGrupoUnidadMedida(IdGrupoUnidadMedida,BaseDatos,ref mensaje_error);

            if (lstDefinicionGrupoUnidadDTO.Count > 0)
            {

                return JsonConvert.SerializeObject(lstDefinicionGrupoUnidadDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }

        public string ObtenerDefinicionUnidadMedidaxIdDefinicionGrupo(int IdDefinicionGrupo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();
            DefinicionGrupoUnidadDTO oDefinicionGrupoUnidadDTO = oDefinicionGrupoUnidadDAO.ObtenerDefinicionUnidadMedidaxIdDefinicionGrupo(IdDefinicionGrupo,BaseDatos,ref mensaje_error);

            if (oDefinicionGrupoUnidadDTO!=null)
            {

                return JsonConvert.SerializeObject(oDefinicionGrupoUnidadDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }

        public string ObtenerDatosxID(int IdGrupoUnidadMedida)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GrupoUnidadMedidaDAO oGrupoUnidadMedidaDAO = new GrupoUnidadMedidaDAO();
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();

            List<GrupoUnidadMedidaDTO> lstGrupoUnidadMedidaDTO = oGrupoUnidadMedidaDAO.ObtenerDatosxID(IdGrupoUnidadMedida,BaseDatos,ref mensaje_error);
            List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO = oDefinicionGrupoUnidadDAO.ObtenerDefinicionesxIdGrupoUnidadMedida(IdGrupoUnidadMedida,BaseDatos,ref mensaje_error);

            string[] array1 = new string[2];
            if (lstGrupoUnidadMedidaDTO.Count > 0)
            {
                array1[0] = JsonConvert.SerializeObject(lstGrupoUnidadMedidaDTO);
                array1[1] = JsonConvert.SerializeObject(lstDefinicionGrupoUnidadDTO);

                return JsonConvert.SerializeObject(array1);
                //return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerGrupoUnidadMedida(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GrupoUnidadMedidaDAO oGrupoUnidadMedidaDAO = new GrupoUnidadMedidaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<GrupoUnidadMedidaDTO> lstGrupoUnidadMedidaDTO = oGrupoUnidadMedidaDAO.ObtenerGrupoUnidadMedida(IdSociedad,BaseDatos,ref mensaje_error, estado);
            if (lstGrupoUnidadMedidaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGrupoUnidadMedidaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string UpdateInsertGrupoUnidadMedida(GrupoUnidadMedidaDTO oGrupoUnidadMedidaDTO,List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO)
        {
            //Cabecera
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GrupoUnidadMedidaDAO oGrupoUnidadMedidaDAO = new GrupoUnidadMedidaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oGrupoUnidadMedidaDTO.IdSociedad = IdSociedad;
            int respuesta = oGrupoUnidadMedidaDAO.UpdateInsertGrupoUnidadMedida(oGrupoUnidadMedidaDTO,BaseDatos,ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }

            //END CABECERA

            //DETALLE
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();
            for (int i = 0; i < lstDefinicionGrupoUnidadDTO.Count; i++)
            {
                lstDefinicionGrupoUnidadDTO[i].IdGrupoUnidadMedida = respuesta;
                int respuesta1 = oDefinicionGrupoUnidadDAO.UpdateInsertDefinicionGrupoUnidad(lstDefinicionGrupoUnidadDTO[i],BaseDatos,ref mensaje_error);
            }
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                return "1";
            }
            //END DETALLE
        }


        
        public string ListarDefinicionGrupoxIdDefinicionSelect(int IdDefinicionGrupo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();
            List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO = oDefinicionGrupoUnidadDAO.ListarDefinicionGrupoxIdDefinicionSelect(IdDefinicionGrupo,BaseDatos,ref mensaje_error);

            if (lstDefinicionGrupoUnidadDTO.Count > 0)
            {

                return JsonConvert.SerializeObject(lstDefinicionGrupoUnidadDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }


        public int EliminarDefinicionGrupoUnidad(int IdDefinicionGrupo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DefinicionGrupoUnidadDAO oDefinicionGrupoUnidadDAO = new DefinicionGrupoUnidadDAO();
            int resultado = oDefinicionGrupoUnidadDAO.Delete(IdDefinicionGrupo,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
        

        public int EliminarGrupoUnidadMedida(int IdGrupoUnidadMedida)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            GrupoUnidadMedidaDAO oGrupoUnidadMedidaDAO = new GrupoUnidadMedidaDAO();
            int resultado = oGrupoUnidadMedidaDAO.Delete(IdGrupoUnidadMedida,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
    
}
