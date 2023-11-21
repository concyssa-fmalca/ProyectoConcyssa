using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ISAP;

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

            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = 0;
            switch (Tabla)
            {
                case "OPCH":
                    resultado = oIntregadorV1DAO.MoverFacturaEnviarSap(Id, IdUsuario, GrupoCreacion);
                    break;
                case "ORPC":
                    resultado = oIntregadorV1DAO.MoverNotaCreditoEnviarSap(Id, IdUsuario, GrupoCreacion);
                    break;
                default:
                    break;
            }


            return resultado;
        }
        public int ObtenerGrupoCreacionEnviarSap()
        {
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.ObtenerGrupoCreacionEnviarSap();

            return resultado;
        }

        public string ObtenerListaDatosTrabajo()
        {
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<ListaTrabajoDTO> lstListaTrabajoDTO = oIntregadorV1DAO.ObtenerListaDatosTrabajo(IdUsuario);
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

            List<IntegradorV1DTO> lstIntegradorV1DTO = pIntregadorV1DAO.ListarEnviarSap(GrupoCreacion, ref mensaje_error);
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
            IntegradorV1DTO oIntegradorV1DTO = oIntregadorV1DAO.ObtenerDatosxIdEnviarSap(IdEnviarSap, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<IntegradorV1Detalle> lstIntegradorV1Detalle = new List<IntegradorV1Detalle>();
                lstIntegradorV1Detalle = oIntregadorV1DAO.ObtenerDatosxIdEnviarSapDetalle(IdEnviarSap, ref mensaje_error);
                oIntegradorV1DTO.detalles = new IntegradorV1Detalle[lstIntegradorV1Detalle.Count()];
                for (int i = 0; i < lstIntegradorV1Detalle.Count; i++)
                {
                    oIntegradorV1DTO.detalles[i] = lstIntegradorV1Detalle[i];
                }

                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oIntregadorV1DAO.ObtenerAnexoEnviarSap(IdEnviarSap, ref mensaje_error);
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

            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.DeleteAnexo(IdEnviarSapAnexos);
            if (resultado > 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarMarcoTrabajo(int GrupoCreacion)
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.EliminarMarcoTrabajo(GrupoCreacion);

            return resultado;
        }
        public int UpdateEnviarSap(IntegradorV1DTO oIntegradorV1DTO)
        {
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.UpdateEnviarSap(oIntegradorV1DTO);

            return resultado;
        }

        public int UpdateCuadrillasEnviarSAP(IntegradorV1Detalle oIntegradorV1DTO)
        {
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            int resultado = oIntregadorV1DAO.UpdateCuadrillasEnviarSAP(oIntegradorV1DTO);

            return resultado;
        }
        public int UpdateAnexosEnviarSAP(int IdEnviarSap, string NombreArchivo)
        {
            string mensaje_error = "";
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            AnexoDTO oAnexoDTO = new AnexoDTO();
            oAnexoDTO.ruta = "/Anexos/" + NombreArchivo;
            oAnexoDTO.NombreArchivo = NombreArchivo;
            oAnexoDTO.IdSociedad = 1;
            oAnexoDTO.Tabla = "EnviarSap";
            oAnexoDTO.IdTabla = IdEnviarSap;

            int resultado = oMovimientoDAO.InsertAnexoMovimiento(oAnexoDTO, ref mensaje_error);

            return resultado;
        }

        public string ObtenerTasaDetSAP()
        {
            string mensaje_error = "";
            IntegradorTasaDetDTO oIntegradorTasaDetDTO = new IntegradorTasaDetDTO();
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorTasaDetDTO> lstIntegradorTasaDetDTO = oIntregadorV1DAO.ObtenerTasaDetSAP(ref mensaje_error);
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
           
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorClasif> lstIntegradorClasif = oIntregadorV1DAO.ObtenerCLABYSADQ(ref mensaje_error);
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
            IntegradorGrupoDetDTO oIntegradorGrupoDetDTO = new IntegradorGrupoDetDTO();
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorGrupoDetDTO> lstIntegradorGrupoDetDTO = oIntregadorV1DAO.ObtenerGrupoDetSAP(ref mensaje_error);
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
            IntegradorGrupoDetDTO oIntegradorGrupoDetDTO = new IntegradorGrupoDetDTO();
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorCondPagoDetDTO> lstIntegradorGrupoDetDTO = oIntregadorV1DAO.ObtenerCondPagoDetSAP(GrupoDet,ref mensaje_error);
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
        public string ObtenerSerieSAP(int ObjCode)
        {
            string mensaje_error = "";
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            List<IntegradorSerieSapDTO> lstIntegradorSerieSapDTO = oIntregadorV1DAO.ObtenerSerieSAP(ObjCode, ref mensaje_error);
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
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();
            ListaTrabajoDTO respuesta = oIntregadorV1DAO.ValidarGrupoEnviado(GrupoCreacion,ref mensaje_error);
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
        public string ConectarSAP(int GrupoCreacion, int BorradorFirme, int SerieFactura, int SerieNotaCredito)
        {
            string respuesta = ConexionSAP.Conectar();

            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();

            var lstResultados = oIntregadorV1DAO.ListarEnviarSapConDetallexGrupoCreacion(GrupoCreacion);

            string respuestaSAP = ".";

            string Mensaje_error = "";
            try
            {

                if(respuesta == "true")
                {
                    ConexionSAP conexion = new ConexionSAP();
                    for (int i = 0; i < lstResultados.Count; i++)
                    {                   
                        switch (lstResultados[i].TablaOriginal)
                        {
                            case "OPCH":
                                respuestaSAP += conexion.AddComprobante(lstResultados[i], BorradorFirme, SerieFactura, ref Mensaje_error) + "\n";
                                break;
                            case "ORPC":
                                respuestaSAP += conexion.AddNotaCredito(lstResultados[i], BorradorFirme, SerieNotaCredito, ref Mensaje_error) + "\n";
                                break;
                            default:
                                break;
                        }

                        //respuestaSAP += Mensaje_error;
                    }
                    ConexionSAP.DesconectarSAP();
                }
            }
            catch (Exception e)
            {
                respuestaSAP = e.Message.ToString();
            }



            return respuestaSAP;
            
        }
        public string ListarCamposxIdObra(int IdObra, DateTime FechaInicio, DateTime FechaFin)
        {
            string mensaje_error = "";
            IntregadorV1DAO oIntregadorV1DAO = new IntregadorV1DAO();

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<IntegradorV1DTO> lstOpchDTO = oIntregadorV1DAO.ListarCamposxIdObra(IdObra, FechaInicio, FechaFin, ref mensaje_error);
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


    }
}
