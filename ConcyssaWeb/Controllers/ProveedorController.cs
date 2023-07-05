using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public string ObtenerProveedores()
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerProveedores(IdSociedad.ToString());
            if (lstProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProveedorDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertProveedor(ProveedorDTO proveedorDTO)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            MovimientoDAO oMovimientoDAO = new MovimientoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oProveedorDAO.UpdateInsertProveedor(proveedorDTO, IdSociedad.ToString());
            if (resultado != 0)
            {

                /*INSERTAR ANEXO*/
                if (proveedorDTO.AnexoDetalle != null)
                {
                    for (int i = 0; i < proveedorDTO.AnexoDetalle.Count; i++)
                    {
                        proveedorDTO.AnexoDetalle[i].ruta = "/Anexos/" + proveedorDTO.AnexoDetalle[i].NombreArchivo;
                        proveedorDTO.AnexoDetalle[i].IdSociedad = IdSociedad;
                        proveedorDTO.AnexoDetalle[i].Tabla = "SociosNegocio";
                        proveedorDTO.AnexoDetalle[i].IdTabla = resultado;
                        string mensaje_error = "error";
                        oMovimientoDAO.InsertAnexoMovimiento(proveedorDTO.AnexoDetalle[i], ref mensaje_error);

                    }
                }
            }

            return resultado;

        }


        public string ObtenerDatosxIDNuevo(int IdProveedor)
        {
            string mensaje_error = "";
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            ProveedorDTO oProveedorDTO = new ProveedorDTO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            oProveedorDTO = oProveedorDAO.ObtenerDatosxIDNuevo(IdProveedor);
            if (oProveedorDTO != null)
            {
                //oDataTableDTO.sEcho = 1;
                //oDataTableDTO.iTotalDisplayRecords = lstProveedorDetalleDTO.Count;
                //oDataTableDTO.iTotalRecords = lstProveedorDetalleDTO.Count;
                //oDataTableDTO.aaData = (lstProveedorDetalleDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oProveedorDTO);

            }
            else
            {
                return mensaje_error;

            }
        }
        public string ObtenerDatosxID(int IdProveedor)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerDatosxID(IdProveedor);

            if (lstProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProveedorDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarProveedor(int IdProveedor)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.Delete(IdProveedor);
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
        public int EliminarAnexo(int IdAnexo)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.DeleteAnexo(IdAnexo);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }
        public int InsertRubroProveedor_X_Provedor(RubroXProveedorDTO oRubroXProveedorDTO)
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.InsertRubroProveedor_X_Provedor(oRubroXProveedorDTO,IdUsuario);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }
        public string ListarRubroProveedor_X_Provedor(int IdProveedor)
        {
            string mensaje_error = "";
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            List<RubroXProveedorDTO> lstRubroXProveedorDTO = oProveedorDAO.ListarRubroProveedor_X_Provedor(IdProveedor, ref mensaje_error);

            if (lstRubroXProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstRubroXProveedorDTO);
            }
            else
            {
                return mensaje_error;
            }
        }
        public int EliminarRubroProveedor_X_Provedor(int Id, int IdUsuario)
        {
            IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.EliminarRubroProveedor_X_Provedor(Id,IdUsuario);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
