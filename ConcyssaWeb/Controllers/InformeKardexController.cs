using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml;

namespace ConcyssaWeb.Controllers
{
    public class InformeKardexController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ListarKardex(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        {
            string mensaje_error = "";
            KardexDAO oKardexDAO = new KardexDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<KardexDTO> lstKardexDTO = oKardexDAO.ObtenerKardex(IdSociedad, IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error);
            if (lstKardexDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstKardexDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }


        public string ListarKardexDT(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        
        {
            string mensaje_error = "";
            KardexDAO oKardexDAO = new KardexDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<KardexDTO> lstKardexDTO = oKardexDAO.ObtenerKardex(IdSociedad, IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error);
            if (lstKardexDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstKardexDTO.Count;
                oDataTableDTO.iTotalRecords = lstKardexDTO.Count;
                oDataTableDTO.aaData = (lstKardexDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
                //return JsonConvert.SerializeObject(lstKardexDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }



        public string GenerarReporte(string NombreReporte, string Formato, int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdSociedad=" + IdSociedad + "&IdArticulo=" + IdArticulo + "&IdAlmacen=" + IdAlmacen + "&FechaInicio=" + FechaInicio.ToString("yyyy-MM-dd") + "&FechaTermino=" + FechaTermino.ToString("yyyy-MM-dd");
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerKardexReportCrystal";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerKardexReportCrystal";
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
