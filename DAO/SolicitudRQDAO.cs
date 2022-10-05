

using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Transactions;

namespace DAO
{
    public class SolicitudRQDAO
    {

        public List<SolicitudRQDTO> ObtenerSolicitudesRQ(int IdSolicitante, string IdSociedad, string FechaInicio, string FechaFinal, int Estado)
        {
            List<SolicitudRQDTO> lstSolicitudRQDTO = new List<SolicitudRQDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudRQ", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitante", IdSolicitante);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
                        oSolicitudRQDTO.IdSolicitudRQ = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSolicitudRQDTO.Serie = drd["Serie"].ToString();
                        oSolicitudRQDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudRQDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oSolicitudRQDTO.Solicitante = drd["Solicitante"].ToString();
                        oSolicitudRQDTO.IdSucursal = int.Parse(drd["IdSucursal"].ToString());
                        oSolicitudRQDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
                        oSolicitudRQDTO.NombreDepartamento = drd["NombreDepartamento"].ToString();
                        oSolicitudRQDTO.IdClaseArticulo = int.Parse(drd["IdClaseArticulo"].ToString());
                        oSolicitudRQDTO.IdTitular = int.Parse(drd["IdTitular"].ToString());
                        oSolicitudRQDTO.IdMoneda = drd["IdMoneda"].ToString();
                        oSolicitudRQDTO.TipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDTO.TotalAntesDescuento = decimal.Parse(drd["TotalAntesDescuento"].ToString());
                        oSolicitudRQDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDTO.Impuesto = decimal.Parse(drd["Impuesto"].ToString());
                        oSolicitudRQDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oSolicitudRQDTO.FechaValidoHasta = Convert.ToDateTime(drd["FechaValidoHasta"].ToString());
                        oSolicitudRQDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudRQDTO.Comentarios = drd["Comentarios"].ToString();
                        oSolicitudRQDTO.Estado = int.Parse(drd["Estado"].ToString());
                        oSolicitudRQDTO.DetalleEstado = drd["DetalleEstado"].ToString();
                        oSolicitudRQDTO.Prioridad = int.Parse(drd["Prioridad"].ToString());
                        lstSolicitudRQDTO.Add(oSolicitudRQDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudRQDTO;
        }



        public List<int> UpdateInsertSolicitud(SolicitudRQDTO oSolicitudRQDTO, SolicitudRQDetalleDTO oSolicitudRQDetalleDTO, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQ", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", oSolicitudRQDTO.IdSolicitudRQ);
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oSolicitudRQDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@Serie", oSolicitudRQDTO.Serie);
                        da.SelectCommand.Parameters.AddWithValue("@Numero", oSolicitudRQDTO.Numero);
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitante", oSolicitudRQDTO.IdSolicitante);
                        da.SelectCommand.Parameters.AddWithValue("@IdSucursal", oSolicitudRQDTO.IdSucursal);
                        da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", oSolicitudRQDTO.IdDepartamento);
                        da.SelectCommand.Parameters.AddWithValue("@NombreDepartamento", oSolicitudRQDTO.NombreDepartamento);
                        da.SelectCommand.Parameters.AddWithValue("@IdClaseArticulo", oSolicitudRQDTO.IdClaseArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdTitular", oSolicitudRQDTO.IdTitular);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oSolicitudRQDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oSolicitudRQDTO.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@TotalAntesDescuento", oSolicitudRQDTO.TotalAntesDescuento);
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oSolicitudRQDTO.IdIndicadorImpuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Impuesto", oSolicitudRQDTO.Impuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oSolicitudRQDTO.Total);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oSolicitudRQDTO.FechaContabilizacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaValidoHasta", oSolicitudRQDTO.FechaValidoHasta);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oSolicitudRQDTO.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Comentarios", oSolicitudRQDTO.Comentarios);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSolicitudRQDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        da.SelectCommand.Parameters.AddWithValue("@Prioridad", oSolicitudRQDTO.Prioridad);
                        int IdInsert = 0;
                        bool EsNuevoIngreso = false;
                        if (oSolicitudRQDTO.IdSolicitudRQ > 0)
                        {
                            IdInsert = oSolicitudRQDTO.IdSolicitudRQ;
                            da.SelectCommand.ExecuteNonQuery();
                        }
                        else
                        {//cuando es un nuevo requerimiento
                            IdInsert = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                            EsNuevoIngreso = true;
                        }

