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
    public class ModeloDAO
    {
        public List<ModeloDTO> ObtenerModelo(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<ModeloDTO> lstModeloDTO = new List<ModeloDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModelo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ModeloDTO oModeloDTO = new ModeloDTO();
                        oModeloDTO.IdModelo = int.Parse(drd["IdModelo"].ToString());
                        oModeloDTO.Codigo = drd["Codigo"].ToString();
                        oModeloDTO.Descripcion = drd["Descripcion"].ToString();
                        oModeloDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oModeloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstModeloDTO.Add(oModeloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstModeloDTO;
        }

        public int UpdateInsertModelo(ModeloDTO oModeloDTO, string BaseDatos, ref string mensaje_error, int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertModelo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdModelo", oModeloDTO.IdModelo);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oModeloDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oModeloDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oModeloDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oModeloDTO.Estado);
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

        public List<ModeloDTO> ObtenerDatosxID(int IdModelo, string BaseDatos, ref string mensaje_error)
        {
            List<ModeloDTO> lstModeloDTO = new List<ModeloDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdModelo", IdModelo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ModeloDTO oModeloDTO = new ModeloDTO();
                        oModeloDTO.IdModelo = int.Parse(drd["IdModelo"].ToString());
                        oModeloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oModeloDTO.Codigo = drd["Codigo"].ToString();
                        oModeloDTO.Descripcion = drd["Descripcion"].ToString();
                        oModeloDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstModeloDTO.Add(oModeloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstModeloDTO;
        }


        public int Delete(int IdModelo, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaModelo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdModelo", IdModelo);
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
