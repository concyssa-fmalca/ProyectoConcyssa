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
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento, ref mensaje_error);

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
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimientoOLD(IdMovimiento, ref mensaje_error);
            oMovimientoDTO.existeObraDestinoUsuario = oMovimientoDAO.ValidarExisteObraxIdUsuario(IdUsuario, oMovimientoDTO.IdObraDestino, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerMovimientosIngresos(int IdBase, int Estado=3)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List <MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosIngresos(IdBase,IdSociedad, ref mensaje_error, Estado);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerMovimientosTranferencias(int IdBase,int Estado = 3)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosTransferencias(IdBase,IdSociedad, IdUsuario, ref mensaje_error, Estado);
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
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<MovimientoTranferenciaFinal> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosTransferenciasFinal(IdObraDestino,IdSociedad, IdUsuario, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerMovimientosSalida(int IdBase,int Estado = 3)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosSalida(IdBase,IdSociedad, ref mensaje_error, Estado, IdUsuario);
            if (mensaje_error.ToString().Length == 0)
            {
                return JsonConvert.SerializeObject(oMovimientoDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string GenerarGuia(int IdMovimiento)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            GRSunatDTO oGRSunatDTO = new GRSunatDTO();

            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento, ref mensaje_error);

            oGRSunatDTO.N_DOC = oMovimientoDTO.SerieGuiaElectronica + "-" + oMovimientoDTO.NumeroGuiaElectronica;//oMovimientoDTO.NumSerieTipoDocumentoRef;
            oGRSunatDTO.TIPO_DOC = "09";
            oGRSunatDTO.FECHA = oMovimientoDTO.FechaContabilizacion.ToString("yyyy-MM-dd");
            oGRSunatDTO.RUC = "20513233605";//oMovimientoDTO.NumDocumentoDestinatario; consyssa
            oGRSunatDTO.TIPO_RUC = "6";
            oGRSunatDTO.NOMBRE = "CONCYSSA SA";//oMovimientoDTO.NombDestinatario; consyssa
            oGRSunatDTO.DIRECCION = "AV. LA MARINA 1039"; //consyssa
            oGRSunatDTO.RUC_EMIS = "20100370426"; //consysaa
            oGRSunatDTO.NOMBRE_EMIS = "ANDES SYSTEMS E.I.R.L."; //consysaa
            oGRSunatDTO.MOT_TRAS = oMovimientoDTO.CodigoMotivoTrasladoSunat;
            oGRSunatDTO.MOT_TRAS_DES = oMovimientoDTO.DescripcionMotivoTrasladoSunat;
            oGRSunatDTO.PESO_BRUTO = oMovimientoDTO.Peso;
            oGRSunatDTO.UNI_PESO_BRUTO = "KGM";
            oGRSunatDTO.BULTOS = oMovimientoDTO.Bulto;
            oGRSunatDTO.TIPO_TRANS = oMovimientoDTO.TipoTransporte;
            oGRSunatDTO.FCH_INICIO = oMovimientoDTO.FechaContabilizacion.ToString("yyyy-MM-dd");
            oGRSunatDTO.RUC_TRANS = oMovimientoDTO.NumDocumentoTransportista;
            oGRSunatDTO.NOM_TRANS = oMovimientoDTO.NombTransportista;
            oGRSunatDTO.PLACA_PRINCIPAL = oMovimientoDTO.PlacaVehiculo;
            oGRSunatDTO.MARCA_PRINCIPAL = oMovimientoDTO.MarcaVehiculo;

            //oGRSunatDTO.LIC_TRANS = oMovimientoDTO.NumIdentidadConductor;
  
            oGRSunatDTO.UBIGEO_LLE = oMovimientoDTO.CodigoUbigeoLlegada;
            oGRSunatDTO.PUNTO_LLE = oMovimientoDTO.DireccionLlegada;
            oGRSunatDTO.RUC_LLE = oMovimientoDTO.NumDocumentoDestinatario;
            oGRSunatDTO.COD_LLE = oMovimientoDTO.CodigoAnexoLlegada;

            oGRSunatDTO.UBIGEO_PAR = oMovimientoDTO.CodigoUbigeoPartida;
            oGRSunatDTO.PUNTO_PAR = oMovimientoDTO.DireccionPartida;
            oGRSunatDTO.RUC_PAR = "20100370426";
            oGRSunatDTO.COD_PAR = oMovimientoDTO.CodigoAnexoPartida;

            oGRSunatDTO.PDF = "SI";
            oGRSunatDTO.OBS1 = oMovimientoDTO.Comentario;
            oGRSunatDTO.OBS2 = "";
            //oGRSunatDTO.ENVIO = "2";
            //oGRSunatDTO.WSDL = "3";

            for (int i = 0; i < oMovimientoDTO.detalles.Count(); i++)
            {
                oGRSunatDTO.ITEMS.Add(new ITEMS
                {
                    CODIGO = oMovimientoDTO.detalles[i].CodigoArticulo,
                    DESCRIPCION = oMovimientoDTO.detalles[i].DescripcionArticulo,
                    TIPOUNI = oMovimientoDTO.detalles[i].TipoUnidadMedida,
                    CANTIDAD= oMovimientoDTO.detalles[i].CantidadBase

                }) ;
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
            oResultadoGRDTO=oAPIGuiaRemisionSunat.SendGuiaRemision(oGRSunatDTO);
                
            if (oResultadoGRDTO.Message.Contains("500"))
            {
                oResultadoGRDTO.Message = oResultadoGRDTO.Message;
                return JsonConvert.SerializeObject(oResultadoGRDTO);
            }

            if (oResultadoGRDTO.Success == true)
            {
                MovimientoDAO dao = new MovimientoDAO();
                dao.GuardarTicketUpdateEstadoGuia(oMovimientoDTO.IdMovimiento, oResultadoGRDTO.Ticket,ref mensaje_error);

                if (oResultadoGRDTO.DetalleAnexo != null)
                {
                    for (int j = 0; j < oResultadoGRDTO.DetalleAnexo.Count(); j++)
                    {
                        oResultadoGRDTO.DetalleAnexo[j].IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
                        oResultadoGRDTO.DetalleAnexo[j].IdTabla = IdMovimiento;
                        oResultadoGRDTO.DetalleAnexo[j].Tabla = "Movimiento";
                        oMovimientoDAO.InsertAnexoMovimiento(oResultadoGRDTO.DetalleAnexo[j], ref mensaje_error);
                    }
                }
                


               

            }



            return JsonConvert.SerializeObject(oResultadoGRDTO);


        }

        public string ObtenerMovimientosIngresosDT(int IdBase,int Estado = 3)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosIngresos(IdBase,IdSociedad, ref mensaje_error, Estado,IdUsuario);
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
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            Valida = oMovimientoDAO.ValidaExtorno(IdMovimiento, ref mensaje_error);
        
            return Valida;

        }



    }
}
