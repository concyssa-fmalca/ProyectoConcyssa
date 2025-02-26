using ConcyssaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DAO;
using DTO;
using Helpers;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

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

        public string login(string usuario, string password,int idsociedad,string BaseDatos,string Alias)
        {
            bool respuesta = false;
            string mensajeError = "";
            UsuarioDTO oUsuarioDTO = null;
            try
            {
                UsuarioDAO oUsuarioDAO = new UsuarioDAO();

                using (SHA512 sha512 = SHA512.Create())
                {
                    // Convierte la contraseña en un array de bytes
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password.ToUpper());

                    // Calcula el hash de la contraseña
                    byte[] hashedBytes = sha512.ComputeHash(passwordBytes);

                    // Convierte el hash en una cadena hexadecimal
                    password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }



                List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ValidarUsuario(usuario, password, idsociedad,BaseDatos, ref mensajeError);
                oUsuarioDTO = lstUsuarioDTO[0];
                if (oUsuarioDTO.Estado == true)
                {
                    HttpContext.Session.SetInt32("IdUsuario", oUsuarioDTO.IdUsuario);
                    //HttpContext.Session.SetInt32("IdUsuario", 4079);
                    HttpContext.Session.SetString("Usuario", oUsuarioDTO.Usuario);
                    HttpContext.Session.SetString("NombUsuario", oUsuarioDTO.Nombre);
                    HttpContext.Session.SetInt32("IdPerfil", oUsuarioDTO.IdPerfil);
                    HttpContext.Session.SetInt32("IdSociedad", oUsuarioDTO.IdSociedad);
                    HttpContext.Session.SetInt32("Estado", Convert.ToInt32(oUsuarioDTO.Estado));
                    HttpContext.Session.SetString("NombreSociedad", oUsuarioDTO.NombreSociedad);
                    HttpContext.Session.SetString("NumeroDocumento", oUsuarioDTO.NumeroDocumento);
                    HttpContext.Session.SetString("NombBase", oUsuarioDTO.NombBase);
                    HttpContext.Session.SetString("Alias", Alias);
                    HttpContext.Session.SetString("BaseDatos", BaseDatos);

                    return JsonConvert.SerializeObject(oUsuarioDTO);
                }
                else
                {
                    return JsonConvert.SerializeObject(oUsuarioDTO);
                }


            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(oUsuarioDTO);
                throw;

            }
        }

        public string obtenerMenu()
        {
            string rpta = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MenuDAO oSeg_MenuBL = new MenuDAO();
            string mensaje_error = "";
            List<MenuDTO> lbeSeg_MenuDTO = oSeg_MenuBL.ListarxPerfil(Convert.ToInt32(HttpContext.Session.GetInt32("IdPerfil")) ,BaseDatos,ref mensaje_error);
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
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CerrarSesion()
        {
      
            return RedirectToAction("Index", "Home");
        }

        public string CargarConexiones()
        {
            SociedadDAO sociedadDAO = new SociedadDAO();
            List<ConexionesBD> lstConexionesBD = sociedadDAO.CargarConexiones();
            if (lstConexionesBD.Count > 0)
            {
                return JsonConvert.SerializeObject(lstConexionesBD);
            }
            else
            {
                return "error";
            }
        }
    }
}