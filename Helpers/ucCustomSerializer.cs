using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class ucCustomSerializer
    {
		public static string Serializar<T>(List<T> lista, char separadorCampo, char separadorRegistro, string[] campos, bool incluirCabeceras = true)
		{
			StringBuilder sb = new StringBuilder();
			StringBuilder sbc = new StringBuilder();
			if (lista != null && lista.Count > 0)
			{
				PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
				if (campos.Length == 0)
				{
					if (incluirCabeceras)
					{
						for (int i = 0; i < propiedades.Length - 1; i++)
						{
							sbc.Append(propiedades[i].Name);
							if (i < propiedades.Length - 1)
							{
								sbc.Append(separadorCampo);
							}
						}
						sb.Append(separadorRegistro);
					}
					for (int n = 0; n < lista.Count; n++)
					{
						propiedades = lista[n].GetType().GetProperties();
						for (int m = 0; m < propiedades.Length; m++)
						{
							string cabece2 = propiedades[m].Name;
							string tipo2 = propiedades[m].PropertyType.ToString();
							object valor2 = propiedades[m].GetValue(lista[n], null);
							if (valor2 != null)
							{
								if (tipo2.Contains("Byte[]"))
								{
									byte[] buffer2 = (byte[])valor2;
									sb.Append(Convert.ToBase64String(buffer2));
								}
								else if (tipo2.Contains("DateTime"))
								{
									sb.Append(Convert.ToDateTime(valor2).ToString("dd-MM-yyyy"));
								}
								else if (!tipo2.Contains("String") || !(cabece2.ToLower() == "estado"))
								{
									if (tipo2.ToLower().Contains("bool") && cabece2.ToLower() == "estado")
									{
										if (valor2.ToString().ToUpper() == "TRUE")
										{
											sb.Append("ACTIVO");
										}
										else
										{
											sb.Append("INACTIVO");
										}
									}
									else
									{
										sb.Append(valor2.ToString().ToUpper());
									}
								}
							}
							else
							{
								sb.Append("");
							}
							if (m < propiedades.Length - 1)
							{
								sb.Append(separadorCampo);
							}
						}
						if (n < lista.Count - 1)
						{
							sb.Append(separadorRegistro);
						}
					}
				}
				else
				{
					List<string> props = new List<string>();
					for (int l = 0; l < propiedades.Length; l++)
					{
						props.Add(propiedades[l].Name);
					}
					if (incluirCabeceras)
					{
						for (int k = 0; k < campos.Length; k++)
						{
							if (props.IndexOf(campos[k]) > -1)
							{
								sb.Append(campos[k]);
								sb.Append(separadorCampo);
							}
						}
						sb = sb.Remove(sb.Length - 1, 1);
						sb.Append(separadorRegistro);
					}
					for (int j2 = 0; j2 < lista.Count; j2++)
					{
						for (int j = 0; j < campos.Length; j++)
						{
							if (props.IndexOf(campos[j]) <= -1)
							{
								continue;
							}
							string cabece = lista[j2].GetType().GetProperty(campos[j]).Name;
							string tipo = lista[j2].GetType().GetProperty(campos[j]).PropertyType.ToString();
							object valor = lista[j2].GetType().GetProperty(campos[j]).GetValue(lista[j2], null);
							if (valor != null)
							{
								if (tipo.Contains("Byte[]"))
								{
									byte[] buffer = (byte[])valor;
									sb.Append(Convert.ToBase64String(buffer));
								}
								else if (tipo.Contains("DateTime"))
								{
									sb.Append(Convert.ToDateTime(valor).ToString("dd-MM-yyyy"));
								}
								else if (tipo.Contains("String") && cabece.ToLower() == "estado")
								{
									if (valor.ToString().ToUpper() == "A")
									{
										sb.Append("ACTIVO");
									}
									else
									{
										sb.Append("INACTIVO");
									}
								}
								else if (tipo.ToLower().Contains("bool") && cabece.ToLower() == "estado")
								{
									if (valor.ToString().ToUpper() == "TRUE")
									{
										sb.Append("ACTIVO");
									}
									else
									{
										sb.Append("INACTIVO");
									}
								}
								else
								{
									sb.Append(valor.ToString());
								}
							}
							else
							{
								sb.Append("");
							}
							sb.Append(separadorCampo);
						}
						sb = sb.Remove(sb.Length - 1, 1);
						sb.Append(separadorRegistro);
					}
					sb = sb.Remove(sb.Length - 1, 1);
				}
			}
			return sb.ToString();
		}

		public static List<T> Deserializar<T>(string archivo, char separadorCampo, char separadorRegistro)
		{
			List<T> lista = new List<T>();
			if (File.Exists(archivo))
			{
				string contenido = File.ReadAllText(archivo);
				string[] registros = contenido.Split(separadorRegistro);
				string[] cabecera = registros[0].Split(separadorCampo);
				for (int i = 1; i < registros.Length; i++)
				{
					string registro = registros[i];
					Type tipoObj = typeof(T);
					T obj = (T)Activator.CreateInstance(tipoObj);
					string[] campos = registro.Split(separadorCampo);
					for (int j = 0; j < campos.Length; j++)
					{
						Type tipoCampo = obj.GetType().GetProperty(cabecera[j]).PropertyType;
						dynamic valor = Convert.ChangeType(campos[j], tipoCampo);
						obj.GetType().GetProperty(cabecera[j]).SetValue(obj, valor);
					}
					lista.Add(obj);
				}
			}
			return lista;
		}

		public static string SerializarObjeto<T>(T obj, char separadorCampo, string archivo = "")
		{
			StringBuilder sb = new StringBuilder();
			PropertyInfo[] propiedades = obj.GetType().GetProperties();
			if (archivo == "")
			{
				for (int k = 0; k < propiedades.Length; k++)
				{
					string tipo = propiedades[k].PropertyType.ToString();
					if (propiedades[k].GetValue(obj, null) != null)
					{
						if (tipo.Contains("Byte[]"))
						{
							byte[] buffer2 = (byte[])propiedades[k].GetValue(obj, null);
							sb.Append(Convert.ToBase64String(buffer2));
						}
						else
						{
							sb.Append(propiedades[k].GetValue(obj, null).ToString());
						}
					}
					else
					{
						sb.Append("");
					}
					if (k < propiedades.Length - 1)
					{
						sb.Append(separadorCampo);
					}
				}
			}
			else if (File.Exists(archivo))
			{
				List<string> campos = File.ReadAllLines(archivo).ToList();
				List<string> props = new List<string>();
				for (int j = 0; j < propiedades.Length; j++)
				{
					props.Add(propiedades[j].Name);
				}
				for (int i = 0; i < campos.Count; i++)
				{
					if (props.IndexOf(campos[i]) <= -1)
					{
						continue;
					}
					string tipo = obj.GetType().GetProperty(campos[i]).PropertyType.ToString();
					object valor = obj.GetType().GetProperty(campos[i]).GetValue(obj, null);
					if (valor != null)
					{
						if (tipo.Contains("Byte[]"))
						{
							byte[] buffer = (byte[])valor;
							sb.Append(Convert.ToBase64String(buffer));
						}
						else
						{
							sb.Append(valor.ToString());
						}
					}
					else
					{
						sb.Append("");
					}
					sb.Append(separadorCampo);
				}
				sb = sb.Remove(sb.Length - 1, 1);
			}
			return sb.ToString();
		}
	}
}
