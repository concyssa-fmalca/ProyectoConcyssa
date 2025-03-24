using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml;

namespace ConcyssaWeb.Controllers
{
    public class DevolucionAdmController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        [HttpPost]
        [Route("DevolucionAdm/AgregarDevolucionAdm")]
        public dynamic AgregarDevolucionAdm([FromBody] DevolucionAdmDTO oDevolucionAdmDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateInsertDevolucionAdm(oDevolucionAdmDTO,BaseDatos);
            
            if (respuesta > 0)
            {
                return respuesta;
            }
            else
            {
                return 0;

            }
        }

        public int AgregarDevolucionAdmDetalle(DevolucionAdmDetalle oDevolucionAdmDetalle)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateInsertDevolucionAdmDetalle(oDevolucionAdmDetalle,BaseDatos);

            if (respuesta > 0)
            {
                return respuesta;
            }
            else
            {
                return 0;

            }
        }

        public string ObtenerDevoluciones(int IdUsuario, int EstadoDevolucion,DateTime FechaInicio,DateTime FechaFin)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DataTableDTO oDataTableDTO = new DataTableDTO();
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = oDevolucionAdmDAO.ObtenerDevoluciones(IdUsuario, EstadoDevolucion,FechaInicio,FechaFin,BaseDatos);
            
            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.iTotalRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.aaData = (lstSolicitudDespachoDTO);
            return JsonConvert.SerializeObject(oDataTableDTO);

        }

        public dynamic ObtenerSolicitudDespachoxId(int IdDevolucion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DataTableDTO oDataTableDTO = new DataTableDTO();
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = oDevolucionAdmDAO.ObtenerDevolucionxId(IdDevolucion,BaseDatos);

            oDataTableDTO.sEcho = 1;
            oDataTableDTO.iTotalDisplayRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.iTotalRecords = lstSolicitudDespachoDTO.Count;
            oDataTableDTO.aaData = (lstSolicitudDespachoDTO);
            return JsonConvert.SerializeObject(oDataTableDTO);

        }

        public int UpdateDevolucionDetalle(int IdDevolucionDetalle, decimal Cantidad)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.UpdateDevolucionAdmDet(IdDevolucionDetalle, Cantidad,BaseDatos);

            if (respuesta > 0)
            {
                return respuesta;
            }
            else
            {
                return 0;

            }

        }

        public int CerrarDevolucionAdm(int IdDevolucion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            if (BaseDatos == "")
            {
                BaseDatos = "AddonsConcyssa";
            }
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            var respuesta = oDevolucionAdmDAO.CerrarDevolucionAdm(IdDevolucion,BaseDatos);

            if (respuesta > 0)
            {
                return respuesta;
            }
            else
            {
                return 0;

            }

        }
        public string ObtenerDevolucionAdmAtender(int IdBase,int IdObra, DateTime FechaInicio, DateTime FechaFin, int EstadoDevolucion, int SerieFiltro)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            List<DevolucionAdmDTO> lstDevolucionAdmDTO = oDevolucionAdmDAO.ObtenerDevolucionesAtender(IdBase, IdObra, FechaInicio, FechaFin, EstadoDevolucion, SerieFiltro,BaseDatos);

            if (lstDevolucionAdmDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstDevolucionAdmDTO);
            }
            else
            {
                return "error";
            }

        }

        public int AtencionConfirmada(int Cantidad, int IdDevolucion, int IdArticulo, int EstadoDevolucion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            DevolucionAdmDAO oDevolucionAdmDAO = new DevolucionAdmDAO();
            int respuesta = oDevolucionAdmDAO.AtencionConfirmada(Cantidad, IdDevolucion, IdArticulo, EstadoDevolucion,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return 0;
            }
            else
            {
                if (respuesta == 0)
                {
                    return 0;
                }
                else
                {
                    return respuesta;
                }
            }
        }

        public string GenerarReporte(int IdBase, int IdObra, string FechaInicio, string FechaFin,int EstadoDevolucion,int SerieFiltro,string Formato)
        {
            string BaseDatos = "";
            if (BaseDatos == "" || BaseDatos == null)
            {
                BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }

            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string response;
            string mensaje_error;


            try
            {
                string strNew = "Formato=" + Formato +
                    "&BaseDatos=" + BaseDatos +
                    "&IdBase=" + IdBase +
                    "&IdObra=" + IdObra +
                    "&FechaInicio=" + FechaInicio +
                    "&FechaFin=" + FechaFin +
                    "&EstadoDevolucion=" + EstadoDevolucion +
                    "&SerieFiltro=" + SerieFiltro;
              
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ReporteDevolucionADM";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "POST";
                //request.ContentType = "application/json;charset=utf-8";
                request.ContentType = "application/x-www-form-urlencoded";


                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);

                requestWriter.Write(strNew);


                requestWriter.Close();



                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();

                //var Resultado = response;
                //XmlSerializer xmlSerializer = new XmlSerializer(response);
                var rr = 33;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(response);
                var dd = "";

                oRespuestaDTO.Result = xDoc.ChildNodes[1].ChildNodes[0].InnerText;
                oRespuestaDTO.Mensaje = xDoc.ChildNodes[1].ChildNodes[1].InnerText;
                oRespuestaDTO.Base64ArchivoPDF = xDoc.ChildNodes[1].ChildNodes[2].InnerText;

                return JsonConvert.SerializeObject(oRespuestaDTO);
            }
            catch (WebException e)
            {
                using (WebResponse responses = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)responses;
                    using (Stream data = responses.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        mensaje_error = reader.ReadToEnd();

                    }
                }

                string err = e.ToString();
            }

            return "";
        }


    }
}
