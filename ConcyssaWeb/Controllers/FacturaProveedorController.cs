using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace ConcyssaWeb.Controllers
{
    public class FacturaProveedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listado()
        {
            return View();
        }

        public string ListarOPCHDT(int IdBase,string EstadoOPCH = "ABIERTO")
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));

            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstado(IdBase,IdSociedad, ref mensaje_error, EstadoOPCH, IdUsuario);
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length==0)
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


        public string ListarOPCHDTModal(string EstadoOPCH = "ABIERTO")
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOPCHxEstadoModal(IdSociedad, ref mensaje_error, EstadoOPCH, IdUsuario);
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

        public string ObtenerOPCHDetalle(int IdOPCH)
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<OPCHDetalle> lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOPCH, ref mensaje_error);
            if (lstOPCHDetalle.Count > 0)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstOPCHDetalle.Count;
                //oDataTableDTO.iTotalRecords = lstOPCHDetalle.Count;
                //oDataTableDTO.aaData = (lstOPCHDetalle);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(lstOPCHDetalle);

            }
            else
            {
                return mensaje_error;

            }
        }

        
        public string UpdateInsertMovimientoFacturaProveedor(OpchDTO oOpchDTO)
        {
            string mensaje_error = "";
            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oOpchDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oOpchDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oOpchDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oOpchDTO.IdUsuario);
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

            oOpchDTO.IdSociedad = IdSociedad;
            oOpchDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            OpchDAO oOpchDAO = new OpchDAO();
            int respuesta = oMovimimientoDAO.InsertUpdateMovimientoOPCH(oOpchDTO, ref mensaje_error);
            int respuesta1 = 0;
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            if (respuesta > 0)
            {
                for (int i = 0; i < oOpchDTO.detalles.Count; i++)
                {
                    oOpchDTO.detalles[i].IdOPCH = respuesta;
                    respuesta1 = oMovimimientoDAO.InsertUpdateOPCHDetalle(oOpchDTO.detalles[i], ref mensaje_error);
                    int respuesta2 = oMovimimientoDAO.InsertUpdateOPCHDetalleCuadrilla(respuesta1, oOpchDTO.detalles[i], ref mensaje_error);

                }
                oOpchDAO.UpdateTotalesOPCH(respuesta, ref mensaje_error);
                if (oOpchDTO.AnexoDetalle!=null)
                {
                    for (int i = 0; i < oOpchDTO.AnexoDetalle.Count; i++)
                    {
                        oOpchDTO.AnexoDetalle[i].ruta = "/Anexos/" + oOpchDTO.AnexoDetalle[i].NombreArchivo;
                        oOpchDTO.AnexoDetalle[i].IdSociedad = oOpchDTO.IdSociedad;
                        oOpchDTO.AnexoDetalle[i].Tabla = "Opch";
                        oOpchDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oOpchDTO.AnexoDetalle[i], ref mensaje_error);
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

        public string ObtenerDatosxIdOpch(int IdOpch)
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            OpchDTO oOpchDTO = oOpchDAO.ObtenerDatosxIdOpch(IdOpch, ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
                lstOPCHDetalle = oOpchDAO.ObtenerDetalleOpch(IdOpch, ref mensaje_error);
                oOpchDTO.detalles = new OPCHDetalle[lstOPCHDetalle.Count()];
                for (int i = 0; i < lstOPCHDetalle.Count; i++)
                {
                    oOpchDTO.detalles[i] = lstOPCHDetalle[i];
                }

                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oOpchDAO.ObtenerAnexoOpch(IdOpch, ref mensaje_error);
                oOpchDTO.AnexoDetalle = new AnexoDTO[lstAnexoDTO.Count()];
                for (int i = 0; i < lstAnexoDTO.Count; i++)
                {
                    oOpchDTO.AnexoDetalle[i] = lstAnexoDTO[i];
                }

                return JsonConvert.SerializeObject(oOpchDTO);
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
        public string UpdateOPCH(OpchDTO oOpchDTO)
        {

            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOpchDAO.UpdateOPCH(IdUsuario, oOpchDTO, ref mensaje_error);

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
        public string UpdateCuadrillas(OPCHDetalle oOPCHDetalle)
        {

            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            int respuesta = oOpchDAO.UpdateCuadrillas(oOPCHDetalle, ref mensaje_error);

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
        public string ObtenerOrigenesFactura(int IdOPCH)
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
         
            List<OpchDTO> lstOpchDTO = oOpchDAO.ObtenerOrigenesFactura(IdOPCH, ref mensaje_error);
            if (lstOpchDTO.Count >= 0 && mensaje_error.Length == 0)
            {
              
                return JsonConvert.SerializeObject(lstOpchDTO);

            }
            else
            {
                return mensaje_error;

            }
        }
        public string ValidarStockExtorno(int IdOPCH)
        {
            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            List<string> SinStock = new List<string>();
            List<OPCHDetalle> lstoOpchDTO = oOpchDAO.ObtenerStockParaExtornoOPCH(IdOPCH, ref mensaje_error);
            for (int i = 0; i < lstoOpchDTO.Count; i++)
            {
                if (lstoOpchDTO[i].Resta == -1) SinStock.Add("No hay Stock para " + lstoOpchDTO[i].DescripcionArticulo);
            }
            if (SinStock.Count != 0)
            {
                return JsonConvert.SerializeObject(SinStock);
            }
            else
            {
                return "bien";
            }

        }
        public string ExtornoConfirmado(int IdOPCH, string EsServicio, string TablaOrigen)
        {

            string mensaje_error = "";
            OpchDAO oOpchDAO = new OpchDAO();
            int respuesta = oOpchDAO.ExtornoConfirmado(IdOPCH, EsServicio, TablaOrigen, ref mensaje_error);

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
