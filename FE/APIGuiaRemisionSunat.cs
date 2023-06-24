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

        //private readonly string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIx" +
        //        "IiwianRpIjoiZDU2MDRlODdkYjE3MmEyODI0NzA5NmEzNTU5NmNmYzVjYTlhMTR" +
        //        "hNDgyY2MxYjg4ODUxNWE3YzA1YTdkMTI0N2Y5NDRkZGZmMTI1MjI3YzkiLCJpYX" +
        //        "QiOjE2MzgzMTEyNTMsIm5iZiI6MTYzODMxMTI1MywiZXhwIjoxNjY5ODQ3MjUzLC" +
        //        "JzdWIiOiIxIiwic2NvcGVzIjpbXX0.YZlzx8DwsMUyN5N7E8S14m7PD2HWwc1yB6" +
        //        "HUgiLyDv-_ll-xUWTUM68RkjOSNL2v_E26_803RAdwzJj1FXkz1Fs_pLRMd_YoNJ" +
        //        "S4crvqsiQWNkxlMcLqJbALKk-j6vKLbArLkzE4V8CYUpOpritNVno0mhED2D-WKt" +
        //        "iVKzeQXJYTxupsR5rmUsmk7erjBLdQPKGmFn6iJSaxOWg67oYVrMMcs9DbKSwxbe" +
        //        "E4lIY5sKKfLuaPhyECNDKxKpqltc4BSEhvrzmmMP2xr4HoyVDkEtsKgTCSvPgscs" +
        //        "6dFO_OFzxki4wFS_9cFg5GhclGS0sueXwyIjmpLrcrzaA288A9rEPPfORwWciynQ" +
        //        "eKcYkgN_VPgjeYxR4-i1QbOF4cZc03fphDDQrhAJF_UXi2-VswBHKZRmvqX9l8ZQ" +
        //        "c6sLLa7-EIMqdYXAUouno9ejHScDQVlU1_EmDPVGtJYjSsc1FIiv6fM7wdnxZZLY" +
        //        "nvfAnbK0aYncpoq8VlhR32_w9um0CJhipNP-X0c9ZxYv89gSeNcXhMs39ZRRv3E7" +
        //        "DCRTbcqjf3Fz5LdVONDfsB8mZF2y8fMEOKEJIK1PERUOL9F9UpzAuHjWkZSlSUWB" +
        //        "vMiEYHxrxSHUP2lKM3FADHwvM5O5dWOGx-W70KEIynwMUBx1ieyKy-PCtY2iZuh8" +
        //        "uBjA4Gud4";


        //Token para producción
        //private readonly string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIx" +
        //    "IiwianRpIjoiZjVjYmQwNGYzNzY5NzdhYmI5ODhhMjc1MDNjNGQxMWJiNjlkOGRlMGJhMGVhY2ZjZjU3Yzc" +
        //    "5ZmU0M2Y2MzUwMzA4YjkwMGE1ZWU2YzliMDEiLCJpYXQiOjE2NDMzOTA3MTgsIm5iZiI6MTY0MzM5MDcxOC" +
        //    "wiZXhwIjoxNjc0OTI2NzE4LCJzdWIiOiI4Iiwic2NvcGVzIjpbXX0.SAdqb5Ky5ASPPFgklyfj3LM_PY1wP" +
        //    "r7TEjp0Tlhy8uQUGgV_ToNaDkyvfoDQVg3mzkB-seG79bzfEXqZS2FZ0yIHNgTnrMrJRoG_Q6vqVULZxfT0" +
        //    "gufKyKq40Lgkk8UgmhJMtGGffAkWZN_PSSwr8__7rHs7SBmEJHAQ5lWovheXqJKwShn18LKhjeasQcZQctX" +
        //    "_uyyTTu1W3bE0N680tx9mVvjT54PHwPcayFcQcJbB9AzV-uLUFt_b10jeC7ZS7U0kjYTWQBAYDh1rck_84y" +
        //    "xB2WDbbzoCSLUg6NycK0r1do8mB97YFJY09dGTbCHi48fuxCU8dQgXPVVVfjcvLFhOQsJwZs4D8cvMBr97J" +
        //    "B1nj9Uoi781RpCT_23hWcycUNINmtgpObhWJvZZjqOWGUW593P3nzr_6djScdm3nR2zuCLnKdStDWGXIMj9" +
        //    "bUzLQEg5KA-GSWb-O68UWaxTMBGre7F2tXIUONGWpCjJu9TXJ9QY0e_oULbK3lP-mtwcWw92LoZjTmeTyor" +
        //    "0s-XoFsq1i1_9UglGPvbmqO6o8Fzwc2P0ZtlCXsHYw-WQ0PIDLN4hyOhi8HUT8pWPJf-pINAf-xI4raWMJm" +
        //    "tPqS8DxcxiRcBPhAorVfkXZj_PWSJM_GGeZKbC0m4eGl1m-zeVDahSiD8F8AJ1NGQfkeGAs4o";


        public ResultadoGRDTO SendGuiaRemision(GRSunatDTO gr)
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

                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //ResponseDTO jsonResponse = js.Deserialize<ResponseDTO>(result);
                    ResponseDTO jsonResponse = JsonSerializer.Deserialize<ResponseDTO>(result);
                    if (jsonResponse.Success == true)
                    {
                        
                        resultado.Success = jsonResponse.Success;
                        resultado.Ticket = jsonResponse.registro;
                        resultado.Message = jsonResponse.information;

                        var resultTicket = ConsultaTicket(resultado.Ticket);

                        if (resultTicket.Contains("500"))
                        {
                            resultado.Message = "Se envio la guia pero se detecto error al consultar ticket";
                            return resultado;
                        }
               

                        if (resultTicket.Contains("false"))
                        {
  
                            ResultadoTicketError respuesta1 = JsonSerializer.Deserialize<ResultadoTicketError>(resultTicket);
                            //resultado.Success = false;
                            resultado.Message = respuesta1.error.desError;
                        }
                        else
                        {
                            ResultadoTicket respuesta1 = JsonSerializer.Deserialize<ResultadoTicket>(resultTicket);
                            //resultado.Success = true;
                            //resultado.Message = jsonResponse.information[0].message;
                            List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                            resultado.FilesMessage = GenerateFiles(respuesta1.pdf, respuesta1.xml, gr, ref lstAnexoDTO);
                            resultado.DetalleAnexo = lstAnexoDTO;
                        }

                    }
                    else
                    {
                        resultado.Success = jsonResponse.Success;
                        resultado.Message = jsonResponse.information;

                        if (jsonResponse.error != null)
                        {
                            resultado.Message = jsonResponse.error.desError;
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


        private string GenerateFiles(string pdf1,string xml1, GRSunatDTO gr, ref List<AnexoDTO> lstAnexoDTO)
        {

            string pdf = pdf1;
            string xml = xml1;

            try
            {
                
                File.WriteAllBytes(@"C:\SMC\Binario\ProyectoConcyssa\wwwroot\Archivos\" + gr.N_DOC + ".pdf", Convert.FromBase64String(pdf1));
                File.WriteAllBytes(@"C:\SMC\Binario\ProyectoConcyssa\wwwroot\Archivos\" + gr.N_DOC + ".xml", Convert.FromBase64String(xml1));

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