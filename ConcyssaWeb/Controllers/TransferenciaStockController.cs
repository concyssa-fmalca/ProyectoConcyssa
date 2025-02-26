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
        public IActionResult TransferenciaMasiva()
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
                respuesta = oMovimimientoDAO.RegistrarMovimientoCompleto(oMovimientoDTO, oMovimientoDTO.detalles[0].ValidarIngresoSalidaOAmbos, BaseDatos,ref mensaje_error);
              

                TranferenciaStockFinalController tr = new TranferenciaStockFinalController();
                oMovimientoDTO.ValidarIngresoSalidaOAmbos = 1;
                oMovimientoDTO.IdTipoDocumento = 333;
                tr.UpdateInsertMovimientoFinal(oMovimientoDTO);

            }
            else
            {
                respuesta = oMovimimientoDAO.RegistrarMovimientoCompleto(oMovimientoDTO, oMovimientoDTO.detalles[0].ValidarIngresoSalidaOAmbos, BaseDatos, ref mensaje_error);
               
                if (respuesta > 0)
                {
                  

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

        public string RealizarTransferenciaMasiva(int IdAlmacenOrigen, int IdAlmacenDestino, int IdTipoProducto, int IdSerieSalida, int IdSerieEntrada)
        {

            string mensaje_error = "";
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            KardexDAO oKardexDAO = new KardexDAO();
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloStockDTO> lstArticuloStockDTO = oKardexDAO.ObtenerStockxAlmacenxTipoProducto(IdAlmacenOrigen, IdTipoProducto, BaseDatos, ref mensaje_error);
            
            List<MovimientoDetalleDTO> lstMovimientoDetalleDTO = new List<MovimientoDetalleDTO>();

            AlmacenDAO almacenDAO = new AlmacenDAO();
            List<AlmacenDTO> almacenDTO = almacenDAO.ObtenerDatosxID(IdAlmacenDestino, BaseDatos, ref mensaje_error);

            if(lstArticuloStockDTO.Count == 0)
            {
                return "No se Encontraron Items para Transferir";
            }

            for (int i = 0; i < lstArticuloStockDTO.Count; i++)
            {
                if (lstArticuloStockDTO[i].PrecioPromedio == 0)
                {
                    List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ListarArticulosxSociedadxAlmacenStockxProducto(IdSociedad, lstArticuloStockDTO[i].IdArticulo, IdAlmacenDestino, BaseDatos, ref mensaje_error, 3);
                    if(lstArticuloDTO[0].PrecioPromedio == 0) { return "No se pudo definir el precio para el articulo " + lstArticuloStockDTO[i].NombArticulo; }
                    lstArticuloStockDTO[i].PrecioPromedio = lstArticuloDTO[0].PrecioPromedio;
                }

                MovimientoDetalleDTO oMovimientoDetalleDTO = new MovimientoDetalleDTO();
                oMovimientoDetalleDTO.IdArticulo = lstArticuloStockDTO[i].IdArticulo;
                oMovimientoDetalleDTO.DescripcionArticulo = lstArticuloStockDTO[i].NombArticulo;
                oMovimientoDetalleDTO.IdDefinicionGrupoUnidad = lstArticuloStockDTO[i].IdUnidadMedidaInv;
                oMovimientoDetalleDTO.IdAlmacen = IdAlmacenOrigen;
                oMovimientoDetalleDTO.Cantidad = lstArticuloStockDTO[i].Stock;
                oMovimientoDetalleDTO.Igv = 0;
                oMovimientoDetalleDTO.PrecioUnidadBase = lstArticuloStockDTO[i].PrecioPromedio;
                oMovimientoDetalleDTO.PrecioUnidadTotal = lstArticuloStockDTO[i].PrecioPromedio;
                oMovimientoDetalleDTO.TotalBase = lstArticuloStockDTO[i].Stock * lstArticuloStockDTO[i].PrecioPromedio;
                oMovimientoDetalleDTO.Total = lstArticuloStockDTO[i].Stock * lstArticuloStockDTO[i].PrecioPromedio;
                oMovimientoDetalleDTO.CuentaContable = 1;
                oMovimientoDetalleDTO.IdCentroCosto = 7;
                oMovimientoDetalleDTO.IdAfectacionIgv = 1;
                oMovimientoDetalleDTO.Descuento = 0;
                oMovimientoDetalleDTO.Referencia = "";
                oMovimientoDetalleDTO.IdCuadrilla = 0;
                oMovimientoDetalleDTO.IdResponsable = 0;
                oMovimientoDetalleDTO.TablaOrigen = "";
                oMovimientoDetalleDTO.IdOrigen = 0;

                lstMovimientoDetalleDTO.Add(oMovimientoDetalleDTO);

            }

            MovimientoDTO oMovimientoDTO = new MovimientoDTO();
            oMovimientoDTO.IdAlmacen = IdAlmacenOrigen;
            oMovimientoDTO.IdTipoDocumento = 1339;
            oMovimientoDTO.IdSerie = IdSerieSalida;
            oMovimientoDTO.Correlativo= 0;
            oMovimientoDTO.IdMoneda= 1;
            oMovimientoDTO.TipoCambio= 0;
            oMovimientoDTO.FechaContabilizacion= DateTime.Today;
            oMovimientoDTO.FechaDocumento= DateTime.Today;
            oMovimientoDTO.IdCentroCosto= 7;
            oMovimientoDTO.Comentario="TRANSFERENCIA MASIVA HACIA " + almacenDTO[0].Descripcion;
            oMovimientoDTO.SubTotal= 0;
            oMovimientoDTO.Impuesto= 0;
            oMovimientoDTO.Total= 0;
            oMovimientoDTO.IdCuadrilla= 2582;
            oMovimientoDTO.EntregadoA= 24151;
            oMovimientoDTO.IdTipoDocumentoRef= 14;
            oMovimientoDTO.NumSerieTipoDocumentoRef= "";
            oMovimientoDTO.IdDestinatario= 0;
            oMovimientoDTO.IdMotivoTraslado = 0;
            oMovimientoDTO.IdTransportista= 0;
            oMovimientoDTO.PlacaVehiculo= "";
            oMovimientoDTO.MarcaVehiculo= "";
            oMovimientoDTO.NumIdentidadConductor= "";

            oMovimientoDTO.NombreConductor= "";
            oMovimientoDTO.ApellidoConductor= "";
            oMovimientoDTO.LicenciaConductor= "";
            oMovimientoDTO.TipoTransporte= "";
            oMovimientoDTO.Peso= 0;
            oMovimientoDTO.Bulto= 0;
            oMovimientoDTO.SGI= "";
            oMovimientoDTO.CodigoAnexoLlegada= "";
            oMovimientoDTO.CodigoUbigeoLlegada= "";
            oMovimientoDTO.DistritoLlegada= "";
            oMovimientoDTO.DireccionLlegada= "";
            oMovimientoDTO.IdProveedor= 0;
            oMovimientoDTO.NroRef= "";
            oMovimientoDTO.detalles = lstMovimientoDetalleDTO;

            oMovimientoDTO.IdSociedad = IdSociedad;
            oMovimientoDTO.IdUsuario = IdUsuario;

            SalidaMercanciaController oSalidaMercanciaController = new SalidaMercanciaController();
            oSalidaMercanciaController.UpdateInsertMovimiento(oMovimientoDTO, BaseDatos);

            for (int i = 0; i < lstMovimientoDetalleDTO.Count; i++)
            {
                lstMovimientoDetalleDTO[i].IdAlmacen = IdAlmacenDestino;
                lstMovimientoDetalleDTO[i].IdMovimiento = 0;
            }


            almacenDTO = almacenDAO.ObtenerDatosxID(IdAlmacenOrigen, BaseDatos, ref mensaje_error);
            oMovimientoDTO.IdTipoDocumento = 330;
            oMovimientoDTO.IdAlmacen = IdAlmacenDestino;
            oMovimientoDTO.IdSerie = IdSerieEntrada;
            oMovimientoDTO.Comentario = "TRANSFERENCIA MASIVA DESDE " + almacenDTO[0].Descripcion;

            EntradaMercanciaController entradaMercanciaController = new EntradaMercanciaController();
            entradaMercanciaController.UpdateInsertMovimiento( oMovimientoDTO, BaseDatos);



            return "OK";
        }

    }
}
