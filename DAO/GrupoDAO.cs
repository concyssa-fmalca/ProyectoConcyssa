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
    public class GrupoDAO
    {

        public List<SubGrupoDTO> ObtenerSubGrupo(int IdSociedad, int IdGrupo, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<SubGrupoDTO> lstSubGrupoDTO = new List<SubGrupoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSubGrupo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupo", IdGrupo);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SubGrupoDTO oSubGrupoDTO = new SubGrupoDTO();
                        oSubGrupoDTO.IdSubGrupo = int.Parse(drd["IdSubGrupo"].ToString());
                        oSubGrupoDTO.IdGrupo = int.Parse(drd["IdGrupo"].ToString());
                        oSubGrupoDTO.Codigo = drd["Codigo"].ToString();
                        oSubGrupoDTO.Descripcion = drd["Descripcion"].ToString();
                        oSubGrupoDTO.DescGrupo = drd["DescGrupo"].ToString();
                        oSubGrupoDTO.DescripcionObraSG = drd["DescripcionObraSG"].ToString();
                        lstSubGrupoDTO.Add(oSubGrupoDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSubGrupoDTO;
        }

        public List<GrupoDTO> ObtenerGrupo(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<GrupoDTO> lstGrupoDTO = new List<GrupoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGrupo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoDTO oGrupoDTO = new GrupoDTO();
                        oGrupoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oGrupoDTO.IdGrupo = int.Parse(drd["IdGrupo"].ToString());
                        oGrupoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oGrupoDTO.Codigo = drd["Codigo"].ToString();
                        oGrupoDTO.Descripcion = drd["Descripcion"].ToString();
                        oGrupoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oGrupoDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());
                        oGrupoDTO.DescripcionObra = drd["DescripcionObra"].ToString();
                        lstGrupoDTO.Add(oGrupoDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGrupoDTO;
        }
        public List<GrupoDTO> ObtenerGrupoxObra(int IdObra, string BaseDatos, ref string mensaje_error)
        {
            List<GrupoDTO> lstGrupoDTO = new List<GrupoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGrupoXObra", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoDTO oGrupoDTO = new GrupoDTO();
                        oGrupoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oGrupoDTO.IdGrupo = int.Parse(drd["IdGrupo"].ToString());
                        oGrupoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oGrupoDTO.Codigo = drd["Codigo"].ToString();
                        oGrupoDTO.Descripcion = drd["Descripcion"].ToString();
                        oGrupoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oGrupoDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());
                        oGrupoDTO.DescripcionObra = drd["DescripcionObra"].ToString();

                        lstGrupoDTO.Add(oGrupoDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGrupoDTO;
        }
        public List<GrupoDTO> ObtenerGrupoxId(int IdGrupo, string BaseDatos, ref string mensaje_error)
        {
            List<GrupoDTO> lstGrupoDTO = new List<GrupoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGrupoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupo", IdGrupo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoDTO oGrupoDTO = new GrupoDTO();
                        oGrupoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oGrupoDTO.IdGrupo = int.Parse(drd["IdGrupo"].ToString());
                        oGrupoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oGrupoDTO.Codigo = drd["Codigo"].ToString();
                        oGrupoDTO.Descripcion = drd["Descripcion"].ToString();
                        oGrupoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oGrupoDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());

                        lstGrupoDTO.Add(oGrupoDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGrupoDTO;
        }

        public List<GrupoDTO> ObtenerDatosxID(int IdGrupo, string BaseDatos, ref string mensaje_error)
        {
            List<GrupoDTO> lstGrupoDTO = new List<GrupoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGrupoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupo", IdGrupo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoDTO oGrupoDTO = new GrupoDTO();
                        oGrupoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oGrupoDTO.IdGrupo = int.Parse(drd["IdGrupo"].ToString());
                        oGrupoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oGrupoDTO.Codigo = drd["Codigo"].ToString();
                        oGrupoDTO.Descripcion = drd["Descripcion"].ToString();
                        oGrupoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstGrupoDTO.Add(oGrupoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGrupoDTO;
        }


        public List<SubGrupoDTO> ObtenerDatosxIdSubGrupo(int IdSubGrupo, string BaseDatos, ref string mensaje_error)
        {
            List<SubGrupoDTO> lstSubGrupoDTO = new List<SubGrupoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGrupoxIdSubGrupo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSubGrupo", IdSubGrupo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SubGrupoDTO oSubGrupoDTO = new SubGrupoDTO();
                        oSubGrupoDTO.IdSubGrupo = int.Parse(drd["IdSubGrupo"].ToString());
                        oSubGrupoDTO.IdGrupo = int.Parse(drd["IdGrupo"].ToString());
                        oSubGrupoDTO.Codigo = (drd["Codigo"].ToString());
                        oSubGrupoDTO.Descripcion = drd["Descripcion"].ToString();
                        oSubGrupoDTO.DescGrupo = drd["DescGrupo"].ToString();
                        oSubGrupoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oSubGrupoDTO.DescripcionObraSG = drd["DescripcionObraSG"].ToString();
                        lstSubGrupoDTO.Add(oSubGrupoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSubGrupoDTO;
        }

        public int UpdateInsertGrupo(GrupoDTO oGrupoDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertGrupo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oGrupoDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupo", oGrupoDTO.IdGrupo);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oGrupoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oGrupoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oGrupoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oGrupoDTO.Estado);
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

        public int UpdateInsertSubGrupo(SubGrupoDTO oSubGrupoDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSubGrupo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSubGrupo", oSubGrupoDTO.IdSubGrupo);
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupo", oSubGrupoDTO.IdGrupo);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oSubGrupoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSubGrupoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSubGrupoDTO.Estado);
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

        public int Delete(int IdGrupo, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarGrupo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupo", IdGrupo);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }

        public int DeleteSubGrupo(int IdSubGrupo, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarSubGrupo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSubGrupo", IdSubGrupo);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }


        
        public List<SubGrupoDTO> ObtenerSubGruposxIdGrupo(int IdGrupo, string BaseDatos, ref string mensaje_error)
        {
            List<SubGrupoDTO> lstSubGrupoDTO = new List<SubGrupoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSubGrupoxIdGrupo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupo", IdGrupo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SubGrupoDTO oSubGrupoDTO = new SubGrupoDTO();
                        oSubGrupoDTO.IdSubGrupo = int.Parse(drd["IdSubGrupo"].ToString());
                        oSubGrupoDTO.IdGrupo = int.Parse(drd["IdGrupo"].ToString());
                        oSubGrupoDTO.Codigo = (drd["Codigo"].ToString());
                        oSubGrupoDTO.Descripcion = drd["Descripcion"].ToString(); ;
                        lstSubGrupoDTO.Add(oSubGrupoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSubGrupoDTO;
        }
        public List<SubGrupoDTO> ObtenerSubGrupoxID(int IdSubGrupo, string BaseDatos, ref string mensaje_error)
        {
            List<SubGrupoDTO> lstSubGrupoDTO = new List<SubGrupoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSubGrupoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSubGrupo", IdSubGrupo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SubGrupoDTO oSubGrupoDTO = new SubGrupoDTO();
                        oSubGrupoDTO.IdSubGrupo = int.Parse(drd["IdSubGrupo"].ToString());
                        oSubGrupoDTO.IdGrupo = int.Parse(drd["IdGrupo"].ToString());
                        oSubGrupoDTO.Codigo = (drd["Codigo"].ToString());
                        oSubGrupoDTO.Descripcion = drd["Descripcion"].ToString(); ;
                        lstSubGrupoDTO.Add(oSubGrupoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSubGrupoDTO;
        }
    }
}
