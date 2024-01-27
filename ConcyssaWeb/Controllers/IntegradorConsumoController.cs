using DAO;
using DTO;
using ISAP;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConcyssaWeb.Controllers
{
    public class IntegradorConsumoController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
        public IActionResult Listado2()
        {
            return View();
        }

        public string ListarMovimientosKardex(int IdObra, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerAlmacenxIdObra(IdObra,BaseDatos,ref mensaje_error);

            List<KardexDTO> lstKardex = new List<KardexDTO>();

            for (int i = 0; i < lstAlmacenDTO.Count; i++)
            {
                lstKardex.AddRange(ObtenerDatosKardex(0, lstAlmacenDTO[0].IdAlmacen, FechaInicio,  FechaTermino, ClaseArticulo, TipoArticulo,false));
            }

            return JsonConvert.SerializeObject(lstKardex);
        }

        public List<KardexDTO> ObtenerDatosKardex(int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo,bool SoloNoEnviadas)
        {
            string mensaje_error = "";
            KardexDAO oKardexDAO = new KardexDAO();
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            List<KardexDTO> lstKardexDTO = new List<KardexDTO>();
 
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
                List<KardexDTO> lstArticulosKardex = new List<KardexDTO>();
                lstArticulosKardex = oKardexDAO.ObtenerArticulosEnKardex(IdAlmacen, FechaInicio, FechaTermino,BaseDatos,ref mensaje_error);
                List<int> ArticulosObtenidos = new List<int>();
                List<int> ArticulosExistentes = new List<int>();
                List<KardexDTO> lstArticulosNoEncontrados = new List<KardexDTO>();

                lstArticuloDTO = oArticuloDAO.ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(IdSociedad, IdAlmacen, TipoArticulo,BaseDatos,ref mensaje_error, 1);
                

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
                    lstKardexDTO.AddRange(oKardexDAO.ObtenerKardexMigración(IdSociedad, ArticulosObtenidos[i], IdAlmacen, FechaInicio, FechaTermino, SoloNoEnviadas,BaseDatos,ref mensaje_error));
                }
            return lstKardexDTO;

        }

        public string ObtenerListaDatosTrabajo()
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            List<ListaTrabajoDTO> lstListaTrabajoDTO = oIntegradorConsumoDAO.ObtenerListaDatosTrabajo(IdUsuario,BaseDatos);
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
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();
            int resultado = oIntegradorConsumoDAO.ObtenerGrupoCreacionEnviarSap(BaseDatos);

            return resultado;
        }

        public int MoverKardexAEnviarSapConsumo(int IdObra, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo,int GrupoCreacion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int respuesta = 0;
            string mensaje_error = "";
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerAlmacenxIdObra(IdObra,BaseDatos,ref mensaje_error);

            List<KardexDTO> lstKardex = new List<KardexDTO>();

            for (int i = 0; i < lstAlmacenDTO.Count; i++)
            {
                lstKardex.AddRange(ObtenerDatosKardex(0, lstAlmacenDTO[0].IdAlmacen, FechaInicio, FechaTermino, ClaseArticulo, TipoArticulo,true));
            }
            List<int> IdsValidas = new List<int>();

            if (lstKardex.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < lstKardex.Count; i++)
            {
                if (lstKardex[i].DocEntrySap == 0)
                {
                    IdsValidas.Add(lstKardex[i].IdKardex);
                }
            }
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();
            for (int i = 0; i < IdsValidas.Count; i++)
            {
                respuesta += oIntegradorConsumoDAO.CopiarKardexEnviarSapConsumo(IdsValidas[i], GrupoCreacion, IdUsuario,BaseDatos);
            }

            return respuesta;

        }

        public string ListarEnviarSapConsumo(int GrupoCreacion)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();

            List<CuentaConsumo> lstCuentaConsumo = new List<CuentaConsumo>();

            List<IntegradorConsumoDTO> lstIntegradorConsumoDTO = oIntegradorConsumoDAO.ListarEnviarSapConsumo(GrupoCreacion,BaseDatos,ref mensaje_error);
            if (lstIntegradorConsumoDTO.Count >= 0 && mensaje_error.Length == 0)
            {

                return JsonConvert.SerializeObject(lstIntegradorConsumoDTO);

            }
            else
            {
                return mensaje_error;

            }
        }

        public string ObtenerMontoDeCuentas(int GrupoCreacion)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();

            List<CuentaConsumo> lstCuentaConsumo = new List<CuentaConsumo>();

            List<IntegradorConsumoDTO> lstIntegradorConsumoDTO = oIntegradorConsumoDAO.ListarEnviarSapConsumo(GrupoCreacion,BaseDatos,ref mensaje_error);
            if (lstIntegradorConsumoDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                lstCuentaConsumo = lstIntegradorConsumoDTO.GroupBy(t => t.CuentaConsumo).Select(g => new CuentaConsumo
                {
                    NumeroCuenta = g.Key,
                    Monto = g.Sum(t => t.CantidadBase * t.PrecioRegistro)
                }).ToList();

                DivisionDAO divisionDAO = new DivisionDAO();

                DivisionDTO divisionDTO = divisionDAO.ObtenerDivisionxIdAlmacen(lstIntegradorConsumoDTO[0].IdAlmacen,BaseDatos,ref mensaje_error);

                decimal MontoTotal = 0;

                for (int i = 0; i < lstCuentaConsumo.Count; i++)
                {
                    MontoTotal += lstCuentaConsumo[i].Monto;
                }

                lstCuentaConsumo.Add(new CuentaConsumo
                {
                    NumeroCuenta = divisionDTO.CuentaContableInv,
                    Monto = MontoTotal
                });


                return JsonConvert.SerializeObject(lstCuentaConsumo);

            }
            else
            {
                return mensaje_error;

            }
        }

        public ObraDTO ObtenerIdObraGrupoCreacion(int GrupoCreacion)
        {
            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();
            ObraDTO oObraDTO = new ObraDTO();
            List<IntegradorConsumoDTO> lstIntegradorConsumoDTO = oIntegradorConsumoDAO.ListarEnviarSapConsumo(GrupoCreacion,BaseDatos,ref mensaje_error);
            if (lstIntegradorConsumoDTO.Count >= 0 && mensaje_error.Length == 0)
            {

                ObraDAO oObraDAO = new ObraDAO();
                oObraDTO = oObraDAO.ObtenerObraxIdAlmacen(lstIntegradorConsumoDTO[0].IdAlmacen,BaseDatos,ref mensaje_error);
                return oObraDTO;

            }
            else
            {
                return oObraDTO;

            }
        }

        public string ProcesarIntegracion(int GrupoCreacion,DateTime FechaContabilizacion,List<CuentaConsumoSAP> Cuentas)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string respuesta = ConexionSAP.Conectar();
            ObraDTO oObraDTO = ObtenerIdObraGrupoCreacion(GrupoCreacion);

            string respuestaSAP = ".";

            for (int i = 0; i < Cuentas.Count; i++)
            {
                if (Cuentas[i].NumeroCuenta is null)
                {
                    return "Hay Centros de Costo sin asignar Cuenta contable, asignelas e intente otra vez";
                }
               
            }
            string mensaje_error = "";

            ConfiguracionSociedadDAO oConfiguracionSociedadDAO = new ConfiguracionSociedadDAO();
            var configuracion = oConfiguracionSociedadDAO.ObtenerConfiguracionSociedad(1, BaseDatos, ref mensaje_error);
            string BaseDatosSAP = configuracion[0].NombreBDSAP;


            try
            {

                if (respuesta == "true")
                {
                    ConexionSAP conexion = new ConexionSAP();
                    respuestaSAP = conexion.AddAsientoContable(oObraDTO.CodProyecto, Cuentas, GrupoCreacion,FechaContabilizacion,BaseDatos,BaseDatosSAP,ref mensaje_error);
                    
                    ConexionSAP.DesconectarSAP();
                }
                else
                {
                    respuestaSAP = "Credenciales no Valida para Conectar a SAP";
                }
            }
            catch (Exception e)
            {
                respuestaSAP = e.Message.ToString();
            }



            return respuestaSAP;

        }

        public int ValidarEnvioSap(int GrupoCreacion)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            IntegradorConsumoDAO oIntegradorConsumoDAO = new IntegradorConsumoDAO();
            int respuesta = 0;
            respuesta = oIntegradorConsumoDAO.ValidarEnvioSap(GrupoCreacion,BaseDatos);
            return respuesta;
        }

    }
}
