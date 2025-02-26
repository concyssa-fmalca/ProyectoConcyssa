using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace ConcyssaWeb.Controllers
{
    public class NotaCreditoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }


        public string ListarORPCDT(int IdBase, string EstadoORPC = "ABIERTO")
        {
            string mensaje_error = "";
            OrpcDAO oOrpcDAO = new OrpcDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OrpcDTO> lstOrpcDTO = oOrpcDAO.ObtenerORPCxEstado(IdBase,IdSociedad,BaseDatos,ref mensaje_error, EstadoORPC, IdUsuario);
            if (lstOrpcDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstOrpcDTO.Count;
                oDataTableDTO.iTotalRecords = lstOrpcDTO.Count;
                oDataTableDTO.aaData = (lstOrpcDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;

            }
        }


        public string ObtenerORPCDetalle(int IdORPC)
        {
            string mensaje_error = "";
            OrpcDAO oOrpcDAO = new OrpcDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<ORPCDetalle> lstORPCDetalle = oOrpcDAO.ObtenerDetalleOrpc(IdORPC,BaseDatos,ref mensaje_error);
            if (lstORPCDetalle.Count > 0)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstORPCDetalle.Count;
                //oDataTableDTO.iTotalRecords = lstORPCDetalle.Count;
                //oDataTableDTO.aaData = (lstORPCDetalle);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstORPCDetalle);

            }
            else
            {
                return mensaje_error;

            }
        }


        public string UpdateInsertMovimientoNotaCredito(OrpcDTO oOrpcDTO,string TblOrigenNC, string IdOrigenNC)
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oOrpcDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oOrpcDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oOrpcDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oOrpcDTO.IdUsuario);
            if (IdSociedad == 0)
            {
                IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            }
            if (IdUsuario == 0)
            {
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            }
            //int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            oOrpcDTO.IdSociedad = IdSociedad;
            oOrpcDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            OrpcDAO oOrpcDAO = new OrpcDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoORPC_Completo(oOrpcDTO, TblOrigenNC, IdOrigenNC, BaseDatos, ref mensaje_error);
            object json = null;
            if (respuesta <= 0)
            {
                json = new { status = false, mensaje = mensaje_error };
                return JsonConvert.SerializeObject(json);
            }
            json = new { status = true, mensaje = mensaje_error };
            return JsonConvert.SerializeObject(json);
            //int respuesta1 = 0
            //if (mensaje_error.Length > 0)
            //{
            //    return mensaje_error;
            //}
            //if (respuesta > 0)
            //{
            //    for (int i = 0; i < oOrpcDTO.detalles.Count; i++)
            //    {
            //        oOrpcDTO.detalles[i].IdORPC = respuesta;
            //        respuesta1 = oMovimimientoDAO.InsertUpdateORPCDetalle(oOrpcDTO.detalles[i],BaseDatos,ref mensaje_error);
            //        int respuesta2 = oMovimimientoDAO.InsertUpdateORPCDetalleCuadrilla(respuesta1, oOrpcDTO.detalles[i],BaseDatos,ref mensaje_error);
            //    }
            //    oOrpcDAO.UpdateTotalesORPC(respuesta,BaseDatos,ref mensaje_error);
            //    if (oOrpcDTO.AnexoDetalle!=null)
            //    {
            //        for (int i = 0; i < oOrpcDTO.AnexoDetalle.Count; i++)
            //        {
            //            oOrpcDTO.AnexoDetalle[i].ruta = "/Anexos/" + oOrpcDTO.AnexoDetalle[i].NombreArchivo;
            //            oOrpcDTO.AnexoDetalle[i].IdSociedad = oOrpcDTO.IdSociedad;
            //            oOrpcDTO.AnexoDetalle[i].Tabla = "Orpc";
            //            oOrpcDTO.AnexoDetalle[i].IdTabla = respuesta;

            //            oMovimimientoDAO.InsertAnexoMovimiento(oOrpcDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);

            //        }
            //    }



            //}


        }

        public string ObtenerDatosxIdOrpc(int IdOrpc)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OrpcDAO oOrpcDAO = new OrpcDAO();
            OrpcDTO oOrpcDTO = oOrpcDAO.ObtenerDatosxIdOrpc(IdOrpc,BaseDatos,ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<ORPCDetalle> lstORPCDetalle = new List<ORPCDetalle>();
                lstORPCDetalle = oOrpcDAO.ObtenerDetalleOrpc(IdOrpc,BaseDatos,ref mensaje_error);
                oOrpcDTO.detalles = new ORPCDetalle[lstORPCDetalle.Count()];
                for (int i = 0; i < lstORPCDetalle.Count; i++)
                {
                    oOrpcDTO.detalles[i] = lstORPCDetalle[i];
                }


                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oOrpcDAO.ObtenerAnexoOrpc(IdOrpc,BaseDatos,ref mensaje_error);
                oOrpcDTO.AnexoDetalle = new AnexoDTO[lstAnexoDTO.Count()];
                for (int i = 0; i < lstAnexoDTO.Count; i++)
                {
                    oOrpcDTO.AnexoDetalle[i] = lstAnexoDTO[i];
                }

                return JsonConvert.SerializeObject(oOrpcDTO);
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
        public string ObtenerTipoCambio(string Moneda, string Fecha)
        {
            string mensaje_error;
            string valida = "";
            string Resultado = "1";

            if (Moneda == "1")
            {
                Resultado = "1";
            }
            else
            {
                WebResponse webResponse;
                HttpWebRequest request;
                Uri uri;
                string response;
                try
                {

                    string cadenaUri = "https://api.apis.net.pe/v1/tipo-cambio-sunat?fecha=" + Fecha;
                    uri = new Uri(cadenaUri, UriKind.RelativeOrAbsolute);
                    request = (HttpWebRequest)WebRequest.Create(uri);
                    request.ContentType = "application/json";
                    webResponse = request.GetResponse();
                    Stream webStream = webResponse.GetResponseStream();
                    StreamReader responseReader = new StreamReader(webStream);
                    response = responseReader.ReadToEnd();
                    Resultado = response;
                    var ff = JsonConvert.DeserializeObject(response);
                    var ddd = "ee";
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
            }

            return Resultado;

        }
        public string UpdateORPC(OrpcDTO oOrpcDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OrpcDAO OOrpcDAO = new OrpcDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = OOrpcDAO.UpdateORPC(IdUsuario, oOrpcDTO,BaseDatos,ref mensaje_error);

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
        public string UpdateCuadrillas(ORPCDetalle oORPCDetalle)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OrpcDAO oOrpcDAO = new OrpcDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOrpcDAO.UpdateCuadrillas(oORPCDetalle,BaseDatos,ref mensaje_error);

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
        public string ValidarStockParaNotaCredito(int IdArticulo, int IdAlmacen, int Cantidad)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OrpcDAO oOrpcDAO = new OrpcDAO();      
            string respuesta = oOrpcDAO.ValidarStockParaNotaCredito(IdArticulo, IdAlmacen, Cantidad,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                return respuesta;
            }

        }
        public string AccionesPorNotaCredito(string TablaOrigen, string IdOrigen, int IdORPC)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OrpcDAO oOrpcDAO = new OrpcDAO();
            string respuesta = oOrpcDAO.AccionesPorNotaCredito(TablaOrigen, IdOrigen, IdORPC,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                return respuesta;
            }

        }
        public string ExtornarORPC(int IdORPC, string TipoProducto, int IdOrigen)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            OrpcDAO oOrpcDAO = new OrpcDAO();
            string respuesta = oOrpcDAO.ExtornarORPC(IdORPC, TipoProducto, IdOrigen,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                return respuesta;
            }

        }

        public int ValidarOrigenDevolucion(int IdORPC)
        {
            OrpcDAO oOrpcDAO = new OrpcDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int respuesta = oOrpcDAO.ExtornarOrigenDevProv(IdORPC,BaseDatos);
            return respuesta;
        }
    }
}
