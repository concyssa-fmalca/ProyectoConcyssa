using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class KardexDAO
    {

        public List<KardexDTO> ObtenerKardex(int IdSociedad,int IdArticulo,int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, string BaseDatos, ref string mensaje_error)
        {
            List<KardexDTO> lstKardexDTO = new List<KardexDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_Kardex", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio.ToString("yyyyMMdd"));
                    da.SelectCommand.Parameters.AddWithValue("@FechaTermino", FechaTermino.ToString("yyyyMMdd"));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        KardexDTO oKardexDTO = new KardexDTO();
                        oKardexDTO.IdKardex = Convert.ToInt32(drd["IdKardex"].ToString());

                        var dd = drd["IdDetalleOPDN"].ToString();
                        oKardexDTO.IdDetalleMovimiento = Convert.ToInt32((drd["IdDetalleMovimiento"].ToString()==""? 0: drd["IdDetalleMovimiento"].ToString()));
                        oKardexDTO.IdDetalleOPDN = Convert.ToInt32((drd["IdDetalleOPDN"].ToString() == "" ? 0 : drd["IdDetalleOPDN"].ToString()));
                        oKardexDTO.IdDetalleOPCH = Convert.ToInt32((drd["IdDetalleOPCH"].ToString() == "" ? 0 : drd["IdDetalleOPCH"].ToString()));
                        oKardexDTO.IdDefinicionGrupoUnidad = Convert.ToInt32(drd["IdDefinicionGrupoUnidad"].ToString());
                        oKardexDTO.CantidadBase = Convert.ToDecimal(drd["CantidadBase"].ToString());
                        oKardexDTO.IdUnidadMedidaBase = Convert.ToInt32(drd["IdUnidadMedidaBase"].ToString());
                        oKardexDTO.PrecioBase = Convert.ToDecimal(drd["PrecioBase"].ToString());
                        oKardexDTO.CantidadRegistro = Convert.ToDecimal(drd["CantidadRegistro"].ToString());
                        oKardexDTO.IdUnidadMedidaRegistro = Convert.ToInt32(drd["IdUnidadMedidaRegistro"].ToString());
                        oKardexDTO.PrecioRegistro = Convert.ToDecimal(drd["PrecioRegistro"].ToString());
                        oKardexDTO.PrecioPromedio = Convert.ToDecimal(drd["PrecioPromedio"].ToString());
                        oKardexDTO.FechaRegistro = Convert.ToDateTime(drd["FechaRegistro"].ToString());
                        oKardexDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oKardexDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oKardexDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oKardexDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oKardexDTO.Saldo = Convert.ToDecimal(drd["Saldo"].ToString());
                        oKardexDTO.DescArticulo = (drd["DescArticulo"].ToString());
                        oKardexDTO.CodigoArticulo = (drd["CodigoArticulo"].ToString());
                        oKardexDTO.DescSerie = (drd["DescSerie"].ToString());
                        oKardexDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oKardexDTO.TipoTransaccion = (drd["TipoTransaccion"].ToString());
                        oKardexDTO.DescUnidadMedidaBase = (drd["DescUnidadMedidaBase"].ToString());
                        oKardexDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oKardexDTO.Comentario = (drd["Comentario"].ToString());
                        oKardexDTO.Modulo = (drd["Modulo"].ToString());
                        oKardexDTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
                        oKardexDTO.TipoDocumentoRef = drd["TipoDocumentoRef"].ToString();
                        oKardexDTO.Cuadrilla = drd["Cuadrilla"].ToString();
                        oKardexDTO.NumRef = drd["NumRef"].ToString();



                        lstKardexDTO.Add(oKardexDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstKardexDTO;
        }

        public List<KardexDTO> ObtenerArticulosEnKardex(int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, string BaseDatos, ref string mensaje_error)
        {
            List<KardexDTO> lstKardexDTO = new List<KardexDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosEnKardex", cn);
   
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
              
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio.ToString("yyyyMMdd"));
                    da.SelectCommand.Parameters.AddWithValue("@FechaTermino", FechaTermino.ToString("yyyyMMdd"));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        KardexDTO oKardexDTO = new KardexDTO();          
                  
                        oKardexDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                    


                        lstKardexDTO.Add(oKardexDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstKardexDTO;
        }


        public ArticuloStockDTO ObtenerArticuloxIdArticuloxIdAlm(int IdArticulo,int IdAlmacen,string BaseDatos, ref string mensaje_error)
        {

            ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerArticuloStockxIdArticuloxIdAlm", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        //ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();
                        oArticuloStockDTO.IdArticuloStock = Convert.ToInt32(drd["IdArticuloStock"].ToString());
                        oArticuloStockDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oArticuloStockDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        oArticuloStockDTO.PrecioPromedio = Convert.ToDecimal(drd["PrecioPromedio"].ToString());
                        //lstArticuloDTO.Add(oArticuloStockDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return oArticuloStockDTO;
        }

        public List<ArticuloStockDTO> ObtenerStockxAlmacen(int IdAlmacen, string BaseDatos, ref string mensaje_error)
        {

            List<ArticuloStockDTO> lstArticuloStockDTO = new List<ArticuloStockDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_InformeInventarioxIdAlmacen", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();
                        oArticuloStockDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oArticuloStockDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oArticuloStockDTO.NombArticulo =(drd["NombArticulo"].ToString());
                        oArticuloStockDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oArticuloStockDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        oArticuloStockDTO.PrecioPromedio = Convert.ToDecimal(drd["PrecioPromedio"].ToString());
                        oArticuloStockDTO.Codigo = (drd["Codigo"].ToString());

                        lstArticuloStockDTO.Add(oArticuloStockDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloStockDTO;
        }

        public List<ArticuloStockDTO> ObtenerStockxAlmacenxTipoProducto(int IdAlmacen,int IdTipoProducto, string BaseDatos, ref string mensaje_error)
        {

            List<ArticuloStockDTO> lstArticuloStockDTO = new List<ArticuloStockDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_InformeInventarioxIdAlmacenConStockTipoArt", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", IdTipoProducto);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();
                        oArticuloStockDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oArticuloStockDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oArticuloStockDTO.NombArticulo = (drd["NombArticulo"].ToString());
                        oArticuloStockDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        oArticuloStockDTO.PrecioPromedio = Convert.ToDecimal(drd["PrecioPromedio"].ToString());
                        oArticuloStockDTO.Codigo = (drd["Codigo"].ToString());
                        oArticuloStockDTO.IdUnidadMedidaInv = int.Parse(drd["IdUnidadMedidaInv"].ToString());

                        lstArticuloStockDTO.Add(oArticuloStockDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloStockDTO;
        }

        public List<KardexDTO> ObtenerKardexMigración(int IdSociedad, int IdArticulo, int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino,bool SoloNoEnviadas, string BaseDatos, ref string mensaje_error)
        {
            List<KardexDTO> lstKardexDTO = new List<KardexDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    string Proc = "SMC_KardexMigracion";
                    cn.Open();
                    if (SoloNoEnviadas)
                    {
                        Proc = "SMC_KardexPendientes";
                    }
                    
                    SqlDataAdapter da = new SqlDataAdapter(Proc, cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio.ToString("yyyyMMdd"));
                    da.SelectCommand.Parameters.AddWithValue("@FechaTermino", FechaTermino.ToString("yyyyMMdd"));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        KardexDTO oKardexDTO = new KardexDTO();
                        oKardexDTO.IdKardex = Convert.ToInt32(drd["IdKardex"].ToString());

                        var dd = drd["IdDetalleOPDN"].ToString();
                        oKardexDTO.IdDetalleMovimiento = Convert.ToInt32((drd["IdDetalleMovimiento"].ToString() == "" ? 0 : drd["IdDetalleMovimiento"].ToString()));
                        oKardexDTO.IdDetalleOPDN = Convert.ToInt32((drd["IdDetalleOPDN"].ToString() == "" ? 0 : drd["IdDetalleOPDN"].ToString()));
                        oKardexDTO.IdDetalleOPCH = Convert.ToInt32((drd["IdDetalleOPCH"].ToString() == "" ? 0 : drd["IdDetalleOPCH"].ToString()));
                        oKardexDTO.IdDefinicionGrupoUnidad = Convert.ToInt32(drd["IdDefinicionGrupoUnidad"].ToString());
                        oKardexDTO.CantidadBase = Convert.ToDecimal(drd["CantidadBase"].ToString());
                        oKardexDTO.IdUnidadMedidaBase = Convert.ToInt32(drd["IdUnidadMedidaBase"].ToString());
                        oKardexDTO.PrecioBase = Convert.ToDecimal(drd["PrecioBase"].ToString());
                        oKardexDTO.CantidadRegistro = Convert.ToDecimal(drd["CantidadRegistro"].ToString());
                        oKardexDTO.IdUnidadMedidaRegistro = Convert.ToInt32(drd["IdUnidadMedidaRegistro"].ToString());
                        oKardexDTO.PrecioRegistro = Convert.ToDecimal(drd["PrecioRegistro"].ToString());
                        oKardexDTO.PrecioPromedio = Convert.ToDecimal(drd["PrecioPromedio"].ToString());
                        oKardexDTO.FechaRegistro = Convert.ToDateTime(drd["FechaRegistro"].ToString());
                        oKardexDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oKardexDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oKardexDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oKardexDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oKardexDTO.Saldo = Convert.ToDecimal(drd["Saldo"].ToString());
                        oKardexDTO.DescArticulo = (drd["DescArticulo"].ToString());
                        oKardexDTO.CodigoArticulo = (drd["CodigoArticulo"].ToString());
                        oKardexDTO.DescSerie = (drd["DescSerie"].ToString());
                        oKardexDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oKardexDTO.TipoTransaccion = (drd["TipoTransaccion"].ToString());
                        oKardexDTO.DescUnidadMedidaBase = (drd["DescUnidadMedidaBase"].ToString());
                        oKardexDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oKardexDTO.Comentario = (drd["Comentario"].ToString());
                        oKardexDTO.Modulo = (drd["Modulo"].ToString());
                        oKardexDTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
                        oKardexDTO.TipoDocumentoRef = drd["TipoDocumentoRef"].ToString();
                        oKardexDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());



                        lstKardexDTO.Add(oKardexDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstKardexDTO;
        }

    }
}
