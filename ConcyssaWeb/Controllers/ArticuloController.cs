using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace ConcyssaWeb.Controllers
{
    public class ArticuloController:Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerArticulosxSociedad(int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerticulosxSociedad(IdSociedad,BaseDatos,ref mensaje_error,estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerArticulosSelect2(string TipoProducto,string IdObra, string searchTerm = "")
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosSelect2(searchTerm, TipoProducto,IdObra,BaseDatos);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }    

        public string ListarArticulosxSociedadxAlmacenStock(int IdAlmacen,int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStock(IdSociedad,IdAlmacen,BaseDatos,ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string ListarPrecioProductoProveedor(int IdArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloPrecioProveedorDTO> lstArticuloPrecioProveedorDTO = oArticuloDAO.ListarPrecioProductoProveedor(IdArticulo,BaseDatos,ref mensaje_error);
            if (lstArticuloPrecioProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloPrecioProveedorDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ListarPrecioProductoProveedorNuevo(int IdArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloPrecioProveedorDTO> lstArticuloPrecioProveedorDTO = oArticuloDAO.ListarPrecioProductoProveedorNuevo(IdArticulo,BaseDatos,ref mensaje_error);
            if (lstArticuloPrecioProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloPrecioProveedorDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(int IdTipoProducto,int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, IdTipoProducto,BaseDatos,ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoConServicios(int TipoItem, int IdTipoProducto, int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoConServicios(TipoItem,IdSociedad, IdAlmacen, IdTipoProducto,BaseDatos,ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoDT(int IdTipoProducto, int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, IdTipoProducto,BaseDatos,ref mensaje_error, estado);
            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (lstArticuloDTO.Count > 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstArticuloDTO.Count;
                oDataTableDTO.iTotalRecords = lstArticuloDTO.Count;
                oDataTableDTO.aaData = (lstArticuloDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ListarArticulosxSociedadxAlmacenStockxProducto(int IdArticulo,int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStockxProducto(IdSociedad, IdArticulo, IdAlmacen,BaseDatos,ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ListarArticulosxSociedadxAlmacenStockxProductoConServicios(int IdArticulo, int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStockxProductoConServicios(IdSociedad, IdArticulo, IdAlmacen,BaseDatos,ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ListarArticulosxSociedadxAlmacenStockxProductoActivoFijo(int IdArticulo, int IdAlmacen, int estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStockxProductoActivoFijo(IdSociedad, IdArticulo, IdAlmacen,BaseDatos,ref mensaje_error, estado);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string ObtenerDatosxID(int IdArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerDatosxID(IdArticulo,BaseDatos,ref mensaje_error);

            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }

        }

        public string SavePrecioProveedor(int IdArticulo,int IdProveedor, decimal PrecioSoles, decimal PrecioDolares, int IdCondicionPagoProveedor, int numeroentrega)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            bool respuesta = oArticuloDAO.SavePrecioProveedor(IdArticulo,IdProveedor, PrecioSoles, PrecioDolares, IdCondicionPagoProveedor, numeroentrega,IdSociedad,BaseDatos,ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }
        public string SavePrecioProveedorNuevo(int IdArticulo, int IdProveedor, decimal PrecioSoles, decimal PrecioDolares, int IdCondicionPagoProveedor, int numeroentrega,int IdObra)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            bool respuesta = oArticuloDAO.SavePrecioProveedorNuevo(IdArticulo, IdProveedor, PrecioSoles, PrecioDolares, IdCondicionPagoProveedor, numeroentrega, IdSociedad,IdObra,BaseDatos,ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }

        public string UpdateInsertArticulo( ArticuloDTO oArticuloDTO)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oArticuloDTO.IdSociedad = IdSociedad;
            bool respuesta = oArticuloDAO.UpdateInsertArticulo(oArticuloDTO,BaseDatos,ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }
        
        public string EliminarArticulo(ArticuloDTO oArticuloDTO)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oArticuloDTO.IdSociedad = IdSociedad;
            bool respuesta = oArticuloDAO.ELiminarArticulo(oArticuloDTO,BaseDatos,ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }

        public string EliminarProductoProveedor(int IdProductoProveedor)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            bool respuesta = oArticuloDAO.EliminarProductoProveedor(IdProductoProveedor,BaseDatos,ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }


        

        public string ObtenerArticulosConStock(int Almacen,int Stock,int TipoItem,int TipoProducto )
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosRequerimientos(Almacen,Stock, TipoItem, TipoProducto, IdSociedad,BaseDatos,ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerArticulosConStockSolicitud(int Almacen, int Stock, int TipoItem, int TipoProducto)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosRequerimientosSolicitud(Almacen, Stock, TipoItem, TipoProducto, IdSociedad,BaseDatos,ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerArticulosActivoFijo()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosActivoFijo(IdSociedad,BaseDatos,ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string ObtenerArticuloxIdArticuloRequerimiento(int IdArticulo, int IdAlmacen, int TipoItem)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            ArticuloDTO oArticuloDTO = oArticuloDAO.ObtenerArticuloxIdArticuloRequerimiento(IdArticulo,IdAlmacen,BaseDatos,ref mensaje_error);
            if (oArticuloDTO!=null)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerArticuloxIdArticulo(int IdArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            ArticuloDTO oArticuloDTO = oArticuloDAO.ObtenerArticuloxIdArticulo(IdArticulo,BaseDatos,ref mensaje_error);
            if (oArticuloDTO != null)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string InsertStockArticuloAlmacen(int IdProducto, int IdAlmacen, decimal StockMinimo, decimal StockMaximo, decimal StockAlerta)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            bool respuesta = oArticuloDAO.InsertStockArticuloAlmacen(IdProducto, IdAlmacen, StockMinimo, StockMaximo, StockAlerta,BaseDatos,ref mensaje_error);
            if (respuesta)
            {
                return "1";
            }
            else
            {
                return mensaje_error;
            }
        }



        public string ObtenerStockArticuloXAlmacen(int IdArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<StockArticuloAlmacenDTO> oArticuloDTO = oArticuloDAO.ObtenerStockArticuloXAlmacen(IdUsuario,IdArticulo,BaseDatos,ref mensaje_error);
            if (oArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerStockArticuloXPendiente(int IdObra,int IdArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> oArticuloDTO = oArticuloDAO.ObtenerStockArticuloXAlmacenXPendiente(IdObra, IdArticulo,BaseDatos,ref mensaje_error);
            if (oArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerArticulosConStockObras(int IdArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulosStockObras(IdArticulo,BaseDatos,ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerStockxProducto(int IdArticulo, int IdAlmacen)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();          
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerStockxProducto(IdArticulo, IdAlmacen,BaseDatos,ref mensaje_error);
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerArticuloxCodigo(string Codigo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            ArticuloDTO oArticuloDTO = oArticuloDAO.ObtenerArticuloxCodigo(Codigo,BaseDatos,ref mensaje_error);
            if (oArticuloDTO != null)
            {
                return JsonConvert.SerializeObject(oArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int EliminarArticuloProveedorPrecio(int IdProveedor, int IdArticulo)
        {
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int respuesta = oArticuloDAO.ELiminarArticuloProveedorPrecio(IdProveedor, IdArticulo,BaseDatos);
            return respuesta;
        }



        public string GuardarFile(IFormFile file)
        {
            List<string> Archivos = new List<string>();
            if (file != null && file.Length > 0)
            {
                try
                {
                    string dir = "wwwroot/Anexos/" + file.FileName;
                    if (Directory.Exists(dir))
                    {
                        ViewBag.Message = "Archivo ya existe";
                    }
                    else
                    {
                        string filePath = Path.Combine(dir, Path.GetFileName(file.FileName));
                        using (Stream fileStream = new FileStream(dir, FileMode.Create, FileAccess.Write))
                        {
                            file.CopyTo(fileStream);
                            Archivos.Add(file.FileName);
                        }

                        ViewBag.Message = "Anexo guardado correctamente";
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error:" + ex.Message.ToString();
                    throw;
                }
            }
            return JsonConvert.SerializeObject(Archivos);
        }




        public string ProcesarExcel(string archivo)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            string filePath = "wwwroot/Anexos/" + archivo;

            List<ExcelArticuloDTO> Datos = new List<ExcelArticuloDTO>();

            // Leer el archivo Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {

                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;


                try
                {
                    for (int row = 2; row <= rowCount; row++)
                    {

                        ExcelArticuloDTO excelDTO = new ExcelArticuloDTO();
                        excelDTO.Codigo = (worksheet.Cells[row, 1].Value.ToString().Trim());
                        excelDTO.Descripcion1 = (worksheet.Cells[row, 2].Value.ToString().Trim());
                        excelDTO.Descripcion2 = (worksheet.Cells[row, 3].Value.ToString().Trim());
                        excelDTO.ArticuloInventario = (worksheet.Cells[row, 4].Value.ToString().Trim()) == "1" ? true:false;
                        excelDTO.ArticuloCompra = (worksheet.Cells[row, 5].Value.ToString().Trim()) == "1" ? true : false;
                        excelDTO.ArticuloVenta = (worksheet.Cells[row, 6].Value.ToString().Trim()) == "1" ? true : false;
                        excelDTO.ActivoFijo = (worksheet.Cells[row,7].Value.ToString().Trim()) == "1" ? true : false;
                        excelDTO.ActivoCatalogo = (worksheet.Cells[row, 8].Value.ToString().Trim()) == "1" ? true : false;
                        excelDTO.GrupoUnidadMedidaBase =int.Parse(worksheet.Cells[row, 9].Value.ToString().Trim());
                        excelDTO.UnidadMedida = int.Parse(worksheet.Cells[row, 10].Value.ToString().Trim());
                        excelDTO.Construccion = int.Parse(worksheet.Cells[row, 11].Value.ToString().Trim());
                        excelDTO.Servicio = int.Parse(worksheet.Cells[row, 12].Value.ToString().Trim());
                        excelDTO.TipoProducto = int.Parse(worksheet.Cells[row, 13].Value.ToString().Trim());
                        Datos.Add(excelDTO);
                    }

                    ArticuloDAO articulo = new ArticuloDAO();
                    ObraDAO obra = new ObraDAO();
                    BaseDAO obase = new BaseDAO();
                    string Mensaje = "";
                    string Rpta = "";
                    for (int i = 0; i < Datos.Count; i++)
                    {
                        int res = articulo.InsertArticuloMasivoExcel(Datos[i], BaseDatos, ref Rpta);
                        if (res > 0)
                        {
                            if (Datos[i].Construccion == 1)
                            {
                                var ObrasConstruccion = obra.ObtenerObraxDivision(2, BaseDatos, ref Rpta);
                                for (int j = 0; j < ObrasConstruccion.Count; j++)
                                {
                                    obase.InsertCatalogoObraMasivo(ObrasConstruccion[j].IdObra, res, Datos[i].TipoProducto, Datos[i].ArticuloInventario, BaseDatos,ref Rpta);
                                }
                            }

                            if (Datos[i].Servicio == 1)
                            {
                                var ObrasServicio = obra.ObtenerObraxDivision(1, BaseDatos, ref Rpta);
                                for (int j = 0; j < ObrasServicio.Count; j++)
                                {
                                    obase.InsertCatalogoObraMasivo(ObrasServicio[j].IdObra, res, Datos[i].TipoProducto, Datos[i].ArticuloInventario, BaseDatos, ref Rpta);
                                }
                            }

                            //Mensaje += "Registrado con Exito - " + Datos[i].Descripcion1 + "</br>";
                        }
                        else
                        {
                            //Mensaje += "Ya existe - " + Datos[i].Descripcion1 + "</br>";
                        }
                    
                    }



                    return Mensaje;

                }
                catch (Exception e)
                {
                    return mensaje_error = "error";
                }

            }

        }

        public string ProcesarExcelProvPrecio(string archivo)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            string filePath = "wwwroot/Anexos/" + archivo;

            List<ExcelPrecioProvDTO> Datos = new List<ExcelPrecioProvDTO>();

            // Leer el archivo Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {

                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;


                try
                {
                    for (int row = 2; row <= rowCount; row++)
                    {

                        ExcelPrecioProvDTO excelDTO = new ExcelPrecioProvDTO();
                        excelDTO.CodigoProducto = (worksheet.Cells[row, 1].Value.ToString().Trim());
                        excelDTO.RUC = (worksheet.Cells[row, 2].Value.ToString().Trim());
                        excelDTO.PrecioExtranjero = decimal.Parse((worksheet.Cells[row, 3].Value.ToString().Trim())=="-"? "0" : worksheet.Cells[row, 3].Value.ToString().Trim());
                        excelDTO.PrecioNacional = decimal.Parse((worksheet.Cells[row, 4].Value.ToString().Trim()) == "-" ? "0" : worksheet.Cells[row, 4].Value.ToString().Trim());
                        excelDTO.CodigoObra = (worksheet.Cells[row, 5].Value.ToString().Trim());
                        //excelDTO.DiasEntrega = 0;//int.Parse((worksheet.Cells[row, 8].Value.ToString().Trim()) == "NULL" ? "0" : worksheet.Cells[row, 8].Value.ToString().Trim());

                        Datos.Add(excelDTO);
                    }
                    string Mensaje = "";

                    ArticuloDAO articulo = new ArticuloDAO();
                    ProveedorDAO proveedor = new ProveedorDAO();
                    ObraDAO obra = new ObraDAO();
                    BaseDAO obase = new BaseDAO();
                    string Rpta = "";
                    for (int i = 0; i < Datos.Count; i++)
                    {
                        List<ArticuloDTO> lstArticulo = articulo.ObtenerArticuloXCodigo(Datos[i].CodigoProducto, BaseDatos, ref Rpta);
                        if (lstArticulo.Count == 0) { Mensaje += "El Articulo " + Datos[i].CodigoProducto + " No Existe </br>"; continue; }
                        if (lstArticulo.Count != 1) { Mensaje += "El Articulo " + Datos[i].CodigoProducto + " Está Duplicado, Verificar </br>"; continue; }
                        int IdArticulo = lstArticulo[0].IdArticulo;


                        List<ProveedorDTO> lstProveedor = proveedor.ObtenerProveedorxNroDoc(Datos[i].RUC, BaseDatos);
                        if (lstProveedor.Count == 0) { Mensaje += "El Proveedor " + Datos[i].RUC + " No Existe </br>"; continue; }
                        if (lstProveedor.Count != 1) { Mensaje += "El Proveedor " + Datos[i].RUC + " Está Duplicado, Verificar </br>"; continue; }
                        int IdProveedor = lstProveedor[0].IdProveedor;
                        int IdCondicionPagoProveedor = lstProveedor[0].CondicionPago;
                        Datos[i].DiasEntrega = lstProveedor[0].DiasEntrega;
                        if (IdCondicionPagoProveedor == 0)
                        {
                            IdCondicionPagoProveedor = 1;
                        }

                        int IdObra = 0;

                        if(Datos[i].CodigoObra != "0")
                        {
                            List<ObraDTO> lstObra = obra.ObtenerObraxCodigo(Datos[i].CodigoObra, BaseDatos, ref Rpta);
                            if (lstObra.Count != 1) { Mensaje += "La Obra " + Datos[i].CodigoObra + " Está Duplicado, Verificar </br>"; continue; }                                                     
                            IdObra = lstObra[0].IdObra;
                            if (IdObra != 0)
                            {
                                articulo.SavePrecioProveedorNuevo(IdArticulo, IdProveedor, Datos[i].PrecioNacional, Datos[i].PrecioExtranjero, IdCondicionPagoProveedor, Datos[i].DiasEntrega, 1, IdObra, BaseDatos, ref mensaje_error);
                            }
                        }
                        else
                        {
                            articulo.SavePrecioProveedorNuevo(IdArticulo, IdProveedor, Datos[i].PrecioNacional, Datos[i].PrecioExtranjero, IdCondicionPagoProveedor, Datos[i].DiasEntrega, 1, IdObra, BaseDatos, ref mensaje_error);

                        }

                    }





                    return Mensaje;

                }
                catch (Exception e)
                {
                    return mensaje_error = "error";
                }

            }

        }

        public string ListarArticulosxAlmacenSelect2(int IdAlmacen, int IdTipoProducto , string searchTerm = "", int IdClaseArticulo = 1)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();

            if (IdClaseArticulo == 1)
            {
                lstArticuloDTO = oArticuloDAO.ListarArticulosxAlmacenSelect2(searchTerm, IdAlmacen, IdTipoProducto, BaseDatos, ref mensaje_error);
            }
            if (IdClaseArticulo == 2)
            {
                lstArticuloDTO = oArticuloDAO.ListarServiciosxAlmacenSelect2(searchTerm, IdAlmacen, BaseDatos, ref mensaje_error);
            }
            if (IdClaseArticulo == 3)
            {
                lstArticuloDTO = oArticuloDAO.ListarActivosFijosxAlmacenSelect2(searchTerm, IdAlmacen, BaseDatos, ref mensaje_error);
            }



            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ListarArticulosActivosSelect2(string searchTerm = "")
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();

           
                lstArticuloDTO = oArticuloDAO.ListarArticulosSelect2(searchTerm, BaseDatos, ref mensaje_error);
       



            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

    }
}
