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
    public class AtencionDespachoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string GenerarReporte(int IdObra, string FechaInicio, string FechaFin, string BaseDatos)
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


            try
            {
                string strNew = "IdObra=" + IdObra + "&FechaIni=" + FechaInicio + "&FechaFin=" + FechaFin + "&BaseDatos=" + BaseDatos;
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerReportCrystal";
                cadenaUri = "http://localhost/ReporteCrystal/ReportCrystal.asmx/ReporteSolicitudDespachoPendiente";
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

        public string GenerarExcel(int IdObra, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                string mensaje_error = "";
                string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

                // Ruta donde deseas guardar el archivo Excel
                string NombreArchivo = "SDespachoPendiente-" + IdObra;
                string filePath = "wwwroot/Anexos/" + NombreArchivo + ".xlsx";

                // Datos de ejemplo (pueden provenir de una función o fuente de datos)

                SolicitudDespachoDAO oSolicitudDespachoDAO = new SolicitudDespachoDAO();
                List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = oSolicitudDespachoDAO.SolicitudDespachoPendienteAprobacion(IdObra, FechaInicio, FechaFin, BaseDatos);

                if (lstSolicitudDespachoDTO.Count == 0)
                {
                    return "SIN DATOS";
                }

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

                    using (ExcelRange range = worksheet.Cells["A1:G1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                    }


                    // Encabezados
                    worksheet.Cells["A1"].Value = "NUMDOC";
                    worksheet.Cells["B1"].Value = "FECHA";
                    worksheet.Cells["C1"].Value = "CODIGO";
                    worksheet.Cells["D1"].Value = "ARTICULO";
                    worksheet.Cells["E1"].Value = "CANTIDAD";
                    worksheet.Cells["F1"].Value = "CUADRILLA";
                    worksheet.Cells["G1"].Value = "OBRA";

                    // Definir el ancho de las columnas
                    worksheet.Column(1).Width = 10; // Ancho de la primera columna (en caracteres)
                    worksheet.Column(2).Width = 12; // Ancho de la primera columna (en caracteres)
                    worksheet.Column(3).Width = 10; // Ancho de la primera columna (en caracteres)
                    worksheet.Column(4).Width = 60;
                    worksheet.Column(5).Width = 10;
                    worksheet.Column(6).Width = 60;
                    worksheet.Column(7).Width = 40;


                    // Rellenar celdas con datos
                    int row = 2; // Empezar en la segunda fila
                    foreach (var registro in lstSolicitudDespachoDTO)
                    {
                        worksheet.Cells["A" + row].Value = registro.NumDoc;
                        worksheet.Cells["B" + row].Value = registro.FechaDocumento.ToString("dd/MM/yyyy");
                        worksheet.Cells["C" + row].Value = registro.Codigo;
                        worksheet.Cells["D" + row].Value = registro.Descripcion;
                        worksheet.Cells["E" + row].Value = registro.Cantidad;
                        worksheet.Cells["F" + row].Value = registro.DescripcionCuadrilla;
                        worksheet.Cells["G" + row].Value = registro.DescripcionObra;
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

    }
}
