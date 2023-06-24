using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoRegistroDTO> lstTipoRegistroDTO = oTipoRegistroDAO.ObtenerTipoRegistro(IdSociedad, estado, ref mensaje_error);
            DataTableDTO oDataTableDTO = new DataTableDTO();
         
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstTipoRegistroDTO.Count;
                oDataTableDTO.iTotalRecords = lstTipoRegistroDTO.Count;
                oDataTableDTO.aaData = (lstTipoRegistroDTO);
                return JsonConvert.SerializeObject(oDataTableDTO);
            
        }

        public string ObtenerTipoRegistrosAjax(int estado = 3)
        {

            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<TipoRegistroDTO> lstTipoRegistroDTO = oTipoRegistroDAO.ObtenerTipoRegistro(IdSociedad, estado, ref mensaje_error);
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
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            List<TipoRegistroDTO> lstTipoRegistroDTO = oTipoRegistroDAO.ObtenerDatosxID(IdTipoRegistro, ref mensaje_error);

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

            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oTipoRegistroDTO.IdSociedad = IdSociedad;
            int respuesta = oTipoRegistroDAO.UpdateInsertTipoRegistro(oTipoRegistroDTO, ref mensaje_error);

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
            string mensaje_error = "";
            TipoRegistroDAO oTipoRegistroDAO = new TipoRegistroDAO();
            int resultado = oTipoRegistroDAO.Delete(IdTipoRegistro, ref mensaje_error);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }






        public string ObtenerSemanas(int IdTipoRegistro,int Anio,int IdObra,int estado = 3)
        {

            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SemanaDTO> lstSemanaDTO = oSemanaDAO.ObtenerSemanas(IdTipoRegistro,Anio, IdObra,IdSociedad, estado, ref mensaje_error);
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
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<SemanaDTO> lstSemanaDTO = oSemanaDAO.ObtenerSemanasxIdTipoRegistro( IdSociedad, estado,IdTipoRegistro, IdObra, ref mensaje_error);
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
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            List<SemanaDTO> lstSemanaDTO = oSemanaDAO.ObtenerDatosxID(IdSemana, ref mensaje_error);

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

            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            oSemanaDTO.IdSociedad = IdSociedad;
            int respuesta = oSemanaDAO.UpdateInsertSemana(oSemanaDTO, ref mensaje_error);

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
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            int resultado = oSemanaDAO.Delete(IdSemana, ref mensaje_error);
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
            string mensaje_error = "";
            SemanaDAO oSemanaDAO = new SemanaDAO();
            List<SemanaDTO> lstSemanaDTO = oSemanaDAO.ObtenerSemanasXIdObraAnio(IdObra,Anio,IdTipoRegistro, ref mensaje_error);

            if (lstSemanaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSemanaDTO);
            }
            else
            {
                return mensaje_error;
            }
        }



    }
}
