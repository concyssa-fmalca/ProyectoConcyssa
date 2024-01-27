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
    public class EstadosDAO
    {
        public List<EstadosDTO> ObtenerEstados(int Modulo, string BaseDatos, ref string mensaje_error)
        {
            List<EstadosDTO> lstGiroDTO = new List<EstadosDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEstados", cn);
                   // da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    if(Modulo!=0)
                        da.SelectCommand.Parameters.AddWithValue("@Modulo", Modulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        EstadosDTO olstGiroDTO = new EstadosDTO();
                        olstGiroDTO.Id = Convert.ToInt32(drd["Id"].ToString());
                        olstGiroDTO.Descripcion = drd["Descripcion"].ToString();
                        olstGiroDTO.Modulo = Convert.ToInt32(drd["Modulo"].ToString());                    
                        lstGiroDTO.Add(olstGiroDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGiroDTO;
        }


        public List<EstadosDTO> ObtenerEstadosUsuario(int IdGiro,int IdUsuario, string BaseDatos, ref string mensaje_error)
        {
            List<EstadosDTO> lstGiroDTO = new List<EstadosDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEstadosXIdUsuario", cn);
                    // da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                 
                    da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdGiro);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        EstadosDTO olstGiroDTO = new EstadosDTO();
                        olstGiroDTO.Id = Convert.ToInt32(drd["Id"].ToString());
                        olstGiroDTO.Descripcion = drd["Descripcion"].ToString();
                        olstGiroDTO.Modulo = Convert.ToInt32(drd["Modulo"].ToString());
                        lstGiroDTO.Add(olstGiroDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGiroDTO;
        }

        public List<EstadosDTO> ObtenerEstadoUsuario(int IdUsuario, string BaseDatos, ref string mensaje_error)
        {
            List<EstadosDTO> lstGiroDTO = new List<EstadosDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerEstadoXIdUsuario", cn);
                    // da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
               
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        EstadosDTO olstGiroDTO = new EstadosDTO();
                        olstGiroDTO.Id = Convert.ToInt32(drd["Id"].ToString());
                      
                        lstGiroDTO.Add(olstGiroDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGiroDTO;
        }


    }
}

