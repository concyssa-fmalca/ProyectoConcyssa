using DTO;
using System.Net;
using System.Text.Json;

namespace FE
{
    public class APIGuiaRemisionSunat
    {

        // Token para Testing
        private readonly string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIx" +
                "IiwianRpIjoiZDU2MDRlODdkYjE3MmEyODI0NzA5NmEzNTU5NmNmYzVjYTlhMTR" +
                "hNDgyY2MxYjg4ODUxNWE3YzA1YTdkMTI0N2Y5NDRkZGZmMTI1MjI3YzkiLCJpYX" +
                "QiOjE2MzgzMTEyNTMsIm5iZiI6MTYzODMxMTI1MywiZXhwIjoxNjY5ODQ3MjUzLC" +
                "JzdWIiOiIxIiwic2NvcGVzIjpbXX0.YZlzx8DwsMUyN5N7E8S14m7PD2HWwc1yB6" +
                "HUgiLyDv-_ll-xUWTUM68RkjOSNL2v_E26_803RAdwzJj1FXkz1Fs_pLRMd_YoNJ" +
                "S4crvqsiQWNkxlMcLqJbALKk-j6vKLbArLkzE4V8CYUpOpritNVno0mhED2D-WKt" +
                "iVKzeQXJYTxupsR5rmUsmk7erjBLdQPKGmFn6iJSaxOWg67oYVrMMcs9DbKSwxbe" +
                "E4lIY5sKKfLuaPhyECNDKxKpqltc4BSEhvrzmmMP2xr4HoyVDkEtsKgTCSvPgscs" +
                "6dFO_OFzxki4wFS_9cFg5GhclGS0sueXwyIjmpLrcrzaA288A9rEPPfORwWciynQ" +
                "eKcYkgN_VPgjeYxR4-i1QbOF4cZc03fphDDQrhAJF_UXi2-VswBHKZRmvqX9l8ZQ" +
                "c6sLLa7-EIMqdYXAUouno9ejHScDQVlU1_EmDPVGtJYjSsc1FIiv6fM7wdnxZZLY" +
                "nvfAnbK0aYncpoq8VlhR32_w9um0CJhipNP-X0c9ZxYv89gSeNcXhMs39ZRRv3E7" +
                "DCRTbcqjf3Fz5LdVONDfsB8mZF2y8fMEOKEJIK1PERUOL9F9UpzAuHjWkZSlSUWB" +
                "vMiEYHxrxSHUP2lKM3FADHwvM5O5dWOGx-W70KEIynwMUBx1ieyKy-PCtY2iZuh8" +
                "uBjA4Gud4";


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

            var fullpath = "https://apiandes.andessystems.com/api/send-document";
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

                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    string json =
                        "{\n\"N_DOC\":\"" + gr.N_DOC + "\"," + "\n" +
                        "\"TIPO_DOC\":\"" + gr.TIPO_DOC + "\"," + "\n" +
                        "\"FECHA\":\"" + gr.FECHA + "\"," + "\n" +
                        "\"RUC\":\"" + gr.RUC + "\"," + "\n" +
                        "\"TIPO_RUC\":\"" + gr.TIPO_RUC + "\"," + "\n" +
                        "\"NOMBRE\":\"" + gr.NOMBRE + "\"," + "\n" +
                        "\"RUC_EMIS\":\"" + gr.RUC_EMIS + "\"," + "\n" +
                        "\"NOMBRE_EMIS\":\"" + gr.NOMBRE_EMIS + "\"," + "\n" +
                        "\"MOT_TRAS\":\"" + gr.MOT_TRAS + "\"," + "\n" +
                        "\"MOT_TRAS_DES\":\"" + gr.MOT_TRAS_DES + "\"," + "\n" +
                        "\"PESO\":" + gr.PESO + "," + "\n" +
                        "\"BULTOS\":" + gr.BULTOS + "," + "\n" +
                        "\"TIPO_TRANS\":\"" + gr.TIPO_TRANS + "\"," + "\n" +
                        "\"FCH_INICIO\":\"" + gr.FCH_INICIO + "\"," + "\n" +
                        "\"RUC_TRANS\":\"" + gr.RUC_TRANS + "\"," + "\n" +
                        "\"NOM_TRANS\":\"" + gr.NOM_TRANS + "\"," + "\n" +
                        "\"PLACA\":\"" + gr.PLACA + "\"," + "\n" +
                        "\"LIC_TRANS\":\"" + gr.LIC_TRANS + "\"," + "\n" +
                        "\"UBIGEO_LLE\":\"" + gr.UBIGEO_LLE + "\"," + "\n" +
                        "\"PUNTO_LLE\":\"" + gr.PUNTO_LLE + "\"," + "\n" +

                        "\"UBIGEO_PAR\":\"" + gr.UBIGEO_PAR + "\"," + "\n" +
                        "\"PUNTO_PAR\":\"" + gr.PUNTO_PAR + "\"," + "\n" +
                        "\"PUERTO\":\"" + gr.PUERTO + "\"," + "\n" +
                        "\"PDF\":\"" + gr.PDF + "\"," + "\n" +
                        "\"ENVIO\":\"" + gr.ENVIO + "\"," + "\n" +
                        "\"WSDL\":\"" + gr.WSDL + "\"," + "\n";

                 

                    var itemsjson = "\"ITEMS\":[" + "\n";
                    foreach (var item in gr.ITEMS)
                    {
                        itemsjson += "{\"CODIGO\":\"" + item.CODIGO + "\"," + "\n" +
                                        "\"DESCRIPCION\":\"" + item.DESCRIPCION + "\"," + "\n" +
                                        "\"TIPOUNI\":\"" + item.TIPOUNI + "\"," + "\n" +
                                        "\"CANTIDAD\":" + item.CANTIDAD.ToString() + "\n},";
                    };



                    json += itemsjson.Remove(itemsjson.Length - 1) + "\n]\n}";

                    streamWriter.Write(json);

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
                        resultado.Message = jsonResponse.information[0].message;
                        //resultado.Result = DTO.Global.eResultado.Correcto;
                        List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                        //Genera archivos PDF y XML
                    
                        resultado.FilesMessage = GenerateFiles(jsonResponse, gr,ref lstAnexoDTO);
                        resultado.DetalleAnexo = lstAnexoDTO;

                    }
                    else
                    {
                        resultado.Message = jsonResponse.information[0].message;
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

        private string GenerateFiles(ResponseDTO jsonResponse, GRSunatDTO gr,ref List<AnexoDTO> lstAnexoDTO)
        {

            string pdf = jsonResponse.information[0].pdf;
            string xml = jsonResponse.information[0].xml;
           
            try
            {
                
                File.WriteAllBytes(@"D:\SmartCode\concyssa\ProyectoConcyssa\ConcyssaWeb\wwwroot\Archivos\" + gr.N_DOC + ".pdf", Convert.FromBase64String(pdf));
                File.WriteAllBytes(@"D:\SmartCode\concyssa\ProyectoConcyssa\ConcyssaWeb\wwwroot\Archivos\" + gr.N_DOC + ".xml", Convert.FromBase64String(xml));

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