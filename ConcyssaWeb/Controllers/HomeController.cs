using ConcyssaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DAO;
using DTO;
using Helpers;

namespace ConcyssaWeb.Controllers
{
    public class HomeController : Controller
    {
        public string TipoLicencia = "";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {

            _logger = logger;
        }

        public IActionResult Index()
        {
            
            HttpContext.Session.SetString("Sociedad", "ddddddddddddd");
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("Sociedad")))
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            HttpContext.Session.SetString("Compra", "0.000");
            HttpContext.Session.SetString("Venta", "0.000");
            return View();
        }

        public bool login(string usuario, string password,int idsociedad)
        {
            bool respuesta = false;
            string mensajeError = "";
            try
            {
                UsuarioDAO oUsuarioDAO = new UsuarioDAO();
                List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ValidarUsuario(usuario, password, idsociedad, ref mensajeError);
                UsuarioDTO oUsuarioDTO = lstUsuarioDTO[0];
                if (oUsuarioDTO.Estado == true)
                {
                    HttpContext.Session.SetInt32("IdUsuario", oUsuarioDTO.IdUsuario);
                    HttpContext.Session.SetString("Usuario", oUsuarioDTO.Usuario);
                    HttpContext.Session.SetString("NombUsuario", oUsuarioDTO.Nombre);
                    HttpContext.Session.SetInt32("IdPerfil", oUsuarioDTO.IdPerfil);
                    HttpContext.Session.SetInt32("IdSociedad", oUsuarioDTO.IdSociedad);
                    HttpContext.Session.SetInt32("Estado", Convert.ToInt32(oUsuarioDTO.Estado));
                    HttpContext.Session.SetString("NombreSociedad", oUsuarioDTO.NombreSociedad);
                    HttpContext.Session.SetString("NumeroDocumento", oUsuarioDTO.NumeroDocumento);
                    HttpContext.Session.SetString("NombBase", oUsuarioDTO.NombBase);

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                return false;
                throw;

            }
        }

        public string obtenerMenu()
        {
            string rpta = "";
            MenuDAO oSeg_MenuBL = new MenuDAO();
            string mensaje_error = "";
            List<MenuDTO> lbeSeg_MenuDTO = oSeg_MenuBL.ListarxPerfil(Convert.ToInt32(HttpContext.Session.GetInt32("IdPerfil")) , ref mensaje_error);
            if (mensaje_error.Length>0)
            {
                return mensaje_error;
            }
            else
            {
                if (lbeSeg_MenuDTO != null && lbeSeg_MenuDTO.Count > 0)
                {
                    rpta = ucCustomSerializer.Serializar(lbeSeg_MenuDTO, '¦', '¬', new string[6] { "Menu", "idMenu", "Descripcion", "Controller", "Action", "Estado" }, incluirCabeceras: false);
                }
                return rpta;
            }
            
        }

        public IActionResult About()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Sociedad")))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult TipoCambio()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Sociedad")))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult CerrarSesion()
        {
      
            return RedirectToAction("Index", "Home");
        }
    }
}