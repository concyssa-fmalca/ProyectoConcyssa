using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class GrupoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public IActionResult ListadoSubGrupo()
        {
            return View();
        }


        public string ObtenerSubGrupo(int IdGrupo,int estado = 3)
        {
            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SubGrupoDTO> lstGrupoDTO = oGrupoDAO.ObtenerSubGrupo(IdSociedad,IdGrupo, ref mensaje_error, estado);
            if (lstGrupoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGrupoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerGrupo(int estado = 3)
        {
            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<GrupoDTO> lstGrupoDTO = oGrupoDAO.ObtenerGrupo(IdSociedad, ref mensaje_error, estado);
            if (lstGrupoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGrupoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerGrupoxObra(int IdObra)
        {
            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<GrupoDTO> lstGrupoDTO = oGrupoDAO.ObtenerGrupoxObra(IdObra, ref mensaje_error);
            if (lstGrupoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGrupoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerGrupoxID(int IdGrupo)
        {
            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdGrupo"));
            List<GrupoDTO> lstGrupoDTO = oGrupoDAO.ObtenerGrupoxId(IdGrupo, ref mensaje_error);
            if (lstGrupoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGrupoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerDatosxID(int IdGrupo)
        {
            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            List<GrupoDTO> lstCodigoUbsoDTO = oGrupoDAO.ObtenerDatosxID(IdGrupo, ref mensaje_error);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerDatosxIdSubGrupo(int IdSubGrupo)
        {
            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            List<SubGrupoDTO> lstSubGrupoDTO = oGrupoDAO.ObtenerDatosxIdSubGrupo(IdSubGrupo, ref mensaje_error);

            if (lstSubGrupoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSubGrupoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertGrupo(GrupoDTO oGrupoDTO)
        {

            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oGrupoDTO.IdSociedad = IdSociedad;
            int respuesta = oGrupoDAO.UpdateInsertGrupo(oGrupoDTO, ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }
        }

        public string UpdateInsertSubGrupo(SubGrupoDTO oSubGrupoDTO)
        {

            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            int respuesta = oGrupoDAO.UpdateInsertSubGrupo(oSubGrupoDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }
        }


        public int EliminarGrupo(int IdGrupo)
        {
            GrupoDAO oGrupoDAO = new GrupoDAO();
            int resultado = oGrupoDAO.Delete(IdGrupo);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarSubGrupo(int IdSubGrupo)
        {
            GrupoDAO oGrupoDAO = new GrupoDAO();
            int resultado = oGrupoDAO.DeleteSubGrupo(IdSubGrupo);
            if (resultado == 0)
            {
                resultado = 1;
            }
            return resultado;
        }

        public string ObtenerSubGruposxIdGrupo(int IdGrupo)
        {
            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            List<SubGrupoDTO> lstSubGrupoDTO = oGrupoDAO.ObtenerSubGruposxIdGrupo(IdGrupo, ref mensaje_error);

            if (lstSubGrupoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSubGrupoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerSubGrupoxID(int IdSubGrupo)
        {
            string mensaje_error = "";
            GrupoDAO oGrupoDAO = new GrupoDAO();
            List<SubGrupoDTO> lstSubGrupoDTO = oGrupoDAO.ObtenerSubGrupoxID(IdSubGrupo, ref mensaje_error);

            if (lstSubGrupoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSubGrupoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

    }
}
