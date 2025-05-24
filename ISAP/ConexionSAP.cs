using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
using Microsoft.AspNetCore.Http;
using SAPbobsCOM;

namespace ISAP
{
    public class ConexionSAP
    {
        static SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();

        public static string Conectar()
        {
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();

            string BaseDatos = httpContextAccessor.HttpContext.Session.GetString("BaseDatos").ToString();

            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            string mensaje_error = "error";
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);

            UsuarioDAO usuarioDAO = new UsuarioDAO();

            var oUsuarioDTO = usuarioDAO.ObtenerDatosxID(Convert.ToInt32(httpContextAccessor.HttpContext.Session.GetInt32("IdUsuario")), BaseDatos, ref mensaje_error);

            string SapServer = "SGCWEB";

            string SapCompanyDB = configuracion[0].NombreBDSAP;
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
                var resultadoConexion = oCompany.Connected;
                if (oCompany != null && resultadoConexion)
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





        }

        public static bool DesconectarSAP()
        {
            oCompany.Disconnect();
            return true;
        }

        public string AddComprobante(IntegradorV1DTO auxComprobante, int BorradorFirme, int SerieFactura, string BaseDatos,string BaseDatosSAP, ref string mensaje_error)
        {
            try
            {

                string errMsg = "";

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



                ObjSAPComprobante.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseInvoices;

                ConsultasSQL oConsultasSQL = new ConsultasSQL();

                ObraDAO oObraDAO = new ObraDAO();
                var datosObra = oObraDAO.ObtenerDatosxID(auxComprobante.IdObra, BaseDatos, ref errMsg);

                DivisionDAO oDivisionDAO = new DivisionDAO();
                var datosDivision = oDivisionDAO.ObtenerDatosxID(datosObra[0].IdDivision, BaseDatos, ref mensaje_error);



                //ObjSAPComprobante.UserFields.Fields.Item("U_EXX_CLABYSADQ").Value = datosDivision[0].IdClasif.ToString();

                ObjSAPComprobante.Project = datosObra[0].CodProyecto;


                TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
                List<TipoRegistroDTO> lstTipoRegistroDTO = oTipoRegistroDAO.ObtenerDatosxID(auxComprobante.IdTipoRegistro, BaseDatos, ref mensaje_error);






                ProveedorDAO oProveedorDAO = new ProveedorDAO();
                var datosProveedor = oProveedorDAO.ObtenerDatosxID(auxComprobante.IdProveedor, BaseDatos);

                ObjSAPComprobante.CardCode = "P" + datosProveedor[0].NumeroDocumento;


                IntregadorV1DAO IntDao = new IntregadorV1DAO();
                IntegradorProveedorDTO datosProveedorSAP = IntDao.ObtenerDatosProveedorSAP("P" + datosProveedor[0].NumeroDocumento, BaseDatosSAP, ref mensaje_error);


                if (auxComprobante.IdTipoRegistro == 5)
                {
                    datosProveedor[0].Afecto4ta = false;
                }
                else
                {
                    if (datosProveedorSAP.WTLiable == "Y")
                    {
                        datosProveedor[0].Afecto4ta = true;
                    }
                    else
                    {
                        datosProveedor[0].Afecto4ta = false;
                    }
                }

                ObjSAPComprobante.CardName = datosProveedorSAP.CardName;
                ObjSAPComprobante.GroupNumber = datosProveedorSAP.GroupNum;
                ObjSAPComprobante.PaymentGroupCode = datosProveedorSAP.GroupNum;

                ObjSAPComprobante.DocDate = auxComprobante.FechaContabilizacion; //fecha contabilizacion


                ObjSAPComprobante.Series = SerieFactura;


                if (auxComprobante.IdTipoDocumento == 18)
                {
                    ObjSAPComprobante.UserFields.Fields.Item("U_EXX_CLABYSADQ").Value = "1";
                }
                else
                {
                    ObjSAPComprobante.UserFields.Fields.Item("U_EXX_CLABYSADQ").Value = "5";
                }

                CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
                var datosCondicionPago = oCondicionPagoDAO.ObtenerDatosxID(auxComprobante.idCondicionPago, BaseDatos);
                var fechaVencimiento = auxComprobante.FechaContabilizacion.AddDays(datosCondicionPago[0].Dias);

                // ObjSAPComprobante.DocDueDate = fechaVencimiento; //fecha vencimiento



                ObjSAPComprobante.TaxDate = auxComprobante.FechaDocumento; //fecha emision

                ObjSAPComprobante.FederalTaxID = datosProveedor[0].NumeroDocumento; //nro ruc



                MonedaDAO oMonedaDao = new MonedaDAO();
                var DatosMoneda = oMonedaDao.ObtenerDatosxID(auxComprobante.IdMoneda, BaseDatos);


                if (DatosMoneda[0].Codigo == "USD")
                {
                    auxComprobante.CodMoneda = "USD";//oConfiguracionDTO[0].CodMonedaUSD;
                }
                if (DatosMoneda[0].Codigo == "S/")
                {
                    auxComprobante.CodMoneda = "SOL";//oConfiguracionDTO[0].CodMonedaPEN;
                }

                ObjSAPComprobante.DocCurrency = auxComprobante.CodMoneda;



                SerieDAO oSerieDAO = new SerieDAO();
                var DatosSerie = oSerieDAO.ObtenerDatosxID(auxComprobante.IdSerie, BaseDatos);

                string SerieRef = auxComprobante.NumSerieTipoDocumentoRef.ToUpper();

                //ObjSAPComprobante.NumAtCard = DatosSerie[0].Serie + "-" + auxComprobante.Correlativo; //F001-255
                ObjSAPComprobante.NumAtCard = SerieRef; //F001-255

                //ObjSAPComprobante.UserFields.Fields.Item("U_CON_nrosisgestion").Value = auxComprobante.NumSerieTipoDocumentoRef.ToUpper();
                ObjSAPComprobante.UserFields.Fields.Item("U_CON_nrosisgestion").Value = DatosSerie[0].Serie + "-" + auxComprobante.Correlativo;

                ObjSAPComprobante.UserFields.Fields.Item("U_CON_OCCompras").Value = auxComprobante.NumOC;
                ObjSAPComprobante.UserFields.Fields.Item("U_CON_PDFFACTURA").Value = "http://192.168.0.201/factdocs/?ruc="+ datosProveedor[0].NumeroDocumento + "&factura="+ SerieRef;

                // ObjSAPComprobante.FolioPrefixString = "FT";//auxComprobante.SerieCpe;
                //auxComprobante.SerieCpe;

                // ObjSAPComprobante.FolioNumber = Convert.ToInt32(auxComprobante.Correlativo);
                try
                {
                    ObjSAPComprobante.FolioNumber = int.Parse(SerieRef.Split("-")[1]);
                }
                catch (Exception e)
                {
                    ObjSAPComprobante.FolioNumber = int.Parse(SerieRef);
                }

                if (auxComprobante.Inventario == false && auxComprobante.Total > 700)
                {
                    ObjSAPComprobante.UserFields.Fields.Item("U_CON_TASADET").Value = auxComprobante.TasaDetraccion.ToString();
                    ObjSAPComprobante.UserFields.Fields.Item("U_CON_GRUPODET").Value = auxComprobante.GrupoDetraccion.ToString();
                    datosProveedor[0].Afecto4ta = false;
                    //if (auxComprobante.CondicionPagoDet > 0)
                    //{
                    //    ObjSAPComprobante.PaymentGroupCode = auxComprobante.CondicionPagoDet;

                    //}
                }

                string NumeroSemana = "0";
                if (auxComprobante.IdTipoRegistro == 4)
                {
                    GiroDAO oGiroDAO = new GiroDAO();
                    List<GiroDTO> giros = oGiroDAO.ObtenerGiroxId(auxComprobante.IdSemana, BaseDatos, ref mensaje_error);
                    ObjSAPComprobante.Comments = lstTipoRegistroDTO[0].NombTipoRegistro + " - Giro " + giros[0].NombSerie + "-" + giros[0].Correlativo + " - " + datosObra[0].Descripcion;
                }
                else
                {
                    SemanaDAO semanaDAO = new SemanaDAO();
                    List<SemanaDTO> semanas = semanaDAO.ObtenerDatosxID(auxComprobante.IdSemana, BaseDatos, ref mensaje_error);
                    ObjSAPComprobante.Comments = lstTipoRegistroDTO[0].NombTipoRegistro + " - Semana " + semanas[0].NumSemana + " - " + datosObra[0].Descripcion;
                    NumeroSemana = semanas[0].NumSemana.ToString().PadLeft(2, '0');
                }


                switch (auxComprobante.IdTipoRegistro)
                {
                    case 1:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "P";
                        ObjSAPComprobante.UserFields.Fields.Item("U_EXX_NUMEREND").Value = datosObra[0].CodProyecto + "P" + NumeroSemana;
                        break;
                    case 4:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "R";
                        GiroDAO oGiroDAO = new GiroDAO();
                        var datosGiro = oGiroDAO.ObtenerGiroxId(auxComprobante.IdSemana, BaseDatos, ref errMsg);
                        string NumGiro = datosGiro[0].NombSerie + "-" + datosGiro[0].Correlativo;

                        ObjSAPComprobante.UserFields.Fields.Item("U_EXX_NUMEREND").Value = NumGiro;
                        break;
                    case 5:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "C";
                        ObjSAPComprobante.UserFields.Fields.Item("U_EXX_NUMEREND").Value = datosObra[0].CodProyecto + "C" + NumeroSemana;
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

                ObjSAPComprobante.JournalMemo = auxComprobante.detalles[0].DescripcionArticulo;
                ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
                List<ConfiguracionSociedadDTO> configuracionSociedad = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
                string CuentaAsocFT = oConsultasSQL.ObtenerCuentaContable(configuracionSociedad[0].ctaAsocFT, BaseDatosSAP);
                ObjSAPComprobante.ControlAccount = CuentaAsocFT;


                TiposDocumentosDAO oTiposDocumentosDAO = new TiposDocumentosDAO();
                List<TiposDocumentosDTO> lstTiposDocumentosDTO = oTiposDocumentosDAO.ObtenerDatosxID(auxComprobante.IdTipoDocumentoRef, BaseDatos, ref mensaje_error);

                if (lstTiposDocumentosDTO[0].CodeSAP == "0" || lstTiposDocumentosDTO[0].CodeSAP == null)
                {
                    ObjSAPComprobante.Indicator = "01";
                    ObjSAPComprobante.FolioPrefixString = "FP";
                }
                else
                {
                    ObjSAPComprobante.Indicator = lstTiposDocumentosDTO[0].CodeSAP;
                    ObjSAPComprobante.FolioPrefixString = lstTiposDocumentosDTO[0].PrefijoSAP;

                }

                decimal SUMASubTotal = 0;
                decimal SUMAIGV = 0;
                decimal SUMATotal = 0;


                foreach (IntegradorV1Detalle auxdetalle in auxComprobante.detalles)
                {


                    ObjSAPComprobante.Lines.ItemDescription = auxdetalle.DescripcionArticulo;

                    ObjSAPComprobante.Lines.ProjectCode = datosObra[0].CodProyecto;
                    ObjSAPComprobante.Lines.ShipDate = auxComprobante.FechaDocumento;


                    ObjSAPComprobante.Lines.Quantity = Convert.ToDouble(auxdetalle.Cantidad);

                    ObjSAPComprobante.Lines.UserFields.Fields.Item("U_SMC_CODIGO_ITEM").Value = auxdetalle.CodigoArticulo;

                    var cuentaContable = "";

                    if (auxComprobante.IdTipoDocumento == 18)
                    {
                        cuentaContable = oConsultasSQL.ObtenerCuentaContable(datosDivision[0].CuentaContable, BaseDatosSAP);
                    }
                    else if (auxComprobante.IdTipoDocumento == 1338)
                    {
                        ObraCatalogoServicioDTO oObraCatalogoServicioDTO = oObraDAO.ObtenerDatosCatalogoServicioxId(auxdetalle.IdArticulo, auxComprobante.IdObra, BaseDatos, ref mensaje_error);
                        cuentaContable = oConsultasSQL.ObtenerCuentaContable(oObraCatalogoServicioDTO.CuentaContable, BaseDatosSAP);
                        if (oObraCatalogoServicioDTO.CuentaContable == "6343403-05-96")
                        {
                            datosProveedor[0].Afecto4ta = true;
                        }
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
                        ObjSAPComprobante.Lines.UnitPrice = (double)Math.Round(auxdetalle.total_valor_item, 2); //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "IGV";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)Math.Round(auxdetalle.total_valor_item, 2); // sin igv
                        ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionCode = "IGV";
                        ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionType = 1;
                        ObjSAPComprobante.Lines.TaxJurisdictions.TaxAmount = Convert.ToDouble(auxdetalle.total_valor_item * auxdetalle.porcentaje_igv); //MontoIGV
                        ObjSAPComprobante.Lines.TaxTotal = Convert.ToDouble(Math.Round(auxdetalle.total_item, 2));

                        SUMASubTotal += Math.Round(auxdetalle.total_valor_item, 2);
                        SUMAIGV += auxdetalle.total_valor_item * auxdetalle.porcentaje_igv;
                        SUMATotal += Math.Round(auxdetalle.total_item, 2);

                    }
                    if (auxdetalle.IdIndicadorImpuesto == 2)
                    {
                        if (auxComprobante.IdTipoDocumentoRef == 11 && datosProveedor[0].Afecto4ta == true)
                        {
                            ObjSAPComprobante.Lines.UnitPrice = (double)Math.Round(auxdetalle.total_valor_item, 2); //sin igv
                            ObjSAPComprobante.Lines.TaxCode = "EXO";//"IGV";
                            //ObjSAPComprobante.Lines.PriceAfterVAT = (double)Math.Round(auxdetalle.total_valor_item * 8/100, 2); // sin igv
                            SUMASubTotal += auxdetalle.total_valor_item - (auxdetalle.total_valor_item * 8 / 100);
                            SUMATotal += auxdetalle.total_item - (auxdetalle.total_item * 8 / 100);
                        }
                        else
                        {
                            ObjSAPComprobante.Lines.UnitPrice = (double)Math.Round(auxdetalle.total_valor_item, 2); //sin igv
                            ObjSAPComprobante.Lines.TaxCode = "EXO";//"IGV";
                            ObjSAPComprobante.Lines.PriceAfterVAT = (double)Math.Round(auxdetalle.total_valor_item, 2); // sin igv
                            SUMASubTotal += auxdetalle.total_valor_item;
                            SUMATotal += auxdetalle.total_item;

                        }
                    }
                    if (auxdetalle.IdIndicadorImpuesto == 3)
                    {
                        ObjSAPComprobante.Lines.UnitPrice = (double)Math.Round(auxdetalle.total_valor_item, 2); //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "INA";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)Math.Round(auxdetalle.total_valor_item, 2); // sin igv
                        SUMASubTotal += auxdetalle.total_valor_item;
                        SUMATotal += auxdetalle.total_item;
                    }
                    if (auxdetalle.IdIndicadorImpuesto == 4)
                    {
                        ObjSAPComprobante.Lines.UnitPrice = (double)Math.Round(auxdetalle.total_valor_item, 2); //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "IGV_RHA";//"IGV";
                        //ObjSAPComprobante.Lines.PriceAfterVAT = (double)Math.Round(auxdetalle.total_valor_item, 2); // sin igv
                        SUMASubTotal += auxdetalle.total_valor_item;
                        SUMAIGV += auxdetalle.total_valor_item * auxdetalle.porcentaje_igv;
                        SUMATotal += auxdetalle.total_item;
                    }


                    ObjSAPComprobante.Lines.Add();
                }


                decimal IGVRedondado = Math.Round(SUMAIGV, 2, MidpointRounding.AwayFromZero);


                decimal Monto2Decimales = auxComprobante.SubTotal + auxComprobante.Impuesto;
                decimal MontoDecimalesCompletos = SUMASubTotal + IGVRedondado;

                if (Monto2Decimales != MontoDecimalesCompletos)
                {
                    //VALIDACION MONTOS         
                    if (auxComprobante.IdTipoDocumentoRef == 11 && datosProveedor[0].Afecto4ta == true)
                    {
                        ObjSAPComprobante.DiscountPercent = 0;
                    }
                    else
                    {
                        //ObjSAPComprobante.DocTotal = Convert.ToDouble(auxComprobante.Total);
                        auxComprobante.Redondeo += Monto2Decimales - MontoDecimalesCompletos;


                    }
                }

                var a = ObjSAPComprobante.Rounding;
                var b = ObjSAPComprobante.RoundingDiffAmount;

                if (auxComprobante.Redondeo != 0)
                {
                    ObjSAPComprobante.Rounding = SAPbobsCOM.BoYesNoEnum.tYES;
                    ObjSAPComprobante.RoundingDiffAmount = (double)auxComprobante.Redondeo;

                }

                if (auxComprobante.PorcDet > 0)
                {
                    //ObjSAPComprobante.Rounding = SAPbobsCOM.BoYesNoEnum.tNO;
                    //ObjSAPComprobante.RoundingDiffAmount = 0;
                    //ObjSAPComprobante.DocTotal = Convert.ToDouble(MontoDecimalesCompletos);


                    string FormatoDet = "DT" + Math.Round(auxComprobante.PorcDet).ToString().PadLeft(2, '0');

                    string CondicionPagoDet = oConsultasSQL.ObtenerCondicionPagoDET(FormatoDet, "P" + datosProveedor[0].NumeroDocumento, BaseDatosSAP);
                    string GrupoDet = oConsultasSQL.ObtenerGrupoDetDesdeSGC(auxComprobante.TipoDet, BaseDatosSAP);

                    switch (FormatoDet)
                    {
                        case "DT1.5":
                            ObjSAPComprobante.UserFields.Fields.Item("U_CON_TASADET").Value = "1";
                            break;
                        case "DT04":
                            ObjSAPComprobante.UserFields.Fields.Item("U_CON_TASADET").Value = "2";
                            break;
                        case "DT10":
                            ObjSAPComprobante.UserFields.Fields.Item("U_CON_TASADET").Value = "3";
                            break;
                        case "DT12":
                            ObjSAPComprobante.UserFields.Fields.Item("U_CON_TASADET").Value = "4";
                            break;
                        case "DT15":
                            ObjSAPComprobante.UserFields.Fields.Item("U_CON_TASADET").Value = "5";
                            break;

                    }

                    ObjSAPComprobante.UserFields.Fields.Item("U_CON_GRUPODET").Value = GrupoDet;


                    ObjSAPComprobante.PaymentGroupCode = int.Parse(CondicionPagoDet);

                }
                


                //REDONDEO

                //ANEXOS
                //try
                //{
                //    for (int i = 0; i < auxComprobante.AnexoDetalle.Count(); i++)
                //    {
                //        string ruta = @"C:\SMC\Binario\ProyectoConcyssa\wwwroot\Anexos\";

                //        SAPbobsCOM.Attachments2 oAttachment = (SAPbobsCOM.Attachments2)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oAttachments2);


                //        if (oAttachment.GetByKey(ObjSAPComprobante.AttachmentEntry))
                //        //if (oAttachment.GetByKey(Convert.ToInt32(docentry)))
                //        {
                //            int s223232adasdasdasdas = oAttachment.Lines.Count;
                //            // the document has already attachments 
                //            oAttachment.Lines.Add();
                //            int sadasdasdasdas = oAttachment.Lines.Count;
                //            oAttachment.Lines.SetCurrentLine(oAttachment.Lines.Count - 1);
                //            oAttachment.Lines.FileName = auxComprobante.AnexoDetalle[i].NombreArchivo; ;//"OS SADE.pdf";
                //            oAttachment.Lines.SourcePath = ruta;
                //            oAttachment.Lines.Override = SAPbobsCOM.BoYesNoEnum.tYES;



                //            if (oAttachment.Update() != 0)
                //            {
                //                errMsg = oCompany.GetLastErrorDescription();
                //                mensaje_error = errMsg;
                //            }
                //        }
                //        else
                //        {
                //            oAttachment.Lines.FileName = auxComprobante.AnexoDetalle[i].NombreArchivo; //ARCHIVO.XML
                //            oAttachment.Lines.SourcePath = ruta;// RUTA DE CARPETA
                //            oAttachment.Lines.Override = SAPbobsCOM.BoYesNoEnum.tYES;
                //            if (oAttachment.Add() != 0)
                //            {
                //                errMsg = oCompany.GetLastErrorDescription();
                //                mensaje_error = errMsg;
                //                throw new Exception(errMsg);
                //            }

                //            // END Adjuntar PDF

                //            string objKey = oCompany.GetNewObjectKey();
                //            oAttachment.GetByKey(Convert.ToInt32(objKey));
                //            int absEntry = oAttachment.AbsoluteEntry;
                //            ObjSAPComprobante.AttachmentEntry = oAttachment.AbsoluteEntry;
                //        }




                //    }
                //}
                //catch (Exception ex)
                //{
                //    return mensaje_error = ex.Message.ToString();
                //}


                var algo = ObjSAPComprobante.GroupNumber;
                int repuesta = ObjSAPComprobante.Add();
                string ee = "";
                if (repuesta != 0)
                {
                    errMsg = oCompany.GetLastErrorDescription();
                    int errCode = oCompany.GetLastErrorCode();
                    //DesconectarSAP();
                    mensaje_error = "Error en " + DatosSerie[0].Serie + "-" + auxComprobante.Correlativo + ":" + errMsg + " Esta Facutra volverá a los registros originales";
                    //ERROR 10000047 - La fecha difiere de ámbito permitido (VALIDAR FECHA DE VENCIMIENTO)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjSAPComprobante);
                    GC.Collect();
                }
                else
                {

                    
                    IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
                    var docentry = oCompany.GetNewObjectKey();
                    var intdocentry = Convert.ToInt32(docentry);

                    if (BorradorFirme != 1)
                    {
                        SAPbobsCOM.Recordset recordset = null;
                        recordset = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        recordset.DoQuery("Select TransId FROM OPCH WHERE DOCENTRY = " + intdocentry);

                        SAPbobsCOM.JournalEntries Journal = null;
                        Journal = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);

                        Journal.GetByKey(Convert.ToInt32(recordset.Fields.Item(0).Value.ToString()));

                        Journal.Reference3 = auxComprobante.detalles[0].CodigoArticulo;
                        Journal.Update();

                    }
                    //else
                    //{
                    //    SAPbobsCOM.Recordset recordset = null;
                    //    recordset = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    //    recordset.DoQuery("Select TransId FROM ODRF WHERE DOCENTRY = " + intdocentry);

                    //    SAPbobsCOM.JournalEntries Journal = null;
                    //    Journal = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);

                    //    Journal.GetByKey(Convert.ToInt32(recordset.Fields.Item(0).Value.ToString()));

                    //    Journal.Reference2 = "TEST ENVIO BORRADOR";
                    //    Journal.Reference3 = auxComprobante.detalles[0].CodigoArticulo;
                    //    Journal.Update();
                    //}
                    

                    //oIntregadorV1DAO.UpdateDocEntryEnviarSAP(auxComprobante.IdEnviarSap, intdocentry);
                    foreach (IntegradorV1Detalle auxdetalle in auxComprobante.detalles)
                    {
                        oIntregadorV1DAO.UpdateDocEntryEnviarSAPDetalle(auxdetalle.IdEnviarSapDetalle, intdocentry, BorradorFirme, BaseDatos);
                        oIntregadorV1DAO.UpdateDocEntryOPCHDetalle(auxdetalle.IdTablaOriginalDetalle, intdocentry, BorradorFirme, BaseDatos);
                    }


                    mensaje_error = "Factura " + DatosSerie[0].Serie + "-" + auxComprobante.Correlativo + " Enviada a SAP con Exito";
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjSAPComprobante);
                    GC.Collect();
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

        public string AddNotaCredito(IntegradorV1DTO auxComprobante, int BorradorFirme, int SerieNotaCredito, string BaseDatos,string BaseDatosSAP, ref string mensaje_error)
        {
            try
            {

                string errMsg = "";

                SAPbobsCOM.Documents ObjSAPComprobante = null;
                if (BorradorFirme == 1)
                {
                    ObjSAPComprobante = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
                }
                else
                {
                    ObjSAPComprobante = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseCreditNotes);
                }

                ObjSAPComprobante.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseCreditNotes;

                ObraDAO oObraDAO = new ObraDAO();
                var datosObra = oObraDAO.ObtenerDatosxID(auxComprobante.IdObra, BaseDatos, ref errMsg);

                DivisionDAO oDivisionDAO = new DivisionDAO();
                var datosDivision = oDivisionDAO.ObtenerDatosxID(datosObra[0].IdDivision, BaseDatos, ref mensaje_error);



                if(auxComprobante.IdTipoDocumento == 18)
                {
                    ObjSAPComprobante.UserFields.Fields.Item("U_EXX_CLABYSADQ").Value = "1";
                }
                else
                {
                    ObjSAPComprobante.UserFields.Fields.Item("U_EXX_CLABYSADQ").Value = "5";
                }

                ConsultasSQL oConsultasSQL = new ConsultasSQL();





                ObjSAPComprobante.Project = datosObra[0].CodProyecto;
                TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
                List<TipoRegistroDTO> lstTipoRegistroDTO = oTipoRegistroDAO.ObtenerDatosxID(auxComprobante.IdTipoRegistro, BaseDatos, ref mensaje_error);

              

                //ObjSAPComprobante.DocTotal = Convert.ToDouble(auxComprobante.Total);

                ProveedorDAO oProveedorDAO = new ProveedorDAO();
                var datosProveedor = oProveedorDAO.ObtenerDatosxID(auxComprobante.IdProveedor, BaseDatos);


                //ObjSAPComprobante.CardCode = "P" + auxComprobante.NumRucEmisor;
                ObjSAPComprobante.CardCode = "P" + datosProveedor[0].NumeroDocumento;

      

                IntregadorV1DAO IntDao = new IntregadorV1DAO();
                IntegradorProveedorDTO datosProveedorSAP = IntDao.ObtenerDatosProveedorSAP("P" + datosProveedor[0].NumeroDocumento, BaseDatosSAP, ref mensaje_error);


            
                ObjSAPComprobante.CardName = datosProveedorSAP.CardName;
                ObjSAPComprobante.GroupNumber = datosProveedorSAP.GroupNum;

                //ObjSAPComprobante.GroupNumber = -1;
                ObjSAPComprobante.DocDate = auxComprobante.FechaContabilizacion; //fecha contabilizacion


                ObjSAPComprobante.Series = SerieNotaCredito;



                CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
                var datosCondicionPago = oCondicionPagoDAO.ObtenerDatosxID(auxComprobante.idCondicionPago, BaseDatos);
                var fechaVencimiento = auxComprobante.FechaContabilizacion.AddDays(datosCondicionPago[0].Dias);

                //ObjSAPComprobante.DocDueDate = fechaVencimiento; //fecha vencimiento

                ObjSAPComprobante.TaxDate = auxComprobante.FechaDocumento; //fecha emision

                ObjSAPComprobante.FederalTaxID = datosProveedor[0].NumeroDocumento; //nro ruc

                MonedaDAO oMonedaDao = new MonedaDAO();
                var DatosMoneda = oMonedaDao.ObtenerDatosxID(auxComprobante.IdMoneda, BaseDatos);


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
                var DatosSerie = oSerieDAO.ObtenerDatosxID(auxComprobante.IdSerie, BaseDatos);


                string SerieRef = auxComprobante.NumSerieTipoDocumentoRef.ToUpper();

                //ObjSAPComprobante.NumAtCard = DatosSerie[0].Serie + "-" + auxComprobante.Correlativo; //F001-255
                ObjSAPComprobante.NumAtCard = SerieRef; //F001-255

                //ObjSAPComprobante.UserFields.Fields.Item("U_CON_nrosisgestion").Value = auxComprobante.NumSerieTipoDocumentoRef.ToUpper();
                ObjSAPComprobante.UserFields.Fields.Item("U_CON_nrosisgestion").Value = DatosSerie[0].Serie + "-" + auxComprobante.Correlativo;

                ObjSAPComprobante.UserFields.Fields.Item("U_CON_OCCompras").Value = auxComprobante.NumOC;

                // ObjSAPComprobante.FolioPrefixString = "FT";//auxComprobante.SerieCpe;

                // ObjSAPComprobante.FolioNumber = Convert.ToInt32(auxComprobante.Correlativo);
                try
                {
                    ObjSAPComprobante.FolioNumber = int.Parse(SerieRef.Split("-")[1]);
                }
                catch (Exception e)
                {
                    ObjSAPComprobante.FolioNumber = int.Parse(SerieRef);
                }


                //if (auxComprobante.detalles[0].nc)




                switch (auxComprobante.IdTipoRegistro)
                {
                    case 1:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "P";
                        break;
                    case 4:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "R";
                        GiroDAO oGiroDAO = new GiroDAO();
                        var datosGiro = oGiroDAO.ObtenerGiroxId(auxComprobante.IdSemana, BaseDatos, ref errMsg);
                        string NumGiro = datosGiro[0].NombSerie + "-" + datosGiro[0].Correlativo;

                        ObjSAPComprobante.UserFields.Fields.Item("U_EXX_NUMEREND").Value = NumGiro;
                        break;
                    case 5:
                        ObjSAPComprobante.UserFields.Fields.Item("U_beas_type").Value = "C";
                        break;
                    default:
                        break;
                }
                ObjSAPComprobante.JournalMemo = auxComprobante.detalles[0].DescripcionArticulo;
                ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
                List<ConfiguracionSociedadDTO> configuracionSociedad = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
                string CuentaAsocNC = oConsultasSQL.ObtenerCuentaContable(configuracionSociedad[0].ctaAsocNC, BaseDatosSAP);
                ObjSAPComprobante.ControlAccount = CuentaAsocNC;


                ObjSAPComprobante.Indicator = "01";
                ObjSAPComprobante.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service;

                //ObjSAPComprobante.DiscountPercent = 0.00;

                TiposDocumentosDAO oTiposDocumentosDAO = new TiposDocumentosDAO();
                List<TiposDocumentosDTO> lstTiposDocumentosDTO = oTiposDocumentosDAO.ObtenerDatosxID(auxComprobante.IdTipoDocumentoRef, BaseDatos, ref mensaje_error);

                if (lstTiposDocumentosDTO[0].CodeSAP == "0" || lstTiposDocumentosDTO[0].CodeSAP == null)
                {
                    ObjSAPComprobante.Indicator = "07";
                    ObjSAPComprobante.FolioPrefixString = "NC";
                }
                else
                {
                    ObjSAPComprobante.Indicator = lstTiposDocumentosDTO[0].CodeSAP;
                    ObjSAPComprobante.FolioPrefixString = lstTiposDocumentosDTO[0].PrefijoSAP;

                }
                foreach (IntegradorV1Detalle auxdetalle in auxComprobante.detalles)
                {


                    ObjSAPComprobante.Lines.ItemDescription = auxdetalle.DescripcionArticulo;

                    ObjSAPComprobante.Lines.ProjectCode = datosObra[0].CodProyecto;
                    ObjSAPComprobante.Lines.ShipDate = auxComprobante.FechaDocumento;


                    ObjSAPComprobante.Lines.Quantity = Convert.ToDouble(auxdetalle.Cantidad);

                    ObjSAPComprobante.Lines.UserFields.Fields.Item("U_SMC_CODIGO_ITEM").Value = auxdetalle.CodigoArticulo;


                    ArticuloDAO articuloDAO = new ArticuloDAO();
                    List<ArticuloDTO> lstArticulo = articuloDAO.ObtenerDatosxID(auxdetalle.IdArticulo, BaseDatos, ref mensaje_error);
                    var cuentaContable = "";

                    if (lstArticulo[0].Inventario)
                    {
                        cuentaContable = oConsultasSQL.ObtenerCuentaContable(datosDivision[0].CuentaContable, BaseDatosSAP);
                    }
                    else
                    {
                        ObraCatalogoServicioDTO oObraCatalogoServicioDTO = oObraDAO.ObtenerDatosCatalogoServicioxId(auxdetalle.IdArticulo, auxComprobante.IdObra, BaseDatos, ref mensaje_error);
                        cuentaContable = oConsultasSQL.ObtenerCuentaContable(oObraCatalogoServicioDTO.CuentaContable, BaseDatosSAP);
                    }

                    ObjSAPComprobante.Lines.AccountCode = cuentaContable;


                    if (auxdetalle.NCIdOPCHDetalle > 0)
                    {
                        IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
                        var datosFactura = oIntregadorV1DAO.ObtenerDocEntryFacturaxNotaCredito(auxdetalle.NCIdOPCHDetalle, BaseDatos);


                        if (datosFactura[0].EnviadoPor == 2)
                        {

                            var DatosDocEntry = oConsultasSQL.ObtenerDetallexDocEntry(datosFactura[0].DocEntry, auxdetalle.CodigoArticulo, auxdetalle.Cantidad,BaseDatosSAP);

                            ObjSAPComprobante.Lines.BaseEntry = datosFactura[0].DocEntry;
                            ObjSAPComprobante.Lines.BaseLine = DatosDocEntry[0].LineNum;
                            ObjSAPComprobante.Lines.BaseType = 18;
                        }

                    }



                    if (auxdetalle.IdIndicadorImpuesto == 1)
                    {
                        ObjSAPComprobante.Lines.UnitPrice = (double)Math.Round(auxdetalle.total_valor_item, 2); //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "IGV";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)Math.Round(auxdetalle.total_valor_item, 2); // sin igv
                        ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionCode = "IGV";
                        ObjSAPComprobante.Lines.TaxJurisdictions.JurisdictionType = 1;
                        ObjSAPComprobante.Lines.TaxJurisdictions.TaxAmount = Convert.ToDouble(Math.Round(auxdetalle.total_igv, 2)); //MontoIGV
                        ObjSAPComprobante.Lines.TaxTotal = Convert.ToDouble(Math.Round(auxdetalle.total_item, 2));
                        ObjSAPComprobante.Lines.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO;
                    }
                    if (auxdetalle.IdIndicadorImpuesto == 2)
                    {
                        ObjSAPComprobante.Lines.UnitPrice = (double)Math.Round(auxdetalle.total_valor_item, 2); //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "EXO";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)Math.Round(auxdetalle.total_valor_item, 2); // sin igv

                    }
                    if (auxdetalle.IdIndicadorImpuesto == 3)
                    {
                        ObjSAPComprobante.Lines.UnitPrice = (double)Math.Round(auxdetalle.total_valor_item, 2); //sin igv
                        ObjSAPComprobante.Lines.TaxCode = "INA";//"IGV";
                        ObjSAPComprobante.Lines.PriceAfterVAT = (double)Math.Round(auxdetalle.total_valor_item, 2); // sin igv

                    }



                    ObjSAPComprobante.Lines.Add();
                }
                


                try
                {
                    for (int i = 0; i < auxComprobante.AnexoDetalle.Count(); i++)
                    {
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
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjSAPComprobante);
                    GC.Collect();
                }
                else
                {
                    IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
                    var docentry = oCompany.GetNewObjectKey();
                    var intdocentry = Convert.ToInt32(docentry);
                    //oIntregadorV1DAO.UpdateDocEntryEnviarSAP(auxComprobante.IdEnviarSap, intdocentry);
                    foreach (IntegradorV1Detalle auxdetalle in auxComprobante.detalles)
                    {
                        oIntregadorV1DAO.UpdateDocEntryORPCDetalle(auxdetalle.IdTablaOriginalDetalle, intdocentry, BorradorFirme, BaseDatos);
                    }
                    //Convert.ToInt32(oCompany.GetNewObjectKey);
                    mensaje_error = "Nota de Credito " + DatosSerie[0].Serie + "-" + auxComprobante.Correlativo + " Enviada a SAP con Exito";

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

        public string AddAsientoContable(string Proyecto, List<CuentaConsumoSAP> Cuentas, int GrupoCreacion, DateTime FechaContabilizacion,int Serie, string BaseDatos,string BaseDatosSAP, ref string mensaje_error)
        {
            try
            {

                string errMsg = "";

                SAPbobsCOM.JournalEntries ObjSAPComprobante = null;

                ObjSAPComprobante = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                //ObjSAPComprobante = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers);


                ConsultasSQL oConsultasSQL = new ConsultasSQL();

                ObjSAPComprobante.Memo = "Movimiento de Consumo de Materiales";

                ObjSAPComprobante.ReferenceDate = FechaContabilizacion;

                ObjSAPComprobante.Series = Serie;

                ObjSAPComprobante.ProjectCode = Proyecto;

                for (int i = 0; i < Cuentas.Count; i++)
                {
                    string CodigoCuentaSAP = oConsultasSQL.ObtenerCuentaContable(Cuentas[i].NumeroCuenta,BaseDatosSAP);

                    ObjSAPComprobante.TaxDate = FechaContabilizacion;//DateTime.Now;
                    ObjSAPComprobante.Lines.AccountCode = CodigoCuentaSAP;
                    ObjSAPComprobante.Lines.Credit = Convert.ToDouble(Cuentas[i].MontoCredito);
                    ObjSAPComprobante.Lines.Debit = Convert.ToDouble(Cuentas[i].MontoDebito);
                    ObjSAPComprobante.Lines.DueDate = FechaContabilizacion;//DateTime.Now;
                    ObjSAPComprobante.Lines.ReferenceDate1 = FechaContabilizacion;//DateTime.Now; 
                    ObjSAPComprobante.Lines.TaxDate = FechaContabilizacion;//DateTime.Now;
                    ObjSAPComprobante.Lines.Add();
                }

                int repuesta = ObjSAPComprobante.Add();
                string ee = "";
                if (repuesta != 0)
                {
                    errMsg = oCompany.GetLastErrorDescription();
                    int errCode = oCompany.GetLastErrorCode();
                    //DesconectarSAP();
                    mensaje_error = "Error:" + errMsg;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjSAPComprobante);
                    GC.Collect();
                }
                else
                {
                    IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();
                    var docentry = oCompany.GetNewObjectKey();
                    var intdocentry = Convert.ToInt32(docentry);


                    oIntegradorConsumoDAO.ActualizarEnvioSap(GrupoCreacion, intdocentry, BaseDatos);


                    mensaje_error = "Enviada a SAP con Exito";
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjSAPComprobante);
                    GC.Collect();
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
