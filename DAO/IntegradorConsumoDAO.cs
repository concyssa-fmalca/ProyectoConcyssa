using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAO
{
    public class IntegradorConsumoDAO
    {
        public List<ListaTrabajoDTO> ObtenerListaDatosTrabajo(int IdUsuario)
        {
            List<ListaTrabajoDTO> lstListaTrabajoDTO = new List<ListaTrabajoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerListasTrabajoConsumoxIdUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ListaTrabajoDTO oListaTrabajoDTO = new ListaTrabajoDTO();
                        oListaTrabajoDTO.GrupoCreacion = int.Parse(drd["GrupoCreacion"].ToString());
                        oListaTrabajoDTO.UsuarioCreacionTabla = int.Parse(drd["UsuarioCreacionTabla"].ToString());
                        oListaTrabajoDTO.FechaCreacionTabla = DateTime.Parse(drd["FechaCreacionTabla"].ToString());
                        oListaTrabajoDTO.Usuario = drd["Usuario"].ToString();
                        oListaTrabajoDTO.Nombre = drd["Nombre"].ToString();
                        oListaTrabajoDTO.EstadoEnviado = drd["EstadoEnviado"].ToString();


                        lstListaTrabajoDTO.Add(oListaTrabajoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstListaTrabajoDTO;
        }

        public int ObtenerGrupoCreacionEnviarSap()
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerGrupoCreacionEnviarSapConsumo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }

        public int CopiarKardexEnviarSapConsumo(int IdKardex, int GrupoCreacion, int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_KardexHaciaEnviarSapConsumo", cn);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@IdKardex", IdKardex);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }

        public List<IntegradorConsumoDTO> ListarEnviarSapConsumo(int GrupoCreacion, ref string mensaje_error)
        {
            List<IntegradorConsumoDTO> lstIntegradorConsumoDTO = new List<IntegradorConsumoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEnviarSapConsumo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);


                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorConsumoDTO oIntegradorConsumoDTO = new IntegradorConsumoDTO();
                        oIntegradorConsumoDTO.IdEnviarSapConsumo = Convert.ToInt32((drd["IdEnviarSapConsumo"].ToString() == "" ? 0 : drd["IdEnviarSapConsumo"].ToString()));
                        oIntegradorConsumoDTO.IdDetalleMovimiento = Convert.ToInt32((drd["IdDetalleMovimiento"].ToString() == "" ? 0 : drd["IdDetalleMovimiento"].ToString()));
                        oIntegradorConsumoDTO.IdDetalleOPDN = Convert.ToInt32((drd["IdDetalleOPDN"].ToString() == "" ? 0 : drd["IdDetalleOPDN"].ToString()));
                        oIntegradorConsumoDTO.IdDetalleOPCH = Convert.ToInt32((drd["IdDetalleOPCH"].ToString() == "" ? 0 : drd["IdDetalleOPCH"].ToString()));
                        oIntegradorConsumoDTO.IdDefinicionGrupoUnidad = Convert.ToInt32(drd["IdDefinicionGrupoUnidad"].ToString());
                        oIntegradorConsumoDTO.CantidadBase = Convert.ToDecimal(drd["CantidadBase"].ToString());
                        oIntegradorConsumoDTO.IdUnidadMedidaBase = Convert.ToInt32(drd["IdUnidadMedidaBase"].ToString());
                        oIntegradorConsumoDTO.PrecioBase = Convert.ToDecimal(drd["PrecioBase"].ToString());
                        oIntegradorConsumoDTO.CantidadRegistro = Convert.ToDecimal(drd["CantidadRegistro"].ToString());
                        oIntegradorConsumoDTO.IdUnidadMedidaRegistro = Convert.ToInt32(drd["IdUnidadMedidaRegistro"].ToString());
                        oIntegradorConsumoDTO.PrecioRegistro = Convert.ToDecimal(drd["PrecioRegistro"].ToString());
                        oIntegradorConsumoDTO.PrecioPromedio = Convert.ToDecimal(drd["PrecioPromedio"].ToString());
                        oIntegradorConsumoDTO.FechaRegistro = Convert.ToDateTime(drd["FechaRegistro"].ToString());
                        oIntegradorConsumoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oIntegradorConsumoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oIntegradorConsumoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorConsumoDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oIntegradorConsumoDTO.Saldo = Convert.ToDecimal(drd["Saldo"].ToString());
                        oIntegradorConsumoDTO.DescArticulo = (drd["DescArticulo"].ToString());
                        oIntegradorConsumoDTO.CodigoArticulo = (drd["CodigoArticulo"].ToString());
                        oIntegradorConsumoDTO.DescSerie = (drd["DescSerie"].ToString());
                        oIntegradorConsumoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oIntegradorConsumoDTO.TipoTransaccion = (drd["TipoTransaccion"].ToString());
                        oIntegradorConsumoDTO.DescUnidadMedidaBase = (drd["DescUnidadMedidaBase"].ToString());
                        oIntegradorConsumoDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oIntegradorConsumoDTO.Comentario = (drd["Comentario"].ToString());
                        oIntegradorConsumoDTO.Modulo = (drd["Modulo"].ToString());
                        oIntegradorConsumoDTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
                        oIntegradorConsumoDTO.TipoDocumentoRef = drd["TipoDocumentoRef"].ToString();
                        oIntegradorConsumoDTO.CuentaConsumo = drd["CuentaConsumo"].ToString();
                        lstIntegradorConsumoDTO.Add(oIntegradorConsumoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstIntegradorConsumoDTO;
        }

        public int ActualizarEnvioSap(int GrupoCreacion, int intdocentry)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ActualizarEnvioSapConsumo", cn);
                        da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@intdocentry", intdocentry);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }

        public int ValidarEnvioSap(int GrupoCreacion)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarEnvioSapConsumo", cn);
                     
                        da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);
               
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
