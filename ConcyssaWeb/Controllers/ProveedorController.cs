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
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oProveedorDAO.UpdateInsertProveedor(proveedorDTO, IdSociedad.ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

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
    }
}
