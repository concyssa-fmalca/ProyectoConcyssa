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
    public class PerfilDAO
    {
        public List<PerfilDTO> ObtenerPerfiles(int IdSociedad,string BaseDatos, ref string mensaje_error)
        {
      
            List <PerfilDTO> lstPerfilDTO = new List<PerfilDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPerfilesxSociedad", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    
                    while (drd.Read())
                    {
                        PerfilDTO oPerfilDTO = new PerfilDTO();
                        oPerfilDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oPerfilDTO.Perfil = drd["Perfil"].ToString();
                        oPerfilDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstPerfilDTO.Add(oPerfilDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstPerfilDTO;
        }

        public int UpdateInsertPerfil(PerfilDTO oPerfilDTO, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertPerfiles", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPerfil", oPerfilDTO.IdPerfil);
                        da.SelectCommand.Parameters.AddWithValue("@Perfil", oPerfilDTO.Perfil);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oPerfilDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oPerfilDTO.Estado);
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


        public List<PerfilDTO> ObtenerDatosxID(int IdPerfil, string BaseDatos)
        {
            List<PerfilDTO> lstPerfilDTO = new List<PerfilDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPerfilesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PerfilDTO oPerfilDTO = new PerfilDTO();
                        oPerfilDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oPerfilDTO.Perfil = drd["Perfil"].ToString();
                        oPerfilDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstPerfilDTO.Add(oPerfilDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstPerfilDTO;
        }

        public int Delete(int IdPerfil, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarPerfil", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
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

    }
}
