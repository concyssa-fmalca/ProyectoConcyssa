using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace ConcyssaWeb.Controllers
{
    public class TipoRegistroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listado()
        {
            return View();
        }

        public IActionResult Semana()
        {
            return View();
        }

        public string ObtenerTipoRegistros(int estado=3)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoRegistroDTO> lstTipoRegistroDTO = oTipoRegistroDAO.ObtenerTipoRegistro(IdSociedad, estado,BaseDatos,ref mensaje_error);
            DataTableDTO oDataTableDTO = new DataTableDTO();
         
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstTipoRegistroDTO.Count;
                oDataTableDTO.iTotalRecords = lstTipoRegistroDTO.Count;
                oDataTableDTO.aaData = (lstTipoRegistroDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);
            
        }

        public string ObtenerTipoRegistrosAjax(int estado = 3)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoRegistroDTO> lstTipoRegistroDTO = oTipoRegistroDAO.ObtenerTipoRegistro(IdSociedad, estado,BaseDatos,ref mensaje_error);
            if (lstTipoRegistroDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoRegistroDTO);
            }
            else
            {
                return mensaje_error;
            }

        }
       


        public string ObtenerDatosxID(int IdTipoRegistro)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            List<TipoRegistroDTO> lstTipoRegistroDTO = oTipoRegistroDAO.ObtenerDatosxID(IdTipoRegistro,BaseDatos,ref mensaje_error);

            if (lstTipoRegistroDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoRegistroDTO[0]);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertTipoRegistro(TipoRegistroDTO oTipoRegistroDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oTipoRegistroDTO.IdSociedad = IdSociedad;
            int respuesta = oTipoRegistroDAO.UpdateInsertTipoRegistro(oTipoRegistroDTO,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }
        }


        public int EliminarTipoRegistro(int IdTipoRegistro)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            int resultado = oTipoRegistroDAO.Delete(IdTipoRegistro,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }






        public string ObtenerSemanas(int IdTipoRegistro,int Anio,int IdObra,int estado = 3)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SemanaDTO> lstSemanaDTO = oSemanaDAO.ObtenerSemanas(IdTipoRegistro,Anio, IdObra,IdSociedad, estado,BaseDatos,ref mensaje_error);
            DataTableDTO oDataTableDTO = new DataTableDTO();
            if (lstSemanaDTO.Count > 0 || mensaje_error=="")
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstSemanaDTO.Count;
                oDataTableDTO.iTotalRecords = lstSemanaDTO.Count;
                oDataTableDTO.aaData = (lstSemanaDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);
            }
            else
            {
                return "error";
            }
        }
        public string ObtenerSemanaAjax(int IdTipoRegistro, int IdObra, int estado = 3)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SemanaDTO> lstSemanaDTO = oSemanaDAO.ObtenerSemanasxIdTipoRegistro( IdSociedad, estado,IdTipoRegistro, IdObra,BaseDatos,ref mensaje_error);
            if (lstSemanaDTO.Count > 0 || mensaje_error == "")
            {
                
                return JsonConvert.SerializeObject(lstSemanaDTO);
            }
            else
            {
                return "error";
            }
        }

        


        public string ObtenerSemanaDatosxID(int IdSemana)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            List<SemanaDTO> lstSemanaDTO = oSemanaDAO.ObtenerDatosxID(IdSemana,BaseDatos,ref mensaje_error);

            if (lstSemanaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSemanaDTO[0]);
            }
            else
            {
                return mensaje_error;
            }
        }



        public string UpdateInsertSemana(SemanaDTO oSemanaDTO)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oSemanaDTO.IdSociedad = IdSociedad;
            int respuesta = oSemanaDAO.UpdateInsertSemana(oSemanaDTO,BaseDatos,ref mensaje_error);

            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta == 1)
                {
                    return "1";
                }
                else
                {
                    return "error";
                }
            }
        }


        public int EliminarSemana(int IdSemana)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            int resultado = oSemanaDAO.Delete(IdSemana,BaseDatos,ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }









        public string validarEmpresaActual()
        {
            string rpta = "";
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            if (IdSociedad == null)
            {
                return "SinBD";
            }
            return rpta;
        }

        public string ObtenerSemanasXIdObraAnioIdTipoRegistro(int IdObra, int Anio,int IdTipoRegistro )
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            List<SemanaDTO> lstSemanaDTO = oSemanaDAO.ObtenerSemanasXIdObraAnio(IdObra,Anio,IdTipoRegistro,BaseDatos,ref mensaje_error);

            if (lstSemanaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSemanaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }

        static DateTime GetFirstDayOfWeek(int year, int week)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                week -= 1;
            }

            return firstMonday.AddDays(week * 7);
        }

        public string GenerarSemanasMasivo() {

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ObraDAO oObraDAO = new ObraDAO();
            string mensaje_error = "";
            List<ObraDTO> lstObraDTO = oObraDAO.ObtenerObra(1, BaseDatos, ref mensaje_error);

            TipoRegistroDAO tipoRegistroDAO = new TipoRegistroDAO();
            List<TipoRegistroDTO> lstTipoRegistroDTO =  tipoRegistroDAO.ObtenerTipoRegistro(1, 1, BaseDatos, ref mensaje_error);


            int year = 2025; // Puedes cambiar el año según necesites
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            int totalWeeks = cal.GetWeekOfYear(new DateTime(year, 12, 31), dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            SemanaDAO oSemanaDAO = new SemanaDAO();

            List<SemanaDTO> Semanas = new List<SemanaDTO>();
            for (int week = 1; week <= totalWeeks; week++)
            {
                DateTime startOfWeek = GetFirstDayOfWeek(year, week);
                DateTime endOfWeek = startOfWeek.AddDays(6);

                SemanaDTO semanaDTO = new SemanaDTO();
                semanaDTO.FechaI = startOfWeek;
                semanaDTO.FechaF = endOfWeek;
                semanaDTO.NumSemana = week;
                Semanas.Add(semanaDTO);
                
            }

            for (int i = 0; i < lstObraDTO.Count; i++)
            {
                if (lstObraDTO[i].IdObra != 2050)
                {
                    continue;
                }

                for (int j = 0;j< lstTipoRegistroDTO.Count; j++)
                {
                    for (int k = 0; k < Semanas.Count-1; k++)
                    {
                        oSemanaDAO.GeneradorMasivoSemanas(Semanas[k].FechaI, Semanas[k].FechaF, Semanas[k].NumSemana, year, lstTipoRegistroDTO[j].IdTipoRegistro, lstObraDTO[i].IdObra, k+1, BaseDatos);
                    }
                }
            }

            return "LISTO";

        }

    }
}
