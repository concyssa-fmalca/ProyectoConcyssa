
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class ArticuloDAO
    {
        
        public List<ArticuloDTO> ListarArticulosxSociedadxAlmacenStockxProducto(int IdSociedad,int IdArticulo,int IdAlmacen, ref string mensaje_error, int Estado = 3)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosxSociedadxAlmacenStockxProducto", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oArticuloDTO.Compra = Convert.ToBoolean(drd["Compra"].ToString());
                        oArticuloDTO.Venta = Convert.ToBoolean(drd["Venta"].ToString());
                        oArticuloDTO.UnidadMedida = (drd["UnidadMedida"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        oArticuloDTO.PrecioPromedio = Convert.ToDecimal(drd["Precio"].ToString());
                        oArticuloDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oArticuloDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());

                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }

        public List<ArticuloDTO> ListarArticulosxSociedadxAlmacenStockxProductoConServicios(int IdSociedad, int IdArticulo, int IdAlmacen, ref string mensaje_error, int Estado = 3)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosxSociedadxAlmacenStockxProductoConServicios", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oArticuloDTO.Compra = Convert.ToBoolean(drd["Compra"].ToString());
                        oArticuloDTO.Venta = Convert.ToBoolean(drd["Venta"].ToString());
                        oArticuloDTO.UnidadMedida = (drd["UnidadMedida"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        oArticuloDTO.PrecioPromedio = Convert.ToDecimal(drd["Precio"].ToString());
                        oArticuloDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oArticuloDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());

                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }
        public List<ArticuloDTO> ListarArticulosxSociedadxAlmacenStockxProductoActivoFijo(int IdSociedad, int IdArticulo, int IdAlmacen, ref string mensaje_error, int Estado = 3)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosxSociedadxAlmacenStockxProductoActivoFijo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oArticuloDTO.Compra = Convert.ToBoolean(drd["Compra"].ToString());
                        oArticuloDTO.Venta = Convert.ToBoolean(drd["Venta"].ToString());
                        oArticuloDTO.UnidadMedida = (drd["UnidadMedida"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        oArticuloDTO.PrecioPromedio = Convert.ToDecimal(drd["Precio"].ToString());
                        oArticuloDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oArticuloDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());

                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }

        public List<ArticuloDTO> ObtenerArticulosRequerimientos(int Almacen, int Stock, int TipoItem,int TipoProducto,int IdSociedad, ref string mensaje_error)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerArticulosRequerimientos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", Almacen);
                    da.SelectCommand.Parameters.AddWithValue("@Stock", Stock);
                    da.SelectCommand.Parameters.AddWithValue("@TipoItem", TipoItem);
                    da.SelectCommand.Parameters.AddWithValue("@TipoProducto", TipoProducto);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.NombUnidadMedida = (drd["NombUnidadMedida"].ToString());
                        oArticuloDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }

        
        public List<ArticuloDTO> ObtenerArticulosRequerimientosSolicitud(int Almacen, int Stock, int TipoItem, int TipoProducto, int IdSociedad, ref string mensaje_error)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerArticulosRequerimientosSolicitud", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", Almacen);
                    da.SelectCommand.Parameters.AddWithValue("@Stock", Stock);
                    da.SelectCommand.Parameters.AddWithValue("@TipoItem", TipoItem);
                    da.SelectCommand.Parameters.AddWithValue("@TipoProducto", TipoProducto);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.NombUnidadMedida = (drd["NombUnidadMedida"].ToString());
                        oArticuloDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }
        public List<ArticuloDTO> ObtenerArticulosActivoFijo(int IdSociedad, ref string mensaje_error)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticuloActivoFijo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.NombUnidadMedida = (drd["NombUnidadMedida"].ToString());
                        oArticuloDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        oArticuloDTO.UnidadMedida = drd["UnidadMedida"].ToString();
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }

        public ArticuloDTO ObtenerArticuloxIdArticuloRequerimiento(int IdArticulo,int IdAlmacen, ref string mensaje_error)
        {
            ArticuloDTO oArticuloDTO = new ArticuloDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerArticuloxIdArticuloRequerimientoNew", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.NombUnidadMedida = (drd["NombUnidadMedida"].ToString());
                        oArticuloDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());
                        oArticuloDTO.IdUnidadMedida = Convert.ToInt32(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oArticuloDTO.UltimoPrecioCompra = Convert.ToDecimal(drd["UltimoPrecioCompra"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
    
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return oArticuloDTO;
        }


        



        public List<ArticuloDTO> ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto(int IdSociedad, int IdAlmacen,int IdTipoProducto, ref string mensaje_error, int Estado = 3)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProducto", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", IdTipoProducto);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oArticuloDTO.Compra = Convert.ToBoolean(drd["Compra"].ToString());
                        oArticuloDTO.Venta = Convert.ToBoolean(drd["Venta"].ToString());
                        oArticuloDTO.UnidadMedida = (drd["UnidadMedida"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }
        public List<ArticuloDTO> ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoConServicios(int TipoItem,int IdSociedad, int IdAlmacen, int IdTipoProducto, ref string mensaje_error, int Estado = 3)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosCatalogoxSociedadxAlmacenStockxIdTipoProductoConServicios", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", IdTipoProducto);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@TipoItem", TipoItem);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oArticuloDTO.Compra = Convert.ToBoolean(drd["Compra"].ToString());
                        oArticuloDTO.Venta = Convert.ToBoolean(drd["Venta"].ToString());
                        oArticuloDTO.UnidadMedida = (drd["UnidadMedida"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }
        public List<ArticuloDTO> ListarArticulosxSociedadxAlmacenStock(int IdSociedad,int IdAlmacen, ref string mensaje_error, int Estado = 3)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosxSociedadxAlmacenStock", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oArticuloDTO.Compra = Convert.ToBoolean(drd["Compra"].ToString());
                        oArticuloDTO.Venta = Convert.ToBoolean(drd["Venta"].ToString());
                        oArticuloDTO.UnidadMedida = (drd["UnidadMedida"].ToString());
                        oArticuloDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }

        public List<ArticuloDTO> ObtenerticulosxSociedad(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosxSociedad", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oArticuloDTO.Compra = Convert.ToBoolean(drd["Compra"].ToString());
                        oArticuloDTO.Venta = Convert.ToBoolean(drd["Venta"].ToString());
                        oArticuloDTO.NombUnidadMedida = (drd["NombUnidadMedida"].ToString());
                        

                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }


        public bool UpdateInsertArticulo(ArticuloDTO oArticuloDTO,ref string mensaje_error)
        {
            bool exito = false;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertArticulo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oArticuloDTO.IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@Codigo", oArticuloDTO.Codigo);
                    da.SelectCommand.Parameters.AddWithValue("@Descripcion1", oArticuloDTO.Descripcion1);
                    da.SelectCommand.Parameters.AddWithValue("@Descripcion2", oArticuloDTO.Descripcion2);
                    da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oArticuloDTO.IdUnidadMedida);
                    da.SelectCommand.Parameters.AddWithValue("@ActivoFijo", oArticuloDTO.ActivoFijo);
                    da.SelectCommand.Parameters.AddWithValue("@ActivoCatalogo", oArticuloDTO.ActivoCatalogo);
                    da.SelectCommand.Parameters.AddWithValue("@IdCodigoUbso", oArticuloDTO.IdCodigoUbso);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oArticuloDTO.IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", oArticuloDTO.Estado);
                    da.SelectCommand.Parameters.AddWithValue("@Inventario", oArticuloDTO.Inventario);
                    da.SelectCommand.Parameters.AddWithValue("@Compra", oArticuloDTO.Compra);
                    da.SelectCommand.Parameters.AddWithValue("@Venta", oArticuloDTO.Venta);
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupoUnidadMedida", oArticuloDTO.IdGrupoUnidadMedida);
                    da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedidaInv", oArticuloDTO.IdUnidadMedidaInv);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oArticuloDTO.IdProveedor);


                    int i = da.SelectCommand.ExecuteNonQuery();
                    if (i > 0)
                    {
                        exito = true;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return exito;
        }

        public List<ArticuloDTO> ObtenerDatosxID(int IdArticulo, ref string mensaje_error)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticuloxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = (drd["Descripcion1"].ToString());
                        oArticuloDTO.Descripcion2 = (drd["Descripcion2"].ToString());
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oArticuloDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        oArticuloDTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oArticuloDTO.Compra = Convert.ToBoolean(drd["Compra"].ToString());
                        oArticuloDTO.Venta = Convert.ToBoolean(drd["Venta"].ToString());
                        oArticuloDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oArticuloDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());
                        oArticuloDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());


                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }


        
        public bool ELiminarArticulo(ArticuloDTO oArticuloDTO, ref string mensaje_error)
        {
            bool exito = false;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ELiminarArticulo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oArticuloDTO.IdArticulo);
                    int i = da.SelectCommand.ExecuteNonQuery();
                    if (i > 0)
                    {
                        exito = true;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return exito;
        }
        
        public bool EliminarProductoProveedor(int IdProductoProveedor, ref string mensaje_error)
        {
            bool exito = false;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ELiminarArticuloProveedor", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdProductoProveedor", IdProductoProveedor);
                    int i = da.SelectCommand.ExecuteNonQuery();
                    if (i > 0)
                    {
                        exito = true;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return exito;
        }

        public bool InsertStockArticuloAlmacen(int IdProducto, int IdAlmacen, decimal StockMinimo, decimal StockMaximo, decimal StockAlerta, ref string mensaje_error)
        {
            bool exito = false;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_InsertStockArticuloAlmacen", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdProducto);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@StockMinimo", StockMinimo);
                    da.SelectCommand.Parameters.AddWithValue("@StockMaximo", StockMaximo);
                    da.SelectCommand.Parameters.AddWithValue("@StockAlerta", StockAlerta);

                    int i = da.SelectCommand.ExecuteNonQuery();
                    if (i > 0)
                    {
                        exito = true;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return exito;
        }

        public List<StockArticuloAlmacenDTO> ObtenerStockArticuloXAlmacen(int IdUsuario,int IdArticulo, ref string mensaje_error)
        {
            List<StockArticuloAlmacenDTO> lstArticuloDTO = new List<StockArticuloAlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerStockAlmacenxIdProducto", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProducto", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        StockArticuloAlmacenDTO oArticuloDTO = new StockArticuloAlmacenDTO();
                        oArticuloDTO.IdProducto = Convert.ToInt32(drd["IdProducto"].ToString());
                        oArticuloDTO.IdAlmacen = int.Parse(drd["IdAlmacen"].ToString());
                        oArticuloDTO.Descripcion = (drd["Descripcion"].ToString());
                        oArticuloDTO.StockMinimo = decimal.Parse(drd["StockMinimo"].ToString());
                        oArticuloDTO.StockMaximo = decimal.Parse(drd["StockMaximo"].ToString());
                        oArticuloDTO.StockAlerta = decimal.Parse(drd["StockAlerta"].ToString());
                        oArticuloDTO.StockAlmacen = decimal.Parse(drd["StockAlmacen"].ToString());

                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }

        
        public List<ArticuloPrecioProveedorDTO> ListarPrecioProductoProveedor(int IdArticulo,ref string mensaje_error)
        {
            List<ArticuloPrecioProveedorDTO> lstArticuloPrecioProveedorDTO = new List<ArticuloPrecioProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPrecioProductoProveedor", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloPrecioProveedorDTO oArticuloPrecioProveedorDTO = new ArticuloPrecioProveedorDTO();
                        //oArticuloPrecioProveedorDTO.IdProducto = Convert.ToInt32(drd["IdProducto"].ToString());
                        oArticuloPrecioProveedorDTO.IdArticuloProveedor = Convert.ToInt32(drd["IdArticuloProveedor"].ToString());
                        oArticuloPrecioProveedorDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oArticuloPrecioProveedorDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oArticuloPrecioProveedorDTO.PrecioSoles = Convert.ToDecimal(drd["PrecioSoles"].ToString());
                        oArticuloPrecioProveedorDTO.PrecioDolares = Convert.ToDecimal(drd["PrecioDolares"].ToString());
                        oArticuloPrecioProveedorDTO.IdCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oArticuloPrecioProveedorDTO.numeroentrega = Convert.ToInt32(drd["numeroentrega"].ToString());
                        oArticuloPrecioProveedorDTO.IdSociedad = Convert.ToInt32( drd["IdSociedad"].ToString());
                        oArticuloPrecioProveedorDTO.Proveedor = (drd["Proveedor"].ToString());
                        oArticuloPrecioProveedorDTO.CondicionPago = (drd["CondicionPago"].ToString());
                        lstArticuloPrecioProveedorDTO.Add(oArticuloPrecioProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloPrecioProveedorDTO;
        }
        public List<ArticuloPrecioProveedorDTO> ListarPrecioProductoProveedorNuevo(int IdArticulo, ref string mensaje_error)
        {
            List<ArticuloPrecioProveedorDTO> lstArticuloPrecioProveedorDTO = new List<ArticuloPrecioProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPrecioProductoProveedorNuevo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloPrecioProveedorDTO oArticuloPrecioProveedorDTO = new ArticuloPrecioProveedorDTO();
                        //oArticuloPrecioProveedorDTO.IdProducto = Convert.ToInt32(drd["IdProducto"].ToString());
                        oArticuloPrecioProveedorDTO.IdArticuloProveedor = Convert.ToInt32(drd["IdArticuloProveedor"].ToString());
                        oArticuloPrecioProveedorDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oArticuloPrecioProveedorDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oArticuloPrecioProveedorDTO.PrecioSoles = Convert.ToDecimal(drd["PrecioSoles"].ToString());
                        oArticuloPrecioProveedorDTO.PrecioDolares = Convert.ToDecimal(drd["PrecioDolares"].ToString());
                        oArticuloPrecioProveedorDTO.IdCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oArticuloPrecioProveedorDTO.numeroentrega = Convert.ToInt32(drd["numeroentrega"].ToString());
                        oArticuloPrecioProveedorDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oArticuloPrecioProveedorDTO.Proveedor = (drd["Proveedor"].ToString());
                        oArticuloPrecioProveedorDTO.CondicionPago = (drd["CondicionPago"].ToString());
                        oArticuloPrecioProveedorDTO.Obra = (drd["Obra"].ToString());
                        lstArticuloPrecioProveedorDTO.Add(oArticuloPrecioProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloPrecioProveedorDTO;
        }


        public bool SavePrecioProveedor(int IdArticulo,int IdProveedor, decimal PrecioSoles, decimal PrecioDolares, int IdCondicionPagoProveedor, int numeroentrega,int IdSociedad, ref string mensaje_error)
        {
            bool exito = false;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_SavePrecioProveedor", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.Parameters.AddWithValue("@PrecioSoles", PrecioSoles);
                    da.SelectCommand.Parameters.AddWithValue("@PrecioDolares", PrecioDolares);
                    da.SelectCommand.Parameters.AddWithValue("@IdCondicionPagoProveedor", IdCondicionPagoProveedor);
                    da.SelectCommand.Parameters.AddWithValue("@numeroentrega", numeroentrega);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);


                    int i = da.SelectCommand.ExecuteNonQuery();
                    if (i > 0)
                    {
                        exito = true;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return exito;
        }
        public bool SavePrecioProveedorNuevo(int IdArticulo, int IdProveedor, decimal PrecioSoles, decimal PrecioDolares, int IdCondicionPagoProveedor, int numeroentrega, int IdSociedad,int IdObra, ref string mensaje_error)
        {
            bool exito = false;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_SavePrecioProveedorNuevo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.Parameters.AddWithValue("@PrecioSoles", PrecioSoles);
                    da.SelectCommand.Parameters.AddWithValue("@PrecioDolares", PrecioDolares);
                    da.SelectCommand.Parameters.AddWithValue("@IdCondicionPagoProveedor", IdCondicionPagoProveedor);
                    da.SelectCommand.Parameters.AddWithValue("@numeroentrega", numeroentrega);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);


                    int i = da.SelectCommand.ExecuteNonQuery();
                    if (i > 0)
                    {
                        exito = true;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return exito;
        }
        public List<ArticuloDTO> ObtenerStockArticuloXAlmacenXPendiente(int IdObra, int IdArticulo, ref string mensaje_error)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerCantidadPendiente", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.CantidadPendiente = decimal.Parse(drd["CantidadPendiente"].ToString());

                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }

        public List<ArticuloDTO> ObtenerArticulosStockObras(int IdArticulo, ref string mensaje_error)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarStockTodasLasObras", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Obra = drd["Obra"].ToString();
                        oArticuloDTO.Almacen = drd["Almacen"].ToString();
                        oArticuloDTO.Stock = decimal.Parse(drd["Stock"].ToString());
                    
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }
    }
}
