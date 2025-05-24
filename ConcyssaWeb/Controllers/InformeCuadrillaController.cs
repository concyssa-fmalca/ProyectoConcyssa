using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml;

namespace ConcyssaWeb.Controllers
{
    public class InformeCuadrillaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string GenerarReporte(string NombreReporte, string Formato, string Cuadrillas, bool Materiales, bool Auxiliares, bool Servicios, bool Extornos, string FechaInicioS, string FechaFin,string BaseDatos)
        {
            if (BaseDatos == "" || BaseDatos == null)
            {
                BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }
            //string[] fechaInicio1 = FechaInicioS.Split(' ');
            //string[] fechaInicio2 = fechaInicio1[0].Split('/');

            //string[] FechaFin1 = FechaFin.Split(' ');
            //string[] FechaFin2 = FechaFin1[0].Split('/');

            //string fechaInicio3 = fechaInicio2[2] + "/" + fechaInicio2[1] + "/" + fechaInicio2[0];
            //string FechaFin3 = FechaFin2[2] + "/" + FechaFin2[1] + "/" + FechaFin2[0];

            IConfiguration _IConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();


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
            oWebServiceDTO.Cuadrillas = Cuadrillas;
            oWebServiceDTO.Materiales = Materiales;
            oWebServiceDTO.Auxiliares = Auxiliares;
            oWebServiceDTO.Servicios = Servicios;
            oWebServiceDTO.Extornos = Extornos;
            oWebServiceDTO.FechaInicioS = FechaInicioS;
            oWebServiceDTO.FechaFin = FechaFin;
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&Cuadrillas=" + Cuadrillas + "&Materiales=" + Materiales + "&Auxiliares=" + Auxiliares + "&Servicios=" + Servicios + "&Extornos=" + Extornos + "&FechaInicio=" + FechaInicioS + "&FechaFin=" + FechaFin +"&BaseDatos="+BaseDatos;
                //cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteInformeConsumoCuadrillas";
                cadenaUri = _IConfiguration["Reporte:UrlBase"] + "/ReportCrystal.asmx/ObtenerReporteInformeConsumoCuadrillas";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);

                request.Method = "POST";
                //request.ContentType = "application/json;charset=utf-8";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 300000;
                request.ReadWriteTimeout = 300000;

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
