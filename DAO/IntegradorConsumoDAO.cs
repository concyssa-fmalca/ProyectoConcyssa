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
    public class IntegradorConsumoDAO
    {
        public List<ListaTrabajoDTO> ObtenerListaDatosTrabajo(int IdUsuario)
        {
            List<ListaTrabajoDTO> lstListaTrabajoDTO = new List<ListaTrabajoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerListasTrabajoConsumoxIdUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ListaTrabajoDTO oListaTrabajoDTO = new ListaTrabajoDTO();
                        oListaTrabajoDTO.GrupoCreacion = int.Parse(drd["GrupoCreacion"].ToString());
                        oListaTrabajoDTO.UsuarioCreacionTabla = int.Parse(drd["UsuarioCreacionTabla"].ToString());
                        oListaTrabajoDTO.FechaCreacionTabla = DateTime.Parse(drd["FechaCreacionTabla"].ToString());
                        oListaTrabajoDTO.Usuario = drd["Usuario"].ToString();
                        oListaTrabajoDTO.Nombre = drd["Nombre"].ToString();
                        oListaTrabajoDTO.EstadoEnviado = drd["EstadoEnviado"].ToString();


                        lstListaTrabajoDTO.Add(oListaTrabajoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstListaTrabajoDTO;
        }

        public int ObtenerGrupoCreacionEnviarSap()
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerGrupoCreacionEnviarSap", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
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

        public int CopiarKardexEnviarSapConsumo(int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, int ClaseArticulo, int TipoArticulo, int GrupoCreacion)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_KardexHaciaEnviarSapConsumo", cn);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
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
    }
}