                        //int rpta = da.SelectCommand.ExecuteNonQuery();
                        //int IdInsertDetalle = 0;
                        for (int i = 0; i < oSolicitudRQDetalleDTO.IdArticulo.Count; i++)
                        {
                            SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQDetalle", cn);
                            dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                            dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQDetalle", oSolicitudRQDetalleDTO.IdSolicitudRQDetalle[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdInsert);
                            dad.SelectCommand.Parameters.AddWithValue("@IdArticulo", oSolicitudRQDetalleDTO.IdArticulo[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@Descripcion", oSolicitudRQDetalleDTO.Descripcion[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oSolicitudRQDetalleDTO.IdUnidadMedida[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@FechaNecesaria", oSolicitudRQDetalleDTO.FechaNecesaria[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@CantidadNecesaria", oSolicitudRQDetalleDTO.CantidadNecesaria[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@PrecioInfo", oSolicitudRQDetalleDTO.PrecioInfo[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oSolicitudRQDetalleDTO.IdIndicadorImpuesto[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@Total", oSolicitudRQDetalleDTO.ItemTotal[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oSolicitudRQDetalleDTO.IdAlmacen[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdProveedor", oSolicitudRQDetalleDTO.IdProveedor[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@NumeroFabricacion", oSolicitudRQDetalleDTO.NumeroFabricacion[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@NumeroSerie", oSolicitudRQDetalleDTO.NumeroSerie[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdLineaNegocio", oSolicitudRQDetalleDTO.IdLineaNegocio[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdCentroCostos", oSolicitudRQDetalleDTO.IdCentroCostos[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdProyecto", oSolicitudRQDetalleDTO.IdProyecto[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@IdMoneda", oSolicitudRQDetalleDTO.IdItemMoneda[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@TipoCambio", 1);
                            dad.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                            dad.SelectCommand.Parameters.AddWithValue("@Referencia", oSolicitudRQDetalleDTO.Referencia[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@EstadoDetalle", oSolicitudRQDetalleDTO.EstadoDetalle[i]);//;oSolicitudRQDetalleDTO.EstadoDetalle[i]);
                            dad.SelectCommand.Parameters.AddWithValue("@Prioridad", oSolicitudRQDetalleDTO.Prioridad[i]);
                            rpta = dad.SelectCommand.ExecuteNonQuery();

                        }

                        if (oSolicitudRQDTO.DetalleAnexo != null)
                        {
                            for (int i = 0; i < oSolicitudRQDTO.DetalleAnexo.Count; i++)
                            {
                                SqlDataAdapter dad = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQAnexos", cn);
                                dad.SelectCommand.CommandType = CommandType.StoredProcedure;
                                dad.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQAnexos", oSolicitudRQDTO.DetalleAnexo[i].IdSolicitudRQAnexos);
                                dad.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdInsert);
                                dad.SelectCommand.Parameters.AddWithValue("@Nombre", oSolicitudRQDTO.DetalleAnexo[i].Nombre);
                                dad.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                                rpta = dad.SelectCommand.ExecuteNonQuery();
                            }
                        }




                        List<int> resultados = new List<int>();
                        if (EsNuevoIngreso)
                        {
                            resultados.Add(5);
                            resultados.Add(IdInsert);
                        }
                        else
                        {
                            resultados.Add(1);
                            resultados.Add(1);
                        }

                        transactionScope.Complete();

                        return resultados;
                    }
                    catch (Exception ex)
                    {
                        List<int> resultadoerror = new List<int>();
                        resultadoerror.Add(0);
                        return resultadoerror;
                    }
                }






            }
        }

        
        public List<SolicitudRQDTO> ObtenerDatosxIDNuevo(int IdSolicitudRQ,int IdAprobador,int IdEtapa)
        {
            List<SolicitudRQDTO> lstSolicitudRQDTO = new List<SolicitudRQDTO>();

            SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oSolicitudRQDTO.IdSolicitudRQ = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSolicitudRQDTO.Serie = drd["Serie"].ToString();
                        oSolicitudRQDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudRQDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oSolicitudRQDTO.Solicitante = drd["Solicitante"].ToString();
                        oSolicitudRQDTO.IdSucursal = int.Parse(drd["IdSucursal"].ToString());
                        oSolicitudRQDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
                        oSolicitudRQDTO.IdClaseArticulo = int.Parse(drd["IdClaseArticulo"].ToString());
                        oSolicitudRQDTO.IdTitular = int.Parse(drd["IdTitular"].ToString());
                        oSolicitudRQDTO.IdMoneda = drd["IdMoneda"].ToString();
                        oSolicitudRQDTO.TipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDTO.TotalAntesDescuento = decimal.Parse(drd["TotalAntesDescuento"].ToString());
                        oSolicitudRQDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDTO.Impuesto = decimal.Parse(drd["Impuesto"].ToString());
                        oSolicitudRQDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oSolicitudRQDTO.FechaValidoHasta = Convert.ToDateTime(drd["FechaValidoHasta"].ToString());
                        oSolicitudRQDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudRQDTO.FechaCreacion = Convert.ToDateTime(drd["FechaCreacion"].ToString());
                        oSolicitudRQDTO.Comentarios = drd["Comentarios"].ToString();
                        oSolicitudRQDTO.Estado = int.Parse(drd["Estado"].ToString());
                        oSolicitudRQDTO.DetalleEstado = drd["DetalleEstado"].ToString();
                        oSolicitudRQDTO.Prioridad = int.Parse(drd["Prioridad"].ToString());
                        //lstSolicitudRQDTO.Add(oSolicitudRQDTO);
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
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDetalleRQxIDParaAprobar", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.Parameters.AddWithValue("@IdAprobador", IdAprobador);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapa", IdEtapa);
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

            oSolicitudRQDTO.Detalle = new SolicitudDetalleDTO[filasdetalle];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDetalleRQxIDParaAprobar", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.Parameters.AddWithValue("@IdAprobador", IdAprobador);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapa", IdEtapa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        SolicitudDetalleDTO oSolicitudRQDetalleDTO = new SolicitudDetalleDTO();
                        oSolicitudRQDetalleDTO.IdSolicitudRQDetalle = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDetalleDTO.IdSolicitudCabecera = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQDetalleDTO.IdArticulo = drd["IdArticulo"].ToString();
                        oSolicitudRQDetalleDTO.Descripcion = drd["Descripcion"].ToString();
                        oSolicitudRQDetalleDTO.IdUnidadMedida = drd["IdUnidadMedida"].ToString();
                        oSolicitudRQDetalleDTO.FechaNecesaria = Convert.ToDateTime(drd["FechaNecesaria"].ToString());
                        oSolicitudRQDetalleDTO.CantidadNecesaria = decimal.Parse(drd["CantidadNecesaria"].ToString());
                        oSolicitudRQDetalleDTO.PrecioInfo = decimal.Parse(drd["PrecioInfo"].ToString());
                        oSolicitudRQDetalleDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDetalleDTO.ItemTotal = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDetalleDTO.IdAlmacen = drd["IdAlmacen"].ToString();
                        oSolicitudRQDetalleDTO.IdProveedor = int.Parse(drd["IdProveedor"].ToString());
                        oSolicitudRQDetalleDTO.NumeroFabricacion = drd["NumeroFabricacion"].ToString();
                        oSolicitudRQDetalleDTO.NumeroSerie = drd["NumeroSerie"].ToString();
                        oSolicitudRQDetalleDTO.IdLineaNegocio = int.Parse(drd["IdLineaNegocio"].ToString());
                        oSolicitudRQDetalleDTO.IdCentroCostos = drd["IdCentroCostos"].ToString();
                        oSolicitudRQDetalleDTO.IdProyecto = drd["IdProyecto"].ToString();
                        oSolicitudRQDetalleDTO.IdItemMoneda = drd["IdMoneda"].ToString();
                        oSolicitudRQDetalleDTO.ItemTipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDetalleDTO.Referencia = drd["Referencia"].ToString();
                        oSolicitudRQDetalleDTO.EstadoDescripcion = drd["Estado"].ToString();
                        oSolicitudRQDetalleDTO.Prioridad = int.Parse(drd["Prioridad"].ToString());
                        oSolicitudRQDetalleDTO.EstadoItemAutorizado = 1;
                        oSolicitudRQDetalleDTO.EstadoDetalle = int.Parse(drd["EstadoDetalle"].ToString());
                        oSolicitudRQDetalleDTO.EstadoDisabled = 1;

                        oSolicitudRQDetalleDTO.TotalCompraLocal = decimal.Parse(drd["TotalCompraLocal"].ToString());
                        oSolicitudRQDetalleDTO.TotalCompraLima = decimal.Parse(drd["TotalCompraLima"].ToString());
                        oSolicitudRQDetalleDTO.TotalDespacho = decimal.Parse(drd["TotalDespacho"].ToString());

                        oSolicitudRQDetalleDTO.IdDetalle = int.Parse(drd["IdDetalle"].ToString());
                        oSolicitudRQDetalleDTO.AprobadoAnterior = int.Parse(drd["AprobadoAnterior"].ToString());

                        
                        //oSolicitudRQDetalleDTO.DescripcionItem = drd["DescripcionItem"].ToString();
                        //lstSolicitudRQDTO.Add(oSolicitudRQDTO.Detalle.Add(oSolicitudRQDetalleDTO));
                        oSolicitudRQDTO.Detalle[posicion] = oSolicitudRQDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

            }



            Int32 filasdetalleAnexo = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudAnexosRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalleAnexo++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oSolicitudRQDTO.DetallesAnexo = new SolicitudRQAnexos[filasdetalleAnexo];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudAnexosRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        SolicitudRQAnexos oSolicitudRQAnexos = new SolicitudRQAnexos();
                        oSolicitudRQAnexos.IdSolicitudRQAnexos = int.Parse(drd["Id"].ToString());
                        oSolicitudRQAnexos.IdSolicitud = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQAnexos.Nombre = drd["Nombre"].ToString();
                        oSolicitudRQAnexos.IdSociedad = int.Parse(drd["IdSociedad"].ToString());

                        oSolicitudRQDTO.DetallesAnexo[posicion] = oSolicitudRQAnexos;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

                lstSolicitudRQDTO.Add(oSolicitudRQDTO);

            }



            return lstSolicitudRQDTO;
        }

        public List<SolicitudRQDTO> ObtenerDatosxID(int IdSolicitudRQ)
        {
            List<SolicitudRQDTO> lstSolicitudRQDTO = new List<SolicitudRQDTO>();

            SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oSolicitudRQDTO.IdSolicitudRQ = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSolicitudRQDTO.Serie = drd["Serie"].ToString();
                        oSolicitudRQDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudRQDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oSolicitudRQDTO.Solicitante = drd["Solicitante"].ToString();
                        oSolicitudRQDTO.IdSucursal = int.Parse(drd["IdSucursal"].ToString());
                        oSolicitudRQDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
                        oSolicitudRQDTO.IdClaseArticulo = int.Parse(drd["IdClaseArticulo"].ToString());
                        oSolicitudRQDTO.IdTitular = int.Parse(drd["IdTitular"].ToString());
                        oSolicitudRQDTO.IdMoneda = drd["IdMoneda"].ToString();
                        oSolicitudRQDTO.TipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDTO.TotalAntesDescuento = decimal.Parse(drd["TotalAntesDescuento"].ToString());
                        oSolicitudRQDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDTO.Impuesto = decimal.Parse(drd["Impuesto"].ToString());
                        oSolicitudRQDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oSolicitudRQDTO.FechaValidoHasta = Convert.ToDateTime(drd["FechaValidoHasta"].ToString());
                        oSolicitudRQDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudRQDTO.FechaCreacion = Convert.ToDateTime(drd["FechaCreacion"].ToString());
                        oSolicitudRQDTO.Comentarios = drd["Comentarios"].ToString();
                        oSolicitudRQDTO.Estado = int.Parse(drd["Estado"].ToString());
                        oSolicitudRQDTO.DetalleEstado = drd["DetalleEstado"].ToString();
                        oSolicitudRQDTO.Prioridad = int.Parse(drd["Prioridad"].ToString());
                        //lstSolicitudRQDTO.Add(oSolicitudRQDTO);
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
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDetalleRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
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

            oSolicitudRQDTO.Detalle = new SolicitudDetalleDTO[filasdetalle];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudDetalleRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        SolicitudDetalleDTO oSolicitudRQDetalleDTO = new SolicitudDetalleDTO();
                        oSolicitudRQDetalleDTO.IdSolicitudRQDetalle = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDetalleDTO.IdSolicitudCabecera = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQDetalleDTO.IdArticulo = drd["IdArticulo"].ToString();
                        oSolicitudRQDetalleDTO.Descripcion = drd["Descripcion"].ToString();
                        oSolicitudRQDetalleDTO.IdUnidadMedida = drd["IdUnidadMedida"].ToString();
                        oSolicitudRQDetalleDTO.FechaNecesaria = Convert.ToDateTime(drd["FechaNecesaria"].ToString());
                        oSolicitudRQDetalleDTO.CantidadNecesaria = decimal.Parse(drd["CantidadNecesaria"].ToString());
                        oSolicitudRQDetalleDTO.PrecioInfo = decimal.Parse(drd["PrecioInfo"].ToString());
                        oSolicitudRQDetalleDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDetalleDTO.ItemTotal = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDetalleDTO.IdAlmacen = drd["IdAlmacen"].ToString();
                        oSolicitudRQDetalleDTO.IdProveedor = int.Parse(drd["IdProveedor"].ToString());
                        oSolicitudRQDetalleDTO.NumeroFabricacion = drd["NumeroFabricacion"].ToString();
                        oSolicitudRQDetalleDTO.NumeroSerie = drd["NumeroSerie"].ToString();
                        oSolicitudRQDetalleDTO.IdLineaNegocio = int.Parse(drd["IdLineaNegocio"].ToString());
                        oSolicitudRQDetalleDTO.IdCentroCostos = drd["IdCentroCostos"].ToString();
                        oSolicitudRQDetalleDTO.IdProyecto = drd["IdProyecto"].ToString();
                        oSolicitudRQDetalleDTO.IdItemMoneda = drd["IdMoneda"].ToString();
                        oSolicitudRQDetalleDTO.ItemTipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDetalleDTO.Referencia = drd["Referencia"].ToString();
                        oSolicitudRQDetalleDTO.EstadoDescripcion = drd["Estado"].ToString();
                        oSolicitudRQDetalleDTO.Prioridad = int.Parse(drd["Prioridad"].ToString());
                        oSolicitudRQDetalleDTO.EstadoItemAutorizado = 1;
                        oSolicitudRQDetalleDTO.EstadoDetalle = int.Parse(drd["EstadoDetalle"].ToString());
                        oSolicitudRQDetalleDTO.EstadoDisabled = 1;

                        oSolicitudRQDetalleDTO.TotalCompraLocal = decimal.Parse(drd["TotalCompraLocal"].ToString());
                        oSolicitudRQDetalleDTO.TotalCompraLima = decimal.Parse(drd["TotalCompraLima"].ToString());
                        oSolicitudRQDetalleDTO.TotalDespacho = decimal.Parse(drd["TotalDespacho"].ToString());

                        oSolicitudRQDetalleDTO.IdDetalle = int.Parse(drd["IdDetalle"].ToString());
                        //oSolicitudRQDetalleDTO.DescripcionItem = drd["DescripcionItem"].ToString();
                        //lstSolicitudRQDTO.Add(oSolicitudRQDTO.Detalle.Add(oSolicitudRQDetalleDTO));
                        oSolicitudRQDTO.Detalle[posicion] = oSolicitudRQDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

            }



            Int32 filasdetalleAnexo = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudAnexosRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalleAnexo++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oSolicitudRQDTO.DetallesAnexo = new SolicitudRQAnexos[filasdetalleAnexo];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudAnexosRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        SolicitudRQAnexos oSolicitudRQAnexos = new SolicitudRQAnexos();
                        oSolicitudRQAnexos.IdSolicitudRQAnexos = int.Parse(drd["Id"].ToString());
                        oSolicitudRQAnexos.IdSolicitud = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQAnexos.Nombre = drd["Nombre"].ToString();
                        oSolicitudRQAnexos.IdSociedad = int.Parse(drd["IdSociedad"].ToString());

                        oSolicitudRQDTO.DetallesAnexo[posicion] = oSolicitudRQAnexos;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

                lstSolicitudRQDTO.Add(oSolicitudRQDTO);

            }



            return lstSolicitudRQDTO;
        }




        public int Delete(int IdSolicitudRQDetalle)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarDetalleSolicitud", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQDetalle", IdSolicitudRQDetalle);
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


