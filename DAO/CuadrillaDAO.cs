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
    public class CuadrillaDAO
    {
        public List<CuadrillaDTO> ObtenerCuadrilla(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<CuadrillaDTO> lstCuadrillaDTO = new List<CuadrillaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCuadrilla", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CuadrillaDTO oCuadrillaDTO = new CuadrillaDTO();
                        oCuadrillaDTO.IdCuadrilla = int.Parse(drd["IdCuadrilla"].ToString());
                        oCuadrillaDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oCuadrillaDTO.IdGrupo = Convert.ToInt32(drd["IdGrupo"].ToString());
                        oCuadrillaDTO.IdSubGrupo = Convert.ToInt32(drd["IdSubGrupo"].ToString());
                        oCuadrillaDTO.Codigo = (drd["Codigo"].ToString());
                        oCuadrillaDTO.Descripcion = (drd["Descripcion"].ToString());
                        oCuadrillaDTO.IdCapataz = Convert.ToInt32(drd["IdCapataz"].ToString());
                        oCuadrillaDTO.IdSupervisor = Convert.ToInt32(drd["IdSupervisor"].ToString());
                        oCuadrillaDTO.IdArea = Convert.ToInt32(drd["IdArea"].ToString());
                        oCuadrillaDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oCuadrillaDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oCuadrillaDTO.EsTercero = Convert.ToBoolean(drd["EsTercero"].ToString());
                        
                        lstCuadrillaDTO.Add(oCuadrillaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCuadrillaDTO;
        }

        
        public int UpdateInsertCuadrilla(CuadrillaDTO oCuadrillaDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCuadrilla", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oCuadrillaDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra ", oCuadrillaDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupo ", oCuadrillaDTO.IdGrupo);
                        da.SelectCommand.Parameters.AddWithValue("@IdSubGrupo", oCuadrillaDTO.IdSubGrupo);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oCuadrillaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oCuadrillaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdCapataz", oCuadrillaDTO.IdCapataz);
                        da.SelectCommand.Parameters.AddWithValue("@IdSupervisor", oCuadrillaDTO.IdSupervisor);
                        da.SelectCommand.Parameters.AddWithValue("@IdArea", oCuadrillaDTO.IdArea);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oCuadrillaDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCuadrillaDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@EsTercero", oCuadrillaDTO.EsTercero);
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


        
        public List<CuadrillaDTO> ObtenerDatosxID(int IdCuadrilla, ref string mensaje_error)
        {
            List<CuadrillaDTO> lstCuadrillaDTO = new List<CuadrillaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCuadrillaxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", IdCuadrilla);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CuadrillaDTO oCuadrillaDTO = new CuadrillaDTO();
                        oCuadrillaDTO.IdCuadrilla = int.Parse(drd["IdCuadrilla"].ToString());
                        oCuadrillaDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oCuadrillaDTO.IdGrupo = Convert.ToInt32(drd["IdGrupo"].ToString());
                        oCuadrillaDTO.IdSubGrupo = Convert.ToInt32(drd["IdSubGrupo"].ToString());
                        oCuadrillaDTO.Codigo = (drd["Codigo"].ToString());
                        oCuadrillaDTO.Descripcion = (drd["Descripcion"].ToString());
                        oCuadrillaDTO.IdCapataz = Convert.ToInt32(drd["IdCapataz"].ToString());
                        oCuadrillaDTO.IdSupervisor = Convert.ToInt32(drd["IdSupervisor"].ToString());
                        oCuadrillaDTO.IdArea = Convert.ToInt32(drd["IdArea"].ToString());
                        oCuadrillaDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oCuadrillaDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oCuadrillaDTO.EsTercero = Convert.ToBoolean(drd["EsTercero"].ToString());

                        lstCuadrillaDTO.Add(oCuadrillaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCuadrillaDTO;
        }


    }
}
