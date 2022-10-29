using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class SerieDAO
    {
        public List<SerieDTO> ObtenerSeries(string IdSociedad)
        {
            List<SerieDTO> lstSerieDTO = new List<SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSerie", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@Estado", 3);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SerieDTO oSerieDTO = new SerieDTO();
                        oSerieDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSerieDTO.Serie = drd["Serie"].ToString();
                        oSerieDTO.NumeroInicial = int.Parse(drd["NumeroInicial"].ToString());
                        oSerieDTO.NumeroFinal = int.Parse(drd["NumeroFinal"].ToString());
                        oSerieDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oSerieDTO.Documento = Convert.ToInt32(drd["Documento"].ToString());
                        lstSerieDTO.Add(oSerieDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSerieDTO;
        }

        public List<SerieDTO> ObtenerSeriesxIdDocumento(int IdSociedad, int IdDocumento)
        {
            List<SerieDTO> lstSerieDTO = new List<SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSeriexIdDocumento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", (IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@IdDocumento", (IdDocumento));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SerieDTO oSerieDTO = new SerieDTO();
                        oSerieDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSerieDTO.Serie = drd["Serie"].ToString();
                        oSerieDTO.NumeroInicial = int.Parse(drd["NumeroInicial"].ToString());
                        oSerieDTO.NumeroFinal = int.Parse(drd["NumeroFinal"].ToString());
                        oSerieDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oSerieDTO.Documento = Convert.ToInt32(drd["Documento"].ToString());
                        oSerieDTO.IdPeriodo = Convert.ToInt32(drd["IdPeriodo"].ToString());
                        oSerieDTO.IsArticulo = Convert.ToBoolean(drd["IsArticulo"].ToString());
                        oSerieDTO.IsServicio = Convert.ToBoolean(drd["IsServicio"].ToString());
                        oSerieDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oSerieDTO.NombIndicadorPeriodo = drd["NombIndicadorPeriodo"].ToString();


                        lstSerieDTO.Add(oSerieDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSerieDTO;
        }

        public List<SerieDTO> ObtenerSeriesNEW(string IdSociedad)
        {
            List<SerieDTO> lstSerieDTO = new List<SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSerieNew", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    //da.SelectCommand.Parameters.AddWithValue("@Estado", 3);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SerieDTO oSerieDTO = new SerieDTO();
                        oSerieDTO.IdDocumento = int.Parse(drd["IdDocumento"].ToString());
                        oSerieDTO.NombDocumento = drd["NombDocumento"].ToString();
                        oSerieDTO.SerieDefecto =(drd["SerieDefecto"].ToString());
                        oSerieDTO.NumeroInicial = int.Parse(drd["NumeroInicial"].ToString());
                        oSerieDTO.NumeroFinal = int.Parse(drd["NumeroFinal"].ToString());
                        lstSerieDTO.Add(oSerieDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSerieDTO;
        }

        public int UpdateInsertSerie(SerieDTO oSerieDTO, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSeries", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oSerieDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@Documento", oSerieDTO.Documento);
                        da.SelectCommand.Parameters.AddWithValue("@Serie", oSerieDTO.Serie);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroInicial", oSerieDTO.NumeroInicial);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroFinal", oSerieDTO.NumeroFinal);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSerieDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        da.SelectCommand.Parameters.AddWithValue("@IdPeriodo", oSerieDTO.IdPeriodo);
                        da.SelectCommand.Parameters.AddWithValue("@IsArticulo", oSerieDTO.IsArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IsServicio", oSerieDTO.IsServicio);

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

        public List<SerieDTO> ObtenerDatosxID(int IdSerie)
        {
            List<SerieDTO> lstSerieDTO = new List<SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSeriesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSerie", IdSerie);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SerieDTO oSerieDTO = new SerieDTO();
                        oSerieDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSerieDTO.Documento = int.Parse(drd["Documento"].ToString());

                        oSerieDTO.Serie = drd["Serie"].ToString();
                        oSerieDTO.NumeroInicial = int.Parse(drd["NumeroInicial"].ToString());
                        oSerieDTO.NumeroFinal = int.Parse(drd["NumeroFinal"].ToString());
                        oSerieDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSerieDTO.Add(oSerieDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSerieDTO;
        }


        public int Delete(int IdSerie)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarSerie", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", IdSerie);
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



        public List<SerieDTO> ValidarNumeracionSerieSolicitudRQ(int IdSerie)
        {
            List<SerieDTO> lstSerieDTO = new List<SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarNumeracionSerieSolicitudRQ", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSerie", IdSerie);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SerieDTO oSerieDTO = new SerieDTO();
                        oSerieDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSerieDTO.Serie = drd["Serie"].ToString();
                        oSerieDTO.NumeroInicial = int.Parse(drd["Numero"].ToString());
                        lstSerieDTO.Add(oSerieDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSerieDTO;
        }


        public SerieDTO ObtenerDatosSerieValidacion(int IdSerie,int IdDocumento,int Orden,string fecha)
        {
            SerieDTO oSerieDTO = new SerieDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDatosSerieValidacion", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSerie", IdSerie);
                    da.SelectCommand.Parameters.AddWithValue("@IdDocumento", IdDocumento);
                    da.SelectCommand.Parameters.AddWithValue("@Orden", Orden);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oSerieDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSerieDTO.Documento = int.Parse(drd["Documento"].ToString());
                        oSerieDTO.Serie = drd["Serie"].ToString();
                        oSerieDTO.NumeroInicial = int.Parse(drd["NumeroInicial"].ToString());
                        oSerieDTO.NumeroFinal = int.Parse(drd["NumeroFinal"].ToString());
                        oSerieDTO.IdPeriodo = int.Parse(drd["IdPeriodo"].ToString());
                        oSerieDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oSerieDTO.IsArticulo = bool.Parse(drd["IsArticulo"].ToString());
                        oSerieDTO.IsServicio = bool.Parse(drd["IsServicio"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return oSerieDTO;
        }
    }
}
