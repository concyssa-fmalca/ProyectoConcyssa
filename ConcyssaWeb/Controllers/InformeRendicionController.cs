using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml;

namespace ConcyssaWeb.Controllers
{
    public class InformeRendicionController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string GenerarReporte(string NombreReporte, string Formato, int IdObra, int IdSemana, int Anio)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            WebServiceDTO oWebServiceDTO = new WebServiceDTO();
            oWebServiceDTO.Formato = Formato;
            oWebServiceDTO.NombreReporte = NombreReporte;
            oWebServiceDTO.IdObra = IdObra;
            oWebServiceDTO.Anio = Anio;
            oWebServiceDTO.IdSemana = IdSemana;
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdObra=" + IdObra + "&Anio=" + Anio + "&IdSemana=" + IdSemana;
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteInformeRendicionSemanas";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteInformeRendicionSemanas";
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
