using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAO
{
    public class TipoAlmacenDAO
    {
        public List<TipoAlmacenDTO> ObtenerTipoAlmacen(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<TipoAlmacenDTO> lstTipoAlmacenDTO = new List<TipoAlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoAlmacen", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoAlmacenDTO oTipoAlmacenDTO = new TipoAlmacenDTO();
                        oTipoAlmacenDTO.IdTipoAlmacen = int.Parse(drd["IdTipoAlmacen"].ToString());
                        oTipoAlmacenDTO.Codigo = drd["Codigo"].ToString();
                        oTipoAlmacenDTO.Descripcion = drd["Descripcion"].ToString();
                        oTipoAlmacenDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oTipoAlmacenDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstTipoAlmacenDTO.Add(oTipoAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoAlmacenDTO;
        }

        public int UpdateInsertTipoAlmacen(TipoAlmacenDTO oTipoAlmacenDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertTipoAlmacen", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoAlmacen", oTipoAlmacenDTO.IdTipoAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oTipoAlmacenDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oTipoAlmacenDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oTipoAlmacenDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oTipoAlmacenDTO.Estado);
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

        public List<TipoAlmacenDTO> ObtenerDatosxID(int IdTipoAlmacen, ref string mensaje_error)
        {
            List<TipoAlmacenDTO> lstTipoAlmacenDTO = new List<TipoAlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoAlmacenxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoAlmacen", IdTipoAlmacen);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoAlmacenDTO oTipoAlmacenDTO = new TipoAlmacenDTO();
                        oTipoAlmacenDTO.IdTipoAlmacen = int.Parse(drd["IdTipoAlmacen"].ToString());
                        oTipoAlmacenDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oTipoAlmacenDTO.Codigo = drd["Codigo"].ToString();
                        oTipoAlmacenDTO.Descripcion = drd["Descripcion"].ToString();
                        oTipoAlmacenDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstTipoAlmacenDTO.Add(oTipoAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoAlmacenDTO;
        }


        public int Delete(int IdTipoAlmacen, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaTipoAlmacen", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoAlmacen", IdTipoAlmacen);
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
