using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net;
using System.Xml;

namespace ConcyssaWeb.Controllers
{
    public class InformeInventarioAlmacenController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public IActionResult StockMinimo()
        {
            return View();
        }

        public IActionResult FacturaAlmacen()
        {
            return View();
        }
        public IActionResult ConsumoProyectado()
        {
            return View();
        }



        public string ListarInventarioxAlmacen(int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            KardexDAO oKardexDAO = new KardexDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloStockDTO> lstArticuloStockDTO = oKardexDAO.ObtenerStockxAlmacen(IdAlmacen,BaseDatos,ref mensaje_error);

            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (lstArticuloStockDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstArticuloStockDTO.Count;
                oDataTableDTO.iTotalRecords = lstArticuloStockDTO.Count;
                oDataTableDTO.aaData = (lstArticuloStockDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
                //return JsonConvert.SerializeObject(lstArticuloStockDTO);
            }
            else
            {
                return mensaje_error;
            }
            return "error";
        }

        public string GenerarReporteStockMinimo(string NombreReporte, string Formato, int IdAlmacen, string BaseDatos)
        {
            if (BaseDatos == "" || BaseDatos == null)
            {
                BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }
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
            oWebServiceDTO.IdAlmacen = IdAlmacen;
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdAlmacen=" + IdAlmacen + "&BaseDatos=" + BaseDatos;
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteStockMinimo";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteStockMinimo";
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
        public string GenerarReporteFacturaAlmacen(string NombreReporte, string Formato, int IdAlmacen, DateTime FechaInicio, DateTime FechaFin, string BaseDatos)
        {
            if (BaseDatos == "" || BaseDatos == null)
            {
                BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }

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
            oWebServiceDTO.IdAlmacen = IdAlmacen;
            oWebServiceDTO.FechaFin = FechaFin.ToString();
            oWebServiceDTO.FechaInicio = FechaInicio;
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdAlmacen=" + IdAlmacen + "&FechaInicio=" + FechaInicio + "&FechaFin= "+ FechaFin + "&BaseDatos=" + BaseDatos;
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteFacturaMueveStock";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ObtenerReporteFacturaMueveStock";
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


        public string GenerarExcel(int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        {
            try
            {
                string mensaje_error = "";
                string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;


                AlmacenDAO oAlmacenDAO = new AlmacenDAO();
                List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerDatosxID(IdAlmacen, BaseDatos, ref mensaje_error);


                // Ruta donde deseas guardar el archivo Excel
                string NombreArchivo = "ExcelInventario-" + lstAlmacenDTO[0].Descripcion.Replace(" ", "");
                string filePath = "wwwroot/Anexos/" + NombreArchivo + ".xlsx";

                // Datos de ejemplo (pueden provenir de una función o fuente de datos)

                KardexDAO oKardexDAO = new KardexDAO();
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                List<ArticuloStockDTO> lstArticuloStockDTO = oKardexDAO.ObtenerStockxAlmacen(IdAlmacen, BaseDatos, ref mensaje_error);

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                // Crear un nuevo archivo Excel
                FileInfo fileInfo = new FileInfo(filePath);

                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                    fileInfo = new FileInfo(filePath); // Crear un nuevo FileInfo después de eliminar
                }

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    // Agregar una hoja al archivo
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Kardex");

                    using (ExcelRange range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                    }


                    // Encabezados
                    worksheet.Cells["A1"].Value = "CODIGO";
                    worksheet.Cells["B1"].Value = "ARTICULO";
                    worksheet.Cells["C1"].Value = "STOCK";
                    worksheet.Cells["D1"].Value = "PRECIO PROMEDIO";
                    worksheet.Cells["E1"].Value = "VALORIZADO";

                    // Definir el ancho de las columnas
                    worksheet.Column(1).Width = 20; // Ancho de la primera columna (en caracteres)
                    worksheet.Column(2).Width = 60; // Ancho de la primera columna (en caracteres)
                    worksheet.Column(3).Width = 10;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(5).Width = 15;


                    // Rellenar celdas con datos
                    int row = 2; // Empezar en la segunda fila
                    foreach (var registro in lstArticuloStockDTO)
                    {
                        worksheet.Cells["A" + row].Value = registro.Codigo;
                        worksheet.Cells["B" + row].Value = registro.NombArticulo;
                        worksheet.Cells["C" + row].Value = registro.Stock;
                        worksheet.Cells["D" + row].Value = registro.PrecioPromedio;
                        worksheet.Cells["E" + row].Value = registro.Stock * registro.PrecioPromedio;
                        row++;
                    }

                    // Guardar el archivo Excel
                    package.Save();
                }

                return NombreArchivo;
            }
            catch (Exception e)
            {
                return "ERROR";
            }
            
        }


        public string GenerarReporteProyeccion(int IdAlmacen, int IdTipoProducto, int AnioI, int AnioF, int MesI, int MesF, string BaseDatos)
        {
            if (BaseDatos == "" || BaseDatos == null)
            {
                BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            }
            RespuestaDTO oRespuestaDTO = new RespuestaDTO();
            WebResponse webResponse;
            HttpWebRequest request;
            Uri uri;
            string cadenaUri;
            string requestData;
            string response;
            string mensaje_error;
            WebServiceDTO oWebServiceDTO = new WebServiceDTO();
            requestData = JsonConvert.SerializeObject(oWebServiceDTO);


            try
            {
                string strNew = "IdAlmacen=" + IdAlmacen + "&IdTipoProducto=" + IdTipoProducto + "&AnioI=" + AnioI + "&AnioF=" + AnioF + "&MesI=" + MesI + "&MesF=" + MesF + "&BaseDatos=" + BaseDatos;
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReporteStockMinimo";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ProyeccionConsumo";
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
