using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class TipoProductoDAO
    {
        public List<TipoProductoDTO> ObtenerTipoProducto(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<TipoProductoDTO> lstTipoProductoDTO = new List<TipoProductoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoProducto", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoProductoDTO oTipoProductoDTO = new TipoProductoDTO();
                        oTipoProductoDTO.IdTipoProducto = int.Parse(drd["IdTipoProducto"].ToString());
                        oTipoProductoDTO.Codigo = drd["Codigo"].ToString();
                        oTipoProductoDTO.Descripcion = drd["Descripcion"].ToString();
                        oTipoProductoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oTipoProductoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstTipoProductoDTO.Add(oTipoProductoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoProductoDTO;
        }

        public int UpdateInsertTipoProducto(TipoProductoDTO oTipoProductoDTO, string BaseDatos, ref string mensaje_error,int IdUsuario)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertTipoProducto", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", oTipoProductoDTO.IdTipoProducto);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oTipoProductoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oTipoProductoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oTipoProductoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oTipoProductoDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioActualizacion", IdUsuario);
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

        public List<TipoProductoDTO> ObtenerDatosxID(int IdTipoProducto, string BaseDatos, ref string mensaje_error)
        {
            List<TipoProductoDTO> lstTipoProductoDTO = new List<TipoProductoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoProductoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", IdTipoProducto);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoProductoDTO oTipoProductoDTO = new TipoProductoDTO();
                        oTipoProductoDTO.IdTipoProducto = int.Parse(drd["IdTipoProducto"].ToString());
                        oTipoProductoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oTipoProductoDTO.Codigo = drd["Codigo"].ToString();
                        oTipoProductoDTO.Descripcion = drd["Descripcion"].ToString();
                        oTipoProductoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstTipoProductoDTO.Add(oTipoProductoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoProductoDTO;
        }


        public int Delete(int IdTipoProducto, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaTipoProducto", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", IdTipoProducto);
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
    }
}
