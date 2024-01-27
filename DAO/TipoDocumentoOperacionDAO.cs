using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class TipoDocumentoOperacionDAO
    {
        public List<TipoDocumentoOperacionDTO> ObtenerTipoDocumentoOperacion(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<TipoDocumentoOperacionDTO> lstTipoDocumentoOperacionDTO = new List<TipoDocumentoOperacionDTO>();
   
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_TipoDocumentoOperacion", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoDocumentoOperacionDTO oTipoDocumentoOperacionDTO = new TipoDocumentoOperacionDTO();
                        oTipoDocumentoOperacionDTO.IdTipoDocumento = int.Parse(drd["IdTipoDocumento"].ToString());
                        oTipoDocumentoOperacionDTO.Descripcion = drd["Descripcion"].ToString();
                        oTipoDocumentoOperacionDTO.Tabla = drd["Tabla"].ToString();
                        oTipoDocumentoOperacionDTO.DescripcionTabla = (drd["DescripcionTabla"].ToString());
                        oTipoDocumentoOperacionDTO.CodeExt = (drd["CodeExt"].ToString());
                        oTipoDocumentoOperacionDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oTipoDocumentoOperacionDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        oTipoDocumentoOperacionDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstTipoDocumentoOperacionDTO.Add(oTipoDocumentoOperacionDTO);
                    }
                    drd.Close();

                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoDocumentoOperacionDTO;
        }


        /*
        public int UpdateInsertTipoAlmacen(TipoDocumentoOperacionDTO oTipoDocumentoOperacionDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertTipoAlmacen", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoAlmacen", oTipoDocumentoOperacionDTO.IdTipoAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oTipoDocumentoOperacionDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oTipoDocumentoOperacionDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oTipoDocumentoOperacionDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oTipoDocumentoOperacionDTO.Estado);
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

        public List<TipoDocumentoOperacionDTO> ObtenerDatosxID(int IdTipoAlmacen, string BaseDatos, ref string mensaje_error)
        {
            List<TipoDocumentoOperacionDTO> lstTipoDocumentoOperacionDTO = new List<TipoDocumentoOperacionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        TipoDocumentoOperacionDTO oTipoDocumentoOperacionDTO = new TipoDocumentoOperacionDTO();
                        oTipoDocumentoOperacionDTO.IdTipoAlmacen = int.Parse(drd["IdTipoAlmacen"].ToString());
                        oTipoDocumentoOperacionDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oTipoDocumentoOperacionDTO.Codigo = drd["Codigo"].ToString();
                        oTipoDocumentoOperacionDTO.Descripcion = drd["Descripcion"].ToString();
                        oTipoDocumentoOperacionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstTipoDocumentoOperacionDTO.Add(oTipoDocumentoOperacionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoDocumentoOperacionDTO;
        }


        public int Delete(int IdTipoAlmacen, string BaseDatos, ref string mensaje_error)
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

        */
    }
}
