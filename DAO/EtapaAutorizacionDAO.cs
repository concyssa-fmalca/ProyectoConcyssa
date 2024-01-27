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
    public class EtapaAutorizacionDAO
    {


        public List<EtapaAutorizacionDTO> ObtenerEtapaAutorizacion(string IdSociedad, string BaseDatos)
        {
            List<EtapaAutorizacionDTO> lstEtapaAutorizacionDTO = new List<EtapaAutorizacionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEtapaAutorizacion", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        EtapaAutorizacionDTO oEtapaAutorizacionDTO = new EtapaAutorizacionDTO();
                        oEtapaAutorizacionDTO.IdEtapaAutorizacion = int.Parse(drd["Id"].ToString());
                        oEtapaAutorizacionDTO.NombreEtapa = drd["NombreEtapa"].ToString();
                        oEtapaAutorizacionDTO.DescripcionEtapa = drd["DescripcionEtapa"].ToString();
                        oEtapaAutorizacionDTO.AutorizacionesRequeridas = int.Parse(drd["AutorizacionesRequeridas"].ToString());
                        oEtapaAutorizacionDTO.RechazosRequeridos = int.Parse(drd["RechazosRequeridos"].ToString());
                        oEtapaAutorizacionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstEtapaAutorizacionDTO.Add(oEtapaAutorizacionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstEtapaAutorizacionDTO;
        }

        public int UpdateInsertEtapaAutorizacion(EtapaAutorizacionDTO oEtapaAutorizacionDTO, string IdSociedad, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertEtapaAutorizacion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", oEtapaAutorizacionDTO.IdEtapaAutorizacion);
                        da.SelectCommand.Parameters.AddWithValue("@NombreEtapa", oEtapaAutorizacionDTO.NombreEtapa);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionEtapa", oEtapaAutorizacionDTO.DescripcionEtapa);
                        da.SelectCommand.Parameters.AddWithValue("@AutorizacionesRequeridas", oEtapaAutorizacionDTO.AutorizacionesRequeridas);
                        da.SelectCommand.Parameters.AddWithValue("@RechazosRequeridos", oEtapaAutorizacionDTO.RechazosRequeridos);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oEtapaAutorizacionDTO.Estado);
                        int IdInsert = 0;

                        if (oEtapaAutorizacionDTO.IdEtapaAutorizacion > 0)
                        {
                            IdInsert = oEtapaAutorizacionDTO.IdEtapaAutorizacion;
                            da.SelectCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            IdInsert = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        }

                        for (int i = 0; i < oEtapaAutorizacionDTO.Detalle.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertEtapaAutorizacionDetalle", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacionDetalle", oEtapaAutorizacionDTO.Detalle[i].IdEtapaAutorizacionDetalle);
                            dad.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdUsuario", oEtapaAutorizacionDTO.Detalle[i].IdUsuario);
                            dad.SelectCommand.Parameters.AddWithValue("@IdDepartamento", oEtapaAutorizacionDTO.Detalle[i].IdDepartamento);
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

        public List<EtapaAutorizacionDTO> ObtenerDatosxID(int IdEtapaAutorizacion, string BaseDatos)
        {
            List<EtapaAutorizacionDTO> lstEtapaAutorizacionDTO = new List<EtapaAutorizacionDTO>();
            EtapaAutorizacionDTO oEtapaAutorizacionDTO = new EtapaAutorizacionDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEtapaAutorizacionxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", IdEtapaAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oEtapaAutorizacionDTO.IdEtapaAutorizacion = int.Parse(drd["Id"].ToString());
                        oEtapaAutorizacionDTO.NombreEtapa = drd["NombreEtapa"].ToString();
                        oEtapaAutorizacionDTO.DescripcionEtapa = drd["DescripcionEtapa"].ToString();
                        oEtapaAutorizacionDTO.AutorizacionesRequeridas = int.Parse(drd["AutorizacionesRequeridas"].ToString());
                        oEtapaAutorizacionDTO.RechazosRequeridos = int.Parse(drd["RechazosRequeridos"].ToString());
                        oEtapaAutorizacionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        //lstEtapaAutorizacionDTO.Add(oEtapaAutorizacionDTO);
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
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEtapaAutorizacionDetallexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", IdEtapaAutorizacion);
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


            oEtapaAutorizacionDTO.Detalles = new EtapaAutorizacionDetalleDTO[filasdetalle];
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEtapaAutorizacionDetallexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", IdEtapaAutorizacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        EtapaAutorizacionDetalleDTO oEtapaAutorizacionDetalleDTO = new EtapaAutorizacionDetalleDTO();
                        oEtapaAutorizacionDetalleDTO.IdEtapaAutorizacionDetalle = int.Parse(drd["Id"].ToString());
                        oEtapaAutorizacionDetalleDTO.IdEtapaAutorizacion = int.Parse(drd["IdEtapaAutorizacion"].ToString());
                        oEtapaAutorizacionDetalleDTO.IdUsuario = int.Parse(drd["IdUsuario"].ToString());
                        oEtapaAutorizacionDetalleDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
                        oEtapaAutorizacionDTO.Detalles[posicion] = oEtapaAutorizacionDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

                lstEtapaAutorizacionDTO.Add(oEtapaAutorizacionDTO);

            }


            return lstEtapaAutorizacionDTO;
        }

        //public List<EtapaAutorizacionDTO> ObtenerDatosxIDConexionEnviada(int IdEtapaAutorizacion,SqlConnection cn)
        //{
        //    List<EtapaAutorizacionDTO> lstEtapaAutorizacionDTO = new List<EtapaAutorizacionDTO>();
        //    EtapaAutorizacionDTO oEtapaAutorizacionDTO = new EtapaAutorizacionDTO();
        //    //using (SqlConnection cn = new Conexion().conectar(BaseDatos))
        //    //{
        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEtapaAutorizacionxID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", IdEtapaAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader drd = da.SelectCommand.ExecuteReader();
        //            while (drd.Read())
        //            {

        //                oEtapaAutorizacionDTO.IdEtapaAutorizacion = int.Parse(drd["Id"].ToString());
        //                oEtapaAutorizacionDTO.NombreEtapa = drd["NombreEtapa"].ToString();
        //                oEtapaAutorizacionDTO.DescripcionEtapa = drd["DescripcionEtapa"].ToString();
        //                oEtapaAutorizacionDTO.AutorizacionesRequeridas = int.Parse(drd["AutorizacionesRequeridas"].ToString());
        //                oEtapaAutorizacionDTO.RechazosRequeridos = int.Parse(drd["RechazosRequeridos"].ToString());
        //                oEtapaAutorizacionDTO.Estado = bool.Parse(drd["Estado"].ToString());
        //                //lstEtapaAutorizacionDTO.Add(oEtapaAutorizacionDTO);
        //            }
        //            drd.Close();


        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    //}


        //    Int32 filasdetalle = 0;
        //    //using (SqlConnection cn = new Conexion().conectar(BaseDatos))
        //    //{
        //        try
        //        {
        //            //cn.Open();
        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEtapaAutorizacionDetallexID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", IdEtapaAutorizacion);
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
        //    //}


        //    oEtapaAutorizacionDTO.Detalles = new EtapaAutorizacionDetalleDTO[filasdetalle];
        //    //using (SqlConnection cn = new Conexion().conectar(BaseDatos))
        //    //{
        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEtapaAutorizacionDetallexID", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", IdEtapaAutorizacion);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader drd = da.SelectCommand.ExecuteReader();
        //            Int32 posicion = 0;
        //            while (drd.Read())
        //            {
        //                EtapaAutorizacionDetalleDTO oEtapaAutorizacionDetalleDTO = new EtapaAutorizacionDetalleDTO();
        //                oEtapaAutorizacionDetalleDTO.IdEtapaAutorizacionDetalle = int.Parse(drd["Id"].ToString());
        //                oEtapaAutorizacionDetalleDTO.IdEtapaAutorizacion = int.Parse(drd["IdEtapaAutorizacion"].ToString());
        //                oEtapaAutorizacionDetalleDTO.IdUsuario = int.Parse(drd["IdUsuario"].ToString());
        //                oEtapaAutorizacionDetalleDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
        //                oEtapaAutorizacionDTO.Detalles[posicion] = oEtapaAutorizacionDetalleDTO;
        //                posicion = posicion + 1;
        //            }
        //            drd.Close();

        //        }
        //        catch (Exception ex)
        //        {
        //        }

        //        lstEtapaAutorizacionDTO.Add(oEtapaAutorizacionDTO);

        //    //}


        //    return lstEtapaAutorizacionDTO;
        //}


        public int Delete(int IdEtapaAutorizacion, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarEtapaAutorizacion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacion", IdEtapaAutorizacion);
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


        public int EliminarDetalleAutorizacion(int IdEtapaAutorizacionDetalle, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarDetalleAutorizacion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEtapaAutorizacionDetalle", IdEtapaAutorizacionDetalle);
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
