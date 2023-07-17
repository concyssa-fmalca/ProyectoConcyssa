using DAO;
using DTO;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;


namespace ConcyssaWeb.Controllers
{
    public class SalidaMercanciaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string UpdateInsertMovimiento(MovimientoDTO oMovimientoDTO)
        {
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oMovimientoDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oMovimientoDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oMovimientoDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oMovimientoDTO.IdUsuario);


            if (IdSociedad == 0)
            {
                IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            }

            if (IdUsuario == 0)
            {
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            }


            string mensaje_error = "";
          
            //int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            //int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            oMovimientoDTO.IdSociedad = IdSociedad;
            oMovimientoDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            int respuesta = oMovimimientoDAO.InsertUpdateMovimiento(oMovimientoDTO, ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                {
                    oMovimientoDTO.detalles[i].IdMovimiento = respuesta;
                    oMovimientoDTO.detalles[i].IdMovimientoDetalle = 0;
                    respuesta1 = oMovimimientoDAO.InsertUpdateMovimientoDetalle(oMovimientoDTO.detalles[i], 0,ref mensaje_error);
                    int respuesta2 = oMovimimientoDAO.InsertUpdateMovimientoDetalleCuadrilla(respuesta1, oMovimientoDTO.detalles[i], ref mensaje_error);

                }
                //for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                //{
                //    oMovimientoDTO.detalles[i].IdMovimientoDetalle = respuesta1;
                //    int respuesta2 = oMovimimientoDAO.InsertUpdateMovimientoDetalleCuadrilla(respuesta1, oMovimientoDTO.detalles[i], ref mensaje_error);
                //}
                if (oMovimientoDTO.AnexoDetalle != null)
                {
                    for (int i = 0; i < oMovimientoDTO.AnexoDetalle.Count; i++)
                    {
                        oMovimientoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oMovimientoDTO.AnexoDetalle[i].NombreArchivo;
                        oMovimientoDTO.AnexoDetalle[i].IdSociedad = oMovimientoDTO.IdSociedad;
                        oMovimientoDTO.AnexoDetalle[i].Tabla = "Movimiento";
                        oMovimientoDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oMovimientoDTO.AnexoDetalle[i], ref mensaje_error);
                    }
                }
                 

            }

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return respuesta.ToString();
                }
                else
                {
                    return mensaje_error;
                }
            }
        }

        public string GenerarSalidaExtorno(int IdMovimiento)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            KardexDAO oKardexDAO = new KardexDAO();
            MovimientoDTO oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosDetallexIdMovimiento(IdMovimiento, ref mensaje_error);
            ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();

            if (mensaje_error.ToString().Length == 0)
            {
                //int validadStock = 0;
                //for (int i = 0; i < oMovimientoDTO.detalles.Count(); i++)
                //{
                //    oArticuloStockDTO = oKardexDAO.ObtenerArticuloxIdArticuloxIdAlm(oMovimientoDTO.detalles[i].IdArticulo, oMovimientoDTO.detalles[i].IdAlmacen, ref mensaje_error);
                //    if (oArticuloStockDTO.Stock < oMovimientoDTO.detalles[i].CantidadBase)
                //    {
                //        validadStock = 1;
                //    }
                //}
                //if (validadStock == 1)
                //{
                //    return "No hay suficiente Stock";
                //}
                EntradaMercanciaController oEntradaMercanciaController = new EntradaMercanciaController();
                oMovimientoDTO.IdTipoDocumento = 335;
                oMovimientoDTO.Comentario = "EXTORNO DEL SALIDA " + oMovimientoDTO.NombSerie + "-" + +oMovimientoDTO.Correlativo;
                oMovimientoDTO.IdMovimiento = 0;
                oMovimientoDTO.IdSerie = 20006;
                //for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                //{
                //    oMovimientoDTO.detalles[i].IdMovimientoDetalle = 0;
                //}

                oEntradaMercanciaController.UpdateInsertMovimiento(oMovimientoDTO);


                return "1";

            }
            else
            {
                return mensaje_error;

            }

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


        public string ListarSalidaModalDT(int IdAlmacen)
        {
            string mensaje_error = "";
            DataTableDTO oDataTableDTO = new DataTableDTO();
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<MovimientoDTO> oMovimientoDTO = oMovimientoDAO.ObtenerMovimientosSalidaModal(IdSociedad, IdAlmacen, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = oMovimientoDTO.Count;
                oDataTableDTO.iTotalRecords = oMovimientoDTO.Count;
                oDataTableDTO.aaData = (oMovimientoDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);
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



        public string ObtenerSGI(string SGI)
        {
            string mensaje_error = "";
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SgiDTO> oSgiDTO = oMovimientoDAO.BuscarSGI(SGI, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {

                return JsonConvert.SerializeObject(oSgiDTO);
            }
            else
            {
                return mensaje_error;
            }

        }


    }
}
