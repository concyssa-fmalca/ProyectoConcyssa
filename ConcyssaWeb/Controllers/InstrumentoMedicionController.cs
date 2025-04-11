using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class InstrumentoMedicionController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string UpdateInsertInstrumentoMedicion(InstrumentoMedicionDTO oInstrumentoMedicionDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            oInstrumentoMedicionDTO.IdSociedad = IdSociedad;
            int respuesta = oInstrumentoMedicionDAO.UpdateInsertInstrumentoMedicion(oInstrumentoMedicionDTO,BaseDatos,ref mensaje_error,IdUsuario);

            if (respuesta > 0)
            {
                if (oInstrumentoMedicionDTO.AnexoDetalle != null)
                {
                    for (int i = 0; i < oInstrumentoMedicionDTO.AnexoDetalle.Count; i++)
                    {
                        if (!oInstrumentoMedicionDTO.AnexoDetalle[i].web)
                        {
                            oInstrumentoMedicionDTO.AnexoDetalle[i].ruta = "/Anexos/" + oInstrumentoMedicionDTO.AnexoDetalle[i].NombreArchivo;
                        }

                        oInstrumentoMedicionDTO.AnexoDetalle[i].IdSociedad = oInstrumentoMedicionDTO.IdSociedad;
                        oInstrumentoMedicionDTO.AnexoDetalle[i].Tabla = "InstrumentoMedicion";
                        oInstrumentoMedicionDTO.AnexoDetalle[i].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oInstrumentoMedicionDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
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
                    return "1";
                }
                else
                {
                    return "error";
                }
            }
        }

        public string ObtenerInstrumentoMedicion()
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<InstrumentoMedicionDTO> lstInstrumentoMedicionDTO = oInstrumentoMedicionDAO.ObtenerInstrumentoMedicion(IdSociedad,BaseDatos,ref mensaje_error);
            if (lstInstrumentoMedicionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstInstrumentoMedicionDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerDatosxID(int IdInstrumentoMedicion)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            List<InstrumentoMedicionDTO> lstInstrumentoMedicionDTO = oInstrumentoMedicionDAO.ObtenerDatosxID(IdInstrumentoMedicion,BaseDatos,ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oInstrumentoMedicionDAO.ObtenerAnexoInstMedicion(IdInstrumentoMedicion,BaseDatos,ref mensaje_error);
                lstInstrumentoMedicionDTO[0].AnexoDetalle = new AnexoDTO[lstAnexoDTO.Count()];
                for (int i = 0; i < lstAnexoDTO.Count; i++)
                {
                    lstInstrumentoMedicionDTO[0].AnexoDetalle[i] = lstAnexoDTO[i];
                }
            }



            if (lstInstrumentoMedicionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstInstrumentoMedicionDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int Eliminar(int IdInstrumentoMedicion)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            int resultado = oInstrumentoMedicionDAO.Delete(IdInstrumentoMedicion,IdUsuario,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
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

        public string UpdateInsertInstrumentoMedicionDetalle(InstrumentoMedicionDetalleDTO oInstrumentoMedicionDetalleDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            int respuesta = oInstrumentoMedicionDAO.UpdateInsertInstrumentoMedicionDetalle(oInstrumentoMedicionDetalleDTO,BaseDatos,ref mensaje_error, IdUsuario);

          


            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }
        }

        public string ObtenerInstrumentoMedicionDetalle(int IdInstrumentoMedicion)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            List<InstrumentoMedicionDetalleDTO> lstInstrumentoMedicionDetalleDTO = oInstrumentoMedicionDAO.ObtenerInstrumentoMedicionDetalle(IdInstrumentoMedicion,BaseDatos,ref mensaje_error);
           
            return JsonConvert.SerializeObject(lstInstrumentoMedicionDetalleDTO);
            
        }

        public string ObtenerDatosDetallexID(int IdInstrumentoMedicionDetalle)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            List<InstrumentoMedicionDetalleDTO> lstInstrumentoMedicionDetalleDTO = oInstrumentoMedicionDAO.ObtenerDatosDetallexID(IdInstrumentoMedicionDetalle,BaseDatos,ref mensaje_error);
         

            if (lstInstrumentoMedicionDetalleDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstInstrumentoMedicionDetalleDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int EliminarDetalle(int IdInstrumentoMedicionDetalle)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            int resultado = oInstrumentoMedicionDAO.DeleteDetalle(IdInstrumentoMedicionDetalle, IdUsuario,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public string UpdateInsertInstrumentoMedicionDetalleDoc(InstrumentoMedicionDetalleDocDTO oInstrumentoMedicionDetalleDocDTO)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();
            int respuesta = oInstrumentoMedicionDAO.UpdateInsertInstrumentoMedicionDetalleDoc(oInstrumentoMedicionDetalleDocDTO,BaseDatos,ref mensaje_error, IdUsuario);

            if (respuesta > 0)
            {
                if (oInstrumentoMedicionDetalleDocDTO.AnexoDetalle != null)
                {

                        oInstrumentoMedicionDetalleDocDTO.AnexoDetalle[0].ruta = "/Anexos/" + oInstrumentoMedicionDetalleDocDTO.AnexoDetalle[0].NombreArchivo;
                        oInstrumentoMedicionDetalleDocDTO.AnexoDetalle[0].IdSociedad = IdSociedad;
                        oInstrumentoMedicionDetalleDocDTO.AnexoDetalle[0].Tabla = "InstrumentoMedicionDetalleDocs";
                        oInstrumentoMedicionDetalleDocDTO.AnexoDetalle[0].IdTabla = respuesta;

                        oMovimimientoDAO.InsertAnexoMovimiento(oInstrumentoMedicionDetalleDocDTO.AnexoDetalle[0],BaseDatos,ref mensaje_error);
                    
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
                    return "1";
                }
                else
                {
                    return "error";
                }
            }
        }

        public string ObtenerInstrumentoMedicionDetalleDoc(int IdInstrumentoMedicionDetalle)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            
            List<InstrumentoMedicionDetalleDocDTO> lstInstrumentoMedicionDetalleDocDTO = oInstrumentoMedicionDAO.ObtenerInstrumentoMedicionDetalleDoc(IdInstrumentoMedicionDetalle,BaseDatos,ref mensaje_error);
            if (lstInstrumentoMedicionDetalleDocDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstInstrumentoMedicionDetalleDocDTO);
            }
            else
            {
                return mensaje_error;
            }
        }


        public string ObtenerDatosDetalleDocxID(int IdInstrumentoMedicionDetalleDoc)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            List<InstrumentoMedicionDetalleDocDTO> lstInstrumentoMedicionDetalleDocDTO = oInstrumentoMedicionDAO.ObtenerDatosDetalleDocxID(IdInstrumentoMedicionDetalleDoc,BaseDatos,ref mensaje_error);
            if (mensaje_error.ToString().Length == 0)
            {
                List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
                lstAnexoDTO = oInstrumentoMedicionDAO.ObtenerAnexoInstMedicionDetalleDoc(IdInstrumentoMedicionDetalleDoc,BaseDatos,ref mensaje_error);
                lstInstrumentoMedicionDetalleDocDTO[0].AnexoDetalle = new AnexoDTO[lstAnexoDTO.Count()];
                for (int i = 0; i < lstAnexoDTO.Count; i++)
                {
                    lstInstrumentoMedicionDetalleDocDTO[0].AnexoDetalle[i] = lstAnexoDTO[i];
                }
            }



            if (lstInstrumentoMedicionDetalleDocDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstInstrumentoMedicionDetalleDocDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        public int EliminarDetalleDoc(int IdInstrumentoMedicionDetalleDoc)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            InstrumentoMedicionDAO oInstrumentoMedicionDAO = new InstrumentoMedicionDAO();
            int resultado = oInstrumentoMedicionDAO.DeleteDetalleDoc(IdInstrumentoMedicionDetalleDoc, IdUsuario,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
    }
}
