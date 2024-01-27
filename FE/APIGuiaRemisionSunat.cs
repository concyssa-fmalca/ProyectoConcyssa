using DTO;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAO;

namespace FE
{
    public class APIGuiaRemisionSunat
    {

        // Token para Testing
        private readonly string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIx" +
            "IiwianRpIjoiNjdmNWE5M2MxNzY4ZmE2NjRlZWVkMGE3MThhODY5YzA1YmQ1ZDYwZjBmZjJjNTQyMmFiNzY" +
            "yNmVlOTllOWExNDRmMDFlMDgxMzJkYmFmNDkiLCJpYXQiOjE2ODY4NDQ3NTcsIm5iZiI6MTY4Njg0NDc1Ny" +
            "wiZXhwIjoxNzE4NDY3MTU3LCJzdWIiOiI4Iiwic2NvcGVzIjpbXX0.j_VwN_fagbZVNw9gzXyeYDzJBJWUX" +
            "ccmzYinecgljgFYhKrdYZGhLwnVjCcX8Uvv_O3D8Pkt3wkhsYJMOtyts_Nyd9e6Wco6gv8McEutV4KU2zV-" +
            "qg_mOppGYi8W9QdYCkaUsWU5wEuWDZASzNc6hT2EY_g9xCRV-i9CmDS7SZ8l56SIpI0mx2SZum17mO4rrM5e" +
            "GZjVBsM4RMX0rjeujyZjXTbgFXLOJWFu7ThWVt_yHF1jmQ49IBzdooCbQFcFtsjQwkzqWUkYGsPjlH8y7QF2" +
            "lpbiSqz3giMWHui-UtWMUP6FZj0JOo2IIvXmbrGqcCG0otFbNkca55f1cdaCxqTEIH9mErm5PT-k0kdP-cdU" +
            "n-wqBwf_S6bZ5fEZITMAJJqnRWKTCSZQSUB9XN9NI6VFk1zvYz_OhMDs5pb-ZurrNFDik60nj1xRmHvwVCSq" +
            "2ukF5r4TbZcYfLw7poJ1JLxOXL9Hd4NG70VrLlBuN54h0wOSa-oe6Sn6ltNYXT-bzRfk1F-NYm9R5cqkW6tWe" +
            "_IWTsuRA9gcWKdvyHQSwMYUwNajK3VuCZssRyRFO5Luw1UTPYLOxNQWQy0YPSG0cYxNagQlnAkazgW5JytyM" +
            "iB2mnATg35jTWky0vLRoTo8LGwTmnjRV1bcSy6HoJcnA8W3ocxxonozXUBDsnDP06w";

        public ResultadoGRDTO SendGuiaRemision(GRSunatDTO gr,int IdMovimiento, string BaseDatos)
        {

            //var fullpath = "https://apiandes.andessystems.com/api/send-document";
            var fullpath = "https://apiandes.andessystems.com/api/send-gre/v1";
            var myUri = new Uri(fullpath);
            var myWebRequest = WebRequest.Create(myUri);
            var myHttpWebRequest = (HttpWebRequest)myWebRequest;

            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            myHttpWebRequest.Accept = "application/json";

            try
            {
                var options3 = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
                var ConvertDTOJson = JsonSerializer.Serialize(gr, options3);

                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(ConvertDTOJson);

                }

            }
            catch (Exception ex1)
            {
                //Se produjo una excepción
            }


            ResultadoGRDTO resultado =
                    new ResultadoGRDTO();

            try
            {
                var httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    string mensaje_error = "";
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //ResponseDTO jsonResponse = js.Deserialize<ResponseDTO>(result);
                    ResponseDTO jsonResponse = JsonSerializer.Deserialize<ResponseDTO>(result);
                    if (jsonResponse.Success == true)
                    {
                        resultado.Success = jsonResponse.Success;
                        resultado.Ticket = jsonResponse.registro;
                        resultado.Message = jsonResponse.information;
                        MovimientoDAO mov = new MovimientoDAO();
                        mov.GuardarTicketUpdateEstadoGuia(IdMovimiento, resultado.Ticket, BaseDatos);
                        var respuesta = RespuestaConsultaTicket(resultado.Ticket,gr);
                        if (respuesta.Success)
                        {
                           
                            resultado.DetalleAnexo = respuesta.DetalleAnexo;
                            mov.UpdateEstadoGuia(IdMovimiento,BaseDatos, ref mensaje_error);
                        }
                        resultado.Message = respuesta.Message;

                        if (respuesta.Message != null && respuesta.Message.Contains("El comprobante fue registrado previamente con otros datos"))
                        {
                            mov.UpdateEstadoGuia(IdMovimiento,BaseDatos, ref mensaje_error);
                        }
                        
                        if (respuesta.DetalleAnexo != null)
                        {
                            resultado.DetalleAnexo = respuesta.DetalleAnexo;
                        }
                        
                    }
                    else
                    {
                        resultado.Success = jsonResponse.Success;
                        resultado.Message = jsonResponse.information;

                        if (jsonResponse.error != null)
                        {
                            resultado.Message = jsonResponse.error.numError +"-"+ jsonResponse.error.desError;
                            if (jsonResponse.error.numError.Contains("1033"))
                            {
                                MovimientoDAO mov = new MovimientoDAO();
                                var TicketObtenido = mov.ObtenerTicketGuia(IdMovimiento, BaseDatos);
                                if (TicketObtenido != "")
                                {
                                    var respuesta = RespuestaConsultaTicket(TicketObtenido, gr);
                                    if (respuesta.Success)
                                    {
                                        resultado.Message = respuesta.Message;
                                        resultado.Success = respuesta.Success;
                                        resultado.DetalleAnexo = respuesta.DetalleAnexo;
                                        mov.UpdateEstadoGuia(IdMovimiento,  BaseDatos, ref mensaje_error);
                                    }
                                }
                            }
                        }
                        //resultado.Message = jsonResponse.information[0].message;
                        //resultado.Result = DTO.Global.eResultado.Error;
                    }
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream())
                          .ReadToEnd();
                //resultado.Result = DTO.Global.eResultado.Error;
                resultado.Message = wex.Message;

            }
            catch (Exception ex)
            {
                //resultado.Result = DTO.Global.eResultado.Error;
                resultado.Message = ex.Message;
            }

