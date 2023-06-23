using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class GiroAutorizacionDAO
    {
        public int ValidarSipuedeAprobarGiro(int IdSolicitudRQ, int IdEtapa)
        {
            int puedeentrar = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarSipuedeAprobarGiro", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdSolicitudRQ);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapa", IdEtapa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        puedeentrar = int.Parse(drd["puedeentrar"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return puedeentrar;
        }


        public int ValidarSipuedeAprobarGiroDetalle(int IdSolicitudRQ, int IdEtapa, int IdDetalle)
        {
            int puedeentrar = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarSipuedeAprobarGiro", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdSolicitudRQ);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapa", IdEtapa);
                    da.SelectCommand.Parameters.AddWithValue("@IdDetalle", IdDetalle);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        puedeentrar = int.Parse(drd["puedeentrar"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return puedeentrar;
        }


        public List<GiroAutorizacionDTO> ObtenerSolicitudesxAutorizar(string IdUsuario, string IdSociedad, string FechaInicio, string FechaFinal, int Estado)
        {
            List<GiroAutorizacionDTO> lstSolicitudRQAutorizacionDTO = new List<GiroAutorizacionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                 
                    cn.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudesxAutorizar", cn);
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGirosxAutorizarNEW", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", int.Parse(IdUsuario));
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    //da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoPR", Estado);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        GiroAutorizacionDTO oSolicitudRQAutorizacionDTO = new GiroAutorizacionDTO();
                        oSolicitudRQAutorizacionDTO.IdGiro = int.Parse(drd["IdGiro"].ToString());
                        oSolicitudRQAutorizacionDTO.UsuarioAprobador = int.Parse(drd["UsuarioAprobador"].ToString());
                        oSolicitudRQAutorizacionDTO.IdResponsable = int.Parse(drd["IdResponsable"].ToString());
                        oSolicitudRQAutorizacionDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oSolicitudRQAutorizacionDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oSolicitudRQAutorizacionDTO.IdSemana = int.Parse(drd["IdSemana"].ToString());
                        oSolicitudRQAutorizacionDTO.MontoDolares = decimal.Parse(drd["MontoDolares"].ToString());
                        oSolicitudRQAutorizacionDTO.MontoSoles = decimal.Parse(drd["MontoSoles"].ToString());
                        oSolicitudRQAutorizacionDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQAutorizacionDTO.Fecha = Convert.ToDateTime(drd["Fecha"].ToString());

                        oSolicitudRQAutorizacionDTO.IdGiroModelo = int.Parse(drd["IdGiroModelo"].ToString());
                        oSolicitudRQAutorizacionDTO.IdEtapa = int.Parse(drd["IdEtapaAutorizacion"].ToString());
                        oSolicitudRQAutorizacionDTO.NombEtapa = (drd["NombEtapa"].ToString());


                        lstSolicitudRQAutorizacionDTO.Add(oSolicitudRQAutorizacionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudRQAutorizacionDTO;
        }

        public List<DetalleGiroAprobacionDTO> ObtenerSolicitudesxAutorizarDetalleGiro(string IdUsuario, string IdSociedad, string FechaInicio, string FechaFinal, int Estado,int IdGiro)
        {
            List<GiroAprobacionDTO> lstGiroAprobacionDTO = new List<GiroAprobacionDTO>();
            List<DetalleGiroAprobacionDTO> lstDetalleGiroAprobacionDTO = new List<DetalleGiroAprobacionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    int aprobar = 0;
                    cn.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudesxAutorizar", cn);
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidosDetalleAutorizarGiro", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", int.Parse(IdUsuario));
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    //da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoPR", Estado);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        aprobar = 0;
                        aprobar = ValidarSipuedeAprobarGiroDetalle(Convert.ToInt32(drd["IdGiro"].ToString()), Convert.ToInt32(drd["IdEtapa"].ToString()), Convert.ToInt32(drd["IdGiroDetalle"].ToString()));

                        if (aprobar == 1)
                        {
                            DetalleGiroAprobacionDTO oDetalleGiroAprobacionDTO = new DetalleGiroAprobacionDTO();
                            oDetalleGiroAprobacionDTO.IdGiro = Convert.ToInt32(drd["IdGiro"].ToString());
                            oDetalleGiroAprobacionDTO.IdGiroDetalle = Convert.ToInt32(drd["IdGiroDetalle"].ToString());
                            oDetalleGiroAprobacionDTO.IdGiroModelo = Convert.ToInt32(drd["IdGiroModelo"].ToString());
                            oDetalleGiroAprobacionDTO.IdCreador = Convert.ToInt32(drd["IdCreador"].ToString());
                            oDetalleGiroAprobacionDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                            oDetalleGiroAprobacionDTO.Proveedor = drd["Proveedor"].ToString();

                            oDetalleGiroAprobacionDTO.Moneda = drd["Moneda"].ToString();
                            oDetalleGiroAprobacionDTO.Monto = Convert.ToDecimal(drd["Monto"].ToString());
                            oDetalleGiroAprobacionDTO.Anexo = drd["Anexo"].ToString();

                            oDetalleGiroAprobacionDTO.Fecha = Convert.ToDateTime(drd["Fecha"].ToString());
                            oDetalleGiroAprobacionDTO.Accion = Convert.ToInt32(drd["Accion"].ToString());
                            oDetalleGiroAprobacionDTO.IdUsuario = Convert.ToInt32(IdUsuario);


                            lstDetalleGiroAprobacionDTO.Add(oDetalleGiroAprobacionDTO);
                        }
                        
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }

            var datos = lstDetalleGiroAprobacionDTO.Where(x => x.IdGiro == IdGiro).ToList();

            return datos;
        }







        public List<GiroAprobacionDTO> ObtenerGiroCabeceraAutorizar(string IdUsuario, string IdSociedad, string FechaInicio, string FechaFinal, int Estado)
        {
            List<GiroAprobacionDTO> lstGiroAprobacionDTO = new List<GiroAprobacionDTO>();
            List<DetalleGiroAprobacionDTO> lstDetalleGiroAprobacionDTO = new List<DetalleGiroAprobacionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    int aprobar = 0;
                    cn.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudesxAutorizar", cn);
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidosDetalleAutorizarGiro", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", int.Parse(IdUsuario));
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    //da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoPR", Estado);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        aprobar = 0;
                        aprobar = ValidarSipuedeAprobarGiroDetalle(Convert.ToInt32(drd["IdGiro"].ToString()), Convert.ToInt32(drd["IdEtapa"].ToString()), Convert.ToInt32(drd["IdGiroDetalle"].ToString()));

                        if (aprobar == 1)
                        {
                            DetalleGiroAprobacionDTO oDetalleGiroAprobacionDTO = new DetalleGiroAprobacionDTO();
                            oDetalleGiroAprobacionDTO.IdGiro = Convert.ToInt32(drd["IdGiro"].ToString());
                            lstDetalleGiroAprobacionDTO.Add(oDetalleGiroAprobacionDTO);
                        }

                    }
                    drd.Close();

                   
                 
                }
                catch (Exception ex)
                {
                }


            }


            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    var datos = lstDetalleGiroAprobacionDTO.GroupBy(IdGiro => IdGiro.IdGiro).ToList();

                    if (datos.Count > 0)
                    {
                        for (int i = 0; i < datos.Count; i++)
                        {
                            cn.Open();
                            SqlDataAdapter das = new SqlDataAdapter("SMC_ListarGiroxID", cn);
                            das.SelectCommand.Parameters.AddWithValue("@IdGiro", datos[i].Key);
                            das.SelectCommand.CommandType = CommandType.StoredProcedure;
                            SqlDataReader dr = das.SelectCommand.ExecuteReader();
                            while (dr.Read())
                            {
                                GiroAprobacionDTO oGiroAprobacionDTO = new GiroAprobacionDTO();
                                oGiroAprobacionDTO.IdGiro = Convert.ToInt32(dr["IdGiro"].ToString());
                                oGiroAprobacionDTO.Obra = dr["Obra"].ToString();

                                oGiroAprobacionDTO.Creador = dr["Creador"].ToString();
                                oGiroAprobacionDTO.Fecha = Convert.ToDateTime(dr["Fecha"].ToString());
                                oGiroAprobacionDTO.MontoSoles = decimal.Parse(dr["MontoSoles"].ToString());
                                oGiroAprobacionDTO.MontoDolares = decimal.Parse(dr["MontoDolares"].ToString());

                                oGiroAprobacionDTO.Solicitante = (dr["Proveedor"].ToString() == "" ? dr["Empleado"].ToString() : dr["Proveedor"].ToString());
                                var FechaIni = Convert.ToDateTime(dr["FechaIni"].ToString());
                                var FechaFin = Convert.ToDateTime(dr["FechaFin"].ToString());
                                oGiroAprobacionDTO.Semana = "Semana " + dr["NumSemana"].ToString() + " del " + FechaIni.ToString("dd/MM") + " al " + FechaFin.ToString("dd/MM");

                                lstGiroAprobacionDTO.Add(oGiroAprobacionDTO);
                            }
                            cn.Close();
                            //dr.Close();
                        }


                    }

                }
                catch (Exception ex)
                {

                 
                }
            }







            return lstGiroAprobacionDTO;
        }








    }
}
