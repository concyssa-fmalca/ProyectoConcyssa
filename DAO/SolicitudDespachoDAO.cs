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

        public List<SolicitudDespachoDTO> ObtenerSolicitudesDespacho(string IdUsuario,string IdSociedad, int ignorar_primeros, int cantidad_filas, string filtro, string BaseDatos)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespacho", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", int.Parse(IdUsuario));
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
                        oSolicitudDespachoDTO.EstadoSolicitud = int.Parse(drd["EstadoSolicitud"].ToString());
                       
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


        public List<SolicitudDespachoDTO> ObtenerDatosxID(int IdSolicitudDespacho, string BaseDatos)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();

            SolicitudDespachoDTO oSolicitudDespachoDTO = new SolicitudDespachoDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oSolicitudDespachoDTO.EstadoSolicitud = int.Parse(drd["EstadoSolicitud"].ToString());




                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }


            Int32 filasdetalle = 0;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oSolicitudDespachoDetalleDTO.CantidadAtendida = decimal.Parse(drd["CantidadAtendida"].ToString());



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
            //using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
            //using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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


        public List<SolicitudDespachoDTO> ObtenerCuadrillaxUsuario(int Idusuario, string BaseDatos, ref string mensaje_error)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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


        public List<SolicitudDespachoDTO> ObtenerObraBasexCuadrilla(int IdCuadrilla, string BaseDatos, ref string mensaje_error)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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



        public int UpdateInsertSolicitudDespacho(SolicitudDespachoDTO oSolicitudDespachoDTO, string BaseDatos, ref string mensaje_error, string IdSociedad,string IdUsuario)
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



        public int Delete(int IdSolicitudDespachoDetalle, string BaseDatos)
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

        public List<SolicitudDespachoDetalleDTO> ObtenerSolicitudesDespachoxObra(int IdSociedad, int IdObra, string BaseDatos)
        {
            List<SolicitudDespachoDetalleDTO> lstSolicitudDespachoDetalleDTO = new List<SolicitudDespachoDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespachoxObra", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra",IdObra);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudDespachoDetalleDTO oSolicitudDespachoDetalleDTO = new SolicitudDespachoDetalleDTO();
                        oSolicitudDespachoDetalleDTO.Id = int.Parse(drd["Id"].ToString());
                        oSolicitudDespachoDetalleDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oSolicitudDespachoDetalleDTO.IdSolicitudDespacho = int.Parse(drd["IdSolicitudDespacho"].ToString());
                        oSolicitudDespachoDetalleDTO.IdItem = int.Parse(drd["IdItem"].ToString());
                        oSolicitudDespachoDetalleDTO.Descripcion = drd["Descripcion"].ToString();
                        oSolicitudDespachoDetalleDTO.SerieyNum = drd["SerieyNum"].ToString();
                        oSolicitudDespachoDetalleDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oSolicitudDespachoDetalleDTO.IdGrupoUnidadMedida = int.Parse(drd["IdGrupoUnidadMedida"].ToString());
                        oSolicitudDespachoDetalleDTO.IdDefinicionGrupoUnidad = int.Parse(drd["IdDefinicionUnidadMedida"].ToString());
                        oSolicitudDespachoDetalleDTO.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oSolicitudDespachoDetalleDTO.CantidadAtendida = Convert.ToDecimal(drd["CantidadAtendida"].ToString());
                        oSolicitudDespachoDetalleDTO.CantidadAtendida = Convert.ToDecimal(drd["CantidadAtendida"].ToString());
                        oSolicitudDespachoDetalleDTO.IdTipoProducto = int.Parse(drd["IdTipoProducto"].ToString());
                        oSolicitudDespachoDetalleDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oSolicitudDespachoDetalleDTO.NombCuadrilla = drd["NombCuadrilla"].ToString();

                        lstSolicitudDespachoDetalleDTO.Add(oSolicitudDespachoDetalleDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudDespachoDetalleDTO;
        }
        public List<SolicitudDespachoDTO> ObtenerSolicitudesDespachoAtender(int IdSociedad, int IdBase,DateTime FechaInicio,DateTime FechaFin, int EstadoSolicitud,int SerieFiltro, string BaseDatos)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();

           
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {

                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespachoCabezeraAprobadas", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoSolicitud", EstadoSolicitud);
                    da.SelectCommand.Parameters.AddWithValue("@SerieFiltro", SerieFiltro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudDespachoDTO oSolicitudDespachoDTO = new SolicitudDespachoDTO();
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
                        oSolicitudDespachoDTO.NombCuadrilla = drd["NombCuadrilla"].ToString();
                        oSolicitudDespachoDTO.SerieyNum = drd["SerieyNum"].ToString();
                        oSolicitudDespachoDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oSolicitudDespachoDTO.EstadoSolicitud = int.Parse(drd["EstadoSolicitud"].ToString());



                        int IdSolicitudDespacho = int.Parse(drd["Id"].ToString());
                        Int32 filasdetalle = 0;
                        using (SqlConnection cn2 = new Conexion().conectar(BaseDatos))
                        {
                            try
                            {
                                cn2.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter("SMC_ListarSolicitudDespachoDetallexIDAprobados", cn2);
                                da2.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", IdSolicitudDespacho);
                                da2.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader dr2 = da2.SelectCommand.ExecuteReader();
                                while (dr2.Read())
                                {
                                    filasdetalle++;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        oSolicitudDespachoDTO.Detalles = new SolicitudDespachoDetalleDTO[filasdetalle];
                        using (SqlConnection cn3 = new Conexion().conectar(BaseDatos))
                        {
                            try
                            {
                                cn3.Open();
                                SqlDataAdapter da3 = new SqlDataAdapter("SMC_ListarSolicitudDespachoDetallexIDAprobados", cn3);
                                da3.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", IdSolicitudDespacho);
                                da3.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader drd3 = da3.SelectCommand.ExecuteReader();
                                Int32 posicion = 0;
                                while (drd3.Read())
                                {
                                    SolicitudDespachoDetalleDTO oSolicitudDespachoDetalleDTO = new SolicitudDespachoDetalleDTO();
                                    oSolicitudDespachoDetalleDTO.Id = int.Parse(drd3["Id"].ToString());
                                    oSolicitudDespachoDetalleDTO.IdItem = int.Parse(drd3["IdItem"].ToString());
                                    oSolicitudDespachoDetalleDTO.CodigoArticulo = (drd3["CodigoArticulo"].ToString());
                                    oSolicitudDespachoDetalleDTO.Descripcion = (drd3["Descripcion"].ToString());
                                    oSolicitudDespachoDetalleDTO.IdUnidadMedida = int.Parse(drd3["IdUnidadMedida"].ToString());
                                    oSolicitudDespachoDetalleDTO.IdGrupoUnidadMedida = int.Parse((String.IsNullOrEmpty(drd3["IdGrupoUnidadMedida"].ToString()) ? "0" : drd3["IdGrupoUnidadMedida"].ToString()));
                                    oSolicitudDespachoDetalleDTO.IdDefinicionGrupoUnidad = int.Parse((String.IsNullOrEmpty(drd3["IdDefinicionUnidadMedida"].ToString()) ? "0" : drd3["IdDefinicionUnidadMedida"].ToString()));
                                    oSolicitudDespachoDetalleDTO.Cantidad = decimal.Parse(drd3["CantidadAprobada"].ToString());
                                    oSolicitudDespachoDetalleDTO.CantidadAtendida = decimal.Parse(drd3["CantidadAtendida"].ToString());



                                    oSolicitudDespachoDTO.Detalles[posicion] = oSolicitudDespachoDetalleDTO;
                                    posicion = posicion + 1;
                                }

                            }
                            catch (Exception ex)
                            {
                            }

                            lstSolicitudDespachoDTO.Add(oSolicitudDespachoDTO);

                        }




                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }

            return lstSolicitudDespachoDTO;
        }

        public int UpdateInsertSolicitudDespachoDetalle(SolicitudDespachoDetalleDTO oSolicitudDespachoDetalleDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertSolicitudDespachoDetalle", cn);
                        dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                        dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespachoDetalle", oSolicitudDespachoDetalleDTO.Id);
                        dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", oSolicitudDespachoDetalleDTO.IdSolicitudDespacho);
                        dad.SelectCommand.Parameters.AddWithValue("@IdItem", oSolicitudDespachoDetalleDTO.IdItem);
                        dad.SelectCommand.Parameters.AddWithValue("@Descripcion", oSolicitudDespachoDetalleDTO.Descripcion);
                        dad.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oSolicitudDespachoDetalleDTO.IdUnidadMedida);
                        dad.SelectCommand.Parameters.AddWithValue("@Cantidad", oSolicitudDespachoDetalleDTO.Cantidad);
                        int rpta = Convert.ToInt32(dad.SelectCommand.ExecuteNonQuery());
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

        public int AtencionConfirmada(int Cantidad,int IdSolicitud,int IdArticulo, int EstadoSolicitud, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter dad = new SqlDataAdapter("SMC_SolicitudAtendida", cn);
                        dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                        dad.SelectCommand.Parameters.AddWithValue("@Cantidad", Cantidad);
                        dad.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                        dad.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                        dad.SelectCommand.Parameters.AddWithValue("@EstadoSolicitud", EstadoSolicitud);
                        int rpta = Convert.ToInt32(dad.SelectCommand.ExecuteNonQuery());
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
        public int CerrarSolicitud(int IdSolicitud,int IdUsuario, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter dad = new SqlDataAdapter("SMC_CerrarSolicitud", cn);
                        dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                        dad.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                        dad.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        int rpta = Convert.ToInt32(dad.SelectCommand.ExecuteNonQuery());
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


        public int UpdateInsertSolicitudDespachoMovil(SolcitudDespachoMovil oSolcitudDespachoMovil, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertSolicitudDespachoMobile", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", oSolcitudDespachoMovil.IdTipoProducto);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oSolcitudDespachoMovil.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oSolcitudDespachoMovil.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oSolcitudDespachoMovil.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oSolcitudDespachoMovil.Comentario);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oSolcitudDespachoMovil.UsuarioCreador);

                        int IdInsert = 0;
                        int rptaDet = 0;
                        bool EsNuevoIngreso = false;


                        IdInsert = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        EsNuevoIngreso = true;


                        //int rpta = da.SelectCommand.ExecuteNonQuery();
                        //int IdInsertDetalle = 0;
                        for (int i = 0; i < oSolcitudDespachoMovil.Detalles.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_InsertSolicitudDespachoDetalleMobile", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespacho", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdItem", oSolcitudDespachoMovil.Detalles[i].IdItem);
                            dad.SelectCommand.Parameters.AddWithValue("@Descripcion", oSolcitudDespachoMovil.Detalles[i].Descripcion);
                            dad.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oSolcitudDespachoMovil.Detalles[i].IdUnidadMedida);
                            dad.SelectCommand.Parameters.AddWithValue("@Cantidad", oSolcitudDespachoMovil.Detalles[i].Cantidad);

                            rptaDet = dad.SelectCommand.ExecuteNonQuery();

                        }


                        transactionScope.Complete();

                        if (rptaDet == 0) return 0;

                        return IdInsert;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }

            }
        }

        public List<SolicitudDetalleMovilAprobacion> ObtenerSolicitudesDespachoDetalleAprobacion(int IdUsuario, DateTime FechaInicio, DateTime FechaFinal, int EstadoPR, string BaseDatos)
        {
            List<SolicitudDetalleMovilAprobacion> lstSolicitudDetalleMovilAprobacion = new List<SolicitudDetalleMovilAprobacion>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespachoDetalleAutorizar", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoPR", EstadoPR);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudDetalleMovilAprobacion oSolicitudDetalleMovilAprobacion = new SolicitudDetalleMovilAprobacion();
                        oSolicitudDetalleMovilAprobacion.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oSolicitudDetalleMovilAprobacion.IdSolicitud = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudDetalleMovilAprobacion.CodigoArticulo = (drd["CodigoArticulo"].ToString());
                        oSolicitudDetalleMovilAprobacion.Descripcion1 = drd["Descripcion1"].ToString();
                        oSolicitudDetalleMovilAprobacion.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudDetalleMovilAprobacion.CantidadSolicitada = decimal.Parse(drd["CantidadSolicitada"].ToString());
                        oSolicitudDetalleMovilAprobacion.CantidadAprobada = decimal.Parse(drd["CantidadAprobada"].ToString());
                        oSolicitudDetalleMovilAprobacion.Accion = int.Parse(drd["Accion"].ToString());
                        oSolicitudDetalleMovilAprobacion.IdSolicitudDespachoModelo = int.Parse(drd["IdSolicitudDespachoModelo"].ToString());
                        oSolicitudDetalleMovilAprobacion.IdSolicitudDespachoDetalle = int.Parse(drd["IdSolicitudDespachoDetalle"].ToString());
                        oSolicitudDetalleMovilAprobacion.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudDetalleMovilAprobacion.FechaCreacion = Convert.ToDateTime(drd["FechaCreacion"].ToString());
                        oSolicitudDetalleMovilAprobacion.EstadoSolicitud = int.Parse(drd["EstadoSolicitud"].ToString());
                        oSolicitudDetalleMovilAprobacion.IdUsuario = int.Parse(drd["IdUsuario"].ToString());
                        oSolicitudDetalleMovilAprobacion.IdEtapa = int.Parse(drd["IdEtapa"].ToString());
                        oSolicitudDetalleMovilAprobacion.Serie = (drd["Serie"].ToString());
                        oSolicitudDetalleMovilAprobacion.Referencia = (drd["Referencia"].ToString());
                        oSolicitudDetalleMovilAprobacion.NombUsuario = (drd["NombUsuario"].ToString());
                        oSolicitudDetalleMovilAprobacion.CodCuadrilla = (drd["CodCuadrilla"].ToString());
                        oSolicitudDetalleMovilAprobacion.NombCuadrilla = (drd["NombCuadrilla"].ToString());

                        lstSolicitudDetalleMovilAprobacion.Add(oSolicitudDetalleMovilAprobacion);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudDetalleMovilAprobacion;
        }

        public int UpdateInsertModeloAprobacionesDespacho(SolicitudDespachoModeloAprobacionesDTO oSolicitudDespachoModeloAprobacionesDTO, string BaseDatos)
        {
         
                int rpta = 0;
                
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
                                SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudDespachoModeloAprobaciones", cn);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", oSolicitudDespachoModeloAprobacionesDTO.IdSolicitud);
                                da.SelectCommand.Parameters.AddWithValue("@IdSolicitudModelo", oSolicitudDespachoModeloAprobacionesDTO.IdSolicitudModelo);
                                da.SelectCommand.Parameters.AddWithValue("@IdAutorizador", oSolicitudDespachoModeloAprobacionesDTO.IdAutorizador);
                                da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oSolicitudDespachoModeloAprobacionesDTO.IdArticulo);
                                da.SelectCommand.Parameters.AddWithValue("@Accion", oSolicitudDespachoModeloAprobacionesDTO.Accion);
                                da.SelectCommand.Parameters.AddWithValue("@IdDetalle", oSolicitudDespachoModeloAprobacionesDTO.IdDetalle);
                                da.SelectCommand.Parameters.AddWithValue("@CantidadItem", oSolicitudDespachoModeloAprobacionesDTO.CantidadItem);
                                da.SelectCommand.Parameters.AddWithValue("@Referencia", oSolicitudDespachoModeloAprobacionesDTO.Referencia);
                                rpta = da.SelectCommand.ExecuteNonQuery();
                                transactionScope.Complete();

                            }
                            catch (Exception ex)
                            {
                                return 0;
                            }
                        }
                    }
                
                return rpta;
           
        }

        public List<SolicitudDespachoDTO> SolicitudDespachoPendienteAprobacion(int IdObra, DateTime FechaInicio, DateTime FechaFin, string BaseDatos)
        {
            List<SolicitudDespachoDTO> lstSolicitudDespachoDTO = new List<SolicitudDespachoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDespachoPendienteAprobacion", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudDespachoDTO oSolicitudDespachoDTO = new SolicitudDespachoDTO();
                        oSolicitudDespachoDTO.NumDoc = (drd["NocDoc"].ToString());
                        oSolicitudDespachoDTO.Codigo = drd["Codigo"].ToString();
                        oSolicitudDespachoDTO.Descripcion = (drd["Descripcion1"].ToString());
                        oSolicitudDespachoDTO.DescripcionCuadrilla = drd["Cuadrilla"].ToString();
                        oSolicitudDespachoDTO.DescripcionObra = drd["Obra"].ToString();
                        oSolicitudDespachoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudDespachoDTO.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());

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

    }
}
