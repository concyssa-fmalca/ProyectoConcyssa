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
    public class SociedadDAO
    {
        public List<SociedadDTO> ObtenerSociedades( string BaseDatos,ref string mensajeError)
        {
            List<SociedadDTO> lstSociedadDTO = new List<SociedadDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSociedades", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SociedadDTO oSociedadDTO = new SociedadDTO();
                        oSociedadDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oSociedadDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oSociedadDTO.Descripcion = drd["Descripcion"].ToString();
                        oSociedadDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oSociedadDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSociedadDTO.Add(oSociedadDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensajeError=ex.Message.ToString();
                }
            }
            return lstSociedadDTO;
        }

        public int UpdateInsertSociedad(SociedadDTO oSociedadDTO, string BaseDatos, ref string error_mensaje,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSociedades", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oSociedadDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@NombreSociedad", oSociedadDTO.NombreSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSociedadDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroDocumento", oSociedadDTO.NumeroDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSociedadDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioActualizacion", IdUsuario);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        error_mensaje= ex.Message.ToString();
                        return 0;
                    }
                }
            }
        }

        public List<SociedadDTO> ObtenerDatosxID(int IdSociedad, string BaseDatos)
        {
            List<SociedadDTO> lstSociedadDTO = new List<SociedadDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSociedadesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SociedadDTO oSociedadDTO = new SociedadDTO();
                        oSociedadDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oSociedadDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oSociedadDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oSociedadDTO.Descripcion = drd["Descripcion"].ToString();
                        oSociedadDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSociedadDTO.Add(oSociedadDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSociedadDTO;
        }

        public List<ConexionesBD> CargarConexiones()
        {
            List<ConexionesBD> lstConexionesBD = new List<ConexionesBD>();
            using (SqlConnection cn = new Conexion().conectar("AddonsConcyssa"))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerConexionesBD", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ConexionesBD oConexionesBD = new ConexionesBD();
                        oConexionesBD.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oConexionesBD.BaseDatos = drd["BaseDatos"].ToString();
                        oConexionesBD.Alias = drd["Alias"].ToString();
                        lstConexionesBD.Add(oConexionesBD);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    lstConexionesBD = new List<ConexionesBD>();
                }
            }
            return lstConexionesBD;
        }


    }
}
