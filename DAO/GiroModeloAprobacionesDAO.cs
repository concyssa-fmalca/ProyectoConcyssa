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
    public class GiroModeloAprobacionesDAO
    {

        public int UpdateInsertModeloAprobacionesGiro(List<GiroModeloAprobacionesDTO> oGiroModeloAprobacionesDTO, string IdSociedad, string BaseDatos)
        {
            if (oGiroModeloAprobacionesDTO.Count() > 0)
            {
                int rpta = 0;
                for (int i = 0; i < oGiroModeloAprobacionesDTO.Count; i++)
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
                                SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQModeloAprobacionesGiro", cn);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@IdGiroModeloAprobaciones", oGiroModeloAprobacionesDTO[i].IdGiroModeloAprobaciones);
                                da.SelectCommand.Parameters.AddWithValue("@IdGiroModelo", oGiroModeloAprobacionesDTO[i].IdGiroModelo);
                                da.SelectCommand.Parameters.AddWithValue("@IdAutorizador", oGiroModeloAprobacionesDTO[i].IdAutorizador);
                                da.SelectCommand.Parameters.AddWithValue("@IdDetalleGiro", oGiroModeloAprobacionesDTO[i].IdDetalleGiro);
                                da.SelectCommand.Parameters.AddWithValue("@Accion", oGiroModeloAprobacionesDTO[i].Accion);
                                da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                                da.SelectCommand.Parameters.AddWithValue("@IdDetalle", oGiroModeloAprobacionesDTO[i].IdDetalle);
                                rpta = da.SelectCommand.ExecuteNonQuery();
                                transactionScope.Complete();

                            }
                            catch (Exception ex)
                            {
                                return 0;
                            }
                        }
                    }
                }
                return rpta;
            }
            else
            {
                return 0;
            }

        }


    }
}
