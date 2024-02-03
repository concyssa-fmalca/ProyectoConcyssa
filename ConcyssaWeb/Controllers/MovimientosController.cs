using DAO;
using DTO;
using FE;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;

namespace ConcyssaWeb.Controllers
{
    public class MovimientosController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }


        public string ObtenerDatosxIdMovimiento(int IdMovimiento)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento,BaseDatos,ref mensaje_error);

            if (mensaje_error.ToString().Length == 0 )
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public string ObtenerDatosxIdMovimientoOLD(int IdMovimiento)
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimientoOLD(IdMovimiento,BaseDatos,ref mensaje_error);
            oMovimientoDTO.existeObraDestinoUsuario = oMovimientoDAO.ValidarExisteObraxIdUsuario(IdUsuario, oMovimientoDTO.IdObraDestino,BaseDatos,ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerMovimientosIngresos(int IdBase,DateTime FechaInicial,DateTime FechaFinal, int Estado=3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List <MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosIngresos(IdBase,IdSociedad, FechaInicial, FechaFinal, BaseDatos,ref mensaje_error, Estado);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerMovimientosTranferencias(int IdBase, int Estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosTransferencias(IdBase,IdSociedad, IdUsuario,BaseDatos,ref mensaje_error, Estado);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerMovimientosTranferenciasFinal(int IdObraDestino)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<MovimientoTranferenciaFinal> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosTransferenciasFinal(IdObraDestino,IdSociedad, IdUsuario,BaseDatos,ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerMovimientosSalida(int IdBase,DateTime FechaInicial,DateTime FechaFinal,int Estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosSalida(IdBase,IdSociedad,BaseDatos,FechaInicial,FechaFinal,ref mensaje_error, Estado, IdUsuario);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public ResultadoGRDTO GenerarPDF(int IdMovimiento)
        {
            var respuesta = new ResultadoGRDTO();
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO mov = new MovimientoDAO();
            var TicketObtenido = mov.ObtenerTicketGuia(IdMovimiento,BaseDatos);
            MovimientoDTO oMovimientoDTO = mov.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento,BaseDatos,ref mensaje_error);
            APIGuiaRemisionSunat api = new APIGuiaRemisionSunat();
            GRSunatDTO aux = new GRSunatDTO();
            aux =(new GRSunatDTO{
                N_DOC = oMovimientoDTO.SerieGuiaElectronica + "-" + oMovimientoDTO.NumeroGuiaElectronica
            });
    

            if (TicketObtenido != "")
            {
                 respuesta = api.RespuestaConsultaTicket(TicketObtenido, aux);
                if (respuesta.DetalleAnexo != null)
                {
                    for (int j = 0; j < respuesta.DetalleAnexo.Count(); j++)
                    {
                        respuesta.DetalleAnexo[j].IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                        respuesta.DetalleAnexo[j].IdTabla = IdMovimiento;
                        respuesta.DetalleAnexo[j].Tabla = "Movimiento";
                        mov.InsertAnexoMovimiento(respuesta.DetalleAnexo[j],BaseDatos,ref mensaje_error);
                    }
                }
            }

            return respuesta;
        }


        public string GenerarGuia(int IdMovimiento)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string mensaje = "";

            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            GRSunatDTO oGRSunatDTO = new GRSunatDTO();


            ConfiguracionSociedadDAO conf = new ConfiguracionSociedadDAO();
            List<ConfiguracionSociedadDTO> oConfiguracionSociedadDTO = conf.ObtenerConfiguracionSociedad(IdSociedad,BaseDatos, ref mensaje);
            if (oConfiguracionSociedadDTO.Count() > 0)
            {}else{return "Debe Configurar Sociedad";}


            MovimientoDTO oMovimientoDTO1 = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento,BaseDatos,ref mensaje_error);
            if (oMovimientoDTO1 != null)
            {
                int re = oMovimientoDAO.AsignarSerieCorrelativoGuia(IdMovimiento, oMovimientoDTO1.IdTipoDocumentoRef, oMovimientoDTO1.IdAlmacen,BaseDatos);
            }

           
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento,BaseDatos,ref mensaje_error);

            oGRSunatDTO.N_DOC = oMovimientoDTO.SerieGuiaElectronica + "-" + oMovimientoDTO.NumeroGuiaElectronica;//oMovimientoDTO.NumSerieTipoDocumentoRef;
            oGRSunatDTO.TIPO_DOC = "09";
            oGRSunatDTO.FECHA = oMovimientoDTO.FechaContabilizacion.ToString("yyyy-MM-dd");
            oGRSunatDTO.RUC = oConfiguracionSociedadDTO[0].Ruc;//"20513233605";//oMovimientoDTO.NumDocumentoDestinatario; consyssa
            oGRSunatDTO.TIPO_RUC = "6";
            oGRSunatDTO.NOMBRE = oMovimientoDTO.NombDestinatario;//"CONCYSSA SA";//oMovimientoDTO.NombDestinatario; consyssa
            oGRSunatDTO.DIRECCION = oConfiguracionSociedadDTO[0].Direccion;//"AV. LA MARINA 1039"; //consyssa
            oGRSunatDTO.RUC_EMIS = oConfiguracionSociedadDTO[0].Ruc;//"20100370426"; //consysaa
            oGRSunatDTO.NOMBRE_EMIS = oConfiguracionSociedadDTO[0].RazonSocial;//"ANDES SYSTEMS E.I.R.L."; //consysaa
            oGRSunatDTO.MOT_TRAS = oMovimientoDTO.CodigoMotivoTrasladoSunat;
            oGRSunatDTO.MOT_TRAS_DES = oMovimientoDTO.DescripcionMotivoTrasladoSunat;
            oGRSunatDTO.PESO_BRUTO = oMovimientoDTO.Peso;
            oGRSunatDTO.UNI_PESO_BRUTO = "KGM";
            oGRSunatDTO.BULTOS = oMovimientoDTO.Bulto;
            oGRSunatDTO.TIPO_TRANS = oMovimientoDTO.TipoTransporte;
            oGRSunatDTO.FCH_INICIO = oMovimientoDTO.FechaContabilizacion.ToString("yyyy-MM-dd");
            oGRSunatDTO.RUC_TRANS = oMovimientoDTO.NumDocumentoTransportista;
            oGRSunatDTO.NOM_TRANS = oMovimientoDTO.NombTransportista;
            var Conduc1 = oMovimientoDTO.PlacaVehiculo;

            if (oMovimientoDTO.PlacaVehiculo.Contains("-"))
            {
                var Conduc = oMovimientoDTO.PlacaVehiculo.Split("-");
                Conduc1 = Conduc[0] + Conduc[1];
            }

            oGRSunatDTO.PLACA_PRINCIPAL = Conduc1;
            oGRSunatDTO.MARCA_PRINCIPAL = oMovimientoDTO.MarcaVehiculo;

            //oGRSunatDTO.LIC_TRANS = oMovimientoDTO.NumIdentidadConductor;
  
            oGRSunatDTO.UBIGEO_LLE = oMovimientoDTO.CodigoUbigeoLlegada;
            oGRSunatDTO.PUNTO_LLE = oMovimientoDTO.DireccionLlegada;
            oGRSunatDTO.RUC_LLE = oMovimientoDTO.NumDocumentoDestinatario;
            oGRSunatDTO.COD_LLE = oMovimientoDTO.CodigoAnexoLlegada;

            oGRSunatDTO.UBIGEO_PAR = oMovimientoDTO.CodigoUbigeoPartida;
            oGRSunatDTO.PUNTO_PAR = oMovimientoDTO.DireccionPartida;
            oGRSunatDTO.RUC_PAR = oConfiguracionSociedadDTO[0].Ruc; 
            oGRSunatDTO.COD_PAR = oMovimientoDTO.CodigoAnexoPartida;

            oGRSunatDTO.PDF = "SI";
            oGRSunatDTO.OBS1 = oMovimientoDTO.Comentario;
            oGRSunatDTO.OBS2 = "";
            //oGRSunatDTO.ENVIO = "2";
            //oGRSunatDTO.WSDL = "3";

            if (oMovimientoDTO.detalles != null)
            {
                for (int i = 0; i < oMovimientoDTO.detalles.Count(); i++)
                {
                    oGRSunatDTO.ITEMS.Add(new ITEMS
                    {
                        CODIGO = oMovimientoDTO.detalles[i].CodigoArticulo,
                        DESCRIPCION = oMovimientoDTO.detalles[i].DescripcionArticulo,
                        TIPOUNI = oMovimientoDTO.detalles[i].TipoUnidadMedida,
                        CANTIDAD = oMovimientoDTO.detalles[i].CantidadBase

                    });
                }
            }
            

            oGRSunatDTO.CONDUCTORES.Add(new CONDUCTORES
            {
                TIPO = "Principal",
                RUC = oMovimientoDTO.NumIdentidadConductor,
                TIPO_RUC = "1",
                LICENCIA = oMovimientoDTO.LicenciaConductor,
                NOMBRES = oMovimientoDTO.NombreConductor,
                APELLIDOS = oMovimientoDTO.ApellidoConductor
            });


            APIGuiaRemisionSunat oAPIGuiaRemisionSunat = new APIGuiaRemisionSunat();
            ResultadoGRDTO oResultadoGRDTO = new ResultadoGRDTO();
            oResultadoGRDTO=oAPIGuiaRemisionSunat.SendGuiaRemision(oGRSunatDTO, oMovimientoDTO.IdMovimiento,BaseDatos);
                
            if (oResultadoGRDTO.Message.Contains("500"))
            {
                oResultadoGRDTO.Message = oResultadoGRDTO.Message;
                return JsonConvert.SerializeObject(oResultadoGRDTO);
            }

            if (oResultadoGRDTO.Message.Contains("1033"))
            {
                oResultadoGRDTO.Message = oResultadoGRDTO.Message;
                MovimientoDAO dao1 = new MovimientoDAO();
                dao1.UpdateEstadoGuia(oMovimientoDTO.IdMovimiento,BaseDatos,ref mensaje_error);

                if (oResultadoGRDTO.DetalleAnexo != null)
                {
                    for (int j = 0; j < oResultadoGRDTO.DetalleAnexo.Count(); j++)
                    {
                        oResultadoGRDTO.DetalleAnexo[j].IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                        oResultadoGRDTO.DetalleAnexo[j].IdTabla = IdMovimiento;
                        oResultadoGRDTO.DetalleAnexo[j].Tabla = "Movimiento";
                        oMovimientoDAO.InsertAnexoMovimiento(oResultadoGRDTO.DetalleAnexo[j],BaseDatos,ref mensaje_error);
                    }
                }

                return JsonConvert.SerializeObject(oResultadoGRDTO);
            }

            if (oResultadoGRDTO.Success == true)
            {
                MovimientoDAO dao = new MovimientoDAO();
                

                if (oResultadoGRDTO.DetalleAnexo != null)
                {
                    for (int j = 0; j < oResultadoGRDTO.DetalleAnexo.Count(); j++)
                    {
                        oResultadoGRDTO.DetalleAnexo[j].IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                        oResultadoGRDTO.DetalleAnexo[j].IdTabla = IdMovimiento;
                        oResultadoGRDTO.DetalleAnexo[j].Tabla = "Movimiento";
                        oMovimientoDAO.InsertAnexoMovimiento(oResultadoGRDTO.DetalleAnexo[j],BaseDatos,ref mensaje_error);
                    }
                }
                

            }



            return JsonConvert.SerializeObject(oResultadoGRDTO);


        }

        public string ObtenerMovimientosIngresosDT(int IdBase,DateTime FechaInicial,DateTime FechaFinal,int Estado = 3)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosIngresos(IdBase,IdSociedad,FechaInicial,FechaFinal,BaseDatos,ref mensaje_error, Estado,IdUsuario);
            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (mensaje_error.ToString().Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = oMovimientoDTO.Count;
                oDataTableDTO.iTotalRecords = oMovimientoDTO.Count;
                oDataTableDTO.aaData = (oMovimientoDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);
                //return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string ValidarExtorno(int IdMovimiento)
        {
            string Valida = "0";
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            Valida = oMovimientoDAO.ValidaExtorno(IdMovimiento,BaseDatos,ref mensaje_error);
        
            return Valida;

        }

        public string UpdateMovimiento(MovimientoDTO oMovimientoDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oMovimientoDAO.UpdateMovimiento(IdUsuario,oMovimientoDTO,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }
        public string UpdateMovimientoSalida(MovimientoDTO oMovimientoDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oMovimientoDAO.UpdateMovimientoSalida(IdUsuario, oMovimientoDTO,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }
        public string UpdateMovimientoTransferencia(MovimientoDTO oMovimientoDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oMovimientoDAO.UpdateMovimientoTransferencia(IdUsuario, oMovimientoDTO,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }

        }

    }
}
