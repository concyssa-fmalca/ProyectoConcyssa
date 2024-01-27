using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ISAP;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using Microsoft.Win32;

namespace ConcyssaWeb.Controllers
{
    public class IntegradorV1Controller : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public IActionResult Listado2()
        {
            return View();
        }


        public int MoverAEnviarSap(string Tabla,int Id, int GrupoCreacion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = 0;
            switch (Tabla)
            {
                case "OPCH":
                    resultado = oIntregadorV1DAO.MoverFacturaEnviarSap(Id, IdUsuario, GrupoCreacion,BaseDatos);
                    break;
                case "ORPC":
                    resultado = oIntregadorV1DAO.MoverNotaCreditoEnviarSap(Id, IdUsuario, GrupoCreacion,BaseDatos);
                    break;
                default:
                    break;
            }


            return resultado;
        }
        public int ObtenerGrupoCreacionEnviarSap()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.ObtenerGrupoCreacionEnviarSap(BaseDatos);

            return resultado;
        }

        public string ObtenerListaDatosTrabajo()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<ListaTrabajoDTO> lstListaTrabajoDTO = oIntregadorV1DAO.ObtenerListaDatosTrabajo(IdUsuario,BaseDatos);
            if (lstListaTrabajoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstListaTrabajoDTO);
            }
            else
            {
                return "error";
            }
        }
        public string ListarEnviarSap(int GrupoCreacion)
        {
            string mensaje_error = "";
            IntregadorV1DAO pIntregadorV1DAO = new IntregadorV1DAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            List<IntegradorV1DTO> lstIntegradorV1DTO = pIntregadorV1DAO.ListarEnviarSap(GrupoCreacion,BaseDatos,ref mensaje_error);
            if (lstIntegradorV1DTO.Count >= 0 && mensaje_error.Length == 0)
            {             
                return JsonConvert.SerializeObject(lstIntegradorV1DTO);

            }
            else
            {
                return mensaje_error;

            }
        }

        public string ObtenerDatosxIdEnviarSap(int IdEnviarSap)
        {
            string mensaje_error = "";
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntegradorV1DTO oIntegradorV1DTO = oIntregadorV1DAO.ObtenerDatosxIdEnviarSap(IdEnviarSap,BaseDatos,ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<IntegradorV1Detalle> lstIntegradorV1Detalle = new List<IntegradorV1Detalle>();
                lstIntegradorV1Detalle = oIntregadorV1DAO.ObtenerDatosxIdEnviarSapDetalle(IdEnviarSap,BaseDatos,ref mensaje_error);
                oIntegradorV1DTO.detalles = new IntegradorV1Detalle[lstIntegradorV1Detalle.Count()];
                for (int i = 0; i < lstIntegradorV1Detalle.Count; i++)
                {
                    oIntegradorV1DTO.detalles[i] = lstIntegradorV1Detalle[i];
                }

                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oIntregadorV1DAO.ObtenerAnexoEnviarSap(IdEnviarSap,BaseDatos,ref mensaje_error);
                oIntegradorV1DTO.AnexoDetalle = new AnexoDTO[lstAnexoDTO.Count()];
                for (int i = 0; i < lstAnexoDTO.Count; i++)
                {
                    oIntegradorV1DTO.AnexoDetalle[i] = lstAnexoDTO[i];
                }

                return JsonConvert.SerializeObject(oIntegradorV1DTO);
            }
            else
            {
                return mensaje_error;
            }
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

        public int EliminarAnexoEnviarSap(int IdEnviarSapAnexos)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.DeleteAnexo(IdEnviarSapAnexos,BaseDatos);
            if (resultado > 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarMarcoTrabajo(int GrupoCreacion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.EliminarMarcoTrabajo(GrupoCreacion,BaseDatos);

            return resultado;
        }
        public int UpdateEnviarSap(IntegradorV1DTO oIntegradorV1DTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.UpdateEnviarSap(oIntegradorV1DTO,BaseDatos);

            return resultado;
        }

        public int UpdateCuadrillasEnviarSAP(IntegradorV1Detalle oIntegradorV1DTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.UpdateCuadrillasEnviarSAP(oIntegradorV1DTO,BaseDatos);

            return resultado;
        }
        public int UpdateAnexosEnviarSAP(int IdEnviarSap, string NombreArchivo)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            AnexoDTO oAnexoDTO = new AnexoDTO();
            oAnexoDTO.ruta = "/Anexos/" + NombreArchivo;
            oAnexoDTO.NombreArchivo = NombreArchivo;
            oAnexoDTO.IdSociedad = 1;
            oAnexoDTO.Tabla = "EnviarSap";
            oAnexoDTO.IdTabla = IdEnviarSap;

            int resultado = oMovimientoDAO.InsertAnexoMovimiento(oAnexoDTO,BaseDatos,ref mensaje_error);

            return resultado;
        }

        public string ObtenerTasaDetSAP()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;


            IntegradorTasaDetDTO oIntegradorTasaDetDTO = new IntegradorTasaDetDTO();
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorTasaDetDTO> lstIntegradorTasaDetDTO = oIntregadorV1DAO.ObtenerTasaDetSAP(BaseDatos,BaseDatosSAP,ref mensaje_error);
            if (lstIntegradorTasaDetDTO.Count > 0)
            {

                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstIntegradorTasaDetDTO);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerClasif()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;


            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;

            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorClasif> lstIntegradorClasif = oIntregadorV1DAO.ObtenerCLABYSADQ(BaseDatos,BaseDatosSAP,ref mensaje_error);
            if (lstIntegradorClasif.Count > 0)
            {

                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstIntegradorClasif);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerGrupoDetSAP()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;

            IntegradorGrupoDetDTO oIntegradorGrupoDetDTO = new IntegradorGrupoDetDTO();
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorGrupoDetDTO> lstIntegradorGrupoDetDTO = oIntregadorV1DAO.ObtenerGrupoDetSAP(BaseDatos,BaseDatosSAP,ref mensaje_error);
            if (lstIntegradorGrupoDetDTO.Count > 0)
            {

                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstIntegradorGrupoDetDTO);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerCondPagoDetSAP(string GrupoDet)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;


            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;

            IntegradorGrupoDetDTO oIntegradorGrupoDetDTO = new IntegradorGrupoDetDTO();
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorCondPagoDetDTO> lstIntegradorGrupoDetDTO = oIntregadorV1DAO.ObtenerCondPagoDetSAP(GrupoDet,BaseDatos,BaseDatosSAP,ref mensaje_error);
            if (lstIntegradorGrupoDetDTO.Count > 0)
            {

                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstIntegradorGrupoDetDTO);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerTipoDocumentoSAP()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;


            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;

            IntegradorGrupoDetDTO oIntegradorGrupoDetDTO = new IntegradorGrupoDetDTO();
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorTipoDocumento> lstIntegradorTipoDocumento = oIntregadorV1DAO.ObtenerTipoDocumentoSAP( BaseDatosSAP, ref mensaje_error);
            if (lstIntegradorTipoDocumento.Count > 0)
            {

                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstIntegradorTipoDocumento);

            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerSerieSAP(int ObjCode)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;


            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorSerieSapDTO> lstIntegradorSerieSapDTO = oIntregadorV1DAO.ObtenerSerieSAP(ObjCode,BaseDatos,BaseDatosSAP,ref mensaje_error);
            if (lstIntegradorSerieSapDTO.Count > 0)
            {

                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstIntegradorSerieSapDTO);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ValidarGrupoEnviado(int GrupoCreacion)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            ListaTrabajoDTO respuesta = oIntregadorV1DAO.ValidarGrupoEnviado(GrupoCreacion,BaseDatos,ref mensaje_error);
            if (respuesta.EstadoEnviado != "")
            {

                //return oDataTableDTO;
                return JsonConvert.SerializeObject(respuesta);

            }
            else
            {
                return mensaje_error;
            }
        }
        public string ConectarSAP(List<int> IdsEnviarSAP, int BorradorFirme, int SerieFactura, int SerieNotaCredito)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string respuesta = ConexionSAP.Conectar();

            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();

            List<IntegradorV1DTO> lstResultados = new List<IntegradorV1DTO>();


            for (int i = 0; i < IdsEnviarSAP.Count; i++)
            {
                IntegradorV1DTO Datos = oIntregadorV1DAO.ListarEnviarSapConDetallexIdEnviarSAP(IdsEnviarSAP[i], BaseDatos);
                lstResultados.Add(Datos);
            }
          
            string mensaje_error = "";

            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);


            string BaseDatosSAP = configuracion[0].NombreBDSAP;

            string respuestaSAP = ".";
            

            try
            {

                if(respuesta == "true")
                {
                    if (lstResultados.Count == 0)
                    {
                        ConexionSAP.DesconectarSAP();
                        return "Ocurrió un Error al Cargar los Datos de los Documentos Seleccionados";
                    }
                    ConexionSAP conexion = new ConexionSAP();
                    for (int i = 0; i < lstResultados.Count; i++)
                    { 
                        int existe = oIntregadorV1DAO.ValidarExisteProveedorSAP(lstResultados[i].RUCProveedor, BaseDatosSAP, ref mensaje_error);
                        if (existe == 0)
                        {
                            respuestaSAP += "El Proveedor "+ lstResultados[i].Proveedor + " No está registrado en SAP \n";
                            continue;
                        }

                        switch (lstResultados[i].TablaOriginal)
                        {
                            case "OPCH":
                                respuestaSAP += conexion.AddComprobante(lstResultados[i], BorradorFirme, SerieFactura,BaseDatos, BaseDatosSAP,ref mensaje_error) + "\n";
                                break;
                            case "ORPC":
                                respuestaSAP += conexion.AddNotaCredito(lstResultados[i], BorradorFirme, SerieNotaCredito,BaseDatos, BaseDatosSAP,ref mensaje_error) + "\n";
                                break;
                            default:
                                break;
                        }

                        //respuestaSAP += Mensaje_error;
                    }
                    ConexionSAP.DesconectarSAP();
                }
                else
                {
                    respuestaSAP = "Credenciales No Validas para Conexión a SAP";
                }
            }
            catch (Exception e)
            {
                respuestaSAP = e.Message.ToString();
            }



            return respuestaSAP;
            
        }
        public string ListarCamposxIdObra(int IdObra, int IdTipoRegistro, int IdSemana)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<IntegradorV1DTO> lstOpchDTO = oIntregadorV1DAO.ListarCamposxIdObra(IdObra, IdTipoRegistro, IdSemana, BaseDatos,ref mensaje_error);
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOpchDTO.Count;
                oDataTableDTO.iTotalRecords = lstOpchDTO.Count;
                oDataTableDTO.aaData = (lstOpchDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;

            }
        }

        public string DescargarExcelProvNoRegistrados(int GrupoCreacion)
        {
            try
            {               
                string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;

                IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();

                var lstResultados = oIntregadorV1DAO.ListarEnviarSapConDetallexGrupoCreacion(GrupoCreacion, BaseDatos);

                string mensaje_error = "";

                ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
                var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);


                string BaseDatosSAP = configuracion[0].NombreBDSAP;

                string respuestaSAP = ".";

                List<string> ProveedoresNoEncontrados = new List<string>();

                for (int i = 0; i < lstResultados.Count; i++)
                {
                    int existe = oIntregadorV1DAO.ValidarExisteProveedorSAP(lstResultados[i].RUCProveedor, BaseDatosSAP, ref mensaje_error);
                    if (existe == 0)
                    {
                        if (!ProveedoresNoEncontrados.Contains(lstResultados[i].RUCProveedor))
                        {
                            ProveedoresNoEncontrados.Add(lstResultados[i].RUCProveedor);
                        }
                    }
                }

                if (ProveedoresNoEncontrados.Count == 0)
                {
                    return "SIN DATOS";
                }

                ProveedorDAO proveedorDAO = new ProveedorDAO();
                List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
                List<IntegradorProveedorDTO> lstProveedorExcelSAPDTO = new List<IntegradorProveedorDTO>();
                for (int i = 0; i < ProveedoresNoEncontrados.Count; i++)
                {
                    lstProveedorDTO = proveedorDAO.ObtenerProveedorxNroDoc(ProveedoresNoEncontrados[i], BaseDatos);
                    IntegradorProveedorDTO oIntegradorProveedorDTO = new IntegradorProveedorDTO();
                    oIntegradorProveedorDTO.CardCode = "P" + lstProveedorDTO[0].NumeroDocumento;
                    oIntegradorProveedorDTO.CardName = lstProveedorDTO[0].RazonSocial;
                    oIntegradorProveedorDTO.CardType = "S";
                    oIntegradorProveedorDTO.GroupCode = 101;
                    oIntegradorProveedorDTO.Address = lstProveedorDTO[0].DireccionFiscal;
                    oIntegradorProveedorDTO.Phone1 = lstProveedorDTO[0].Telefono;
                    oIntegradorProveedorDTO.Phone2 = "";
                    oIntegradorProveedorDTO.FederalTaxID = lstProveedorDTO[0].NumeroDocumento;
                    oIntegradorProveedorDTO.Currency = "##";

                    oIntegradorProveedorDTO.County = "Lima";
                    oIntegradorProveedorDTO.Country = "PE";

                    oIntegradorProveedorDTO.SubjectToWithholdingTax = lstProveedorDTO[0].Afecto4ta ? "Y" : "N" ;
                    oIntegradorProveedorDTO.BilltoDefault = "FISCAL";
                    oIntegradorProveedorDTO.U_EXX_TIPOPERS = (lstProveedorDTO[0].TipoPersona == 1) ? "TPN": "TPJ";
                    oIntegradorProveedorDTO.U_EXX_TIPODOCU = lstProveedorDTO[0].TipoDocumento;
                    oIntegradorProveedorDTO.U_EXX_APELLPAT = "";
                    oIntegradorProveedorDTO.U_EXX_APELLMAT = "";
                    oIntegradorProveedorDTO.U_EXX_PRIMERNO = "";
                    oIntegradorProveedorDTO.U_EXX_SEGUNDNO = "";
                    oIntegradorProveedorDTO.Properties15 = "Y";
                    lstProveedorExcelSAPDTO.Add(oIntegradorProveedorDTO);
                }

                string originalFilePath =  "wwwroot/Requerimiento/PlantillaCreaProvSAP.xlsx";
                string nuevaRutaArchivoCopia = "wwwroot/Anexos/MigrarProvSAP.xlsx";

                // Realizar copia del archivo original
                System.IO.File.Copy(originalFilePath, nuevaRutaArchivoCopia, true);

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                // Comenzar a registrar desde la fila 3 en la copia
                FileInfo fileInfo = new FileInfo(nuevaRutaArchivoCopia);

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    // Obtener la hoja de trabajo (worksheet)
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Puedes ajustar el índice según tu necesidad

                    // Encontrar la última fila usada en la columna A (por ejemplo)
                    int ultimaFilaUsada = worksheet.Dimension.End.Row;

                    // Comenzar a registrar desde la fila 3
                    int nuevaFilaInicio = 3;

                    for (int i = 0; i < lstProveedorExcelSAPDTO.Count; i++) // Por ejemplo, registrar 5 conjuntos de datos
                    {              
                        // Registros en columnas A a T
                   
                        worksheet.Cells["A" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].CardCode;
                        worksheet.Cells["B" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].CardName;
                        worksheet.Cells["C" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].CardType;
                        worksheet.Cells["D" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].GroupCode;
                        worksheet.Cells["E" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].Address;
                        worksheet.Cells["F" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].Phone1;
                        worksheet.Cells["G" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].Phone2;
                        worksheet.Cells["H" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].FederalTaxID;
                        worksheet.Cells["I" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].Currency;
                        worksheet.Cells["J" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].County;
                        worksheet.Cells["K" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].Country;
                        worksheet.Cells["L" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].SubjectToWithholdingTax;
                        worksheet.Cells["M" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].BilltoDefault;
                        worksheet.Cells["N" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].U_EXX_TIPOPERS;
                        worksheet.Cells["O" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].U_EXX_TIPODOCU;
                        worksheet.Cells["P" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].U_EXX_APELLPAT;
                        worksheet.Cells["Q" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].U_EXX_APELLMAT;
                        worksheet.Cells["R" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].U_EXX_PRIMERNO;
                        worksheet.Cells["S" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].U_EXX_SEGUNDNO;
                        worksheet.Cells["T" + nuevaFilaInicio].Value = lstProveedorExcelSAPDTO[i].Properties15;                   
                        nuevaFilaInicio++;
                    }


                    // Guardar los cambios
                    package.Save();
                }
                return "OK";
            }catch(Exception e)
            {
                return e.Message.ToString();
            }
        }

    }
}
