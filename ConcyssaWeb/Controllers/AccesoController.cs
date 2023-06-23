using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using DAO;
using DTO;
using Helpers;

namespace ConcyssaWeb.Controllers
{
    public class AccesoController : Controller
    {

        public IActionResult Listado()
        {
            return View();
        }


        public string obtenerListaMenurol()
        {
            string mensaje_error = "";
            string rpta = "";
            MenuDAO oSeg_MenuBL = new MenuDAO();
            UsuarioDTO oSeg_UsuarioDTO = new UsuarioDTO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int IdPerfil = Convert.ToInt32(HttpContext.Session.GetInt32("IdPerfil")); ;

            Seg_RolMenuVistaDTO oSeg_RolMenuVistaDTO = oSeg_MenuBL.ListarMenurol(IdUsuario, IdPerfil,ref mensaje_error);
            if (oSeg_RolMenuVistaDTO != null)
            {
                if (oSeg_RolMenuVistaDTO.listaRol != null && oSeg_RolMenuVistaDTO.listaRol.Count > 0)
                {
                    rpta += ucCustomSerializer.Serializar(oSeg_RolMenuVistaDTO.listaRol, '¦', '¬', new string[0], incluirCabeceras: false);
                }
                rpta += "\u00af";
                if (oSeg_RolMenuVistaDTO.listaMenu != null && oSeg_RolMenuVistaDTO.listaMenu.Count > 0)
                {
                    rpta += ucCustomSerializer.Serializar(oSeg_RolMenuVistaDTO.listaMenu, '¦', '¬', new string[3] { "Menu", "idMenu", "Descripcion" }, incluirCabeceras: false);
                }
                int TipoUsuarioLogin = 1;
                rpta += "\u00af";
                rpta += TipoUsuarioLogin;
            }
            return rpta;
        }


        public string ObtenerAccesos(int IdPerfil)
        {
    
            string mensaje_error = "";
            string rpta = "";
            MenuDAO oAccesoDAO = new MenuDAO();
            List<beCampoString3> lstAcceso = oAccesoDAO.obtenerMenuPerfil(IdPerfil,ref mensaje_error);
           
            if (lstAcceso != null && lstAcceso.Count > 0)
            {
                rpta = ucCustomSerializer.Serializar(lstAcceso, '¦', '¬', new string[0], incluirCabeceras: false);
            }
            else
            {
                rpta = mensaje_error;
            }
            return rpta;
        }



        public string grabarAcceso(string datos)
        {

            string mensaje_error = ""; string rpta = "";
            //Stream st = HttpContext.Request.Body;
            //StreamReader sr = new StreamReader(st);
            //var data = sr.ReadToEnd();
            MenuDAO oSeg_MenuBL = new MenuDAO();
            if (oSeg_MenuBL.GrabarAccesos(datos, Convert.ToInt32( HttpContext.Session.GetInt32("Usuario")) , ref mensaje_error )  )
            {
                rpta = "1";
            }else
            {
                rpta = mensaje_error;
            }
            return rpta;
        }
















        public string ObtenerAccesosPerfil(int IdPerfil)
        {
            string mensaje_error = "";
            AccesoDAO oAccesoDAO = new AccesoDAO();
            List<AccesoDTO> lstAccesoDTO = oAccesoDAO.ObtenerAccesos(IdPerfil,ref mensaje_error);
            if (lstAccesoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAccesoDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerMenus()
        {
            MenuDAO oMenuDAO = new MenuDAO();
            List<MenuDTO> lstMenuDTO = oMenuDAO.ObtenerMenus();
            if (lstMenuDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMenuDTO);
            }
            else
            {
                return "error";
            }
        }

        public int GuardarAcceso(int IdPerfil, List<int> ArrayAccesos)
        {
            AccesoDAO oAccesoDAO = new AccesoDAO();
            int resultado = oAccesoDAO.UpdateInsertAcceso(IdPerfil, ArrayAccesos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarAccesoxPerfil(int IdPerfil)
        {
            AccesoDAO oAccesoDAO = new AccesoDAO();
            int resultado = oAccesoDAO.Delete(IdPerfil);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
