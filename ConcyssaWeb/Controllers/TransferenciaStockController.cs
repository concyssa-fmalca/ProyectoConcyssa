using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class TransferenciaStockController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string UpdateInsertMovimiento(MovimientoDTO oMovimientoDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            int ValidarSoloSalida = 0;
            int respuesta = 0;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            oMovimientoDTO.IdSociedad = IdSociedad;
            oMovimientoDTO.IdUsuario = IdUsuario;
            MovimientoDAO oMovimimientoDAO = new MovimientoDAO();

            if (oMovimientoDTO.TranferenciaDirecta == 1)
            {
                respuesta = oMovimimientoDAO.InsertUpdateMovimiento(oMovimientoDTO,BaseDatos,ref mensaje_error);
                if (respuesta > 0)
                {
                    for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                    {
                        oMovimientoDTO.detalles[i].IdMovimiento = respuesta;
                        int respuesta1 = oMovimimientoDAO.InsertUpdateMovimientoDetalle(oMovimientoDTO.detalles[i], oMovimientoDTO.detalles[i].ValidarIngresoSalidaOAmbos,BaseDatos,ref mensaje_error);
                    }


                    if (oMovimientoDTO.AnexoDetalle != null)
                    {
                        for (int i = 0; i < oMovimientoDTO.AnexoDetalle.Count; i++)
                        {
                            oMovimientoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oMovimientoDTO.AnexoDetalle[i].NombreArchivo;
                            oMovimientoDTO.AnexoDetalle[i].IdSociedad = oMovimientoDTO.IdSociedad;
                            oMovimientoDTO.AnexoDetalle[i].Tabla = "Movimiento";
                            oMovimientoDTO.AnexoDetalle[i].IdTabla = respuesta;

                            oMovimimientoDAO.InsertAnexoMovimiento(oMovimientoDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
                        }
                    }

                }


                TranferenciaStockFinalController tr = new TranferenciaStockFinalController();
                oMovimientoDTO.ValidarIngresoSalidaOAmbos = 1;
                oMovimientoDTO.IdTipoDocumento = 333;
                tr.UpdateInsertMovimientoFinal(oMovimientoDTO);

            }
            else
            {
                respuesta = oMovimimientoDAO.InsertUpdateMovimiento(oMovimientoDTO,BaseDatos,ref mensaje_error);
                if (mensaje_error.Length > 0)
                {
                    return mensaje_error;
                }
                if (respuesta > 0)
                {
                    for (int i = 0; i < oMovimientoDTO.detalles.Count; i++)
                    {
                        oMovimientoDTO.detalles[i].IdMovimiento = respuesta;
                        int respuesta1 = oMovimimientoDAO.InsertUpdateMovimientoDetalle(oMovimientoDTO.detalles[i], oMovimientoDTO.detalles[i].ValidarIngresoSalidaOAmbos,BaseDatos,ref mensaje_error);
                    }


                    if (oMovimientoDTO.AnexoDetalle != null)
                    {
                        for (int i = 0; i < oMovimientoDTO.AnexoDetalle.Count; i++)
                        {
                            oMovimientoDTO.AnexoDetalle[i].ruta = "/Anexos/" + oMovimientoDTO.AnexoDetalle[i].NombreArchivo;
                            oMovimientoDTO.AnexoDetalle[i].IdSociedad = oMovimientoDTO.IdSociedad;
                            oMovimientoDTO.AnexoDetalle[i].Tabla = "Movimiento";
                            oMovimientoDTO.AnexoDetalle[i].IdTabla = respuesta;

                            oMovimimientoDAO.InsertAnexoMovimiento(oMovimientoDTO.AnexoDetalle[i],BaseDatos,ref mensaje_error);
                        }
                    }


                    if (oMovimientoDTO.ValidarIngresoSalidaOAmbos == 2) //solo salida
                    {
                        int respuesta2 = oMovimimientoDAO.InsertUpdateTranferenciaPrevia(oMovimientoDTO, respuesta,BaseDatos,ref mensaje_error);
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
                    return mensaje_error;
                }
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

    }
}
