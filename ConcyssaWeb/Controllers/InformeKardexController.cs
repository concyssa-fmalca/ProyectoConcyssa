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
using Newtonsoft.Json.Linq;

namespace ConcyssaWeb.Controllers
{
    public class InformeKardexController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public IActionResult KardexTributario()
        {
            return View();
        }

        public string ListarKardex(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            KardexDAO oKardexDAO = new KardexDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<KardexDTO> lstKardexDTO = oKardexDAO.ObtenerKardex(IdSociedad, IdArticulo, IdAlmacen, FechaInicio, FechaTermino,BaseDatos,ref mensaje_error);
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
            List<KardexDTO> lstKardexDTO = ObtenerDatosKardex(IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ClaseArticulo, TipoArticulo,0);

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

            IConfiguration _IConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();


            try
            {
                string strNew = "NombreReporte=" + NombreReporte + "&Formato=" + Formato + "&IdSociedad=" + IdSociedad + "&IdArticulo=" + IdArticulo + "&IdAlmacen=" + IdAlmacen + "&FechaInicio=" + FechaInicio.ToString("yyyy-MM-dd") + "&FechaTermino=" + FechaTermino.ToString("yyyy-MM-dd");
                //cadenaUri = "https://localhost:44315/ReportCrystal.asmx/ObtenerKardexReportCrystal";
                cadenaUri = _IConfiguration["Reporte:UrlBase"] + "/ReportCrystal.asmx/ObtenerKardexReportCrystal";
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

            List<KardexDTO> lstKardexDTO = ObtenerDatosKardex(IdArticulo, IdAlmacen, FechaInicio, FechaTermino, ClaseArticulo, TipoArticulo,0);

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

                using (ExcelRange range = worksheet.Cells["A1:T1"])
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
                worksheet.Cells["S1"].Value = "Cuadrilla";
                worksheet.Cells["T1"].Value = "NroRef";


                // Definir el ancho de las columnas
                worksheet.Column(1).Width = 20; // Ancho de la primera columna (en caracteres)
                worksheet.Column(3).Width = 40;
                worksheet.Column(5).Width = 20;
                worksheet.Column(9).Width = 20;
                worksheet.Column(18).Width = 20;
                worksheet.Column(19).Width = 40;

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
                    worksheet.Cells["S" + row].Value = registro.Cuadrilla;
                    worksheet.Cells["T" + row].Value = registro.NumRef;
                    row++;
                }

                // Guardar el archivo Excel
                package.Save();
            }

