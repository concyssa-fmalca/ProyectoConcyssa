using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
using Microsoft.AspNetCore.Http;



namespace ISAP
{
    public class ConexionSAP
    {
        static SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();

        public static string Conectar()
        {
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();


            //ConfiguracionBL oconfiguracionBL = new ConfiguracionBL();
            //int IdSociedad = int.Parse(HttpContext.Current.Session["IdSociedad"].ToString());
            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            string mensaje_error = "error";
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, ref mensaje_error);
            //SociedadDAO sociedadDAO = new SociedadDAO();
            //var Sociedad = sociedadDAO.ObtenerDatosxID(IdSociedad);
            //List<ConfiguracionDTO> lstConfiguracionDTO = new List<ConfiguracionDTO>();
            //lstConfiguracionDTO = oconfiguracionBL.ObtenerDatosConfiguracionxIdEmpresa(IdEmpresa);
            //if (lstConfiguracionDTO.Count() > 0)
            //{
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            //UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            var oUsuarioDTO = usuarioDAO.ObtenerDatosxID(Convert.ToInt32(httpContextAccessor.HttpContext.Session.GetInt32("IdUsuario")), ref mensaje_error);
           // var oUsuarioDTO = usuarioDAO.ObtenerDatosxID(4070, ref mensaje_error);
            string SapServer = "SGCWEB";

            string SapCompanyDB = configuracion[0].NombreBDSAP;//"SBO_TITAN_PRB";
            string SapUserName = "";
            string SapPassword = "";

            SapUserName = oUsuarioDTO[0].SapUsuario;
            SapPassword = oUsuarioDTO[0].SapPassword;


            int SapDbServerType = 12;
            bool SapUseTrusted = false;
            string SapDbUserName = "sa";
            string SapDbPassword = "C0ncy$$@$ql";
            try
            {
                switch (SapDbServerType)
                {
                    case 1:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL;
                        break;
                    case 2:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_DB_2;
                        break;
                    case 3:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_SYBASE;
                        break;
                    case 4:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
                        break;
                    case 5:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MAXDB;
                        break;
                    case 6:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                        break;
                    case 7:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                        break;
                    case 8:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                        break;
                    case 9:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                        break;
                    case 10:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
                        break;
                    case 11:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017;
                        break;
                    case 12:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
                        break;
                    default:
                        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                        break;
                }

                oCompany.UseTrusted = SapUseTrusted;
                oCompany.Server = SapServer;
                oCompany.DbUserName = SapDbUserName;
                oCompany.DbPassword = SapDbPassword;
                oCompany.CompanyDB = SapCompanyDB;
                oCompany.UserName = SapUserName;
                oCompany.Password = SapPassword;

                // oCompany.LicenseServer = "192.168.100.21:40000";
                //oCompany.LicenseServer = SapServer + ":30000";
                oCompany.language = SAPbobsCOM.BoSuppLangs.ln_Spanish_La;
                var asd = oCompany.Connected;
                if (oCompany != null && asd)
                {
                    return "true";
                }
                if (oCompany.Connect() == 0)
                {
                    return "true";
                }
                else
                {
                    string errMsg = oCompany.GetLastErrorDescription();
                    int errCode = oCompany.GetLastErrorCode();
                    var ee = errMsg;
                    return errMsg;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                throw;
            }
            //}
            //else
            //{
            //    return "No hay configuracion creado para la empresa";
            //}




        }

        public static bool DesconectarSAP()
        {
            oCompany.Disconnect();
            return true;
        }

