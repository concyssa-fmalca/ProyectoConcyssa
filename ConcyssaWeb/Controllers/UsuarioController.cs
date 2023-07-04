using Microsoft.AspNetCore.Mvc;
using DAO;
using DTO;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class UsuarioController : Controller
    {


        // GET: Usuario
        public IActionResult Listado()
        {
            return View();
        }


        public string ObtenerUsuarios()
        {
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerUsuarios(ref mensaje_error);
            if (lstUsuarioDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUsuarioDTO);
            }
            else
            {
                return mensaje_error;
            }

        }

        public string ObtenerPerfiles()
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            if (!String.IsNullOrEmpty(IdSociedad.ToString()))
            {
                string mensaje_error = "";
                PerfilDAO oPerfilDAO = new PerfilDAO();
                List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerPerfiles(IdSociedad, ref mensaje_error);
                if (lstPerfilDTO.Count > 0)
                {
                    return JsonConvert.SerializeObject(lstPerfilDTO);
                }
                else
                {
                    return mensaje_error;
                }
            }
            else
            {
                return "No cuenta con una Sociedad";
            }


        }

        public string ObtenerUsuariosAutorizadores()
        {
            //string valida = "";
            //valida = validarEmpresaActual();
            //if (valida != "")
            //{
            //    return valida;
            //}
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerUsuariosAutorizadores(IdSociedad.ToString());
            if (lstUsuarioDTO.Count > 0)
            {
                List<UsuarioDTO> olstUsuarioDTO = new List<UsuarioDTO>();
                for (int i = 0; i < lstUsuarioDTO.Count; i++)
                {
                    if (lstUsuarioDTO[i].IdUsuario== IdUsuario)
                    {
                        olstUsuarioDTO.Add(lstUsuarioDTO[i]);
                    }
                   
                }
                return JsonConvert.SerializeObject(olstUsuarioDTO);
            }
            else
            {
                return "error";
            }

        }

        public string ObtenerUsuariosAutorizadoresEtapa()
        {
            //string valida = "";
            //valida = validarEmpresaActual();
            //if (valida != "")
            //{
            //    return valida;
            //}
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerUsuariosAutorizadores(IdSociedad.ToString());
            if (lstUsuarioDTO.Count > 0)
            {
                List<UsuarioDTO> olstUsuarioDTO = new List<UsuarioDTO>();
                for (int i = 0; i < lstUsuarioDTO.Count; i++)
                {
                    
                        olstUsuarioDTO.Add(lstUsuarioDTO[i]);
                   

                }
                return JsonConvert.SerializeObject(olstUsuarioDTO);
            }
            else
            {
                return "error";
            }

        }

        public string ObtenerSociedades()
        {
            string mensaje_error = "";
            SociedadDAO oSociedadDAO = new SociedadDAO();
            List<SociedadDTO> lstSociedadDTO = oSociedadDAO.ObtenerSociedades(ref mensaje_error);
            if (lstSociedadDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSociedadDTO);
            }
            else
            {
                return mensaje_error;
            }

        }

        public int UpdateInsertUsuario(UsuarioDTO usuarioDTO)
        {
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.UpdateInsertUsuario(usuarioDTO, ref mensaje_error);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdUsuario)
        {
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerDatosxID(IdUsuario, ref mensaje_error);

            if (lstUsuarioDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUsuarioDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerBasesxIdUsuario()
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerBasesxIdUsuario(IdUsuario, ref mensaje_error);

            if (lstUsuarioDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUsuarioDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int EliminarUsuario(int IdUsuario)
        {
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.Delete(IdUsuario);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ObtenerBaseAlmacenxIdUsuario(int IdUsuario)
        {
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioBaseAlmacenDTO> lstUsuarioBaseAlmacenDTO = oUsuarioDAO.ObtenerBaseAlmacenxIdUsuario(IdUsuario, ref mensaje_error);
            if (lstUsuarioBaseAlmacenDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUsuarioBaseAlmacenDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerBaseAlmacenxIdUsuarioSession()
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioBaseAlmacenDTO> lstUsuarioBaseAlmacenDTO = oUsuarioDAO.ObtenerBaseAlmacenxIdUsuario(IdUsuario, ref mensaje_error);
            if (lstUsuarioBaseAlmacenDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUsuarioBaseAlmacenDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int GuardarAlmacenBasexUsuario(UsuarioBaseAlmacenDTO oUsuarioBaseAlmacenDTO)
        {
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.UpdateInsertUsuarioBaseAlmacen(oUsuarioBaseAlmacenDTO, ref mensaje_error);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarUsuarioBase(int IdUsuarioBase)
        {
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.DeleteUsuarioBase(IdUsuarioBase);
            if (resultado == 0)
            {
                resultado = 1;
            }
            return resultado;
        }


        

    }
}