            return NombreArchivo;
        }

        public List<KardexDTO> ObtenerDatosKardex(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo, int IdSociedad)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            KardexDAO oKardexDAO = new KardexDAO();
            if(IdSociedad == 0)
            {
                IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            }
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<KardexDTO> lstKardexDTO = new List<KardexDTO>();

            if (IdArticulo != 0)
            {
                lstKardexDTO = oKardexDAO.ObtenerKardex(IdSociedad, IdArticulo, IdAlmacen, FechaInicio, FechaTermino,BaseDatos,ref mensaje_error);
            }
            else
            {
                ArticuloDAO oArticuloDAO = new ArticuloDAO();
                List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
                List<KardexDTO> lstArticulosKardex = new List<KardexDTO>();
                lstArticulosKardex = oKardexDAO.ObtenerArticulosEnKardex(IdAlmacen, FechaInicio, FechaTermino,BaseDatos,ref mensaje_error);
                List<int> ArticulosObtenidos = new List<int>();          
                List<int> ArticulosExistentes = new List<int>();          
                List<KardexDTO> lstArticulosNoEncontrados = new List<KardexDTO>();
                if (ClaseArticulo == 1)
                {
                    lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, TipoArticulo,BaseDatos,ref mensaje_error, 1);
                }
                else
                {
                    lstArticuloDTO = oArticuloDAO.ObtenerArticulosActivoFijo(1,BaseDatos,ref mensaje_error);
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
                        DatoLimpio.Cuadrilla = "-";
                        DatoLimpio.NumRef = "-";
                        lstArticulosNoEncontrados.Add(DatoLimpio);
                    }
                

                }


                for (int i = 0; i < ArticulosObtenidos.Count; i++)
                {
                    lstKardexDTO.AddRange(oKardexDAO.ObtenerKardex(IdSociedad, ArticulosObtenidos[i], IdAlmacen, FechaInicio, FechaTermino,BaseDatos,ref mensaje_error));
                }
                for (int i = 0; i < lstArticulosNoEncontrados.Count; i++)
                {
                    lstKardexDTO.Add(lstArticulosNoEncontrados[i]);
                }

            }

            return lstKardexDTO;
           
        }


        public string GenerarExcelKardexTributario(int IdAlmacen, int Mes, int Anio, string CodOp, string LetraCorrelativo)
        {

            object json = null;
            try
            {
                AlmacenDAO almacenDao = new AlmacenDAO();
                KardexDAO oKardexDAO = new KardexDAO();

                string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
                string mensaje_error = "";

                //OBTENER EL NOMBRE DEL ALMACEN EN CASO SEA UNICO
                string NombAlmacen = "TODOS";
                List<AlmacenDTO> lstAlmacenDTO = new List<AlmacenDTO>();

                if (IdAlmacen > 0)
                {
                    lstAlmacenDTO = almacenDao.ObtenerDatosxID(IdAlmacen, BaseDatos, ref mensaje_error);
                }
                if (lstAlmacenDTO.Count > 0)
                {
                    NombAlmacen = lstAlmacenDTO[0].Descripcion;
                }

                //OBTENEMOS LOS MOVIMIENTOS DEL MES
                List<KardexTributario> lstKardexDTO = oKardexDAO.ObtenerKardexTributario(IdAlmacen, Mes, Anio, CodOp, LetraCorrelativo, BaseDatos, ref mensaje_error);
                if (mensaje_error.Length > 0)
                {
                    json = new { status = false, mensaje = "No se pudieron cargar los movimientos del mes seleccionado: " + mensaje_error };
                    return JsonConvert.SerializeObject(json);
                }

                //List<GrupoKardexTributarioDTO> agrupadosPorAlmacenYArticulo = lstKardexDTO
                //.GroupBy(a => new { a.IdAlmacen, a.IdArticulo })
                //.Select(g => new GrupoKardexTributarioDTO
                //{
                //    IDAlmacen = g.Key.IdAlmacen,
                //    IDArticulo = g.Key.IdArticulo,
                //    Kardex = g.ToList()
                //})
                //.ToList();

                List<int> almacenesUnicos = new List<int>();


                if (IdAlmacen > 0)
                {
                    almacenesUnicos.Add(IdAlmacen);
      
                }else
                {
                    List<AlmacenDTO> Almacenes = almacenDao.ObtenerAlmacen(1, BaseDatos, ref mensaje_error, 1);
                    if (mensaje_error.Length > 0)
                    {
                        json = new { status = false, mensaje = "Ocurrió un error al cargar los almacenes: " + mensaje_error };
                        return JsonConvert.SerializeObject(json);
                    }
                    for (int i = 0; i < Almacenes.Count(); i++)
                    {
                        almacenesUnicos.Add(Almacenes[i].IdAlmacen);
                    }
                }

               

                List<KardexTributario> SaldoInicialGlobal = new List<KardexTributario>();

                foreach (int Almacen in almacenesUnicos)
                {
                    //string DatosArticulos = "";
                    //int CantidadItems = 0;
                    //foreach (var grupo in agrupadosPorAlmacenYArticulo)
                    //{
                    //    if (grupo.IDAlmacen == Almacen)
                    //    {
                    //        CantidadItems++;
                    //        DatosArticulos += grupo.IDArticulo.ToString() + ",";
                    //    }
                    //}
                    //DatosArticulos = DatosArticulos.Substring(0, DatosArticulos.Length - 1);

                    List<KardexTributario> lstSaldoInicialxAlmacen = oKardexDAO.ObtenerSaldoInicialKardexTributario(Almacen, Mes, Anio, CodOp, "", BaseDatos, ref mensaje_error);
                    if (mensaje_error.Length>0)
                    {
                        json = new { status = false, mensaje = "No se pudieron cargar los saldos iniciales: " + mensaje_error };
                        return JsonConvert.SerializeObject(json);
                    }
                    //if (lstSaldoInicialxAlmacen.Count != CantidadItems)
                    //{
                    //    json = new { status = false, mensaje = "No se pudieron cargar todos los saldos iniciales: " + mensaje_error };
                    //    return JsonConvert.SerializeObject(json);
                    //}
                    SaldoInicialGlobal.AddRange(lstSaldoInicialxAlmacen);
                }

                string NombreArchivo = "KardexTributario-" + NombAlmacen + " " + Anio.ToString() + "-" + Mes.ToString();
                string filePath = "wwwroot/Anexos/" + NombreArchivo + ".xlsx";


                lstKardexDTO.AddRange(SaldoInicialGlobal);

                lstKardexDTO = lstKardexDTO.OrderBy(a => a.IdAlmacen).ThenBy(a => a.IdArticulo).ThenBy(a => a.IdKardex).ToList();



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

                    using (ExcelRange range = worksheet.Cells["A1:AJ1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        range.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                    }


                    // Encabezados
                    worksheet.Cells["A1"].Value = "PERIODO";// "CAMPO1";
                    worksheet.Cells["B1"].Value = "N.OPERACION";//"CAMPO2";
                    worksheet.Cells["C1"].Value = "CORRELATIVO";//"CAMPO3";
                    worksheet.Cells["D1"].Value = "CODIGO ESTAB. SUNAT";//"CAMPO4";
                    worksheet.Cells["E1"].Value = "CATALOGO";//"CAMPO5";
                    worksheet.Cells["F1"].Value = "TIPO EXISTENCIA";//"CAMPO6";
                    worksheet.Cells["G1"].Value = "CODIGO PRODUCTO";//"CAMPO7";
                    worksheet.Cells["H1"].Value = "CATALOGO2";//"CAMPO8";
                    worksheet.Cells["I1"].Value = "UNSPSC";//"CAMPO9";
                    worksheet.Cells["J1"].Value = "FECHA OPERACION";//"CAMPO10";
                    worksheet.Cells["K1"].Value = "TIPO DE DOC";//"CAMPO11";
                    worksheet.Cells["L1"].Value = "SERIE";//"CAMPO12";
                    worksheet.Cells["M1"].Value = "NUM.DOC";//"CAMPO13";
                    worksheet.Cells["N1"].Value = "TIPO OPERACION";//"CAMPO14";
                    worksheet.Cells["O1"].Value = "DESCRIPCION DEL PRODUCTO";//"CAMPO15";
                    worksheet.Cells["P1"].Value = "UNIDAD MEDIDA";//"CAMPO16";
                    worksheet.Cells["Q1"].Value = "METODO COSTO";//"CAMPO17";
                    worksheet.Cells["R1"].Value = "UNIDADES ENTRADA";//"CAMPO18";
                    worksheet.Cells["S1"].Value = "COSTO UNIT ENTRADA";//"CAMPO19";
                    worksheet.Cells["T1"].Value = "ENTRADA EN SOLES";//"CAMPO20";
                    worksheet.Cells["U1"].Value = "UNIDADES SALIDA";//"CAMPO21";
                    worksheet.Cells["V1"].Value = "COSTO UNIT SALIDA";//"CAMPO22";
                    worksheet.Cells["W1"].Value = "SALIDA EN SOLES";//"CAMPO23";
                    worksheet.Cells["X1"].Value = "UNIDADES SALDO";//"CAMPO24";
                    worksheet.Cells["Y1"].Value = "COSTO UNIT SALDO";//"CAMPO25";
                    worksheet.Cells["Z1"].Value = "SALDO EN SOLES";//"CAMPO26";
                    worksheet.Cells["AA1"].Value = "ESTADO OP.";//"CAMPO27";
                    worksheet.Cells["AB1"].Value = "CAMPO28";//"CAMPO28";
                    worksheet.Cells["AC1"].Value = "FechaContabilizacion";
                    worksheet.Cells["AD1"].Value = "FechaSistema";
                    worksheet.Cells["AE1"].Value = "FechaDocumento";
                    worksheet.Cells["AF1"].Value = "NumUniversal";
                    worksheet.Cells["AG1"].Value = "TipoArticulo";
                    worksheet.Cells["AH1"].Value = "NombreContrato";
                    worksheet.Cells["AI1"].Value = "Proveedor";
                    worksheet.Cells["AJ1"].Value = "TIPO OP SGC";



                    //// Definir el ancho de las columnas
                    worksheet.Column(9).Width = 20; // Ancho de la primera columna (en caracteres)
                    worksheet.Column(10).Width = 20;
                    worksheet.Column(15).Width = 40;
                    worksheet.Column(34).Width = 40;
                    worksheet.Column(35).Width = 40;
                    worksheet.Column(36).Width = 40;


                    // Rellenar celdas con datos
                    int row = 2; // Empezar en la segunda fila
                    int contador = 1;
                    foreach (var registro in lstKardexDTO)
                    {

                        worksheet.Cells["A" + row].Value = registro.CAMPO1;
                        worksheet.Cells["B" + row].Value = registro.CAMPO2;
                        worksheet.Cells["C" + row].Value = String.Format("{0}{1:D9}", LetraCorrelativo, contador);
                        worksheet.Cells["D" + row].Value = registro.CAMPO4;
                        worksheet.Cells["E" + row].Value = registro.CAMPO5;
                        worksheet.Cells["F" + row].Value = registro.CAMPO6;
                        worksheet.Cells["G" + row].Value = registro.CAMPO7;
                        worksheet.Cells["H" + row].Value = registro.CAMPO8;
                        worksheet.Cells["I" + row].Value = registro.CAMPO9;
                        worksheet.Cells["J" + row].Value = registro.CAMPO10;
                        worksheet.Cells["K" + row].Value = registro.CAMPO11;
                        worksheet.Cells["L" + row].Value = registro.CAMPO12;
                        worksheet.Cells["M" + row].Value = registro.CAMPO13;
                        worksheet.Cells["N" + row].Value = registro.CAMPO14;
                        worksheet.Cells["O" + row].Value = registro.CAMPO15;
                        worksheet.Cells["P" + row].Value = registro.CAMPO16;
                        worksheet.Cells["Q" + row].Value = registro.CAMPO17;
                        worksheet.Cells["R" + row].Value = registro.CAMPO18;
                        worksheet.Cells["S" + row].Value = registro.CAMPO19;
                        worksheet.Cells["T" + row].Value = registro.CAMPO20;
                        worksheet.Cells["U" + row].Value = registro.CAMPO21;
                        worksheet.Cells["V" + row].Value = registro.CAMPO22;
                        worksheet.Cells["W" + row].Value = registro.CAMPO23;
                        worksheet.Cells["X" + row].Value = registro.CAMPO24;
                        worksheet.Cells["Y" + row].Value = registro.CAMPO25;
                        worksheet.Cells["Z" + row].Value = registro.CAMPO26;
                        worksheet.Cells["AA" + row].Value = registro.CAMPO27;
                        worksheet.Cells["AB" + row].Value = registro.CAMPO28;
                        worksheet.Cells["AC" + row].Value = registro.FechaContabilizacion;
                        worksheet.Cells["AD" + row].Value = registro.FechaRegistro;
                        worksheet.Cells["AE" + row].Value = registro.CAMPO10;
                        worksheet.Cells["AF" + row].Value = registro.IdKardex;
                        worksheet.Cells["AG" + row].Value = registro.TipoArticulo;
                        worksheet.Cells["AH" + row].Value = registro.NombreContrato;
                        worksheet.Cells["AI" + row].Value = registro.RUC +" - "+ registro.RazonSocial;
                        worksheet.Cells["AJ" + row].Value = registro.DescripcionMovimiento;
                        row++;
                        contador++;
                    }

                    // Guardar el archivo Excel
                    package.Save();
                }


                if (mensaje_error.Length > 0)
                {
                    json = new { status = false, mensaje = mensaje_error };
                    return JsonConvert.SerializeObject(json);
                }

                json = new { status = true, mensaje = mensaje_error, NombreArchivo = NombreArchivo };
                return JsonConvert.SerializeObject(json);
            }
            catch(Exception e)
            {
                json = new { status = false, mensaje = e.Message.ToString() };
                return JsonConvert.SerializeObject(json);
            }

           

        }



    }
}