        public string AddComprobante(IntegradorV1DTO auxComprobante, int BorradorFirme, int SerieFactura, ref string mensaje_error)
        {
            try
            {

            string errMsg = "";
            // int IdEmpresa = int.Parse(HttpContext.Current.Session["IdEmpresa"].ToString());
            //Factura
            SAPbobsCOM.Documents ObjSAPComprobante = null;
            if (BorradorFirme == 1)
            {
                 ObjSAPComprobante = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
            }
            else
            {
                ObjSAPComprobante = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
            }

          
            //SAPbobsCOM.Documents ObjSAPComprobante = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

            //if (FacturReserva > 0)
            //{
            //    ObjSAPComprobante.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseInvoices;
            //    ObjSAPComprobante.ReserveInvoice = SAPbobsCOM.BoYesNoEnum.tYES;
            //}
            //else
            //{
            ObjSAPComprobante.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseInvoices;
            //}


            //ConsultasHanaBL consultas = new ConsultasHanaBL();
            //string Cuenta = consultas.ObtenerCuentaAsociadaProveedores("P" + auxComprobante.NumRucEmisor);
            //if (Cuenta == "")
            //{ }
            //else
            //{

            

                ConsultasSQL oConsultasSQL = new ConsultasSQL();
                ////ObjSAPComprobante.ControlAccount = cuentaContable;
                ////}
                ObraDAO oObraDAO = new ObraDAO();
                var datosObra = oObraDAO.ObtenerDatosxID(auxComprobante.IdObra, ref errMsg);

                DivisionDAO oDivisionDAO = new DivisionDAO();
                var datosDivision = oDivisionDAO.ObtenerDatosxID(datosObra[0].IdDivision,ref mensaje_error);


               
                //ObjSAPComprobante.UserFields.Fields.Item("U_EXX_CLABYSADQ").Value = datosDivision[0].IdClasif.ToString();
                
                ObjSAPComprobante.Project = datosObra[0].Codigo;
            ObjSAPComprobante.Comments = "Prueba Migracion desde IntegradorV1";

            ObjSAPComprobante.DocTotal = Convert.ToDouble(auxComprobante.Total);

                ProveedorDAO oProveedorDAO = new ProveedorDAO();
                var datosProveedor = oProveedorDAO.ObtenerDatosxID(auxComprobante.IdProveedor);


                //ObjSAPComprobante.CardCode = "P" + auxComprobante.NumRucEmisor;
                ObjSAPComprobante.CardCode = "P" + datosProveedor[0].NumeroDocumento;

                ObjSAPComprobante.CardName = datosProveedor[0].RazonSocial;



            //ObjSAPComprobante.GroupNumber = -1;
            ObjSAPComprobante.DocDate = auxComprobante.FechaContabilizacion; //fecha contabilizacion
                
                
                ObjSAPComprobante.Series = SerieFactura;

              


                CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
                var datosCondicionPago = oCondicionPagoDAO.ObtenerDatosxID(auxComprobante.idCondicionPago);
                var fechaVencimiento = auxComprobante.FechaContabilizacion.AddDays(datosCondicionPago[0].Dias);

                ObjSAPComprobante.DocDueDate = fechaVencimiento; //fecha vencimiento



                ObjSAPComprobante.TaxDate = auxComprobante.FechaDocumento; //fecha emision

            ObjSAPComprobante.FederalTaxID = datosProveedor[0].NumeroDocumento; //nro ruc

                //string codTipoDoc = auxComprobante.CodCpe;
                //ObjSAPComprobante.Indicator = codTipoDoc;

                MonedaDAO oMonedaDao = new MonedaDAO();
                var DatosMoneda = oMonedaDao.ObtenerDatosxID(auxComprobante.IdMoneda);


                if (DatosMoneda[0].Codigo == "USD")
                {
                    auxComprobante.CodMoneda = "USD";//oConfiguracionDTO[0].CodMonedaUSD;
                }
                if (DatosMoneda[0].Codigo == "S/")
                {
                    auxComprobante.CodMoneda = "SOL";//oConfiguracionDTO[0].CodMonedaPEN;
                }

                ObjSAPComprobante.DocCurrency = auxComprobante.CodMoneda;

               // ObjSAPComprobante.Series =

                SerieDAO oSerieDAO = new SerieDAO();
                var DatosSerie = oSerieDAO.ObtenerDatosxID(auxComprobante.IdSerie);


                ObjSAPComprobante.NumAtCard = DatosSerie[0].Serie + "-" + auxComprobante.Correlativo; //F001-255

            ObjSAPComprobante.UserFields.Fields.Item("U_CON_nrosisgestion").Value = auxComprobante.NumSerieTipoDocumentoRef.ToUpper();

            ObjSAPComprobante.UserFields.Fields.Item("U_CON_OCCompras").Value = auxComprobante.NumOC;

            ObjSAPComprobante.FolioPrefixString = "FT";//auxComprobante.SerieCpe;
            ObjSAPComprobante.FolioNumber = Convert.ToInt32(auxComprobante.Correlativo);

            if(auxComprobante.Inventario==false && auxComprobante.Total > 700)
            {
                    ObjSAPComprobante.UserFields.Fields.Item("U_CON_TASADET").Value = auxComprobante.TasaDetraccion.ToString();
                    ObjSAPComprobante.UserFields.Fields.Item("U_CON_GRUPODET").Value = auxComprobante.GrupoDetraccion.ToString();
                    //ObjSAPComprobante.GroupNumber = auxComprobante.CondicionPagoDet;
                    //ObjSAPComprobante.GroupNumber = 26;
                    if (auxComprobante.CondicionPagoDet > 0)
                    {
                        ObjSAPComprobante.PaymentGroupCode = auxComprobante.CondicionPagoDet;

                    }
                }

            switch (auxComprobante.IdTipoRegistro)
            {
                case 1:
                    ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "P";
                    break;
                case 4:
                    ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "R";
                    GiroDAO oGiroDAO = new GiroDAO();
                    var datosGiro = oGiroDAO.ObtenerGiroxId(auxComprobante.IdSemana, ref errMsg);
                    string NumGiro = datosGiro[0].NombSerie + "-" + datosGiro[0].Correlativo;

                    ObjSAPComprobante.UserFields.Fields.Item("U_EXX_NUMEREND").Value = NumGiro;
                    break;
                case 5:
                    ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "C";
                    break;
                default:
                    break;
            }


            switch (auxComprobante.IdTipoDocumentoRef)
            {
                case 16:
                    double CON_M3 = Convert.ToDouble(auxComprobante.ConsumoM3);
                    ObjSAPComprobante.UserFields.Fields.Item("U_CON_M3").Value = CON_M3;
                    double CON_HW = Convert.ToDouble(auxComprobante.ConsumoHW);
                    ObjSAPComprobante.UserFields.Fields.Item("U_CON_KWH").Value = CON_HW;
                    break;
               
                default:
                    break;
            }


            ObjSAPComprobante.Indicator = "01";
            ObjSAPComprobante.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service;

            //ObjSAPComprobante.DiscountPercent = 0.00;


            foreach (IntegradorV1Detalle auxdetalle in auxComprobante.detalles)
            {


                ObjSAPComprobante.Lines.ItemDescription = auxdetalle.DescripcionArticulo;

                ObjSAPComprobante.Lines.ProjectCode = datosObra[0].Codigo;
                ObjSAPComprobante.Lines.ShipDate = auxComprobante.FechaDocumento;


                ObjSAPComprobante.Lines.Quantity = Convert.ToDouble(auxdetalle.Cantidad);

                ObjSAPComprobante.Lines.UserFields.Fields.Item("U_SMC_CODIGO_ITEM").Value = auxdetalle.CodigoArticulo;

               var cuentaContable = "";


                    if (auxComprobante.IdTipoDocumento == 18)
                    {
                         cuentaContable = oConsultasSQL.ObtenerCuentaContable(datosDivision[0].CuentaContable);
                    }
                    else if (auxComprobante.IdTipoDocumento == 1338)
                    {
                        ObraCatalogoServicioDTO oObraCatalogoServicioDTO = oObraDAO.ObtenerDatosCatalogoServicioxId(auxdetalle.IdArticulo, auxComprobante.IdObra, ref mensaje_error);
                        cuentaContable = oConsultasSQL.ObtenerCuentaContable(oObraCatalogoServicioDTO.CuentaContable);
                    }

                ObjSAPComprobante.Lines.AccountCode = cuentaContable;

                    if (datosProveedor[0].Afecto4ta == true)
                    {
                        ObjSAPComprobante.Lines.WTLiable = SAPbobsCOM.BoYesNoEnum.tYES;
                    }
                    else
                    {
                        ObjSAPComprobante.Lines.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO;
                    }


                    if (auxdetalle.IdIndicadorImpuesto == 1)
                {
                    ObjSAPComprobante.Lines.UnitPrice = (double)auxdetalle.total_valor_item; //sin igv
                    ObjSAPComprobante.Lines.TaxCode = "IGV";//"IGV";
                    ObjSAPComprobante.Lines.PriceAfterVAT = (double)auxdetalle.total_valor_item; // sin igv
                    ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionCode = "IGV";
                    ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionType = 1;
                    ObjSAPComprobante.Lines.TaxJurisdictions.TaxAmount = Convert.ToDouble(auxdetalle.total_igv); //MontoIGV
                    ObjSAPComprobante.Lines.TaxTotal = Convert.ToDouble(auxdetalle.total_item);
                   
                }
                if (auxdetalle.IdIndicadorImpuesto == 2)
                {
                        ObjSAPComprobante.Lines.UnitPrice = (double)auxdetalle.total_valor_item; //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "EXO";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)auxdetalle.total_valor_item; // sin igv
                        //ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionCode = "IGV";
                        //ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionType = 1;
                        //ObjSAPComprobante.Lines.TaxJurisdictions.TaxAmount = Convert.ToDouble(auxdetalle.total_igv); //MontoIGV
                        //ObjSAPComprobante.Lines.TaxTotal = Convert.ToDouble(auxdetalle.total_item);
                        //ObjSAPComprobante.Lines.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO;
                }
                if (auxdetalle.IdIndicadorImpuesto == 3)
                {
                        ObjSAPComprobante.Lines.UnitPrice = (double)auxdetalle.total_valor_item; //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "INA";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)auxdetalle.total_valor_item; // sin igv
                        //ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionCode = "IGV";
                        //ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionType = 1;
                        //ObjSAPComprobante.Lines.TaxJurisdictions.TaxAmount = Convert.ToDouble(auxdetalle.total_igv); //MontoIGV
                        //ObjSAPComprobante.Lines.TaxTotal = Convert.ToDouble(auxdetalle.total_item);
                        //ObjSAPComprobante.Lines.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO;
                }


                    ObjSAPComprobante.Lines.Add();
                }


                try
                {
                    for (int i = 0; i < auxComprobante.AnexoDetalle.Count(); i++)
                    {

                        //string ruta = @"C:\Users\fperez\Documents\ANEXOS\";
                        //string ruta = @"C:\SMC\Fuentes\ProyectoConcyssa\ConcyssaWeb\wwwroot\Anexos\";
                        string ruta = @"C:\SMC\Binario\ProyectoConcyssa\wwwroot\Anexos\";

                        SAPbobsCOM.Attachments2 oAttachment = (SAPbobsCOM.Attachments2)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oAttachments2);


                        if (oAttachment.GetByKey(ObjSAPComprobante.AttachmentEntry))
                        //if (oAttachment.GetByKey(Convert.ToInt32(docentry)))
                        {
                            int s223232adasdasdasdas = oAttachment.Lines.Count;
                            // the document has already attachments 
                            oAttachment.Lines.Add();
                            int sadasdasdasdas = oAttachment.Lines.Count;
                            oAttachment.Lines.SetCurrentLine(oAttachment.Lines.Count - 1);
                            oAttachment.Lines.FileName = auxComprobante.AnexoDetalle[i].NombreArchivo; ;//"OS SADE.pdf";
                            oAttachment.Lines.SourcePath = ruta;
                            oAttachment.Lines.Override = SAPbobsCOM.BoYesNoEnum.tYES;



                            if (oAttachment.Update() != 0)
                            {
                                errMsg = oCompany.GetLastErrorDescription();
                                mensaje_error = errMsg;
                            }
                        }
                        else
                        {
                            oAttachment.Lines.FileName = auxComprobante.AnexoDetalle[i].NombreArchivo; //ARCHIVO.XML
                            oAttachment.Lines.SourcePath = ruta;// RUTA DE CARPETA
                            oAttachment.Lines.Override = SAPbobsCOM.BoYesNoEnum.tYES;
                            if (oAttachment.Add() != 0)
                            {
                                errMsg = oCompany.GetLastErrorDescription();
                                mensaje_error = errMsg;
                                throw new Exception(errMsg);
                            }

                            // END Adjuntar PDF

                            string objKey = oCompany.GetNewObjectKey();
                            oAttachment.GetByKey(Convert.ToInt32(objKey));
                            int absEntry = oAttachment.AbsoluteEntry;
                            ObjSAPComprobante.AttachmentEntry = oAttachment.AbsoluteEntry;
                        }




                    }
                }
                catch (Exception ex)
                {
                   return  mensaje_error = ex.Message.ToString();
                }

                var algo = ObjSAPComprobante.GroupNumber;
                int repuesta = ObjSAPComprobante.Add();
            string ee = "";
            if (repuesta != 0)
            {
                errMsg = oCompany.GetLastErrorDescription();
                int errCode = oCompany.GetLastErrorCode();
                    //DesconectarSAP();
                    mensaje_error = "Error en " + DatosSerie[0].Serie + "-" + auxComprobante.Correlativo + ":"+ errMsg + " Esta Facutra volverá a los registros originales";
                //GC.Collect();
            }
            else
            {
                    IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
                    var docentry = oCompany.GetNewObjectKey();
                    var intdocentry = Convert.ToInt32(docentry);
                    //oIntregadorV1DAO.UpdateDocEntryEnviarSAP(auxComprobante.IdEnviarSap, intdocentry);
                    foreach (IntegradorV1Detalle auxdetalle in auxComprobante.detalles)
                    {
                        oIntregadorV1DAO.UpdateDocEntryEnviarSAPDetalle(auxdetalle.IdEnviarSapDetalle, intdocentry,BorradorFirme);
                        oIntregadorV1DAO.UpdateDocEntryOPCHDetalle(auxdetalle.IdTablaOriginalDetalle, intdocentry,BorradorFirme);
                    }


                    mensaje_error = "Factura "+ DatosSerie[0].Serie + "-" + auxComprobante.Correlativo + " Enviada a SAP con Exito";
                //GC.Collect();
                //DesconectarSAP();
            }
            return mensaje_error;
            }
            catch (Exception e)
            {
                
                mensaje_error = e.Message.ToString();
                return mensaje_error;
            }
            // }
        }

