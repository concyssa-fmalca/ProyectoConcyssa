using DTO;
using Helpers;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class MenuDAO
    {

		
		public List<beCampoString3> obtenerMenuPerfil(int IdPerfil, ref string mensaje_error)
		{
			List<beCampoString3> listaAcceso = null;
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("SMC_AccesoXPerfil", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					if (dr != null)
					{
						listaAcceso = new List<beCampoString3>();
						while (dr.Read())
						{
							beCampoString3 oSeg_Rol = new beCampoString3();
							oSeg_Rol.Campo1 = dr["IdPerfil"].ToString();
							oSeg_Rol.Campo2 = dr["idMenu"].ToString();
							oSeg_Rol.Campo3 = dr["Acceso"].ToString();
							listaAcceso.Add(oSeg_Rol);
						}
					}
				}
				catch (Exception ex)
				{
					mensaje_error = ex.Message.ToString();
				}
			}
			return listaAcceso;
		}


		public List<MenuDTO> ObtenerMenus()
		{
			List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMenus", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					SqlDataReader drd = da.SelectCommand.ExecuteReader();
					while (drd.Read())
					{
						MenuDTO oMenuDTO = new MenuDTO();
						oMenuDTO.idMenu = int.Parse(drd["IdMenu"].ToString());
						oMenuDTO.Menu = int.Parse(drd["Menu"].ToString());
						oMenuDTO.Estado = bool.Parse(drd["Estado"].ToString());
						oMenuDTO.Orden = int.Parse(drd["Orden"].ToString());
						//oMenuDTO.Posicion = int.Parse(drd["Orden"].ToString());
						lstMenuDTO.Add(oMenuDTO);
					}
					drd.Close();


				}
				catch (Exception ex)
				{
				}
			}
			return lstMenuDTO;
		}
	

		public List<MenuDTO> ListarTodo()
		{
			List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_ListarTodo", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					while (dr.Read())
					{
						MenuDTO oMenuDTO = new MenuDTO();
						oMenuDTO.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
						oMenuDTO.Descripcion = dr["Descripcion"].ToString();
						oMenuDTO.Controller = dr["Controller"].ToString();
						oMenuDTO.Action = dr["Action"].ToString();
						oMenuDTO.Menu = Convert.ToInt32(dr["Menu"].ToString());
						oMenuDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
						oMenuDTO.FechaUltimaModificacion = Convert.ToDateTime(dr["FechaUltimaModificacion"].ToString());
						oMenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
						oMenuDTO.UsuarioUltimaModificacion = Convert.ToInt32(dr["UsuarioUltimaModificacion"].ToString());
						oMenuDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
						oMenuDTO.Orden = Convert.ToInt32(dr["Orden"].ToString());
						lstMenuDTO.Add(oMenuDTO);
					}
				}
				catch (Exception)
				{
				}
			}
			return lstMenuDTO;
		}

		public List<MenuDTO> ListarxID_Usuario(int idUsuario)
		{
			List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_ListarxID_Usuario", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					da.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					while (dr.Read())
					{
						MenuDTO oMenuDTO = new MenuDTO();
						oMenuDTO.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
						oMenuDTO.Descripcion = dr["Descripcion"].ToString();
						oMenuDTO.Controller = dr["Controller"].ToString();
						oMenuDTO.Action = dr["Action"].ToString();
						oMenuDTO.Menu = Convert.ToInt32(dr["Menu"].ToString());
						oMenuDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
						oMenuDTO.FechaUltimaModificacion = Convert.ToDateTime(dr["FechaUltimaModificacion"].ToString());
						oMenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
						oMenuDTO.UsuarioUltimaModificacion = Convert.ToInt32(dr["UsuarioUltimaModificacion"].ToString());
						oMenuDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
						lstMenuDTO.Add(oMenuDTO);
					}
				}
				catch (Exception)
				{
				}
			}
			return lstMenuDTO;
		}

		public List<List<decimal>> DashBoard(int periodo)
		{
			List<List<decimal>> lista = new List<List<decimal>>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("SP_Dashboard_BarChart", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					da.SelectCommand.Parameters.AddWithValue("@Periodo", periodo);
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					if (dr != null)
					{
						while (dr.Read())
						{
							List<decimal> lst = new List<decimal>();
							lst.Add(Convert.ToInt32(dr["Year"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Total"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Enero"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Febrero"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Marzo"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Abril"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Mayo"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Junio"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Julio"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Agosto"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Septiembre"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Octubre"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Noviembre"].ToString()));
							lst.Add(Convert.ToDecimal(dr["Diciembre"].ToString()));
							lista.Add(lst);
						}
					}
				}
				catch (Exception)
				{
				}
			}
			return lista;
		}

		public int UpdateInsert(MenuDTO oSeg_Menu )
		{
			TransactionOptions transactionOptions = default(TransactionOptions);
			transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
			transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
			TransactionOptions option = transactionOptions;
			using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
			{
				using (SqlConnection cn = new Conexion().conectar())
				{
					try
					{
						cn.Open();
						SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_UpdateInsert", cn);
						da.SelectCommand.CommandType = CommandType.StoredProcedure;
						da.SelectCommand.Parameters.AddWithValue("@idMenu", oSeg_Menu.idMenu);
						da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSeg_Menu.Descripcion);
						da.SelectCommand.Parameters.AddWithValue("@Controller", oSeg_Menu.Controller);
						da.SelectCommand.Parameters.AddWithValue("@Action", oSeg_Menu.Action);
						da.SelectCommand.Parameters.AddWithValue("@Menu", oSeg_Menu.Menu);
						da.SelectCommand.Parameters.AddWithValue("@FechaCreacion", oSeg_Menu.FechaCreacion);
						da.SelectCommand.Parameters.AddWithValue("@FechaUltimaModificacion", oSeg_Menu.FechaUltimaModificacion);
						da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oSeg_Menu.UsuarioCreacion);
						da.SelectCommand.Parameters.AddWithValue("@UsuarioUltimaModificacion", oSeg_Menu.UsuarioUltimaModificacion);
						da.SelectCommand.Parameters.AddWithValue("@Estado", oSeg_Menu.Estado);
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

		public int Delete(MenuDTO oSeg_Menu )
		{
			TransactionOptions transactionOptions = default(TransactionOptions);
			transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
			transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
			TransactionOptions option = transactionOptions;
			using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
			{
				using (SqlConnection cn = new Conexion().conectar())
				{
					try
					{
						cn.Open();
						SqlDataAdapter da = new SqlDataAdapter("SP_Seg_Menu_Delete", cn);
						da.SelectCommand.CommandType = CommandType.StoredProcedure;
						da.SelectCommand.Parameters.AddWithValue("@idMenu", oSeg_Menu.idMenu);
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

		public List<MenuDTO> ConsultarMenu_1()
		{
			List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("Seg_TraeMenu_1", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					while (dr.Read())
					{
						MenuDTO oMenuDTO = new MenuDTO();
						oMenuDTO.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
						oMenuDTO.Descripcion = dr["Descripcion"].ToString();
						oMenuDTO.Controller = dr["Controller"].ToString();
						oMenuDTO.Action = dr["Action"].ToString();
						oMenuDTO.Menu = Convert.ToInt32(dr["Menu"].ToString());
						oMenuDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
						oMenuDTO.FechaUltimaModificacion = Convert.ToDateTime(dr["FechaUltimaModificacion"].ToString());
						oMenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
						oMenuDTO.UsuarioUltimaModificacion = Convert.ToInt32(dr["UsuarioUltimaModificacion"].ToString());
						oMenuDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
						lstMenuDTO.Add(oMenuDTO);
					}
				}
				catch (Exception)
				{
				}
			}
			return lstMenuDTO;
		}

		public List<MenuDTO> ConsultarMenu_2()
		{
			List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("Seg_TraeMenu_2", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					while (dr.Read())
					{
						MenuDTO oMenuDTO = new MenuDTO();
						oMenuDTO.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
						oMenuDTO.Descripcion = dr["Descripcion"].ToString();
						oMenuDTO.Controller = dr["Controller"].ToString();
						oMenuDTO.Action = dr["Action"].ToString();
						oMenuDTO.Menu = Convert.ToInt32(dr["Menu"].ToString());
						oMenuDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
						oMenuDTO.FechaUltimaModificacion = Convert.ToDateTime(dr["FechaUltimaModificacion"].ToString());
						oMenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
						oMenuDTO.UsuarioUltimaModificacion = Convert.ToInt32(dr["UsuarioUltimaModificacion"].ToString());
						oMenuDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
						lstMenuDTO.Add(oMenuDTO);
					}
				}
				catch (Exception)
				{
				}
			}
			return lstMenuDTO;
		}

		public List<MenuDTO> ConsultarMenu_3()
		{
			List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("Seg_TraeMenu_3", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					while (dr.Read())
					{
						MenuDTO oMenuDTO = new MenuDTO();
						oMenuDTO.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
						oMenuDTO.Descripcion = dr["Descripcion"].ToString();
						oMenuDTO.Controller = dr["Controller"].ToString();
						oMenuDTO.Action = dr["Action"].ToString();
						oMenuDTO.Menu = Convert.ToInt32(dr["Menu"].ToString());
						oMenuDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
						oMenuDTO.FechaUltimaModificacion = Convert.ToDateTime(dr["FechaUltimaModificacion"].ToString());
						oMenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
						oMenuDTO.UsuarioUltimaModificacion = Convert.ToInt32(dr["UsuarioUltimaModificacion"].ToString());
						oMenuDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
						lstMenuDTO.Add(oMenuDTO);
					}
				}
				catch (Exception)
				{
				}
			}
			return lstMenuDTO;
		}

		public Seg_RolMenuVistaDTO ListarMenurol(int IdUsuario,int IdPerfil,ref string mensaje_error)
		{
			Seg_RolMenuVistaDTO oSeg_RolMenuVistaDTO = new Seg_RolMenuVistaDTO();
			List<beCampoString> listaRol = new List<beCampoString>();
			List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("SP_Seg_MenuRolListas", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					da.SelectCommand.Parameters.AddWithValue("@idUsuario", IdUsuario);
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					if (dr != null)
					{
						while (dr.Read())
						{
							beCampoString oSeg_Rol = new beCampoString();
							oSeg_Rol.Campo1 = dr["IdPerfil"].ToString();
							oSeg_Rol.Campo2 = dr["Perfil"].ToString();
							listaRol.Add(oSeg_Rol);
						}
						oSeg_RolMenuVistaDTO.listaRol = listaRol;
						if (dr.NextResult())
						{
							while (dr.Read())
							{
								MenuDTO oMenuDTO = new MenuDTO();
								oMenuDTO.idMenu = Convert.ToInt32(dr["IdMenu"].ToString());
								oMenuDTO.Descripcion = dr["Descripcion"].ToString();
								oMenuDTO.Controller = dr["Controller"].ToString();
								oMenuDTO.Action = dr["Action"].ToString();
								oMenuDTO.Menu = Convert.ToInt32(dr["Menu"].ToString());
								oMenuDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
								oMenuDTO.FechaUltimaModificacion = Convert.ToDateTime(dr["FechaUltimaModificacion"].ToString());
								oMenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
								oMenuDTO.UsuarioUltimaModificacion = Convert.ToInt32(dr["UsuarioUltimaModificacion"].ToString());
								oMenuDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
								lstMenuDTO.Add(oMenuDTO);
							}
							oSeg_RolMenuVistaDTO.listaMenu = lstMenuDTO;
						}
					}
				}
				catch (Exception ex)
				{
					mensaje_error = ex.Message.ToString();
				}
			}
			return oSeg_RolMenuVistaDTO;
		}

		public bool GrabarAccesos( string lista, int idUsuario,ref string mensaje_error)
		{
			bool exito = false;
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("SMC_AccesosInsertUpdate", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					da.SelectCommand.Parameters.AddWithValue("@Lista", lista);
					da.SelectCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
					int i = da.SelectCommand.ExecuteNonQuery();
					if (i > 0)
					{
						exito = true;
					}
				}
				catch (Exception ex)
				{
					mensaje_error=ex.Message.ToString();	
				}
			}
			return exito;
		}

		public List<beCampoString3> obtenerMenuRol(int IdRol,ref string mensajeError)
		{
			List<beCampoString3> listaAcceso = null;
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open();
					SqlDataAdapter da = new SqlDataAdapter("SP_Seg_AccesoXRol", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					da.SelectCommand.Parameters.AddWithValue("@idRol", IdRol);
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					if (dr != null)
					{
						listaAcceso = new List<beCampoString3>();
						while (dr.Read())
						{
							beCampoString3 oSeg_Rol = new beCampoString3();
							oSeg_Rol.Campo1 = dr["idRol"].ToString();
							oSeg_Rol.Campo2 = dr["idMenu"].ToString();
							oSeg_Rol.Campo3 = dr["Acceso"].ToString();
							listaAcceso.Add(oSeg_Rol);
						}
					}
				}
				catch (Exception)
				{
				}
			}
			return listaAcceso;
		}

		public List<MenuDTO> ListarxPerfil(int IdPerfil, ref string mensajeError)
		{
			List<MenuDTO> lstMenuDTO = new List<MenuDTO>();
			using (SqlConnection cn = new Conexion().conectar())
			{
				try
				{
					cn.Open(); 
					SqlDataAdapter da = new SqlDataAdapter("SMC_Menu_ListaXPerfil", cn);
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
					SqlDataReader dr = da.SelectCommand.ExecuteReader();
					while (dr.Read())
					{
						MenuDTO oMenuDTO = new MenuDTO();
						oMenuDTO.idMenu = Convert.ToInt32(dr["idMenu"].ToString());
						oMenuDTO.Descripcion = dr["Descripcion"].ToString();
						oMenuDTO.Controller = dr["Controller"].ToString();
						oMenuDTO.Action = dr["Action"].ToString();
						oMenuDTO.Menu = Convert.ToInt32(dr["Menu"].ToString());
						oMenuDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
						oMenuDTO.FechaUltimaModificacion = Convert.ToDateTime(dr["FechaUltimaModificacion"].ToString());
						oMenuDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
						oMenuDTO.UsuarioUltimaModificacion = Convert.ToInt32(dr["UsuarioUltimaModificacion"].ToString());
						oMenuDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
						lstMenuDTO.Add(oMenuDTO);
					}
				}
				catch (Exception e)
				{
					mensajeError = e.Message.ToString();
				}
			}
			return lstMenuDTO;
		}
	}
}
