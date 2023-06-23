

using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class AlmacenDAO
    {
        public List<AlmacenDTO> ObtenerAlmacen(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<AlmacenDTO> lstAlmacenDTO = new List<AlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAlmacen", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        int dd=( String.IsNullOrEmpty(drd["IdObra"].ToString())) ? 1 : 2;
                        AlmacenDTO oAlmacenDTO = new AlmacenDTO();
                        oAlmacenDTO.IdAlmacen = int.Parse(drd["IdAlmacen"].ToString());
                        oAlmacenDTO.IdObra = Convert.ToInt32( (String.IsNullOrEmpty(drd["IdObra"].ToString()))? "0" : drd["IdObra"].ToString());
                        oAlmacenDTO.Codigo = drd["Codigo"].ToString();
                        oAlmacenDTO.Descripcion = drd["Descripcion"].ToString();
                        oAlmacenDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oAlmacenDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstAlmacenDTO.Add(oAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstAlmacenDTO;
        }




        public List<AlmacenDTO> ObtenerAlmacenxIdUsuario(int IdSociedad,int IdUsuario, ref string mensaje_error, int Estado = 3)
        {
            List<AlmacenDTO> lstAlmacenDTO = new List<AlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAlmacenxIdUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                       
                        AlmacenDTO oAlmacenDTO = new AlmacenDTO();
                        oAlmacenDTO.IdAlmacen = int.Parse(drd["IdAlmacen"].ToString());
                        oAlmacenDTO.IdObra = Convert.ToInt32((String.IsNullOrEmpty(drd["IdObra"].ToString())) ? "0" : drd["IdObra"].ToString());
                        oAlmacenDTO.Codigo = drd["Codigo"].ToString();
                        oAlmacenDTO.Descripcion = drd["Descripcion"].ToString();
                        oAlmacenDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oAlmacenDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstAlmacenDTO.Add(oAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstAlmacenDTO;
        }









        public int UpdateInsertAlmacen(AlmacenDTO oAlmacenDTO, ref string mensaje_error, int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertAlmacen", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oAlmacenDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oAlmacenDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oAlmacenDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oAlmacenDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oAlmacenDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAlmacenDTO.Estado);
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

        public List<AlmacenDTO> ObtenerDatosxID(int IdAlmacen, ref string mensaje_error)
        {
            List<AlmacenDTO> lstAlmacenDTO = new List<AlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAlmacenxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AlmacenDTO oAlmacenDTO = new AlmacenDTO();
                        oAlmacenDTO.IdAlmacen = int.Parse(drd["IdAlmacen"].ToString());
                        oAlmacenDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oAlmacenDTO.IdObra = Convert.ToInt32((String.IsNullOrEmpty(drd["IdObra"].ToString())) ? "0" : drd["IdObra"].ToString());
                        oAlmacenDTO.Codigo = drd["Codigo"].ToString();
                        oAlmacenDTO.Descripcion = drd["Descripcion"].ToString();
                        oAlmacenDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstAlmacenDTO.Add(oAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstAlmacenDTO;
        }

        
        public List<AlmacenDTO> ObtenerAlmacenxIdObra(int IdObra, ref string mensaje_error)
        {
            List<AlmacenDTO> lstAlmacenDTO = new List<AlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAlmacenxIdObra", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AlmacenDTO oAlmacenDTO = new AlmacenDTO();
                        oAlmacenDTO.IdAlmacen = int.Parse(drd["IdAlmacen"].ToString());
                        oAlmacenDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oAlmacenDTO.IdObra = Convert.ToInt32((String.IsNullOrEmpty(drd["IdObra"].ToString())) ? "0" : drd["IdObra"].ToString());
                        oAlmacenDTO.Codigo = drd["Codigo"].ToString();
                        oAlmacenDTO.Descripcion = drd["Descripcion"].ToString();
                        oAlmacenDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstAlmacenDTO.Add(oAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstAlmacenDTO;
        }

        public int Delete(int IdAlmacen, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaAlmacen", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
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
