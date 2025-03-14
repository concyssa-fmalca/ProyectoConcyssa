using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace ConcyssaWeb.Controllers
{
    public class ConfiguracionSociedadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string ObtenerConfiguracionSociedad()
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            List<ConfiguracionSociedadDTO> oConfiguracionSociedadDTO = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(IdSociedad,BaseDatos,ref mensaje_error);

            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oConfiguracionSociedadDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string UpdateInsertConfiguracionSociedad(ConfiguracionSociedadDTO oConfiguracionSociedadDTO)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int respuesta = oConfiguracionSociedadDAO.UpdateInsertConfiguracionSociedad(oConfiguracionSociedadDTO, IdSociedad,BaseDatos,ref mensaje_error);

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

        public bool EnviarCorreo2025(CorreoDTO oCorreoDTO, string Asunto, List<string> Correos, string ContenidoHTML, string BaseDatos, ref string mensaje_error)
        {
            bool Enviado = false;
            string errorCode = "OK";
            try
            {
                using (SmtpClient SmtpServer = new SmtpClient(oCorreoDTO.Servidor))
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(oCorreoDTO.Email, Asunto);

                        for (int i = 0; i < Correos.Count; i++)
                        {
                            message.To.Add(Correos[i]);

                        }

                        message.Subject = Asunto;
                        message.IsBodyHtml = true;
                        message.Body = ContenidoHTML;

                        SmtpServer.Port = oCorreoDTO.Puerto;
                        SmtpServer.EnableSsl = oCorreoDTO.SSL;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new NetworkCredential(oCorreoDTO.Email, oCorreoDTO.Clave);

                        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                        bool exception = false;
                        try
                        {
                            SmtpServer.Send(message);
                        }
                        catch (SmtpFailedRecipientsException ex)
                        {
                            exception = true;
                            for (int i = 0; i < ex.InnerExceptions.Length; i++)
                            {
                                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                                errorCode = status.ToString();
                                if (status == SmtpStatusCode.MailboxBusy)
                                {
                                    mensaje_error = "Receptor ocupado";
                                }
                                if (status == SmtpStatusCode.MailboxUnavailable)
                                {
                                    mensaje_error = "Dirección de correo no disponible";
                                }
                                else
                                {
                                    mensaje_error = "Falló el envío";
                                }
                            }
                            return false;
                        }
                        catch (SmtpException ex)
                        {
                            errorCode = ex.StatusCode.ToString();
                            exception = true;
                            mensaje_error = "Error " + ex.Message + "(" + errorCode + ")";
                            return false;
                        }


                        catch (Exception ex)
                        {
                            errorCode = "**";
                            exception = true;
                            mensaje_error = "Límite de reitentos: {0}" + ex.ToString() + errorCode;
                            return false;
                        }


                    }
                }

            }
            catch (WebException e)
            {
                using (WebResponse responses = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)responses;
                    using (Stream data = responses.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        var dd = reader.ReadToEnd();
                    }
                }
                mensaje_error = e.ToString();
                return false;
            }


            return true;
        }


    }
}
