using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml;

namespace ConcyssaWeb.Controllers
{
    public class ReporteLogisticaController : Controller
    {
        public IActionResult CompraAnual()
        {
            return View();
        }
        public IActionResult CompraProducto()
        {
            return View();
        }
        public IActionResult CompraProveedor()
        {
            return View();
        }
        public IActionResult ListaPrecio()
        {
            return View();
        }
        public IActionResult OrdenCompraPendiente()
        {
            return View();
        }
        public IActionResult OrdenServicioPendiente()
        {
            return View();
        }
        public IActionResult ProveedorNuevo()
        {
            return View();
        }
        public IActionResult ProveedorUnico()
        {
            return View();
        }

        public IActionResult ReporteEntrega()
        {
            return View();
        }

        public IActionResult StockSinConsumo()
        {
            return View();
        }

        public IActionResult GuiaSinFactura()
        {
            return View();
        }

        public string GenerarReporteCompraAnual(string NombreReporte,string Anno,Boolean Detalle, string Formato, int Id)
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
            oWebServiceDTO.Id = Id;
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&Anno=" + Anno + "&Detalle=" + Detalle;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteCompraAnual";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteCompraAnual";
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



        public string GenerarReporteCompraProducto(string NombreReporte, string Formato, int IdArticulo)
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
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdArticulo=" + IdArticulo;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteCompraPorProducto";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteCompraPorProducto";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(strNew);
                requestWriter.Close();
                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(response);
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

        public string GenerarReporteCompraProveedor(string NombreReporte, string Formato, int IdProveedor,string fechaIni,string fechaFin,int IdTIpo)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            fechaIni = Convert.ToDateTime(fechaIni).ToString("dd/MM/yyyy");
            fechaFin = Convert.ToDateTime(fechaFin).ToString("dd/MM/yyyy");

            WebServiceDTO oWebServiceDTO = new WebServiceDTO();
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdProveedor=" + IdProveedor + "&fechaIni=" + fechaIni + "&fechaFin=" + fechaFin + "&IdTIpo=" + IdTIpo;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteCompraProveedor";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteCompraProveedor";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(strNew);
                requestWriter.Close();
                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(response);
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

        
        public string GenerarReporteEntrega(string NombreReporte, string Formato, string Anno)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&Anno=" + Anno;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteEntrega";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteCompraAnual";
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

        public string GenerarReporteStockSinConsumo(string NombreReporte, string Formato, int IdObra, int IdTipo, int Meses)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdObra=" + IdObra + "&IdTipo=" + IdTipo + "&Meses=" + Meses;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteStockSinconsumo";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteStockSinconsumo";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
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


        public string GenerarReporteListaPrecio(string NombreReporte, string Formato, int IdDivision, int IdTipoProducto, int IdArticulo)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdDivision=" + IdDivision + "&IdTipoProducto=" + IdTipoProducto + "&IdArticulo=" + IdArticulo;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteListaPrecios";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteListaPrecios";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
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


        
        public string GenerarReporteProveedorNuevo(string NombreReporte, string Formato, int Anio)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&Anio=" + Anio ;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteProveedoresNuevos";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteProveedoresNuevos";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
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

        public string GenerarReporteProveedorUnico(string NombreReporte, string Formato, string FechaIni, string FechaFin)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            FechaIni = Convert.ToDateTime(FechaIni).ToString("dd/MM/yyyy");
            FechaFin = Convert.ToDateTime(FechaFin).ToString("dd/MM/yyyy");
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&FechaIni=" + FechaIni + "&FechaFin=" + FechaFin;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteProveedoresUnicos";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteProveedoresUnicos";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
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


        
        public string GenerarReporteOcPendiente(string NombreReporte, string Formato, int IdProveedor,int IdPedido, int IdBase)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdProveedor=" + IdProveedor + "&IdBase=" + IdBase + "&IdPedido=" + IdPedido;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteOcPendiente";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteOcPendiente";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
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

        public string GenerarReporteOsPendiente(string NombreReporte, string Formato, int IdProyecto)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdProyecto=" + IdProyecto;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteOsPendiente";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteOsPendiente";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
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




        public string GenerarReporteGuiaSinFactura(string NombreReporte, string Formato, string fechaIni, string fechaFin)
        {
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            fechaIni = Convert.ToDateTime(fechaIni).ToString("dd/MM/yyyy");
            fechaFin = Convert.ToDateTime(fechaFin).ToString("dd/MM/yyyy");

            WebServiceDTO oWebServiceDTO = new WebServiceDTO();
            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&fechaIni=" + fechaIni + "&fechaFin=" + fechaFin;
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteGuiaSinFactura";
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteGuiaSinFactura";
                uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write(strNew);
                requestWriter.Close();
                webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(response);
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
