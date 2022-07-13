using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerClientes()
        {
            ClienteDAO oClienteDAO = new ClienteDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<ClienteDTO> lstClienteDTO = oClienteDAO.ObtenerClientes(IdSociedad.ToString());

            if (lstClienteDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstClienteDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertCliente(ClienteDTO clienteDTO)
        {

            ClienteDAO oClienteDAO = new ClienteDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int resultado = oClienteDAO.UpdateInsertCliente(clienteDTO, IdSociedad.ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdCliente)
        {
            ClienteDAO oClienteDAO = new ClienteDAO();
            List<ClienteDTO> lstClienteDTO = oClienteDAO.ObtenerDatosxID(IdCliente);

            if (lstClienteDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstClienteDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarCliente(int IdCliente)
        {
            ClienteDAO oClienteDAO = new ClienteDAO();
            int resultado = oClienteDAO.Delete(IdCliente);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ConsultarDocumento(int Tipo, string Documento)
        {
            ClienteDAO oClienteDAO = new ClienteDAO();
            string datos = oClienteDAO.ConsultarDocumento(Tipo, Documento);
            if (datos != "Error")
            {
                return datos;
            }
            else
            {
                return "error";
            }
        }
    }
}
