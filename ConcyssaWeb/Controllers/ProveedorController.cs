using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace ConcyssaWeb.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerProveedores()
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerProveedores(IdSociedad.ToString(),BaseDatos);
            if (lstProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProveedorDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertProveedor(ProveedorDTO proveedorDTO)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oProveedorDAO.UpdateInsertProveedor(proveedorDTO, IdSociedad.ToString(),BaseDatos);
            if (resultado != 0)
            {

                /*INSERTAR ANEXO*/
                if (proveedorDTO.AnexoDetalle != null)
                {
                    for (int i = 0; i < proveedorDTO.AnexoDetalle.Count; i++)
                    {
                        proveedorDTO.AnexoDetalle[i].ruta = "/Anexos/" + proveedorDTO.AnexoDetalle[i].NombreArchivo;
                        proveedorDTO.AnexoDetalle[i].IdSociedad = IdSociedad;
                        proveedorDTO.AnexoDetalle[i].Tabla = "SociosNegocio";
                        proveedorDTO.AnexoDetalle[i].IdTabla = resultado;
                        string mensaje_error = "error";
                        oMovimientoDAO.InsertAnexoMovimiento(proveedorDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);

                    }
                }
            }

            return resultado;

        }


        public string ObtenerDatosxIDNuevo(int IdProveedor)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            ProveedorDTO oProveedorDTO = new ProveedorDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            oProveedorDTO = oProveedorDAO.ObtenerDatosxIDNuevo(IdProveedor,BaseDatos);
            if (oProveedorDTO != null)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstProveedorDetalleDTO.Count;
                //oDataTableDTO.iTotalRecords = lstProveedorDetalleDTO.Count;
                //oDataTableDTO.aaData = (lstProveedorDetalleDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oProveedorDTO);

            }
            else
            {
                return mensaje_error;

            }
        }
        public string ObtenerDatosxID(int IdProveedor)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerDatosxID(IdProveedor,BaseDatos);

            if (lstProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProveedorDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarProveedor(int IdProveedor)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.Delete(IdProveedor,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
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
        public int EliminarAnexo(int IdAnexo)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.DeleteAnexo(IdAnexo,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
        public int InsertRubroProveedor_X_Provedor(RubroXProveedorDTO oRubroXProveedorDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.InsertRubroProveedor_X_Provedor(oRubroXProveedorDTO,IdUsuario,BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }
        public string ListarRubroProveedor_X_Provedor(int IdProveedor)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            List<RubroXProveedorDTO> lstRubroXProveedorDTO = oProveedorDAO.ListarRubroProveedor_X_Provedor(IdProveedor,BaseDatos,ref mensaje_error);

            if (lstRubroXProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstRubroXProveedorDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public int EliminarRubroProveedor_X_Provedor(int Id, int IdUsuario)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.EliminarRubroProveedor_X_Provedor(Id,IdUsuario,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
        public int UpdateCondicionPagoProveedor(ProveedorDTO proveedorDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.UpdateCondicionPagoProveedor(proveedorDTO,BaseDatos);
            if (resultado != 0)
            {
               return resultado;
            }

            return resultado;

        }


        public string ProcesarExcelProveedores(string archivo)
        {
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            string filePath = "wwwroot/Anexos/" + archivo;

            List<ProveedorExcelDTO> Datos = new List<ProveedorExcelDTO>();

            // Leer el archivo Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {

                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;


                try
                {
                    for (int row = 2; row <= rowCount; row++)
                    {

                        ProveedorExcelDTO excelDTO = new ProveedorExcelDTO();
                        excelDTO.Codigo = (worksheet.Cells[row, 1].Value.ToString().Trim());
                        excelDTO.NroDoc = (worksheet.Cells[row, 2].Value.ToString().Trim());
                        excelDTO.RazonSocial = ((worksheet.Cells[row, 3].Value.ToString().Trim()));
                        excelDTO.Direccion = ((worksheet.Cells[row, 4].Value == null ? "" : worksheet.Cells[row, 4].Value.ToString().Trim()));
                        excelDTO.CondPagoSAp = int.Parse(worksheet.Cells[row, 5].Value.ToString().Trim());
                        excelDTO.Correo = (worksheet.Cells[row, 7].Value == null ? "" : worksheet.Cells[row, 6].Value.ToString().Trim());     

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
                        switch (Datos[i].CondPagoSAp)
                        {
                            case -1:
                                Datos[i].CondicionPago = 1;
                                break;
                            case 1:
                                Datos[i].CondicionPago = 2;
                                break;
                            case 2:
                                Datos[i].CondicionPago = 20016;
                                break;
                            case 3:
                                Datos[i].CondicionPago = 20008;
                                break;
                            case 5:
                                Datos[i].CondicionPago = 20010;
                                break;
                            case 12:
                                Datos[i].CondicionPago = 1;
                                break;
                            case 14:
                                Datos[i].CondicionPago = 20017;
                                break;
                            case 16:
                                Datos[i].CondicionPago = 20018;
                                break;
                            default:
                                Mensaje += "El Proveedor " + Datos[i].NroDoc + " Tiene una Condicion de Pago No Controlada, Verificar </br>"; continue;
                               
                        }



                        List<ProveedorDTO> lstProveedor = proveedor.ObtenerProveedorxNroDoc(Datos[i].NroDoc, BaseDatos);
                        if (lstProveedor.Count > 1) { Mensaje += "El Proveedor " + Datos[i].NroDoc + " Está Duplicado, Verificar </br>"; continue; }
                        if (lstProveedor.Count == 0) { 
                            

                            ProveedorDTO proveedorDTO = new ProveedorDTO();
                            proveedorDTO.IdProveedor = 0;
                            proveedorDTO.IdSociedad = IdSociedad;
                            proveedorDTO.NumeroDocumento = Datos[i].NroDoc;
                            proveedorDTO.RazonSocial = Datos[i].RazonSocial;
                            proveedorDTO.CondicionPago = Datos[i].CondicionPago;
                            proveedorDTO.DireccionFiscal = Datos[i].Direccion;
                            proveedorDTO.CodigoCliente = Datos[i].Codigo;
                            proveedorDTO.FechaIngreso = DateTime.Now;
                            proveedorDTO.TipoDocumento = 6;
                            proveedorDTO.TipoPersona = 2;
                            proveedorDTO.Departamento = 0;
                            proveedorDTO.Provincia = 0;
                            proveedorDTO.Distrito = 0;
                            proveedorDTO.Pais = 193;
                            proveedorDTO.Estado = true;
                            proveedorDTO.Tipo = 2;
                            proveedorDTO.DiasEntrega = 0;
                            proveedorDTO.Afecto4ta = false;
                            proveedorDTO.Email = Datos[i].Correo;

                            int rpt = proveedor.UpdateInsertProveedor(proveedorDTO, IdSociedad.ToString(), BaseDatos);
                            if(rpt == 0) { Mensaje += "El Proveedor " + Datos[i].NroDoc + " No se Registro, Verificar </br>"; continue; }
                            
                        
                        
                        }
                        
                  
                    }


                    if (Mensaje == "")
                    {
                        Mensaje = "Archivo Procesado Correctamente";
                    }
                   


                    return Mensaje;

                }
                catch (Exception e)
                {
                    return mensaje_error = "error";
                }

            }

        }
    }
}