            return resultado;
        }


        private string ConsultaTicket(string Ticket)
        {
            Thread.Sleep(5000);
            var fullpath = "https://apiandes.andessystems.com/ApiSUNAT/api/send-gre/v1/checkTicket/"+ Ticket;
            var myUri = new Uri(fullpath);
            var myWebRequest = WebRequest.Create(myUri);
            var myHttpWebRequest = (HttpWebRequest)myWebRequest;

            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            myHttpWebRequest.Accept = "application/json";

            var resultado = "";

            try
            {
                var httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    //ResultadoTicket jsonResponse = JsonSerializer.Deserialize<ResultadoTicket>(result);
                    resultado = result;
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                resultado = wex.Message;

            }
            return resultado;
        }

        public ResultadoGRDTO RespuestaConsultaTicket(string Ticket, GRSunatDTO gr)
        {
            ResultadoGRDTO resultado = new ResultadoGRDTO();

            var resultTicket = ConsultaTicket(Ticket);

            if (resultTicket.Contains("(500) Internal Server Error"))
            {
                resultado.Message = "500-Se encontro error al consultar ticket";
                resultado.Success = false;
                return resultado;
            }

            if (resultTicket.Contains("false"))
            {

                ResultadoTicketError respuesta1 = JsonSerializer.Deserialize<ResultadoTicketError>(resultTicket);
                //resultado.Success = false;
                resultado.Message = respuesta1.error.desError;
                resultado.Success = false;
            }
            else
            {
                ResultadoTicket respuesta1 = JsonSerializer.Deserialize<ResultadoTicket>(resultTicket);
                //resultado.Success = true;
                //resultado.Message = jsonResponse.information[0].message;
                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                resultado.FilesMessage = GenerateFiles(respuesta1.pdf, respuesta1.xml, gr, ref lstAnexoDTO);
                resultado.Success = respuesta1.Success;
                resultado.Message = respuesta1.message;
                resultado.DetalleAnexo = lstAnexoDTO;
            }

            return resultado;
        }



        private string GenerateFiles(string pdf1,string xml1, GRSunatDTO gr, ref List<AnexoDTO> lstAnexoDTO)
        {

            string pdf = pdf1;
            string xml = xml1;

            try
            {
                //C:\Users\Dell\Documents\GitHub\ProyectoConcyssa\ConcyssaWeb\wwwroot\Archivos
                File.WriteAllBytes(@"C:\SMC\Binario\ProyectoConcyssa\wwwroot\Archivos\" + gr.N_DOC + ".pdf", Convert.FromBase64String(pdf1));
                File.WriteAllBytes(@"C:\SMC\Binario\ProyectoConcyssa\wwwroot\Archivos\" + gr.N_DOC + ".xml", Convert.FromBase64String(xml1));

                //File.WriteAllBytes(@"C:\Users\Dell\Documents\GitHub\ProyectoConcyssa\ConcyssaWeb\wwwroot\Archivos\" + gr.N_DOC + ".pdf", Convert.FromBase64String(pdf1));
                //File.WriteAllBytes(@"C:\Users\Dell\Documents\GitHub\ProyectoConcyssa\ConcyssaWeb\wwwroot\Archivos\" + gr.N_DOC + ".xml", Convert.FromBase64String(xml1));

                lstAnexoDTO.Add(new AnexoDTO
                {
                    ruta = @"..\Archivos\" + gr.N_DOC + ".pdf",
                    NombreArchivo = gr.N_DOC + ".pdf"
                });
                lstAnexoDTO.Add(new AnexoDTO
                {
                    ruta = @"..\Archivos\" + gr.N_DOC + ".xml",
                    NombreArchivo = gr.N_DOC + ".xml"
                });
                return "OK";
            }
            catch (Exception ex)
            {
                
                return ex.Message;
            }
        }

    }
}