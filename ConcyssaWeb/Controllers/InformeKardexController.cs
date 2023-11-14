using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Xml;
using OfficeOpenXml;
using Microsoft.Win32;
using OfficeOpenXml.Style;

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


        public string ListarKardexDT(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo)
        
        {
            string mensaje_error = "";
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<KardexDTO> lstKardexDTO = ObtenerDatosKardex(IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ClaseArticulo, TipoArticulo);

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

        public string GenerarExcel(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo, string NombreAlmacen)
        {
            // Ruta donde deseas guardar el archivo Excel
            string NombreArchivo = "ExcelKardex-" + NombreAlmacen.Replace(" ","");
            string filePath = "wwwroot/Anexos/"+ NombreArchivo + ".xlsx" ;

            // Datos de ejemplo (pueden provenir de una función o fuente de datos)

            List<KardexDTO> lstKardexDTO = ObtenerDatosKardex(IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ClaseArticulo, TipoArticulo);

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

                using (ExcelRange range = worksheet.Cells["A1:R1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                }


                // Encabezados
                worksheet.Cells["A1"].Value = "Modulo";
                worksheet.Cells["B1"].Value = "DescArticulo";
                worksheet.Cells["C1"].Value = "Codigo Articulo";
                worksheet.Cells["D1"].Value = "Fecha Registro";
                worksheet.Cells["E1"].Value = "TipoTransaccion";
                worksheet.Cells["F1"].Value = "Numero";
                worksheet.Cells["G1"].Value = "Fecha Documento";
                worksheet.Cells["H1"].Value = "Fecha Contabilizacion";
                worksheet.Cells["I1"].Value = "Tipo Documento";
                worksheet.Cells["J1"].Value = "Serie y Numero";
                worksheet.Cells["K1"].Value = "Unidad de Medida";
                worksheet.Cells["L1"].Value = "Precio In/Out";
                worksheet.Cells["M1"].Value = "In/Out";
                worksheet.Cells["N1"].Value = "Valor Mov.";
                worksheet.Cells["O1"].Value = "Saldo";
                worksheet.Cells["P1"].Value = "Costo Promedio";
                worksheet.Cells["Q1"].Value = "Valorizado";
                worksheet.Cells["R1"].Value = "Usuario";


                // Definir el ancho de las columnas
                worksheet.Column(1).Width = 20; // Ancho de la primera columna (en caracteres)
                worksheet.Column(3).Width = 40;
                worksheet.Column(5).Width = 20;
                worksheet.Column(9).Width = 20;
                worksheet.Column(18).Width = 20;

                // Rellenar celdas con datos
                int row = 2; // Empezar en la segunda fila
                foreach (var registro in lstKardexDTO)
                {
                    string FechaRegistro = registro.FechaRegistro.ToString().Split(" ")[0];
                    if (FechaRegistro == "01/01/1999") FechaRegistro = "-";

                    string FechaDocumento = registro.FechaDocumento.ToString().Split(" ")[0];
                    if (FechaDocumento == "01/01/1999") FechaDocumento = "-";

                    string FechaContabilizacion = registro.FechaContabilizacion.ToString().Split(" ")[0];
                    if (FechaContabilizacion == "01/01/1999") FechaContabilizacion = "-";

                    string SerieCorrelativo = registro.DescSerie + "-" + registro.Correlativo;
                    if (registro.DescSerie == "-") SerieCorrelativo = "-";





                    worksheet.Cells["A" + row].Value = registro.Modulo;
                    worksheet.Cells["B" + row].Value = registro.CodigoArticulo;
                    worksheet.Cells["C" + row].Value = registro.DescArticulo;
                    worksheet.Cells["D" + row].Value = FechaRegistro;
                    worksheet.Cells["E" + row].Value = registro.TipoTransaccion;
                    worksheet.Cells["F" + row].Value = SerieCorrelativo;
                    worksheet.Cells["G" + row].Value = FechaDocumento;
                    worksheet.Cells["H" + row].Value = FechaContabilizacion;
                    worksheet.Cells["I" + row].Value = registro.TipoDocumentoRef.ToString();
                    worksheet.Cells["J" + row].Value = registro.NumSerieTipoDocumentoRef;
                    worksheet.Cells["K" + row].Value = registro.DescUnidadMedidaBase;
                    worksheet.Cells["L" + row].Value = registro.CantidadBase;
                    worksheet.Cells["M" + row].Value = registro.PrecioBase;
                    worksheet.Cells["N" + row].Value = registro.CantidadBase * registro.PrecioBase;
                    worksheet.Cells["O" + row].Value = registro.Saldo;
                    worksheet.Cells["P" + row].Value = registro.PrecioPromedio;
                    worksheet.Cells["Q" + row].Value = registro.PrecioPromedio * registro.Saldo;
                    worksheet.Cells["R" + row].Value = registro.NombUsuario;
                    row++;
                }

                // Guardar el archivo Excel
                package.Save();
            }

            return NombreArchivo;
        }

        public List<KardexDTO> ObtenerDatosKardex(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo)
        {
            string mensaje_error = "";
            KardexDAO oKardexDAO = new KardexDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<KardexDTO> lstKardexDTO = new List<KardexDTO>();

            if (IdArticulo != 0)
            {
                lstKardexDTO = oKardexDAO.ObtenerKardex(IdSociedad, IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error);
            }
            else
            {
                ArticuloDAO oArticuloDAO = new ArticuloDAO();
                List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
                List<KardexDTO> lstArticulosKardex = new List<KardexDTO>();
                lstArticulosKardex = oKardexDAO.ObtenerArticulosEnKardex(IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error);
                List<int> ArticulosObtenidos = new List<int>();          
                List<int> ArticulosExistentes = new List<int>();          
                List<KardexDTO> lstArticulosNoEncontrados = new List<KardexDTO>();
                if (ClaseArticulo == 1)
                {
                    lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, TipoArticulo, ref mensaje_error, 1);
                }
                else
                {
                    lstArticuloDTO = oArticuloDAO.ObtenerArticulosActivoFijo(1, ref mensaje_error);
                }

                for (int i = 0; i < lstArticulosKardex.Count; i++)
                {
                    for (int j = 0; j < lstArticuloDTO.Count; j++)
                    {
                        if (lstArticulosKardex[i].IdArticulo == lstArticuloDTO[j].IdArticulo)
                        {
                            ArticulosObtenidos.Add(lstArticuloDTO[j].IdArticulo);
                        }
                    }
                }

                for (int i = 0; i < lstArticuloDTO.Count; i++)
                {
                    if (!ArticulosObtenidos.Contains(lstArticuloDTO[i].IdArticulo))
                    {
                        KardexDTO DatoLimpio = new KardexDTO();

                        DatoLimpio.IdDetalleMovimiento = 0;
                        DatoLimpio.IdDetalleOPDN = 0;
                        DatoLimpio.IdDetalleOPCH = 0;
                        DatoLimpio.IdDefinicionGrupoUnidad = 0;
                        DatoLimpio.CantidadBase = 0;
                        DatoLimpio.IdUnidadMedidaBase = 0;
                        DatoLimpio.PrecioBase = 0;
                        DatoLimpio.CantidadRegistro = 0;
                        DatoLimpio.IdUnidadMedidaRegistro = 0;
                        DatoLimpio.PrecioRegistro = 0;
                        DatoLimpio.PrecioPromedio = 0;
                        DatoLimpio.FechaRegistro = Convert.ToDateTime("1999/01/01 00:00:00");
                        DatoLimpio.FechaContabilizacion = Convert.ToDateTime("1999/01/01 00:00:00");
                        DatoLimpio.FechaDocumento = Convert.ToDateTime("1999/01/01 00:00:00");
                        DatoLimpio.IdAlmacen = IdAlmacen;
                        DatoLimpio.IdArticulo = lstArticuloDTO[i].IdArticulo;
                        DatoLimpio.Saldo = 0;
                        DatoLimpio.DescArticulo = lstArticuloDTO[i].Descripcion1;
                        DatoLimpio.CodigoArticulo = lstArticuloDTO[i].Codigo;
                        DatoLimpio.DescSerie = "-";
                        DatoLimpio.Correlativo = 0;
                        DatoLimpio.TipoTransaccion = "-";
                        DatoLimpio.DescUnidadMedidaBase = "-";
                        DatoLimpio.NombUsuario = "-";
                        DatoLimpio.Comentario = "-";
                        DatoLimpio.Modulo = "SALDO INICIAL";
                        DatoLimpio.NumSerieTipoDocumentoRef = "-";
                        DatoLimpio.TipoDocumentoRef = "-";
                        lstArticulosNoEncontrados.Add(DatoLimpio);
                    }
                

                }


                for (int i = 0; i < ArticulosObtenidos.Count; i++)
                {
                    lstKardexDTO.AddRange(oKardexDAO.ObtenerKardex(IdSociedad, ArticulosObtenidos[i], IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error));
                }
                for (int i = 0; i < lstArticulosNoEncontrados.Count; i++)
                {
                    lstKardexDTO.Add(lstArticulosNoEncontrados[i]);
                }

            }

            return lstKardexDTO;
           
        }
    }
}
