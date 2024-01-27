

using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class AccesoDAO
    {
        public List<AccesoDTO> ObtenerAccesos(int IdPerfil,string BaseDatos, ref string mensaje_error)
        {
            List<AccesoDTO> lstAccesoDTO = new List<AccesoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAccesosxPerfil", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AccesoDTO oAccesoDTO = new AccesoDTO();
                        oAccesoDTO.IdAceeso = int.Parse(drd["IdAcceso"].ToString());
                        oAccesoDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oAccesoDTO.Perfil = drd["Perfil"].ToString();
                        oAccesoDTO.IdMenu = int.Parse(drd["IdMenu"].ToString());
                        oAccesoDTO.Menu = drd["Menu"].ToString();
                        oAccesoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstAccesoDTO.Add(oAccesoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstAccesoDTO;
        }


        public bool GrabarAccesos(string lista, int idUsuario, string BaseDatos)
        {
            bool exito = false;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_Seg_AccesosInsertUpdate", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Lista", lista);
                    da.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                    int i = da.SelectCommand.ExecuteNonQuery();
                    if (i > 0)
                    {
                        exito = true;
                    }
                }
                catch (Exception)
                {
                }
            }
            return exito;
        }



        public int UpdateInsertAcceso(int IdPerfil, List<int> ArrayAccesos, string BaseDatos)
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
                        int rpta = 0;
                        for (int i = 0; i < ArrayAccesos.Count; i++)
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertAccesos", cn);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
                            da.SelectCommand.Parameters.AddWithValue("@IdMenu", ArrayAccesos[i]);
                            da.SelectCommand.Parameters.AddWithValue("@Estado", 1);
                            rpta = da.SelectCommand.ExecuteNonQuery();
                        }

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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarAccesoxPerfil", cn);
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
