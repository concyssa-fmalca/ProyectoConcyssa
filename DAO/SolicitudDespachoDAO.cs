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
    public class SolicitudDespachoDAO
    {

        public List<SolicitudDespachoDTO> ObtenerSolicitudesDespacho(string IdUsuario,string IdSociedad, int ignorar_primeros, int cantidad_filas, string filtro)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespacho", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@ignorar_primeros", ignorar_primeros);
                    da.SelectCommand.Parameters.AddWithValue("@cantidad_filas", cantidad_filas);
                    da.SelectCommand.Parameters.AddWithValue("@filtro", filtro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudDespachoDTO oSolicitudDespachoDTO = new SolicitudDespachoDTO();
                        oSolicitudDespachoDTO.Id = int.Parse(drd["Id"].ToString());
                        oSolicitudDespachoDTO.Serie = drd["Serie"].ToString();
                        oSolicitudDespachoDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudDespachoDTO.DescripcionCuadrilla = drd["DescripcionCuadrilla"].ToString();
                        oSolicitudDespachoDTO.DescripcionObra = drd["DescripcionObra"].ToString();
                        oSolicitudDespachoDTO.DescripcionBase = (drd["DescripcionBase"].ToString());
                        oSolicitudDespachoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudDespachoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                       
                        lstSolicitudDespachoDTO.Add(oSolicitudDespachoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudDespachoDTO;
        }


        public List<SolicitudDespachoDTO> ObtenerDatosxID(int IdSolicitudDespacho)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();

            SolicitudDespachoDTO oSolicitudDespachoDTO = new SolicitudDespachoDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespachoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", IdSolicitudDespacho);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oSolicitudDespachoDTO.Id = int.Parse(drd["Id"].ToString());
                        oSolicitudDespachoDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSolicitudDespachoDTO.Serie = drd["Serie"].ToString();
                        oSolicitudDespachoDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudDespachoDTO.IdClaseProducto = int.Parse(drd["IdClaseProducto"].ToString());
                        oSolicitudDespachoDTO.IdTipoProducto = int.Parse(drd["IdTipoProducto"].ToString());
                        oSolicitudDespachoDTO.IdCuadrilla = int.Parse(drd["IdCuadrilla"].ToString());
                        oSolicitudDespachoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oSolicitudDespachoDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oSolicitudDespachoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudDespachoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oSolicitudDespachoDTO.Comentario = (String.IsNullOrEmpty(drd["Comentario"].ToString()) ? "" : drd["Comentario"].ToString());




                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }


            Int32 filasdetalle = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespachoDetallexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", IdSolicitudDespacho);
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

            oSolicitudDespachoDTO.Detalles = new SolicitudDespachoDetalleDTO[filasdetalle];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespachoDetallexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", IdSolicitudDespacho);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        SolicitudDespachoDetalleDTO oSolicitudDespachoDetalleDTO = new SolicitudDespachoDetalleDTO();
                        oSolicitudDespachoDetalleDTO.Id = int.Parse(drd["Id"].ToString());
                        oSolicitudDespachoDetalleDTO.IdItem = int.Parse(drd["IdItem"].ToString());
                        oSolicitudDespachoDetalleDTO.CodigoArticulo =  (drd["CodigoArticulo"].ToString());
                        oSolicitudDespachoDetalleDTO.Descripcion = (drd["Descripcion"].ToString());
                        oSolicitudDespachoDetalleDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oSolicitudDespachoDetalleDTO.IdGrupoUnidadMedida = int.Parse((String.IsNullOrEmpty(drd["IdGrupoUnidadMedida"].ToString()) ? "0" : drd["IdGrupoUnidadMedida"].ToString()));
                        oSolicitudDespachoDetalleDTO.IdDefinicionGrupoUnidad = int.Parse((String.IsNullOrEmpty(drd["IdDefinicionUnidadMedida"].ToString()) ? "0" : drd["IdDefinicionUnidadMedida"].ToString()));
                        oSolicitudDespachoDetalleDTO.Cantidad = decimal.Parse(drd["Cantidad"].ToString());



                        oSolicitudDespachoDTO.Detalles[posicion] = oSolicitudDespachoDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

                lstSolicitudDespachoDTO.Add(oSolicitudDespachoDTO);

            }

            //Int32 filasdetalleAnexo = 0;
            //using (SqlConnection cn = new Conexion().conectar())
            //{
            //    try
            //    {
            //        cn.Open();
            //        SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosSolicitudRQxId", cn);
            //        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
            //        da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //        SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
            //        while (dr1.Read())
            //        {
            //            filasdetalleAnexo++;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}

            //oSolicitudRQDTO.AnexoDetalle = new AnexoDTO[filasdetalleAnexo];
            //using (SqlConnection cn = new Conexion().conectar())
            //{
            //    try
            //    {
            //        cn.Open();
            //        SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosSolicitudRQxId", cn);
            //        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
            //        da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //        SqlDataReader drd = da.SelectCommand.ExecuteReader();
            //        Int32 posicion = 0;
            //        while (drd.Read())
            //        {
            //            AnexoDTO oAnexoDTO = new AnexoDTO();
            //            oAnexoDTO.IdAnexo = Convert.ToInt32(drd["IdAnexo"].ToString());
            //            oAnexoDTO.ruta = (drd["ruta"].ToString());
            //            oAnexoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
            //            oAnexoDTO.Tabla = (drd["Tabla"].ToString());
            //            oAnexoDTO.IdTabla = Convert.ToInt32(drd["IdTabla"].ToString());
            //            oAnexoDTO.NombreArchivo = (drd["NombreArchivo"].ToString());
            //            oSolicitudRQDTO.AnexoDetalle[posicion] = oAnexoDTO;
            //            posicion = posicion + 1;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //    }

            //    lstSolicitudRQDTO.Add(oSolicitudRQDTO);

            //}



            return lstSolicitudDespachoDTO;
        }


        public List<SolicitudDespachoDTO> ObtenerCuadrillaxUsuario(int Idusuario, ref string mensaje_error)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerCuadrillaxUsuario", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Idusuario", Idusuario);
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudDespachoDTO oSolicitudDespachoDTO = new SolicitudDespachoDTO();
                        oSolicitudDespachoDTO.IdCuadrilla = int.Parse(drd["IdCuadrilla"].ToString());
                        oSolicitudDespachoDTO.DescripcionCuadrilla = drd["DescripcionCuadrilla"].ToString();
                        oSolicitudDespachoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oSolicitudDespachoDTO.IdBase = int.Parse(drd["IdBase"].ToString());

                        lstSolicitudDespachoDTO.Add(oSolicitudDespachoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSolicitudDespachoDTO;
        }


        public List<SolicitudDespachoDTO> ObtenerObraBasexCuadrilla(int IdCuadrilla, ref string mensaje_error)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerObraBasexCuadrilla", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", IdCuadrilla);
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudDespachoDTO oSolicitudDespachoDTO = new SolicitudDespachoDTO();
                        oSolicitudDespachoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oSolicitudDespachoDTO.DescripcionObra = drd["DescripcionObra"].ToString();
                        oSolicitudDespachoDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oSolicitudDespachoDTO.DescripcionBase = drd["DescripcionBase"].ToString();

                        lstSolicitudDespachoDTO.Add(oSolicitudDespachoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSolicitudDespachoDTO;
        }



        public int UpdateInsertSolicitudDespacho(SolicitudDespachoDTO oSolicitudDespachoDTO, ref string mensaje_error, string IdSociedad,string IdUsuario)
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
                        int rpta = 0;
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudDespacho", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", oSolicitudDespachoDTO.Id);
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oSolicitudDespachoDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@Serie", oSolicitudDespachoDTO.Serie);
                        da.SelectCommand.Parameters.AddWithValue("@Numero", oSolicitudDespachoDTO.Numero);
                        da.SelectCommand.Parameters.AddWithValue("@IdClaseProducto", oSolicitudDespachoDTO.IdClaseProducto);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", oSolicitudDespachoDTO.IdTipoProducto);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oSolicitudDespachoDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oSolicitudDespachoDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdBase", oSolicitudDespachoDTO.IdBase);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oSolicitudDespachoDTO.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oSolicitudDespachoDTO.FechaContabilizacion);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oSolicitudDespachoDTO.Comentario);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", int.Parse(IdUsuario));
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                      
                        int IdInsert = 0;
                        bool EsNuevoIngreso = false;
                        if (oSolicitudDespachoDTO.Id > 0)
                        {
                            IdInsert = oSolicitudDespachoDTO.Id;
                            da.SelectCommand.ExecuteNonQuery();
                        }
                        else
                        {//cuando es un nuevo requerimiento
                            IdInsert = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                            EsNuevoIngreso = true;
                        }

                        //int rpta = da.SelectCommand.ExecuteNonQuery();
                        //int IdInsertDetalle = 0;
                        for (int i = 0; i < oSolicitudDespachoDTO.Detalle.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertSolicitudDespachoDetalle", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespachoDetalle", oSolicitudDespachoDTO.Detalle[i].Id);
                            dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdItem", oSolicitudDespachoDTO.Detalle[i].IdItem);
                            dad.SelectCommand.Parameters.AddWithValue("@Descripcion", oSolicitudDespachoDTO.Detalle[i].Descripcion);
                            dad.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oSolicitudDespachoDTO.Detalle[i].IdUnidadMedida);
                            dad.SelectCommand.Parameters.AddWithValue("@Cantidad", oSolicitudDespachoDTO.Detalle[i].Cantidad);
                           
                            rpta = dad.SelectCommand.ExecuteNonQuery();

                        }

                        //if (oSolicitudRQDTO.AnexoDetalle != null)
                        //{
                        //    for (int i = 0; i < oSolicitudRQDTO.AnexoDetalle.Count; i++)
                        //    {
                        //        SqlDataAdapter dad = new SqlDataAdapter("SMC_InsertUpdateAnexo", cn);
                        //        dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //        dad.SelectCommand.Parameters.AddWithValue("@ruta", "/Anexos/" + oSolicitudRQDTO.AnexoDetalle[i].NombreArchivo);
                        //        dad.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        //        dad.SelectCommand.Parameters.AddWithValue("@Tabla", "SolicitudRQ");
                        //        dad.SelectCommand.Parameters.AddWithValue("@IdTabla", IdInsert);
                        //        dad.SelectCommand.Parameters.AddWithValue("@NombreArchivo", oSolicitudRQDTO.AnexoDetalle[i].NombreArchivo);
                        //        rpta = dad.SelectCommand.ExecuteNonQuery();
                        //    }
                        //}


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



        public int Delete(int IdSolicitudDespachoDetalle)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarDetalleSolicitudDespacho", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespachoDetalle", IdSolicitudDespachoDetalle);
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