        public string AddComprobante2(IntegradorV1DTO auxComprobante, int BorradorFirme, ref string mensaje_error)
        {
            mensaje_error = "Prueba Vacia";
            return mensaje_error;
        }

        public string AddNotaCredito(IntegradorV1DTO auxComprobante, int BorradorFirme, int SerieNotaCredito, ref string mensaje_error)
        {
            try
            {

                string errMsg = "";
                // int IdEmpresa = int.Parse(HttpContext.Current.Session["IdEmpresa"].ToString());
                //Factura
                SAPbobsCOM.Documents ObjSAPComprobante = null;
                if (BorradorFirme == 1)
                {
                    ObjSAPComprobante = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
                }
                else
                {
                    ObjSAPComprobante = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseCreditNotes);
                }


                //SAPbobsCOM.Documents ObjSAPComprobante = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

                //if (FacturReserva > 0)
                //{
                //    ObjSAPComprobante.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseInvoices;
                //    ObjSAPComprobante.ReserveInvoice = SAPbobsCOM.BoYesNoEnum.tYES;
                //}
                //else
                //{
                ObjSAPComprobante.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseCreditNotes;
                //}


                //ConsultasHanaBL consultas = new ConsultasHanaBL();
                //string Cuenta = consultas.ObtenerCuentaAsociadaProveedores("P" + auxComprobante.NumRucEmisor);
                //if (Cuenta == "")
                //{ }
                //else
                //{

              

                ////ObjSAPComprobante.ControlAccount = cuentaContable;
                ////}
                ObraDAO oObraDAO = new ObraDAO();
                var datosObra = oObraDAO.ObtenerDatosxID(auxComprobante.IdObra, ref errMsg);

                DivisionDAO oDivisionDAO = new DivisionDAO();
                var datosDivision = oDivisionDAO.ObtenerDatosxID(datosObra[0].IdDivision, ref mensaje_error);


                //ObjSAPComprobante.UserFields.Fields.Item("U_EXX_CLABYSADQ").Value = datosDivision[0].IdClasif.ToString();

                ConsultasSQL oConsultasSQL = new ConsultasSQL();
             
            
              
                
                
                ObjSAPComprobante.Project = datosObra[0].Codigo;
                ObjSAPComprobante.Comments = "Prueba Migracion desde IntegradorV1";

                ObjSAPComprobante.DocTotal = Convert.ToDouble(auxComprobante.Total);

                ProveedorDAO oProveedorDAO = new ProveedorDAO();
                var datosProveedor = oProveedorDAO.ObtenerDatosxID(auxComprobante.IdProveedor);


                //ObjSAPComprobante.CardCode = "P" + auxComprobante.NumRucEmisor;
                ObjSAPComprobante.CardCode = "P" + datosProveedor[0].NumeroDocumento;

                ObjSAPComprobante.CardName = datosProveedor[0].RazonSocial;



                //ObjSAPComprobante.GroupNumber = -1;
                ObjSAPComprobante.DocDate = auxComprobante.FechaContabilizacion; //fecha contabilizacion
               
                
                ObjSAPComprobante.Series = SerieNotaCredito;

                

                CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
                var datosCondicionPago = oCondicionPagoDAO.ObtenerDatosxID(auxComprobante.idCondicionPago);
                var fechaVencimiento = auxComprobante.FechaContabilizacion.AddDays(datosCondicionPago[0].Dias);

                ObjSAPComprobante.DocDueDate = fechaVencimiento; //fecha vencimiento



                ObjSAPComprobante.TaxDate = auxComprobante.FechaDocumento; //fecha emision

                ObjSAPComprobante.FederalTaxID = datosProveedor[0].NumeroDocumento; //nro ruc

                //string codTipoDoc = auxComprobante.CodCpe;
                //ObjSAPComprobante.Indicator = codTipoDoc;

                MonedaDAO oMonedaDao = new MonedaDAO();
                var DatosMoneda = oMonedaDao.ObtenerDatosxID(auxComprobante.IdMoneda);


                if (DatosMoneda[0].Codigo == "USD")
                {
                    auxComprobante.CodMoneda = "USD";//oConfiguracionDTO[0].CodMonedaUSD;
                }
                if (DatosMoneda[0].Codigo == "S/")
                {
                    auxComprobante.CodMoneda = "SOL";//oConfiguracionDTO[0].CodMonedaPEN;
                }

                ObjSAPComprobante.DocCurrency = auxComprobante.CodMoneda;

                // ObjSAPComprobante.Series =

                SerieDAO oSerieDAO = new SerieDAO();
                var DatosSerie = oSerieDAO.ObtenerDatosxID(auxComprobante.IdSerie);


                ObjSAPComprobante.NumAtCard = DatosSerie[0].Serie + "-" + auxComprobante.Correlativo; //F001-255

                ObjSAPComprobante.UserFields.Fields.Item("U_CON_nrosisgestion").Value = auxComprobante.NumSerieTipoDocumentoRef.ToUpper();

                ObjSAPComprobante.UserFields.Fields.Item("U_CON_OCCompras").Value = auxComprobante.NumOC;

                ObjSAPComprobante.FolioPrefixString = "NC";//auxComprobante.SerieCpe;
                ObjSAPComprobante.FolioNumber = Convert.ToInt32(auxComprobante.Correlativo);

                //if (auxComprobante.detalles[0].nc)

                switch (auxComprobante.IdTipoRegistro)
                {
                    case 1:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "P";
                        break;
                    case 4:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "R";
                        GiroDAO oGiroDAO = new GiroDAO();
                        var datosGiro = oGiroDAO.ObtenerGiroxId(auxComprobante.IdSemana, ref errMsg);
                        string NumGiro = datosGiro[0].NombSerie + "-" + datosGiro[0].Correlativo;

                        ObjSAPComprobante.UserFields.Fields.Item("U_EXX_NUMEREND").Value = NumGiro;
                        break;
                    case 5:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "C";
                        break;
                    default:
                        break;
                }


                ObjSAPComprobante.Indicator = "01";
                ObjSAPComprobante.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service;

                //ObjSAPComprobante.DiscountPercent = 0.00;

                
                foreach (IntegradorV1Detalle auxdetalle in auxComprobante.detalles)
                {


                    ObjSAPComprobante.Lines.ItemDescription = auxdetalle.DescripcionArticulo;

                    ObjSAPComprobante.Lines.ProjectCode = datosObra[0].Codigo;
                    ObjSAPComprobante.Lines.ShipDate = auxComprobante.FechaDocumento;


                    ObjSAPComprobante.Lines.Quantity = Convert.ToDouble(auxdetalle.Cantidad);

                    ObjSAPComprobante.Lines.UserFields.Fields.Item("U_SMC_CODIGO_ITEM").Value = auxdetalle.CodigoArticulo;


                    ArticuloDAO articuloDAO = new ArticuloDAO();
                    List<ArticuloDTO> lstArticulo = articuloDAO.ObtenerDatosxID(auxdetalle.IdArticulo, ref mensaje_error);
                    var cuentaContable = "";

                    if (lstArticulo[0].Inventario)
                    {
                        cuentaContable = oConsultasSQL.ObtenerCuentaContable(datosDivision[0].CuentaContable);
                    }
                    else
                    {
                        ObraCatalogoServicioDTO oObraCatalogoServicioDTO = oObraDAO.ObtenerDatosCatalogoServicioxId(auxdetalle.IdArticulo, auxComprobante.IdObra, ref mensaje_error);
                        cuentaContable = oConsultasSQL.ObtenerCuentaContable(oObraCatalogoServicioDTO.CuentaContable);
                    }

                    ObjSAPComprobante.Lines.AccountCode = cuentaContable;


                    if(auxdetalle.NCIdOPCHDetalle > 0)
                    {
                        IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
                        var datosFactura =  oIntregadorV1DAO.ObtenerDocEntryFacturaxNotaCredito(auxdetalle.NCIdOPCHDetalle);
                    

                        if (datosFactura[0].EnviadoPor == 2)
                        {

                            var DatosDocEntry = oConsultasSQL.ObtenerDetallexDocEntry(datosFactura[0].DocEntry,auxdetalle.CodigoArticulo,auxdetalle.Cantidad);

                            ObjSAPComprobante.Lines.BaseEntry = datosFactura[0].DocEntry;
                            ObjSAPComprobante.Lines.BaseLine = DatosDocEntry[0].LineNum;
                            ObjSAPComprobante.Lines.BaseType = 18;
                        }

                    }



                    if (auxdetalle.IdIndicadorImpuesto == 1)
                    {
                        ObjSAPComprobante.Lines.UnitPrice = (double)auxdetalle.total_valor_item; //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "IGV";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)auxdetalle.total_valor_item; // sin igv
                        ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionCode = "IGV";
                        ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionType = 1;
                        ObjSAPComprobante.Lines.TaxJurisdictions.TaxAmount = Convert.ToDouble(auxdetalle.total_igv); //MontoIGV
                        ObjSAPComprobante.Lines.TaxTotal = Convert.ToDouble(auxdetalle.total_item);
                        ObjSAPComprobante.Lines.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO;
                    }
                    if (auxdetalle.IdIndicadorImpuesto == 2)
                    {
                        ObjSAPComprobante.Lines.UnitPrice = (double)auxdetalle.total_valor_item; //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "EXO";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)auxdetalle.total_valor_item; // sin igv
                        //ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionCode = "IGV";
                        //ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionType = 1;
                        //ObjSAPComprobante.Lines.TaxJurisdictions.TaxAmount = Convert.ToDouble(auxdetalle.total_igv); //MontoIGV
                        //ObjSAPComprobante.Lines.TaxTotal = Convert.ToDouble(auxdetalle.total_item);
                        //ObjSAPComprobante.Lines.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO;
                    }
                    if ( auxdetalle.IdIndicadorImpuesto == 3)
                    {
                        ObjSAPComprobante.Lines.UnitPrice = (double)auxdetalle.total_valor_item; //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "INA";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)auxdetalle.total_valor_item; // sin igv
                        //ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionCode = "IGV";
                        //ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionType = 1;
                        //ObjSAPComprobante.Lines.TaxJurisdictions.TaxAmount = Convert.ToDouble(auxdetalle.total_igv); //MontoIGV
                        //ObjSAPComprobante.Lines.TaxTotal = Convert.ToDouble(auxdetalle.total_item);
                        //ObjSAPComprobante.Lines.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO;
                    }



                    ObjSAPComprobante.Lines.Add();
                }


                try
                {
                    for (int i = 0; i < auxComprobante.AnexoDetalle.Count(); i++)
                    {

                        //string ruta = @"C:\Users\fperez\Documents\ANEXOS\";
                        //string ruta = @"C:\SMC\Fuentes\ProyectoConcyssa\ConcyssaWeb\wwwroot\Anexos\";
                        string ruta = @"C:\SMC\Binario\ProyectoConcyssa\wwwroot\Anexos\";

                        SAPbobsCOM.Attachments2 oAttachment = (SAPbobsCOM.Attachments2)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oAttachments2);


                        if (oAttachment.GetByKey(ObjSAPComprobante.AttachmentEntry))
                        //if (oAttachment.GetByKey(Convert.ToInt32(docentry)))
                        {
                            int s223232adasdasdasdas = oAttachment.Lines.Count;
                            // the document has already attachments 
                            oAttachment.Lines.Add();
                            int sadasdasdasdas = oAttachment.Lines.Count;
                            oAttachment.Lines.SetCurrentLine(oAttachment.Lines.Count - 1);
                            oAttachment.Lines.FileName = auxComprobante.AnexoDetalle[i].NombreArchivo; ;//"OS SADE.pdf";
                            oAttachment.Lines.SourcePath = ruta;
                            oAttachment.Lines.Override = SAPbobsCOM.BoYesNoEnum.tYES;



                            if (oAttachment.Update() != 0)
                            {
                                errMsg = oCompany.GetLastErrorDescription();
                                mensaje_error = errMsg;
                            }
                        }
                        else
                        {
                            oAttachment.Lines.FileName = auxComprobante.AnexoDetalle[i].NombreArchivo; //ARCHIVO.XML
                            oAttachment.Lines.SourcePath = ruta;// RUTA DE CARPETA
                            oAttachment.Lines.Override = SAPbobsCOM.BoYesNoEnum.tYES;
                            if (oAttachment.Add() != 0)
                            {
                                errMsg = oCompany.GetLastErrorDescription();
                                mensaje_error = errMsg;
                                throw new Exception(errMsg);
                            }

                            // END Adjuntar PDF

                            string objKey = oCompany.GetNewObjectKey();
                            oAttachment.GetByKey(Convert.ToInt32(objKey));
                            int absEntry = oAttachment.AbsoluteEntry;
                            ObjSAPComprobante.AttachmentEntry = oAttachment.AbsoluteEntry;
                        }




                    }
                }
                catch (Exception ex)
                {
                    return mensaje_error = ex.Message.ToString();
                }


