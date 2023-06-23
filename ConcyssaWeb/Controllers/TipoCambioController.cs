using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace ConcyssaWeb.Controllers
{
    public class TipoCambioController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerTipoCambio(int estado = 3)
        {
            string mensaje_error = "";
            TipoCambioDAO oTipoCambioDAO = new TipoCambioDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoCambioDTO> lstGlosaContableDTO = oTipoCambioDAO.ObtenerTipoCambio( ref mensaje_error);
            if (lstGlosaContableDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGlosaContableDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosxID(int IdMoneda, DateTime Fecha)
        {
            string mensaje_error = "";
            TipoCambioDAO oGlosaContableDAO = new TipoCambioDAO();
            List<TipoCambioDTO> lstGlosaContableDTO = oGlosaContableDAO.ObtenerDatosxID(IdMoneda, Fecha, ref mensaje_error);

            if (lstGlosaContableDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGlosaContableDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        


        public string UpdateInsertTipoCambio(TipoCambioDTO oTipoCambio)
        {

            string mensaje_error = "";
            TipoCambioDAO oTipoCambioDAO = new TipoCambioDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oTipoCambio.IdSociedad = IdSociedad;
            int respuesta = oTipoCambioDAO.UpdateInsertTipoCambio(oTipoCambio, ref mensaje_error);

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

        public string ActualizarTipoCambio()
        {
            string resultado = "0";
            try
            {
                MonedaDAO oMonedaDAO = new MonedaDAO();
                int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));              
                List<MonedaDTO> monedaDTOs=   oMonedaDAO.ObtenerMonedas(IdSociedad.ToString());
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                foreach (MonedaDTO item in monedaDTOs)
                {
                    updateTipoCambio(item, date);

                }
                resultado = "1";
                 
            }
            catch (Exception es)
            {
                resultado = "0";
            }
            return resultado; ;
        }

        private  void updateTipoCambio(MonedaDTO item, string Fecha)
         {
            
            string TipoCambio = ObtenerTipoCambio(item.IdMoneda.ToString(), Fecha);
            if (TipoCambio != "1")
            {
                TipoCambioSunatDTO? cambio = JsonConvert.DeserializeObject<TipoCambioSunatDTO>(TipoCambio);
                TipoCambioDTO oTipoCambio = new TipoCambioDTO();
                oTipoCambio.IdMoneda = item.IdMoneda;
                oTipoCambio.Fecha = Convert.ToDateTime(cambio?.fecha);
                oTipoCambio.TipoCambioCompra = Convert.ToDecimal(cambio?.compra);
                oTipoCambio.TipoCambioVenta = Convert.ToDecimal(cambio?.venta);
                oTipoCambio.Origen = Convert.ToString(cambio?.origen);
                UpdateInsertTipoCambio(oTipoCambio);
            }
        }

        private string ObtenerTipoCambio(string Moneda, string Fecha)
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
                    throw new Exception(e.ToString());
                  
                }
            }

            return Resultado;

        }

        public string ObtenerTipoCambio(int IdMoneda, DateTime Fecha)
        {
            string mensaje_error = "";
            TipoCambioDAO oGlosaContableDAO = new TipoCambioDAO();
            List<TipoCambioDTO> lstGlosaContableDTO = oGlosaContableDAO.ObtenerDatosxID(IdMoneda, Fecha, ref mensaje_error);

            if (lstGlosaContableDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstGlosaContableDTO);
            }
            else
            {
                MonedaDTO item= new MonedaDTO();
                item.IdMoneda = IdMoneda;
                string date = Fecha.ToString("yyyy-MM-dd");
                updateTipoCambio(item, date);
                ////lstGlosaContableDTO.
            }
            return "";
        }



        public int EliminarGlosaContable(int IdGlosaContable)
        {
            string mensaje_error = "";
            GlosaContableDAO oGlosaContableDAO = new GlosaContableDAO();
            int resultado = oGlosaContableDAO.Delete(IdGlosaContable, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
