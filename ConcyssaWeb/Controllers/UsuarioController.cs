using Microsoft.AspNetCore.Mvc;
using DAO;
using DTO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace ConcyssaWeb.Controllers
{
    public class UsuarioController : Controller
    {


        // GET: Usuario
        public IActionResult Listado()
        {
            return View();
        }
        public IActionResult CambiarClave()
        {
            return View();
        }


        public string ObtenerUsuarios()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerUsuarios(BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            if (!String.IsNullOrEmpty(IdSociedad.ToString()))
            {
                string mensaje_error = "";
                PerfilDAO oPerfilDAO = new PerfilDAO();
                List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerPerfiles(IdSociedad,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerUsuariosAutorizadores(IdSociedad.ToString(),BaseDatos);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerUsuariosAutorizadores(IdSociedad.ToString(),BaseDatos);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SociedadDAO oSociedadDAO = new SociedadDAO();
            List<SociedadDTO> lstSociedadDTO = oSociedadDAO.ObtenerSociedades(BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();

            string password = "";
            using (SHA512 sha512 = SHA512.Create())
            {
                // Convierte la contraseña en un array de bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(usuarioDTO.Password);

                // Calcula el hash de la contraseña
                byte[] hashedBytes = sha512.ComputeHash(passwordBytes);

                // Convierte el hash en una cadena hexadecimal
                password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }

            usuarioDTO.Password = password;

            int resultado = oUsuarioDAO.UpdateInsertUsuario(usuarioDTO,BaseDatos,ref mensaje_error);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdUsuario)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerDatosxID(IdUsuario,BaseDatos,ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerBasesxIdUsuario(IdUsuario,BaseDatos,ref mensaje_error);

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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.Delete(IdUsuario,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ObtenerBaseAlmacenxIdUsuario(int IdUsuario)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioBaseAlmacenDTO> lstUsuarioBaseAlmacenDTO = oUsuarioDAO.ObtenerBaseAlmacenxIdUsuario(IdUsuario,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioBaseAlmacenDTO> lstUsuarioBaseAlmacenDTO = oUsuarioDAO.ObtenerBaseAlmacenxIdUsuario(IdUsuario,BaseDatos,ref mensaje_error);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.UpdateInsertUsuarioBaseAlmacen(oUsuarioBaseAlmacenDTO,BaseDatos,ref mensaje_error);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarUsuarioBase(int IdUsuarioBase)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.DeleteUsuarioBase(IdUsuarioBase,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }
            return resultado;
        }

        public string codificar()
        {

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerUsuariosCodificar(4053, BaseDatos, ref mensaje_error);

            for (int i = 0; i < lstUsuarioDTO.Count; i++)
            {

                using (SHA512 sha512 = SHA512.Create())
                {
                    // Convierte la contraseña en un array de bytes
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(lstUsuarioDTO[i].Password.ToUpper());

                    // Calcula el hash de la contraseña
                    byte[] hashedBytes = sha512.ComputeHash(passwordBytes);

                    // Convierte el hash en una cadena hexadecimal
                    lstUsuarioDTO[i].Password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }


                oUsuarioDAO.UpdatePassword(lstUsuarioDTO[i].IdUsuario, lstUsuarioDTO[i].Password, BaseDatos, ref mensaje_error);
            }


            return "";
        }
        public int UpdateClaveUser(int IdUsuario, string Clave)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();

            using (SHA512 sha512 = SHA512.Create())
            {
                // Convierte la contraseña en un array de bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Clave.ToUpper());

                // Calcula el hash de la contraseña
                byte[] hashedBytes = sha512.ComputeHash(passwordBytes);

                // Convierte el hash en una cadena hexadecimal
                Clave = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }


            int resultado = oUsuarioDAO.UpdatePassword(IdUsuario, Clave, BaseDatos, ref mensaje_error);

            return resultado;
        }

        public string CambiarPassword(string ClaveActual, string ClaveNueva)
        {
            string mensaje_error = "";
            string Clave2 = ClaveActual.ToUpper();
            string BaseDatos = HttpContext.Session.GetString("BaseDatos").ToString();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Clave2);
                byte[] hashedBytes = sha512.ComputeHash(passwordBytes);
                Clave2 = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(ClaveActual);
                byte[] hashedBytes = sha512.ComputeHash(passwordBytes);
                ClaveActual = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(ClaveNueva);
                byte[] hashedBytes = sha512.ComputeHash(passwordBytes);
                ClaveNueva = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }


            int rpta = oUsuarioDAO.CambiarPassword(ClaveActual, ClaveNueva, IdUsuario, BaseDatos, ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                mensaje_error = "";
                rpta = oUsuarioDAO.CambiarPassword(Clave2, ClaveNueva, IdUsuario, BaseDatos, ref mensaje_error);
            }

            object json = null;

            if (rpta == 0)
            {
                json = new { status = false, mensaje = mensaje_error };
            }
            else
            {
                json = new { status = true, mensaje = mensaje_error };
            }

            return JsonConvert.SerializeObject(json);
        }



    }
}
