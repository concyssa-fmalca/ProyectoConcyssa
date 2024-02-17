using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO;

namespace DAO
{
    public class DevolucionAdmDAO
    {
        public int UpdateInsertDevolucionAdm(DevolucionAdmDTO oDevolucionAdmDTO, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertDevolucionAdm", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", oDevolucionAdmDTO.IdTipoProducto);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oDevolucionAdmDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oDevolucionAdmDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oDevolucionAdmDTO.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oDevolucionAdmDTO.Comentario);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oDevolucionAdmDTO.UsuarioCreador);

                        int IdInsert = 0;
                        int rptaDet = 0;
                        bool EsNuevoIngreso = false;


                        IdInsert = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        EsNuevoIngreso = true;


                        //int rpta = da.SelectCommand.ExecuteNonQuery();
                        //int IdInsertDetalle = 0;
                        for (int i = 0; i < oDevolucionAdmDTO.Detalles.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_InsertDevolucionAdmDetalle", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdDevolucionAdm", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdItem", oDevolucionAdmDTO.Detalles[i].IdItem);
                            dad.SelectCommand.Parameters.AddWithValue("@Descripcion", oDevolucionAdmDTO.Detalles[i].Descripcion);
                            dad.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oDevolucionAdmDTO.Detalles[i].IdUnidadMedida);
                            dad.SelectCommand.Parameters.AddWithValue("@Cantidad", oDevolucionAdmDTO.Detalles[i].Cantidad);

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

        public int UpdateInsertDevolucionAdmDetalle(DevolucionAdmDetalle oDevolucionAdmDetalle, string BaseDatos)
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
                        
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_InsertDevolucionAdmDetalle", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdDevolucionAdm", oDevolucionAdmDetalle.IdDevolucionAdm);
                            dad.SelectCommand.Parameters.AddWithValue("@IdItem", oDevolucionAdmDetalle.IdItem);
                            dad.SelectCommand.Parameters.AddWithValue("@Descripcion", oDevolucionAdmDetalle.Descripcion);
                            dad.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oDevolucionAdmDetalle.IdUnidadMedida);
                            dad.SelectCommand.Parameters.AddWithValue("@Cantidad", oDevolucionAdmDetalle.Cantidad);

                        rpta = dad.SelectCommand.ExecuteNonQuery();

                        


                        transactionScope.Complete();

                        if (rpta == 0) return 0;

                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }

            }
        }

        public List<DevolucionAdmDTO> ObtenerDevoluciones(int IdUsuario, int EstadoDevolucion,DateTime FechaInicio,DateTime FechaFin, string BaseDatos)
        {
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = new List<DevolucionAdmDTO>();


            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {

                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDevoluciones", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoDevolucion", EstadoDevolucion);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DevolucionAdmDTO oDevolucionAdmDTO = new DevolucionAdmDTO();
                        oDevolucionAdmDTO.Id = int.Parse(drd["Id"].ToString());
                        oDevolucionAdmDTO.Correlativo = drd["Correlativo"].ToString();
                        oDevolucionAdmDTO.IdTipoProducto = int.Parse(drd["IdTipoProducto"].ToString());
                        oDevolucionAdmDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oDevolucionAdmDTO.EstadoDevolucion = int.Parse(drd["EstadoDevolucion"].ToString());
                        oDevolucionAdmDTO.UsuarioCreador = int.Parse(drd["UsuarioCreador"].ToString());
                        oDevolucionAdmDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oDevolucionAdmDTO.NombObra = drd["NombObra"].ToString();
                        oDevolucionAdmDTO.IdCuadrilla = int.Parse(drd["IdCuadrilla"].ToString());
                        oDevolucionAdmDTO.NombCuadrilla = drd["NombCuadrilla"].ToString();
                        oDevolucionAdmDTO.Comentario = (String.IsNullOrEmpty(drd["Comentario"].ToString()) ? "" : drd["Comentario"].ToString());




                        int IdDevolucion = int.Parse(drd["Id"].ToString());
                        Int32 filasdetalle = 0;
                        using (SqlConnection cn2 = new Conexion().conectar(BaseDatos))
                        {
                            try
                            {
                                cn2.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter("SMC_ListarDevolucionAdmDetallexID", cn2);
                                da2.SelectCommand.Parameters.AddWithValue("@IdDevolucion", IdDevolucion);
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

                        oDevolucionAdmDTO.Detalles = new DevolucionAdmDetalle[filasdetalle];
                        using (SqlConnection cn3 = new Conexion().conectar(BaseDatos))
                        {
                            try
                            {
                                cn3.Open();
                                SqlDataAdapter da3 = new SqlDataAdapter("SMC_ListarDevolucionAdmDetallexID", cn3);
                                da3.SelectCommand.Parameters.AddWithValue("@IdDevolucion", IdDevolucion);
                                da3.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader drd3 = da3.SelectCommand.ExecuteReader();
                                Int32 posicion = 0;
                                while (drd3.Read())
                                {
                                    DevolucionAdmDetalle oDevolucionAdmDetalle = new DevolucionAdmDetalle();
                                    oDevolucionAdmDetalle.Id = int.Parse(drd3["Id"].ToString());
                                    oDevolucionAdmDetalle.IdItem = int.Parse(drd3["IdItem"].ToString());
                                    oDevolucionAdmDetalle.CodigoArticulo = (drd3["CodigoArticulo"].ToString());
                                    oDevolucionAdmDetalle.Descripcion = (drd3["Descripcion"].ToString());
                                    oDevolucionAdmDetalle.IdUnidadMedida = int.Parse(drd3["IdUnidadMedida"].ToString());
                                    oDevolucionAdmDetalle.IdGrupoUnidadMedida = int.Parse((String.IsNullOrEmpty(drd3["IdGrupoUnidadMedida"].ToString()) ? "0" : drd3["IdGrupoUnidadMedida"].ToString()));
                                    oDevolucionAdmDetalle.IdDefinicionGrupoUnidad = int.Parse((String.IsNullOrEmpty(drd3["IdDefinicionUnidadMedida"].ToString()) ? "0" : drd3["IdDefinicionUnidadMedida"].ToString()));
                                    oDevolucionAdmDetalle.Cantidad = decimal.Parse(drd3["Cantidad"].ToString());
                                    oDevolucionAdmDetalle.CantidadAtendida = decimal.Parse(drd3["CantidadAtendida"].ToString());



                                    oDevolucionAdmDTO.Detalles[posicion] = oDevolucionAdmDetalle;
                                    posicion = posicion + 1;
                                }

                            }
                            catch (Exception ex)
                            {
                            }

                            lstSolicitudDespachoDTO.Add(oDevolucionAdmDTO);

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

        public List<DevolucionAdmDTO> ObtenerDevolucionxId(int Id, string BaseDatos)
        {
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = new List<DevolucionAdmDTO>();


            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {

                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDevolucionxId", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdDevolucion", Id);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DevolucionAdmDTO oDevolucionAdmDTO = new DevolucionAdmDTO();
                        oDevolucionAdmDTO.Id = int.Parse(drd["Id"].ToString());
                        oDevolucionAdmDTO.Correlativo = drd["Correlativo"].ToString();
                        oDevolucionAdmDTO.IdTipoProducto = int.Parse(drd["IdTipoProducto"].ToString());
                        oDevolucionAdmDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oDevolucionAdmDTO.EstadoDevolucion = int.Parse(drd["EstadoDevolucion"].ToString());
                        oDevolucionAdmDTO.UsuarioCreador = int.Parse(drd["UsuarioCreador"].ToString());
                        oDevolucionAdmDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oDevolucionAdmDTO.NombObra = drd["NombObra"].ToString();
                        oDevolucionAdmDTO.IdCuadrilla = int.Parse(drd["IdCuadrilla"].ToString());
                        oDevolucionAdmDTO.NombCuadrilla = drd["NombCuadrilla"].ToString();
                        oDevolucionAdmDTO.Comentario = (String.IsNullOrEmpty(drd["Comentario"].ToString()) ? "" : drd["Comentario"].ToString());




                        int IdDevolucion = int.Parse(drd["Id"].ToString());
                        Int32 filasdetalle = 0;
                        using (SqlConnection cn2 = new Conexion().conectar(BaseDatos))
                        {
                            try
                            {
                                cn2.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter("SMC_ListarDevolucionAdmDetallexID", cn2);
                                da2.SelectCommand.Parameters.AddWithValue("@IdDevolucion", IdDevolucion);
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

                        oDevolucionAdmDTO.Detalles = new DevolucionAdmDetalle[filasdetalle];
                        using (SqlConnection cn3 = new Conexion().conectar(BaseDatos))
                        {
                            try
                            {
                                cn3.Open();
                                SqlDataAdapter da3 = new SqlDataAdapter("SMC_ListarDevolucionAdmDetallexID", cn3);
                                da3.SelectCommand.Parameters.AddWithValue("@IdDevolucion", IdDevolucion);
                                da3.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader drd3 = da3.SelectCommand.ExecuteReader();
                                Int32 posicion = 0;
                                while (drd3.Read())
                                {
                                    DevolucionAdmDetalle oDevolucionAdmDetalle = new DevolucionAdmDetalle();
                                    oDevolucionAdmDetalle.Id = int.Parse(drd3["Id"].ToString());
                                    oDevolucionAdmDetalle.IdItem = int.Parse(drd3["IdItem"].ToString());
                                    oDevolucionAdmDetalle.CodigoArticulo = (drd3["CodigoArticulo"].ToString());
                                    oDevolucionAdmDetalle.Descripcion = (drd3["Descripcion"].ToString());
                                    oDevolucionAdmDetalle.IdUnidadMedida = int.Parse(drd3["IdUnidadMedida"].ToString());
                                    oDevolucionAdmDetalle.IdGrupoUnidadMedida = int.Parse((String.IsNullOrEmpty(drd3["IdGrupoUnidadMedida"].ToString()) ? "0" : drd3["IdGrupoUnidadMedida"].ToString()));
                                    oDevolucionAdmDetalle.IdDefinicionGrupoUnidad = int.Parse((String.IsNullOrEmpty(drd3["IdDefinicionUnidadMedida"].ToString()) ? "0" : drd3["IdDefinicionUnidadMedida"].ToString()));
                                    oDevolucionAdmDetalle.Cantidad = decimal.Parse(drd3["Cantidad"].ToString());
                                    oDevolucionAdmDetalle.CantidadAtendida = decimal.Parse(drd3["CantidadAtendida"].ToString());



                                    oDevolucionAdmDTO.Detalles[posicion] = oDevolucionAdmDetalle;
                                    posicion = posicion + 1;
                                }

                            }
                            catch (Exception ex)
                            {
                            }

                            lstSolicitudDespachoDTO.Add(oDevolucionAdmDTO);

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

        public int UpdateDevolucionAdmDet(int Id, decimal Cantidad, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateDevolucionAdmDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Id", Id);
                        da.SelectCommand.Parameters.AddWithValue("@Cantidad", Cantidad);


                        rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());

                        transactionScope.Complete();

                        if (rpta > 0)
                        {
                            return rpta;
                        }
                        else
                        {
                            return 0;
                        }


                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }

            }
        }


        public int CerrarDevolucionAdm(int Id, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_CerrarDevolucionAdm", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Id", Id);


                        rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());

                        transactionScope.Complete();

                        if (rpta > 0)
                        {
                            return rpta;
                        }
                        else
                        {
                            return 0;
                        }


                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }

            }
        }

        public List<DevolucionAdmDTO> ObtenerDevolucionesAtender(int IdBase, DateTime FechaInicio, DateTime FechaFin, int EstadoDevolucion, int SerieFiltro, string BaseDatos)
        {
            List<DevolucionAdmDTO> lstSolicitudDespachoDTO = new List<DevolucionAdmDTO>();


            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {

                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDevolucionesAdmCabezera", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoDevolucion", EstadoDevolucion);
                    da.SelectCommand.Parameters.AddWithValue("@SerieFiltro", SerieFiltro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DevolucionAdmDTO oDevolucionAdmDTO = new DevolucionAdmDTO();
                        oDevolucionAdmDTO.Id = int.Parse(drd["Id"].ToString());
                        oDevolucionAdmDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oDevolucionAdmDTO.Serie = drd["Serie"].ToString();
                        oDevolucionAdmDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oDevolucionAdmDTO.IdClaseProducto = int.Parse(drd["IdClaseProducto"].ToString());
                        oDevolucionAdmDTO.IdTipoProducto = int.Parse(drd["IdTipoProducto"].ToString());
                        oDevolucionAdmDTO.IdCuadrilla = int.Parse(drd["IdCuadrilla"].ToString());
                        oDevolucionAdmDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oDevolucionAdmDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oDevolucionAdmDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oDevolucionAdmDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oDevolucionAdmDTO.Comentario = (String.IsNullOrEmpty(drd["Comentario"].ToString()) ? "" : drd["Comentario"].ToString());
                        oDevolucionAdmDTO.NombCuadrilla = drd["NombCuadrilla"].ToString();
                        oDevolucionAdmDTO.SerieyNum = drd["SerieyNum"].ToString();
                        oDevolucionAdmDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oDevolucionAdmDTO.EstadoDevolucion= int.Parse(drd["EstadoDevolucion"].ToString());





                        int IdDevolucion = int.Parse(drd["Id"].ToString());
                        Int32 filasdetalle = 0;
                        using (SqlConnection cn2 = new Conexion().conectar(BaseDatos))
                        {
                            try
                            {
                                cn2.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter("SMC_ListarDevolucionAdmDetallexID", cn2);
                                da2.SelectCommand.Parameters.AddWithValue("@IdDevolucion", IdDevolucion);
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

                        oDevolucionAdmDTO.Detalles = new DevolucionAdmDetalle[filasdetalle];
                        using (SqlConnection cn3 = new Conexion().conectar(BaseDatos))
                        {
                            try
                            {
                                cn3.Open();
                                SqlDataAdapter da3 = new SqlDataAdapter("SMC_ListarDevolucionAdmDetallexID", cn3);
                                da3.SelectCommand.Parameters.AddWithValue("@IdDevolucion", IdDevolucion);
                                da3.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader drd3 = da3.SelectCommand.ExecuteReader();
                                Int32 posicion = 0;
                                while (drd3.Read())
                                {
                                    DevolucionAdmDetalle oDevolucionAdmDetalle = new DevolucionAdmDetalle();
                                    oDevolucionAdmDetalle.Id = int.Parse(drd3["Id"].ToString());
                                    oDevolucionAdmDetalle.IdItem = int.Parse(drd3["IdItem"].ToString());
                                    oDevolucionAdmDetalle.CodigoArticulo = (drd3["CodigoArticulo"].ToString());
                                    oDevolucionAdmDetalle.Descripcion = (drd3["Descripcion"].ToString());
                                    oDevolucionAdmDetalle.IdUnidadMedida = int.Parse(drd3["IdUnidadMedida"].ToString());
                                    oDevolucionAdmDetalle.IdGrupoUnidadMedida = int.Parse((String.IsNullOrEmpty(drd3["IdGrupoUnidadMedida"].ToString()) ? "0" : drd3["IdGrupoUnidadMedida"].ToString()));
                                    oDevolucionAdmDetalle.IdDefinicionGrupoUnidad = int.Parse((String.IsNullOrEmpty(drd3["IdDefinicionUnidadMedida"].ToString()) ? "0" : drd3["IdDefinicionUnidadMedida"].ToString()));
                                    oDevolucionAdmDetalle.Cantidad = decimal.Parse(drd3["Cantidad"].ToString());
                                    oDevolucionAdmDetalle.CantidadAtendida = decimal.Parse(drd3["CantidadAtendida"].ToString());



                                    oDevolucionAdmDTO.Detalles[posicion] = oDevolucionAdmDetalle;
                                    posicion = posicion + 1;
                                }

                            }
                            catch (Exception ex)
                            {
                            }

                            lstSolicitudDespachoDTO.Add(oDevolucionAdmDTO);

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
        public int AtencionConfirmada(int Cantidad, int IdDevolucion, int IdArticulo, int EstadoDevolucion, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter dad = new SqlDataAdapter("SMC_DevolucionAtendida", cn);
                        dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                        dad.SelectCommand.Parameters.AddWithValue("@Cantidad", Cantidad);
                        dad.SelectCommand.Parameters.AddWithValue("@IdDevolucion", IdDevolucion);
                        dad.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                        dad.SelectCommand.Parameters.AddWithValue("@EstadoDevolucion", EstadoDevolucion);
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
    }
}
