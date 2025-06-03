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
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaTermino", FechaTermino);
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
                        oKardexDTO.NombResponsable = (String.IsNullOrEmpty(drd["NombResponsable"].ToString()) ? "-" : drd["NombResponsable"].ToString());



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
              
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaTermino", FechaTermino);
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

        public List<ArticuloStockDTO> ObtenerStockxAlmacen(int IdAlmacen, int TipoArticulo,string BaseDatos, ref string mensaje_error)
        {

            List<ArticuloStockDTO> lstArticuloStockDTO = new List<ArticuloStockDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_InformeInventarioxIdAlmacen", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@TipoArticulo", TipoArticulo);
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
                        oArticuloStockDTO.UnidadMedida = (drd["UnidadMedida"].ToString());
                        oArticuloStockDTO.TipoArticulo = (drd["TipoArticulo"].ToString());

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

        public List<KardexTributario> ObtenerKardexTributario(int IdAlmacen, int Mes, int Anio, string CodOp, string LetraCorrelativo, string BaseDatos, ref string mensaje_error)
        {
            List<KardexTributario> lstKardexDTO = new List<KardexTributario>();
            int numeroFila = 0;
            string Fila = "";
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_KardexTributario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@Mes", Mes);
                    da.SelectCommand.Parameters.AddWithValue("@Anio", Anio);
                    da.SelectCommand.Parameters.AddWithValue("@CodOp", CodOp);
                    da.SelectCommand.Parameters.AddWithValue("@LetraCorrelativo", LetraCorrelativo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        numeroFila++;
                        KardexTributario oKardexDTO = new KardexTributario();
                        Fila = "CAMPO1";
                        oKardexDTO.CAMPO1 = (drd["CAMPO1"].ToString()); Fila = "CAMPO2";
                        oKardexDTO.CAMPO2 = (drd["CAMPO2"].ToString()); Fila = "CAMPO3";
                        oKardexDTO.CAMPO3 = (drd["CAMPO3"].ToString()); Fila = "CAMPO4";
                        oKardexDTO.CAMPO4 = (drd["CAMPO4"].ToString()); Fila = "CAMPO5";
                        oKardexDTO.CAMPO5 = (drd["CAMPO5"].ToString()); Fila = "CAMPO6";
                        oKardexDTO.CAMPO6 = (drd["CAMPO6"].ToString()); Fila = "CAMPO7";
                        oKardexDTO.CAMPO7 = (drd["CAMPO7"].ToString()); Fila = "CAMPO8";
                        oKardexDTO.CAMPO8 = (drd["CAMPO8"].ToString()); Fila = "CAMPO9";
                        oKardexDTO.CAMPO9 = (drd["CAMPO9"].ToString()); Fila = "CAMPO10";
                        oKardexDTO.CAMPO10 = (drd["CAMPO10"].ToString()); Fila = "CAMPO11";
                        oKardexDTO.CAMPO11 = (drd["CAMPO11"].ToString()); Fila = "CAMPO12";
                        oKardexDTO.CAMPO12 = (drd["CAMPO12"].ToString()); Fila = "CAMPO13";
                        oKardexDTO.CAMPO13 = (drd["CAMPO13"].ToString()); Fila = "CAMPO14";
                        oKardexDTO.CAMPO14 = (drd["CAMPO14"].ToString()); Fila = "CAMPO15";
                        oKardexDTO.CAMPO15 = (drd["CAMPO15"].ToString()); Fila = "CAMPO16";
                        oKardexDTO.CAMPO16 = (drd["CAMPO16"].ToString()); Fila = "CAMPO17";
                        oKardexDTO.CAMPO17 = (drd["CAMPO17"].ToString()); Fila = "CAMPO18";
                        oKardexDTO.CAMPO18 = Convert.ToDecimal(drd["CAMPO18"].ToString()); Fila = "CAMPO19";
                        oKardexDTO.CAMPO19 = Convert.ToDecimal(drd["CAMPO19"].ToString()); Fila = "CAMPO20";
                        oKardexDTO.CAMPO20 = Convert.ToDecimal(drd["CAMPO20"].ToString()); Fila = "CAMPO21";
                        oKardexDTO.CAMPO21 = Convert.ToDecimal(drd["CAMPO21"].ToString()); Fila = "CAMPO22";
                        oKardexDTO.CAMPO22 = Convert.ToDecimal(drd["CAMPO22"].ToString()); Fila = "CAMPO23";
                        oKardexDTO.CAMPO23 = Convert.ToDecimal(drd["CAMPO23"].ToString()); Fila = "CAMPO24";
                        oKardexDTO.CAMPO24 = Convert.ToDecimal(drd["CAMPO24"].ToString()); Fila = "CAMPO25";
                        oKardexDTO.CAMPO25 = Convert.ToDecimal(drd["CAMPO25"].ToString()); Fila = "CAMPO26";
                        oKardexDTO.CAMPO26 = Convert.ToDecimal(drd["CAMPO26"].ToString()); Fila = "CAMPO27";
                        oKardexDTO.CAMPO27 = Convert.ToInt32(drd["CAMPO27"].ToString()); Fila = "CAMPO28";
                        oKardexDTO.CAMPO28 = (drd["CAMPO28"].ToString()); Fila = "IdAlmacen";
                        oKardexDTO.IdAlmacen = int.Parse(drd["IdAlmacen"].ToString()); Fila = "IdArticulo";
                        oKardexDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString()); Fila = "IdKardex";
                        oKardexDTO.IdKardex = int.Parse(drd["IdKardex"].ToString());
                        oKardexDTO.FechaContabilizacion = (drd["FechaContabilizacion"].ToString());
                        oKardexDTO.FechaRegistro = (drd["FechaRegistro"].ToString());
                        oKardexDTO.TipoArticulo = (drd["TipoArticulo"].ToString());
                        oKardexDTO.NombreContrato = (drd["NombreContrato"].ToString());
                        oKardexDTO.RazonSocial = (drd["RazonSocial"].ToString());
                        oKardexDTO.RUC = (drd["RUC"].ToString());
                        oKardexDTO.DescripcionMovimiento = (drd["DescripcionMovimiento"].ToString());


                        lstKardexDTO.Add(oKardexDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    lstKardexDTO = new List<KardexTributario>();
                    mensaje_error = "Error en Campo:"+ Fila +" Numero Fila:"+numeroFila +" "+ ex.Message.ToString();
                }
            }
            return lstKardexDTO;
        }


        public List<KardexTributario> ObtenerSaldoInicialKardexTributario(int IdAlmacen, int Mes, int Anio, string CodOp, string @Ids, string BaseDatos, ref string mensaje_error)
        {
            List<KardexTributario> lstKardexDTO = new List<KardexTributario>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("ObtenerSaldoInicial", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@Mes", Mes);
                    da.SelectCommand.Parameters.AddWithValue("@Anio", Anio);
                    da.SelectCommand.Parameters.AddWithValue("@CodOp", CodOp);
                    da.SelectCommand.Parameters.AddWithValue("@Ids", @Ids);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.CommandTimeout = 0;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    
                    while (drd.Read())
                    {
                        KardexTributario oKardexDTO = new KardexTributario();
                        oKardexDTO.CAMPO1 = (drd["CAMPO1"].ToString());
                        oKardexDTO.CAMPO2 = (drd["CAMPO2"].ToString());
                        oKardexDTO.CAMPO3 = (drd["CAMPO3"].ToString());
                        oKardexDTO.CAMPO4 = (drd["CAMPO4"].ToString());
                        oKardexDTO.CAMPO5 = (drd["CAMPO5"].ToString());
                        oKardexDTO.CAMPO6 = (drd["CAMPO6"].ToString());
                        oKardexDTO.CAMPO7 = (drd["CAMPO7"].ToString());
                        oKardexDTO.CAMPO8 = (drd["CAMPO8"].ToString());
                        oKardexDTO.CAMPO9 = (drd["CAMPO9"].ToString());
                        oKardexDTO.CAMPO10 = (drd["CAMPO10"].ToString());
                        oKardexDTO.CAMPO11 = (drd["CAMPO11"].ToString());
                        oKardexDTO.CAMPO12 = (drd["CAMPO12"].ToString());
                        oKardexDTO.CAMPO13 = (drd["CAMPO13"].ToString());
                        oKardexDTO.CAMPO14 = (drd["CAMPO14"].ToString());
                        oKardexDTO.CAMPO15 = (drd["CAMPO15"].ToString());
                        oKardexDTO.CAMPO16 = (drd["CAMPO16"].ToString());
                        oKardexDTO.CAMPO17 = (drd["CAMPO17"].ToString());
                        oKardexDTO.CAMPO18 = Convert.ToDecimal(drd["CAMPO18"].ToString());
                        oKardexDTO.CAMPO19 = Convert.ToDecimal(drd["CAMPO19"].ToString());
                        oKardexDTO.CAMPO20 = Convert.ToDecimal(drd["CAMPO20"].ToString());
                        oKardexDTO.CAMPO21 = Convert.ToDecimal(drd["CAMPO21"].ToString());
                        oKardexDTO.CAMPO22 = Convert.ToDecimal(drd["CAMPO22"].ToString());
                        oKardexDTO.CAMPO23 = Convert.ToDecimal(drd["CAMPO23"].ToString());
                        oKardexDTO.CAMPO24 = Convert.ToDecimal(drd["CAMPO24"].ToString());
                        oKardexDTO.CAMPO25 = Convert.ToDecimal(drd["CAMPO25"].ToString());
                        oKardexDTO.CAMPO26 = Convert.ToDecimal(drd["CAMPO26"].ToString());
                        oKardexDTO.CAMPO27 = Convert.ToInt32(drd["CAMPO27"].ToString());
                        oKardexDTO.CAMPO28 = (drd["CAMPO28"].ToString());
                        oKardexDTO.IdAlmacen = int.Parse(drd["IdAlmacen"].ToString());
                        oKardexDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oKardexDTO.IdKardex = int.Parse(drd["IdKardex"].ToString());
                        oKardexDTO.FechaContabilizacion = (drd["FechaContabilizacion"].ToString());
                        oKardexDTO.FechaRegistro = (drd["FechaRegistro"].ToString());
                        oKardexDTO.TipoArticulo = (drd["TipoArticulo"].ToString());
                        oKardexDTO.NombreContrato = (drd["NombreContrato"].ToString());
                        oKardexDTO.RazonSocial = (drd["RazonSocial"].ToString());
                        oKardexDTO.RUC = (drd["RUC"].ToString());
                        oKardexDTO.DescripcionMovimiento = (drd["DescripcionMovimiento"].ToString());


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
