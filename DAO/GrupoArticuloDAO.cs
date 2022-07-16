

using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class GrupoArticuloDAO
    {


        public List<GrupoArticuloDTO> ObtenerGrupoArticulo(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<GrupoArticuloDTO> lstGrupoArticuloDTO = new List<GrupoArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGrupoArticulo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoArticuloDTO oGrupoArticuloDTO = new GrupoArticuloDTO();
                        oGrupoArticuloDTO.IdGrupoArticulo = int.Parse(drd["IdGrupoArticulo"].ToString());
                        oGrupoArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oGrupoArticuloDTO.Descripcion = drd["Descripcion"].ToString();
                        oGrupoArticuloDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oGrupoArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstGrupoArticuloDTO.Add(oGrupoArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGrupoArticuloDTO;
        }

        public int UpdateInsertGrupoArticulo(GrupoArticuloDTO oGrupoArticuloDTO, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertGrupoArticulo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupoArticulo", oGrupoArticuloDTO.IdGrupoArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oGrupoArticuloDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oGrupoArticuloDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oGrupoArticuloDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oGrupoArticuloDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return 0;
                    }
                }
            }
        }

        public List<GrupoArticuloDTO> ObtenerDatosxID(int IdGrupoArticulo, ref string mensaje_error)
        {
            List<GrupoArticuloDTO> lstGrupoArticuloDTO = new List<GrupoArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGrupoArticuloxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupoArticulo", IdGrupoArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoArticuloDTO oGrupoArticuloDTO = new GrupoArticuloDTO();
                        oGrupoArticuloDTO.IdGrupoArticulo = int.Parse(drd["IdGrupoArticulo"].ToString());
                        oGrupoArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oGrupoArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oGrupoArticuloDTO.Descripcion = drd["Descripcion"].ToString();
                        oGrupoArticuloDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstGrupoArticuloDTO.Add(oGrupoArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGrupoArticuloDTO;
        }


        public int Delete(int IdGrupoArticulo, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaGrupoArticulo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupoArticulo", IdGrupoArticulo);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return -1;
                    }
                }
            }
        }











        public List<GrupoArticuloDTO> ListarGrupoArticulos(int IdSociedad)
        {
            List<GrupoArticuloDTO> listarGruposArticuloDTO = new List<GrupoArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_GrupoArticulos", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        GrupoArticuloDTO oMa_GRupoArticulo = new GrupoArticuloDTO();
                        oMa_GRupoArticulo.IdGrupoArticulo = Convert.ToInt32(dr["IdGrupoArticulo"].ToString());
                        oMa_GRupoArticulo.Descripcion = dr["Descripcion"].ToString();
                        oMa_GRupoArticulo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        listarGruposArticuloDTO.Add(oMa_GRupoArticulo);
                    }
                }
                catch (Exception ex)
                {
                }

                return listarGruposArticuloDTO;
            }
        }

        public int UpdateInsert(GrupoArticuloDTO oGrupoArticuloDTO,  int IdSociedad)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_GrupoArticulo_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupoArticulo", oGrupoArticuloDTO.IdGrupoArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oGrupoArticuloDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaCreacion", oGrupoArticuloDTO.FechaCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaUltimaModificacion", oGrupoArticuloDTO.FechaUltimaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oGrupoArticuloDTO.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioUltimaModificacion", oGrupoArticuloDTO.UsuarioUltimaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oGrupoArticuloDTO.Estado);

                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        
                        return rpta;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
            }

        }

        public List<GrupoArticuloDTO> ListarxID(int IdGrupoArticulo, int IdSociedad)
        {
            List<GrupoArticuloDTO> lstGrupoArticuloDTO = new List<GrupoArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Ma_GrupoArticulo_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupoArticulo", IdGrupoArticulo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        GrupoArticuloDTO oGrupoArticuloDTO = new GrupoArticuloDTO();
                        oGrupoArticuloDTO.IdGrupoArticulo = Convert.ToInt32(dr["IdGrupoArticulo"].ToString());
                        oGrupoArticuloDTO.Descripcion = dr["Descripcion"].ToString();
                        //campos Nuevos para cuentas
                        oGrupoArticuloDTO.CtaGasto = dr["CtaGasto"].ToString();
                        oGrupoArticuloDTO.CtaIngresos = dr["CtaIngresos"].ToString();
                        oGrupoArticuloDTO.CtaExistencias = dr["CtaExistencias"].ToString();
                        oGrupoArticuloDTO.CtaCostoBienes = dr["CtaCostoBienes"].ToString();
                        oGrupoArticuloDTO.CtaDotacion = dr["CtaDotacion"].ToString();
                        oGrupoArticuloDTO.CtaDesviacion = dr["CtaDesviacion"].ToString();
                        oGrupoArticuloDTO.CtaDiferenciaPrecios = dr["CtaDiferenciaPrecios"].ToString();
                        oGrupoArticuloDTO.CtaAjusteStockNegativo = dr["CtaAjusteStockNegativo"].ToString();
                        oGrupoArticuloDTO.CtaReduccion = dr["CtaReduccion"].ToString();
                        oGrupoArticuloDTO.CtaAumento = dr["CtaAumento"].ToString();
                        oGrupoArticuloDTO.CtaDevolucionxVentas = dr["CtaDevolucionxVentas"].ToString();
                        oGrupoArticuloDTO.CtaIngresosExtranjeros = dr["CtaIngresosExtranjeros"].ToString();
                        oGrupoArticuloDTO.CtaCostosExtranjeros = dr["CtaCostosExtranjeros"].ToString();
                        oGrupoArticuloDTO.CtaCompras = dr["CtaCompras"].ToString();
                        oGrupoArticuloDTO.CtaComprasDevolucion = dr["CtaComprasDevolucion"].ToString();
                        oGrupoArticuloDTO.CtaCostosMercanciasCompradas = dr["CtaCostosMercanciasCompradas"].ToString();
                        oGrupoArticuloDTO.CtaDiferenciaTipoCambio = dr["CtaDiferenciaTipoCambio"].ToString();
                        oGrupoArticuloDTO.CtaCompensacionMercancias = dr["CtaCompensacionMercancias"].ToString();
                        oGrupoArticuloDTO.CtaReduccionLibroMayor = dr["CtaReduccionLibroMayor"].ToString();
                        oGrupoArticuloDTO.CtaStockTrabajoEnCurso = dr["CtaStockTrabajoEnCurso"].ToString();
                        oGrupoArticuloDTO.CtaDesviacionStockWIP = dr["CtaDesviacionStockWIP"].ToString();
                        oGrupoArticuloDTO.CtaRevalorizacionStock = dr["CtaRevalorizacionStock"].ToString();
                        oGrupoArticuloDTO.CtaContrapartidaRevalorizacionInventario = dr["CtaContrapartidaRevalorizacionInventario"].ToString();
                        oGrupoArticuloDTO.CtaCompensacionGasto = dr["CtaCompensacionGasto"].ToString();
                        oGrupoArticuloDTO.CtaStockTransito = dr["CtaStockTransito"].ToString();
                        oGrupoArticuloDTO.CtaCreditoVentas = dr["CtaCreditoVentas"].ToString();
                        oGrupoArticuloDTO.CtaCreditoCompras = dr["CtaCreditoCompras"].ToString();

                        lstGrupoArticuloDTO.Add(oGrupoArticuloDTO);
                    }
                }
                catch (Exception)
                {
                }
            }
            return lstGrupoArticuloDTO;
        }

        public int Delete(GrupoArticuloDTO oGrupoArticuloDTO, int IdSociedad)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_GrupoArticulo_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupoArticulo", oGrupoArticuloDTO.IdGrupoArticulo);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }
        }


        public int GuardarCuentaxIdGrupoArticulo(GrupoArticuloDTO oGrupoArticuloDTO, int periodo, int IdSociedad)
        {
            //ff
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SP_Ma_GrupoArticulo_GuardarCuentaxIdGrupoArticulo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupoArticulo", oGrupoArticuloDTO.IdGrupoArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@FechaUltimaModificacion", oGrupoArticuloDTO.FechaUltimaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioUltimaModificacion", oGrupoArticuloDTO.UsuarioUltimaModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@CtaAjusteStockNegativo", oGrupoArticuloDTO.CtaAjusteStockNegativo);
                        da.SelectCommand.Parameters.AddWithValue("@CtaAumento", oGrupoArticuloDTO.CtaAumento);
                        da.SelectCommand.Parameters.AddWithValue("@CtaCompensacionGasto", oGrupoArticuloDTO.CtaCompensacionGasto);
                        da.SelectCommand.Parameters.AddWithValue("@CtaCompensacionMercancias", oGrupoArticuloDTO.CtaCompensacionMercancias);
                        da.SelectCommand.Parameters.AddWithValue("@CtaCompras", oGrupoArticuloDTO.CtaCompras);
                        da.SelectCommand.Parameters.AddWithValue("@CtaComprasDevolucion", oGrupoArticuloDTO.CtaComprasDevolucion);
                        da.SelectCommand.Parameters.AddWithValue("@CtaContrapartidaRevalorizacionInventario", oGrupoArticuloDTO.CtaContrapartidaRevalorizacionInventario);
                        da.SelectCommand.Parameters.AddWithValue("@CtaCostoBienes", oGrupoArticuloDTO.CtaCostoBienes);
                        da.SelectCommand.Parameters.AddWithValue("@CtaCostosExtranjeros", oGrupoArticuloDTO.CtaCostosExtranjeros);
                        da.SelectCommand.Parameters.AddWithValue("@CtaCostosMercanciasCompradas", oGrupoArticuloDTO.CtaCostosMercanciasCompradas);
                        da.SelectCommand.Parameters.AddWithValue("@CtaCreditoCompras", oGrupoArticuloDTO.CtaCreditoCompras);
                        da.SelectCommand.Parameters.AddWithValue("@CtaCreditoVentas", oGrupoArticuloDTO.CtaCreditoVentas);
                        da.SelectCommand.Parameters.AddWithValue("@CtaDesviacion", oGrupoArticuloDTO.CtaDesviacion);
                        da.SelectCommand.Parameters.AddWithValue("@CtaDesviacionStockWIP", oGrupoArticuloDTO.CtaDesviacionStockWIP);
                        da.SelectCommand.Parameters.AddWithValue("@CtaDevolucionxVentas", oGrupoArticuloDTO.CtaDevolucionxVentas);
                        da.SelectCommand.Parameters.AddWithValue("@CtaDiferenciaPrecios", oGrupoArticuloDTO.CtaDiferenciaPrecios);
                        da.SelectCommand.Parameters.AddWithValue("@CtaDiferenciaTipoCambio", oGrupoArticuloDTO.CtaDiferenciaTipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@CtaDotacion", oGrupoArticuloDTO.CtaDotacion);
                        da.SelectCommand.Parameters.AddWithValue("@CtaExistencias", oGrupoArticuloDTO.CtaExistencias);
                        da.SelectCommand.Parameters.AddWithValue("@CtaGasto", oGrupoArticuloDTO.CtaGasto);
                        da.SelectCommand.Parameters.AddWithValue("@CtaIngresos", oGrupoArticuloDTO.CtaIngresos);
                        da.SelectCommand.Parameters.AddWithValue("@CtaIngresosExtranjeros", oGrupoArticuloDTO.CtaIngresosExtranjeros);
                        da.SelectCommand.Parameters.AddWithValue("@CtaReduccion", oGrupoArticuloDTO.CtaReduccion);
                        da.SelectCommand.Parameters.AddWithValue("@CtaReduccionLibroMayor", oGrupoArticuloDTO.CtaReduccionLibroMayor);
                        da.SelectCommand.Parameters.AddWithValue("@CtaRevalorizacionStock", oGrupoArticuloDTO.CtaRevalorizacionStock);
                        da.SelectCommand.Parameters.AddWithValue("@CtaStockTrabajoEnCurso", oGrupoArticuloDTO.CtaStockTrabajoEnCurso);
                        da.SelectCommand.Parameters.AddWithValue("@CtaStockTransito", oGrupoArticuloDTO.CtaStockTransito);

                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        
                        return rpta;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
