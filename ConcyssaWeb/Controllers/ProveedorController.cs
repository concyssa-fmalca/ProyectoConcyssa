using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using static ConcyssaWeb.Controllers.SolicitudDespachoController;
using static DTO.ClienteDTO;

namespace ConcyssaWeb.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerProveedores(bool Logistica = false,bool AgregarConcyssa=false)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerProveedores(IdSociedad.ToString(),Logistica, AgregarConcyssa,BaseDatos);
            if (lstProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProveedorDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerProveedoresSelect2(string searchTerm="")
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerProveedoresSelect2(searchTerm,BaseDatos);
            if (lstProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProveedorDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerProveedoresDataTable(string ClientParameters,bool Logistica = false)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            DataTableParameter dtp = JsonConvert.DeserializeObject<DataTableParameter>(ClientParameters);

            object json = null;


            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerProveedoresDataTable(dtp.start, dtp.length, dtp.search.value, IdSociedad.ToString(), Logistica, BaseDatos);           
            int Cantidad = oProveedorDAO.ObtenerProveedoresTotal(dtp.search.value,IdSociedad.ToString(), Logistica, BaseDatos);

            json = new { draw = dtp.draw, recordsFiltered = Cantidad, recordsTotal = Cantidad, data = lstProveedorDTO };
                
            return JsonConvert.SerializeObject(json);

        }

        class ResponseConsultaRuc
        {
            public string ruc { get; set; }
            public string dni { get; set; }
        }

        public int UpdateInsertProveedor(ProveedorDTO proveedorDTO)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            if(proveedorDTO.TipoDocumento == 1 || proveedorDTO.TipoDocumento == 6)
            {
                string responseBody = "";
                var url = "";
                if (proveedorDTO.TipoDocumento == 1)
                {
                    url = $"https://e-factura.tuscomprobantes.pe/wsconsulta/dni/" + proveedorDTO.NumeroDocumento;
                }
                else
                {
                    url = $"https://e-factura.tuscomprobantes.pe/wsconsulta/ruc/" + proveedorDTO.NumeroDocumento;
                }
             
                RespuestaTusComprobantes oResponseConsultaRuc = new RespuestaTusComprobantes();


                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream strReader = response.GetResponseStream())
                        {
                            if (strReader == null) { }
                            else
                                using (StreamReader objReader = new StreamReader(strReader))
                                {
                                    responseBody = objReader.ReadToEnd();
                                    oResponseConsultaRuc = JsonConvert.DeserializeObject<RespuestaTusComprobantes>(responseBody);
                                }
                        }
                    }
                }
                catch
                {
                    return -3;
                }

                if(oResponseConsultaRuc.mensaje != null)
                {
                    return -4;
                }
          
            }



            int resultado = oProveedorDAO.UpdateInsertProveedor(proveedorDTO, IdSociedad.ToString(),BaseDatos);
            if (resultado > 0)
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
                        string mensaje_error = "";
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

        public string Encrypt(string dato)
        {
            string hash = "encriptando bd";
            byte[] data = UTF8Encoding.UTF8.GetBytes(dato);

            MD5 md5 = MD5.Create();
            TripleDES tripDES = TripleDES.Create();

            tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public string Desencrypt(string dato)
        {

            dato = dato.Replace(" ", "+");
            string hash = "encriptando bd";
            byte[] data = Convert.FromBase64String(dato);

            MD5 md5 = MD5.Create();
            TripleDES tripDES = TripleDES.Create();

            tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
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

        public string CrearUsuarioPortalProv(int IdProveedor)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerDatosxID(IdProveedor, BaseDatos);
            string mensaje_error = "";

            ConfiguracionSociedadDAO ConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            List<ConfiguracionSociedadDTO> lstConfiguracionSociedadDTO = ConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);

            Random random = new Random();

            string Password = lstProveedorDTO[0].NumeroDocumento + "$" + random.Next(100, 999).ToString();
            Password = Encrypt(Password);

            int resultado = oProveedorDAO.CrearUsuarioPortalProv(lstProveedorDTO[0], Password, lstConfiguracionSociedadDTO[0].BasePortalProv, ref mensaje_error);

            object json = null;

            if (resultado == 0)
            {
                json = new { status = false, mensaje = mensaje_error };
            }
            else
            {

                try
                {
                    string from = "concyssa.smc@gmail.com";//"compras.smartcode@gmail.com";//"kevin.huancahuari@smartcode.pe";
                    string correo = from;
                    string password = "tlbvngkvjcetzunr";//"N0r3ply2023";//"Eco2023";//"pncnbartepxhiyuc";
                    string displayName = "[CONCYSSA] - CREDENCIALES AL PORTAL DE PROVEEDORES";
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(from, displayName);


                    //mail.To.Add(oPedidoDTO.EmailProveedor);
                    //mail.To.Add(DatosConfig[0].Correo);
                    mail.To.Add("cristhian.chacaliaza@smartcode.pe");
                    mail.Subject = "CREDENCIALES AL PORTAL DE PROVEEDORES";
                    //mail.CC.Add(new MailAddress("camala145@gmail.com"));
                    mail.Body = TemplateEmail(lstProveedorDTO[0].NumeroDocumento, Desencrypt(Password));//"El proveedor " + DatosProv.DescripcionProveedor + " ha ingresado documentos al portal web."; //TemplateEmail(Estado, Lista, Comentarios);

                    mail.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                    client.Credentials = new NetworkCredential(from, password);
                    client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false


                    client.Send(mail);
                }
                catch (Exception e)
                {
                    mensaje_error = e.Message.ToString();
                  
                }

    

                json = new { status = true, mensaje = mensaje_error };
            }

            return JsonConvert.SerializeObject(json);

        }



        public string TemplateEmail(string Usuario, string Clave)
        {
            string bodyhtml;
            bodyhtml = @"
              <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN'
     'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
     <html xmlns='http://www.w3.org/1999/xhtml'
     style='font-family:  Helvetica, Arial, sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
     <head>
     <meta name='viewport' content='width=device-width'/>
     <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>
     <title>Adminto - Responsive Bootstrap 5 Admin Dashboard</title>

     </head>

     <body itemscope itemtype='http://schema.org/EmailMessage'
     style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em; background-color: #f6f6f6; margin: 0;'
     bgcolor='#f6f6f6'>

     <table class='body-wrap'
     style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #f6f6f6; margin: 0;'
     bgcolor='#f6f6f6'>
     <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
     <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;'
         valign='top'></td>
     <td class='container' width='600'
         style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;'
         valign='top'>
         <div class='content'
              style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;'>
             <table class='main' width='100%' cellpadding='0' cellspacing='0'
                    style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px;  margin: 0; border: none;'
                    >
                 <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                     <td class='content-wrap aligncenter'
                         style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;padding: 20px;border: 3px solid #5fab3f;border-radius: 7px; background-color: #fff;'
                         align='center' valign='top'>
                         <table width='100%' cellpadding='0' cellspacing='0'
                                style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                             <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                 <br class='content-block'
                                     style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;'
                                     valign='top'>
                                     <h2 class='aligncenter'
                                         style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 20px; color: #000; line-height: 1.2em; font-weight: 400; text-align: center; margin: 40px 0 0;'
                                         align='center'>
                                         <a href='#' style='display: block;margin-bottom: 10px;'> <img src='https://smartcode.pe/img/elements/LOGO_CONCYSSA.jpg' height='50' alt='logo'/></a> <br/>
                                         ¡Bienvenido al Portal de Proveedores!</h2>

                                            <p style='text-align:center'>Se envían las credenciales de acceso:</p>
                                 </td>
                             </tr>
                             <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                 <td class='content-block aligncenter'
                                     style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: center; margin: 0; padding: 0 0 20px;'
                                     align='center' valign='top'>
                                     <table class='invoice'
                                            style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; text-align: left; width: 80%; margin: 40px auto;'>
                                         <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                
                                         </tr>
                                         <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                             <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                 valign='top'><b>USUARIO: </b>" + Usuario + @"
                                             </td>
                                         </tr>
                                         <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                             <td style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                 valign='top'><b>CLAVE: </b>" + Clave + @"
                                             </td>
                                         </tr>
                                         <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                                             <td align='center' class=""content-block aligncenter"" style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;'
                                                 valign='top'> <a href='http://google.com'><button style='background-color: #4d8038;color: white; width: 200px;height: 50px;'>INGRESAR</button></a>
                                             </td>
                                         </tr>                                
                                 </table>
                                 </td>
                             </tr>
     
     
                         </table>
                     </td>
                 </tr>
             </table>
             <div class='footer'
                  style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;'>
                 <table width='100%'
                        style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
                     <tr style='font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;'>
 
                     </tr>
                 </table>
             </div>
         </div>
     </td>
     <td style = 'font-family: Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;'
         valign= 'top' ></td >
     </tr>
     </table>";

            return bodyhtml;
        }

    }
}
