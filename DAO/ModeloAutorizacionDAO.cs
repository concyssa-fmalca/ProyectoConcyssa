

using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class ModeloAutorizacionDAO
    {

        public List<ModeloAutorizacionDTO> ObtenerModeloAutorizacion(string IdSociedad, string BaseDatos)
        {
            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = new List<ModeloAutorizacionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacion", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ModeloAutorizacionDTO oModeloAutorizacionDTO = new ModeloAutorizacionDTO();
                        oModeloAutorizacionDTO.IdModeloAutorizacion = int.Parse(drd["Id"].ToString());
                        oModeloAutorizacionDTO.NombreModelo = drd["NombreModelo"].ToString();
                        oModeloAutorizacionDTO.DescripcionModelo = drd["DescripcionModelo"].ToString();
                        oModeloAutorizacionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstModeloAutorizacionDTO.Add(oModeloAutorizacionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstModeloAutorizacionDTO;
        }




        public int UpdateInsertModeloAutorizacion(ModeloAutorizacionDTO oModeloAutorizacionDTO, string IdSociedad, string BaseDatos)
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
                        int rpta = 0;
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertModeloAutorizacion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", oModeloAutorizacionDTO.IdModeloAutorizacion);
                        da.SelectCommand.Parameters.AddWithValue("@NombreModelo", oModeloAutorizacionDTO.NombreModelo);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionModelo", oModeloAutorizacionDTO.DescripcionModelo);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oModeloAutorizacionDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        int IdInsert = 0;

                        if (oModeloAutorizacionDTO.IdModeloAutorizacion > 0)
                        {
                            IdInsert = oModeloAutorizacionDTO.IdModeloAutorizacion;
                            da.SelectCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            IdInsert = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        }

                        for (int i = 0; i < oModeloAutorizacionDTO.DetalleAutor.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertModeloAutorizacionAutor", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacionAutor", oModeloAutorizacionDTO.DetalleAutor[i].IdModeloAutorizacionAutor);
                            dad.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdAutor", oModeloAutorizacionDTO.DetalleAutor[i].IdAutor);
                            dad.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                            rpta = dad.SelectCommand.ExecuteNonQuery();
                        }

                        for (int i = 0; i < oModeloAutorizacionDTO.DetalleEtapa.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertModeloAutorizacionEtapa", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacionEtapa", oModeloAutorizacionDTO.DetalleEtapa[i].IdModeloAutorizacionEtapa);
                            dad.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdEtapa", oModeloAutorizacionDTO.DetalleEtapa[i].IdEtapa);
                            dad.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                            rpta = dad.SelectCommand.ExecuteNonQuery();
                        }

                        for (int i = 0; i < oModeloAutorizacionDTO.DetalleDocumento.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertModeloAutorizacionDocumento", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacionDocumento", oModeloAutorizacionDTO.DetalleDocumento[i].IdModeloAutorizacionDocumento);
                            dad.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdDocumento", oModeloAutorizacionDTO.DetalleDocumento[i].IdDocumento);
                            dad.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                            rpta = dad.SelectCommand.ExecuteNonQuery();
                        }

                        for (int i = 0; i < oModeloAutorizacionDTO.DetalleCondicion.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertModeloAutorizacionCondicion", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacionCondicion", oModeloAutorizacionDTO.DetalleCondicion[i].IdModeloAutorizacionCondicion);
                            dad.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@Condicion", oModeloAutorizacionDTO.DetalleCondicion[i].Condicion);
                            dad.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                            rpta = dad.SelectCommand.ExecuteNonQuery();
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



        public List<ModeloAutorizacionDTO> ObtenerDatosxID(int IdModeloAutorizacion, string BaseDatos)
        {
            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = new List<ModeloAutorizacionDTO>();
            ModeloAutorizacionDTO oModeloAutorizacionDTO = new ModeloAutorizacionDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oModeloAutorizacionDTO.IdModeloAutorizacion = int.Parse(drd["Id"].ToString());
                        oModeloAutorizacionDTO.NombreModelo = drd["NombreModelo"].ToString();
                        oModeloAutorizacionDTO.DescripcionModelo = drd["DescripcionModelo"].ToString();
                        oModeloAutorizacionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }

            //autor
            Int32 filasdetalle = 0;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionAutorxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalle++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oModeloAutorizacionDTO.DetallesAutor = new ModeloAutorizacionAutorDTO[filasdetalle];
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionAutorxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        ModeloAutorizacionAutorDTO oModeloAutorizacionAutorDTO = new ModeloAutorizacionAutorDTO();
                        oModeloAutorizacionAutorDTO.IdModeloAutorizacionAutor = int.Parse(drd["Id"].ToString());
                        oModeloAutorizacionAutorDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
                        oModeloAutorizacionAutorDTO.IdAutor = int.Parse(drd["IdAutor"].ToString());
                        //oModeloAutorizacionAutorDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
                        oModeloAutorizacionDTO.DetallesAutor[posicion] = oModeloAutorizacionAutorDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
                //autor
            }


            //etapas
            Int32 filasdetalleEtapas = 0;
            using (SqlConnection cn1 = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn1.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionEtapaxID", cn1);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalleEtapas++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oModeloAutorizacionDTO.DetallesEtapa = new ModeloAutorizacionEtapaDTO[filasdetalleEtapas];
            using (SqlConnection cn1 = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn1.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionEtapaxID", cn1);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        ModeloAutorizacionEtapaDTO oModeloAutorizacionEtapaDTO = new ModeloAutorizacionEtapaDTO();
                        oModeloAutorizacionEtapaDTO.IdModeloAutorizacionEtapa = int.Parse(drd["Id"].ToString());
                        oModeloAutorizacionEtapaDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
                        oModeloAutorizacionEtapaDTO.IdEtapa = int.Parse(drd["IdEtapa"].ToString());
                        oModeloAutorizacionEtapaDTO.DescripcionEtapa = drd["DescripcionEtapa"].ToString();
                        oModeloAutorizacionEtapaDTO.AutorizacionesRequeridas = int.Parse(drd["AutorizacionesRequeridas"].ToString());
                        oModeloAutorizacionEtapaDTO.RechazosRequeridos = int.Parse(drd["RechazosRequeridos"].ToString());
                        oModeloAutorizacionDTO.DetallesEtapa[posicion] = oModeloAutorizacionEtapaDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
            }

            //etapas



            //documentos
            Int32 filasdetalleDocumentos = 0;
            using (SqlConnection cn2 = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn2.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionDocumentoxID", cn2);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalleDocumentos++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oModeloAutorizacionDTO.DetallesDocumento = new ModeloAutorizacionDocumentoDTO[filasdetalleDocumentos];
            using (SqlConnection cn2 = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn2.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionDocumentoxID", cn2);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        ModeloAutorizacionDocumentoDTO oModeloAutorizacionDocumentoDTO = new ModeloAutorizacionDocumentoDTO();
                        oModeloAutorizacionDocumentoDTO.IdModeloAutorizacionDocumento = int.Parse(drd["Id"].ToString());
                        oModeloAutorizacionDocumentoDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
                        oModeloAutorizacionDocumentoDTO.IdDocumento = int.Parse(drd["IdDocumento"].ToString());
                        oModeloAutorizacionDTO.DetallesDocumento[posicion] = oModeloAutorizacionDocumentoDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

                //documentos
            }




            //condiciones
            Int32 filasdetalleCondiciones = 0;
            using (SqlConnection cn2 = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn2.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionCondicionxID", cn2);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalleCondiciones++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oModeloAutorizacionDTO.DetallesCondicion = new ModeloAutorizacionCondicionDTO[filasdetalleCondiciones];
            using (SqlConnection cn2 = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn2.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionCondicionxID", cn2);
                    da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        ModeloAutorizacionCondicionDTO oModeloAutorizacionCondicionDTO = new ModeloAutorizacionCondicionDTO();
                        oModeloAutorizacionCondicionDTO.IdModeloAutorizacionCondicion = int.Parse(drd["Id"].ToString());
                        oModeloAutorizacionCondicionDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
                        oModeloAutorizacionCondicionDTO.Condicion = drd["Condicion"].ToString();
                        oModeloAutorizacionDTO.DetallesCondicion[posicion] = oModeloAutorizacionCondicionDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

                //condiciones



                lstModeloAutorizacionDTO.Add(oModeloAutorizacionDTO);
            }



            return lstModeloAutorizacionDTO;

        }



        public int EliminarModeloAutorizacionDetalleEtapa(int IdModeloAutorizacionEtapa, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarModeloAutorizacionDetalleEtapa", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacionEtapa", IdModeloAutorizacionEtapa);
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


        public int EliminarModeloAutorizacionDetalleCondicion(int IdModeloAutorizacionCondicion, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarModeloAutorizacionDetalleCondicion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacionCondicion", IdModeloAutorizacionCondicion);
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


        public int EliminarModeloAutorizacionDetalleAutor(int IdModeloAutorizacionAutor, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarModeloAutorizacionDetalleAutor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacionAutor", IdModeloAutorizacionAutor);
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



        public List<ModeloAutorizacionDTO> VerificarExisteModeloSolicitud(int IdSociedad,int IdDocumento, string BaseDatos)
        {
            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = new List<ModeloAutorizacionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_VerificarExisteModeloSolicitud", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdDocumento", IdDocumento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ModeloAutorizacionDTO oModeloAutorizacionDTO = new ModeloAutorizacionDTO();
                        oModeloAutorizacionDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
                        lstModeloAutorizacionDTO.Add(oModeloAutorizacionDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }
            return lstModeloAutorizacionDTO;
        }





        //public List<ModeloAutorizacionDTO> ObtenerDatosxIDConexionEnviada(int IdModeloAutorizacion,SqlConnection cn)
        //{
        //    List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = new List<ModeloAutorizacionDTO>();
        //    ModeloAutorizacionDTO oModeloAutorizacionDTO = new ModeloAutorizacionDTO();

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader drd = da.SelectCommand.ExecuteReader();
        //            while (drd.Read())
        //            {

        //                oModeloAutorizacionDTO.IdModeloAutorizacion = int.Parse(drd["Id"].ToString());
        //                oModeloAutorizacionDTO.NombreModelo = drd["NombreModelo"].ToString();
        //                oModeloAutorizacionDTO.DescripcionModelo = drd["DescripcionModelo"].ToString();
        //                oModeloAutorizacionDTO.Estado = bool.Parse(drd["Estado"].ToString());
        //            }
        //            drd.Close();


        //        }
        //        catch (Exception ex)
        //        {
        //        }


        //    //autor
        //    Int32 filasdetalle = 0;

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionAutorxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
        //            while (dr1.Read())
        //            {
        //                filasdetalle++;
        //            }
        //            dr1.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //        }


        //    oModeloAutorizacionDTO.DetallesAutor = new ModeloAutorizacionAutorDTO[filasdetalle];

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionAutorxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader drd = da.SelectCommand.ExecuteReader();
        //            Int32 posicion = 0;
        //            while (drd.Read())
        //            {
        //                ModeloAutorizacionAutorDTO oModeloAutorizacionAutorDTO = new ModeloAutorizacionAutorDTO();
        //                oModeloAutorizacionAutorDTO.IdModeloAutorizacionAutor = int.Parse(drd["Id"].ToString());
        //                oModeloAutorizacionAutorDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
        //                oModeloAutorizacionAutorDTO.IdAutor = int.Parse(drd["IdAutor"].ToString());
        //                oModeloAutorizacionAutorDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
        //                oModeloAutorizacionDTO.DetallesAutor[posicion] = oModeloAutorizacionAutorDTO;
        //                posicion = posicion + 1;
        //            }
        //            drd.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        //autor



        //    //etapas
        //    Int32 filasdetalleEtapas = 0;

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionEtapaxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
        //            while (dr1.Read())
        //            {
        //                filasdetalleEtapas++;
        //            }
        //            dr1.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //        }


        //    oModeloAutorizacionDTO.DetallesEtapa = new ModeloAutorizacionEtapaDTO[filasdetalleEtapas];

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionEtapaxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader drd = da.SelectCommand.ExecuteReader();
        //            Int32 posicion = 0;
        //            while (drd.Read())
        //            {
        //                ModeloAutorizacionEtapaDTO oModeloAutorizacionEtapaDTO = new ModeloAutorizacionEtapaDTO();
        //                oModeloAutorizacionEtapaDTO.IdModeloAutorizacionEtapa = int.Parse(drd["Id"].ToString());
        //                oModeloAutorizacionEtapaDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
        //                oModeloAutorizacionEtapaDTO.IdEtapa = int.Parse(drd["IdEtapa"].ToString());
        //                oModeloAutorizacionEtapaDTO.DescripcionEtapa = drd["DescripcionEtapa"].ToString();
        //                oModeloAutorizacionEtapaDTO.AutorizacionesRequeridas = int.Parse(drd["AutorizacionesRequeridas"].ToString());
        //                oModeloAutorizacionEtapaDTO.RechazosRequeridos = int.Parse(drd["RechazosRequeridos"].ToString());
        //                oModeloAutorizacionDTO.DetallesEtapa[posicion] = oModeloAutorizacionEtapaDTO;
        //                posicion = posicion + 1;
        //            }
        //            drd.Close();

        //        }
        //        catch (Exception ex)
        //        {
        //        }


        //    //etapas



        //    //documentos
        //    Int32 filasdetalleDocumentos = 0;

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionDocumentoxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
        //            while (dr1.Read())
        //            {
        //                filasdetalleDocumentos++;
        //            }
        //            dr1.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //        }


        //    oModeloAutorizacionDTO.DetallesDocumento = new ModeloAutorizacionDocumentoDTO[filasdetalleDocumentos];

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionDocumentoxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader drd = da.SelectCommand.ExecuteReader();
        //            Int32 posicion = 0;
        //            while (drd.Read())
        //            {
        //                ModeloAutorizacionDocumentoDTO oModeloAutorizacionDocumentoDTO = new ModeloAutorizacionDocumentoDTO();
        //                oModeloAutorizacionDocumentoDTO.IdModeloAutorizacionDocumento = int.Parse(drd["Id"].ToString());
        //                oModeloAutorizacionDocumentoDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
        //                oModeloAutorizacionDocumentoDTO.IdDocumento = int.Parse(drd["IdDocumento"].ToString());
        //                oModeloAutorizacionDTO.DetallesDocumento[posicion] = oModeloAutorizacionDocumentoDTO;
        //                posicion = posicion + 1;
        //            }
        //            drd.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //        }

        //        //documentos





        //    //condiciones
        //    Int32 filasdetalleCondiciones = 0;

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionCondicionxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
        //            while (dr1.Read())
        //            {
        //                filasdetalleCondiciones++;
        //            }
        //            dr1.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //        }


        //    oModeloAutorizacionDTO.DetallesCondicion = new ModeloAutorizacionCondicionDTO[filasdetalleCondiciones];

        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarModeloAutorizacionCondicionxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader drd = da.SelectCommand.ExecuteReader();
        //            Int32 posicion = 0;
        //            while (drd.Read())
        //            {
        //                ModeloAutorizacionCondicionDTO oModeloAutorizacionCondicionDTO = new ModeloAutorizacionCondicionDTO();
        //                oModeloAutorizacionCondicionDTO.IdModeloAutorizacionCondicion = int.Parse(drd["Id"].ToString());
        //                oModeloAutorizacionCondicionDTO.IdModeloAutorizacion = int.Parse(drd["IdModeloAutorizacion"].ToString());
        //                oModeloAutorizacionCondicionDTO.Condicion = drd["Condicion"].ToString();
        //                oModeloAutorizacionDTO.DetallesCondicion[posicion] = oModeloAutorizacionCondicionDTO;
        //                posicion = posicion + 1;
        //            }
        //            drd.Close();
        //    }
        //        catch (Exception ex)
        //        {
        //        }

        //        //condiciones



        //        lstModeloAutorizacionDTO.Add(oModeloAutorizacionDTO);




        //    return lstModeloAutorizacionDTO;

        //}



        public List<ResultadoCondicionDTO> ObtenerResultadoCondicion(string Procedimiento, int IdSolicitud,int IdDocumento, string BaseDatos)
        {
            List<ResultadoCondicionDTO> lstResultadoCondicionDTO = new List<ResultadoCondicionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(Procedimiento, cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                    da.SelectCommand.Parameters.AddWithValue("@IdDocumento", IdDocumento);
                    da.SelectCommand.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    da.SelectCommand.Parameters.Add("@message", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                    int fila = da.SelectCommand.ExecuteNonQuery();

                    //if (fila>0)
                    //{
                    ResultadoCondicionDTO oResultadoCondicionDTO = new ResultadoCondicionDTO();
                    oResultadoCondicionDTO.EntraAlProcesoAutorizar = Convert.ToInt32(da.SelectCommand.Parameters["@resultado"].Value);//int.Parse(drd["@resultado"].ToString());
                    oResultadoCondicionDTO.Mensaje = da.SelectCommand.Parameters["@message"].Value.ToString();

                    lstResultadoCondicionDTO.Add(oResultadoCondicionDTO);
                    //}




                }
                catch (Exception ex)
                {
                }
            }
            return lstResultadoCondicionDTO;
        }


        
        public int EliminarModeloAutorizacion(int IdModeloAutorizacion, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarModeloAutorizacion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdModeloAutorizacion", IdModeloAutorizacion);
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



        public List<ModeloAutorizacionDTO> ValidarAutoresxTipoDocumento(int IdAutor, int IdTipoDocumento, string BaseDatos)
        {
            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = new List<ModeloAutorizacionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarAutoresModeloxDocumento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAutor", (IdAutor));
                    da.SelectCommand.Parameters.AddWithValue("@IdDocumento", (IdTipoDocumento));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ModeloAutorizacionDTO oModeloAutorizacionDTO = new ModeloAutorizacionDTO();
                        oModeloAutorizacionDTO.IdModeloAutorizacion = int.Parse(drd["Id"].ToString());
                        oModeloAutorizacionDTO.NombreModelo = drd["NombreModelo"].ToString();
                        oModeloAutorizacionDTO.DescripcionModelo = drd["DescripcionModelo"].ToString();
                        oModeloAutorizacionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstModeloAutorizacionDTO.Add(oModeloAutorizacionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstModeloAutorizacionDTO;
        }


    }
}