                int repuesta = ObjSAPComprobante.Add();
                string ee = "";
                if (repuesta != 0)
                {
                    errMsg = oCompany.GetLastErrorDescription();
                    int errCode = oCompany.GetLastErrorCode();
                    //DesconectarSAP();
                    mensaje_error = "Error en " + DatosSerie[0].Serie + "-" + auxComprobante.Correlativo + ":" + errMsg + " Esta Nota de Crédito volverá a los registros originales";
                    //GC.Collect();
                }
                else
                {
                    IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
                    var docentry = oCompany.GetNewObjectKey();
                    var intdocentry = Convert.ToInt32(docentry);
                    //oIntregadorV1DAO.UpdateDocEntryEnviarSAP(auxComprobante.IdEnviarSap, intdocentry);
                    foreach (IntegradorV1Detalle auxdetalle in auxComprobante.detalles)
                    {                 
                        oIntregadorV1DAO.UpdateDocEntryORPCDetalle(auxdetalle.IdTablaOriginalDetalle, intdocentry, BorradorFirme);
                    }
                    //Convert.ToInt32(oCompany.GetNewObjectKey);
                    mensaje_error = "Nota de Credito " + DatosSerie[0].Serie + "-" + auxComprobante.Correlativo + " Enviada a SAP con Exito";
                    //GC.Collect();
                    //DesconectarSAP();
                }
                return mensaje_error;
            }
            catch (Exception e)
            {

                mensaje_error = e.Message.ToString();
                return mensaje_error;
            }
            // }
        }

    }
}