        public int DeleteAnexo(int IdSolicitudRQAnexos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarAnexoSolicitud", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQAnexo", IdSolicitudRQAnexos);
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


        public void EnviarCorreo(string Correo, string Solicitante, string Serie, int Numero)
        {

            string body;


            body = "<body>" +
                "<h1>Se creo una nueva Solicitud</h1>" +
                "<h4>Detalles de Solicitud:</h4>" +
                "<span>Serie Solicitud: " + Serie + "-" + Numero + "</span>" +
                "<br/><br/><span></span>" +
                "</body>";

            string msge = "";
            string from = "mail.mineratitan@gmail.com";
            string correo = from;
            string password = "itjgiwuyjxuvdzfb";
            string displayName = "SMC - Proceso de Autorizacion";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from, displayName);
            mail.To.Add(Correo);

            mail.Subject = "Autorizacion";
            mail.Body = body;

            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
            client.Credentials = new NetworkCredential(from, password);
            client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
            client.Send(mail);



        }



        public int AñadirDocNum(string DocNum, string IdSolicitud, int Localidad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_AñadirDocNum", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@DocNum", DocNum);
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@Localidad", Localidad);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
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


        public int GuardarMovimiento(List<SolicitudDetalleDTO> detalle, int IdAutor, int TipoTransaccion, string DocNum)
        {
            int rpta = 0;
            if (detalle.Count() > 0)
            {
                for (int i = 0; i < detalle.Count; i++)
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
                                SqlDataAdapter da = new SqlDataAdapter("SMC_GuardarMovimientos", cn);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@IdSolicitudDetalle", detalle[i].IdSolicitudRQDetalle);
                                da.SelectCommand.Parameters.AddWithValue("@Cantidad", detalle[i].CantidadProcesar);
                                da.SelectCommand.Parameters.AddWithValue("@IdAutor", IdAutor); //usuario que hizo la transaccion
                                da.SelectCommand.Parameters.AddWithValue("@TipoTransaccion", TipoTransaccion);
                                da.SelectCommand.Parameters.AddWithValue("@DocNum", DocNum);
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
            }
            return rpta;
        }



        public int CerrarSolicitud(int IdSolictudRQ, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_CerrarSolicitud", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolictudRQ);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
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


        public List<int> ValidaItemsTerminados(int IdSolicitud)
        {
            List<int> items = new List<int>();
            int valor = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarItemsTerminados", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        valor = int.Parse(drd["Terminado"].ToString());
                        items.Add(valor);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return items;
        }


        public int ActualizarEstadoMovimientoRQ(int IdSolictudRQ, int Localidad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ActualizarEstadoMovimientoRQ", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolictudRQ);
                        da.SelectCommand.Parameters.AddWithValue("@Localidad", Localidad);
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




    }
}
