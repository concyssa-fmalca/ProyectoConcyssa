using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class IntegradorConsumoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ListarMovimientosKardex(int IdObra, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo)
        {
            string mensaje_error = "";
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerAlmacenxIdObra(IdObra,ref mensaje_error);

            List<KardexDTO> lstKardex = new List<KardexDTO>();

            for (int i = 0; i < lstAlmacenDTO.Count; i++)
            {
                lstKardex.AddRange(ObtenerDatosKardex(0, lstAlmacenDTO[0].IdAlmacen, FechaInicio,  FechaTermino, ClaseArticulo, TipoArticulo));
            }

            return JsonConvert.SerializeObject(lstKardex);
        }

        public List<KardexDTO> ObtenerDatosKardex(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo)
        {
            string mensaje_error = "";
            KardexDAO oKardexDAO = new KardexDAO();

            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            List<KardexDTO> lstKardexDTO = new List<KardexDTO>();
 
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
                List<KardexDTO> lstArticulosKardex = new List<KardexDTO>();
                lstArticulosKardex = oKardexDAO.ObtenerArticulosEnKardex(IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error);
                List<int> ArticulosObtenidos = new List<int>();
                List<int> ArticulosExistentes = new List<int>();
                List<KardexDTO> lstArticulosNoEncontrados = new List<KardexDTO>();

                lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, TipoArticulo, ref mensaje_error, 1);
                

                for (int i = 0; i < lstArticulosKardex.Count; i++)
                {
                    for (int j = 0; j < lstArticuloDTO.Count; j++)
                    {
                        if (lstArticulosKardex[i].IdArticulo == lstArticuloDTO[j].IdArticulo)
                        {
                            ArticulosObtenidos.Add(lstArticuloDTO[j].IdArticulo);
                        }
                    }
                }

                for (int i = 0; i < ArticulosObtenidos.Count; i++)
                {
                    lstKardexDTO.AddRange(oKardexDAO.ObtenerKardex(IdSociedad, ArticulosObtenidos[i], IdAlmacen, FechaInicio, FechaTermino, ref mensaje_error));
                }
            return lstKardexDTO;

        }

        public string ObtenerListaDatosTrabajo()
        {
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<ListaTrabajoDTO> lstListaTrabajoDTO = oIntegradorConsumoDAO.ObtenerListaDatosTrabajo(IdUsuario);
            if (lstListaTrabajoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstListaTrabajoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int ObtenerGrupoCreacionEnviarSap()
        {
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();
            int resultado = oIntegradorConsumoDAO.ObtenerGrupoCreacionEnviarSap();

            return resultado;
        }

        public int MoverKardexAEnviarSapConsumo(int IdObra, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo,int GrupoCreacion)
        {
            int respuesta = 0;
            string mensaje_error = "";
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerAlmacenxIdObra(IdObra, ref mensaje_error);

            List<KardexDTO> lstKardex = new List<KardexDTO>();

            for (int i = 0; i < lstAlmacenDTO.Count; i++)
            {
                lstKardex.AddRange(ObtenerDatosKardex(0, lstAlmacenDTO[0].IdAlmacen, FechaInicio, FechaTermino, ClaseArticulo, TipoArticulo));
            }
            List<int> IdsValidas = new List<int>();

            for (int i = 0; i < lstKardex.Count; i++)
            {
                if (lstKardex[i].DocEntrySap == 0)
                {
                    IdsValidas.Add(lstKardex[i].IdKardex);
                }
            }



            return respuesta;

        }

    }
}
