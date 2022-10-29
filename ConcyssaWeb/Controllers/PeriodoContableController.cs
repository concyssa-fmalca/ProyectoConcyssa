using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class PeriodoContableController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerPeriodoContableDT(int Estado = 3)
        {
            string mensaje_error = "";
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            DataTableDTO oDataTableDTO = new DataTableDTO();
            List<PeriodoContableDTO> lstPeriodoContableDTO = oPeriodoDAO.ObtenerPeriodoContable(IdSociedad, Estado, ref mensaje_error);
            if (lstPeriodoContableDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                oDataTableDTO.sEcho = 1;
                oDataTableDTO.iTotalDisplayRecords = lstPeriodoContableDTO.Count;
                oDataTableDTO.iTotalRecords = lstPeriodoContableDTO.Count;
                oDataTableDTO.aaData = (lstPeriodoContableDTO);
                //return oDataTableDTO;
                return JsonConvert.SerializeObject(oDataTableDTO);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerPeriodoContable(int Estado = 3)
        {
            string mensaje_error = "";
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            List<PeriodoContableDTO> lstPeriodoContableDTO = oPeriodoDAO.ObtenerPeriodoContable(IdSociedad, Estado, ref mensaje_error);
            if (lstPeriodoContableDTO.Count >= 0 && mensaje_error.Length == 0)
            {
                return JsonConvert.SerializeObject(lstPeriodoContableDTO);

            }
            else
            {
                return mensaje_error;
            }
        }

        public string ObtenerPeriodoContablexId(int IdPeriodoContable)
        {
            string mensaje_error = "";
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            PeriodoContableDTO oPeriodoContableDTO = oPeriodoDAO.ObtenerPeriodoContablexId(IdPeriodoContable, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;

            }
            return JsonConvert.SerializeObject(oPeriodoContableDTO);
        }



        public string UpdateInsertPeriodoContable(PeriodoContableDTO oPeriodoContableDTO, 
            string FechaContabilizacionI,string FechaContabilizacionF
            ,string FechaVencimientoI, string FechaVencimientoF
            ,string FechaDocumentoI, string FechaDocumentoF)
        {
            string mensaje_error = "";
            List<PeriodoContableFechaDTO> oPeriodoContableFechaDTO = new List<PeriodoContableFechaDTO>();
            oPeriodoContableFechaDTO.Add(new PeriodoContableFechaDTO
            {
                FechaInicio = Convert.ToDateTime(FechaContabilizacionI) ,
                FechaFinal = Convert.ToDateTime(FechaContabilizacionF),
                Descripcion = "FECHA CONTABILIZACION",
                Orden = 1
            });

            oPeriodoContableFechaDTO.Add(new PeriodoContableFechaDTO
            {
                FechaInicio = Convert.ToDateTime(FechaVencimientoI),
                FechaFinal = Convert.ToDateTime(FechaVencimientoF),
                Descripcion = "FECHA VENCIMIENTO",
                Orden = 2
            });

            oPeriodoContableFechaDTO.Add(new PeriodoContableFechaDTO
            {
                FechaInicio = Convert.ToDateTime(FechaDocumentoI),
                FechaFinal = Convert.ToDateTime(FechaDocumentoF),
                Descripcion = "FECHA DOCUMENTO",
                Orden = 3
            });


            int IdSociedad = Convert.ToInt32((String.IsNullOrEmpty(oPeriodoContableDTO.IdSociedad.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad")) : oPeriodoContableDTO.IdSociedad);
            int IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(oPeriodoContableDTO.IdUsuario.ToString())) ? Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario")) : oPeriodoContableDTO.IdUsuario);
            if (IdSociedad == 0)
            {
                IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            }
            if (IdUsuario == 0)
            {
                IdUsuario = Convert.ToInt32(HttpContext.Session.GetInt32("IdUsuario"));
            }
            oPeriodoContableDTO.IdSociedad = IdSociedad;
            oPeriodoContableDTO.IdUsuario = IdUsuario;
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();

            int respuesta = oPeriodoDAO.UpdateInsertPeriodoContable(oPeriodoContableDTO, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            else
            {
                if (respuesta > 0)
                {
                    for (int i = 0; i < oPeriodoContableFechaDTO.Count; i++)
                    {
                        oPeriodoContableFechaDTO[i].IdPeriodoContable = respuesta;
                        int respuesta2 = oPeriodoDAO.UpdateInsertPeriodoContableFecha(oPeriodoContableFechaDTO[i], ref mensaje_error);
                    }

                    return respuesta.ToString();
                }
            }
            return respuesta.ToString();

        }

        public string EliminarPeriodoContable(int IdPeriodoContable)
        {
            string mensaje_error = "";
            PeriodoDAO oPeriodoDAO = new PeriodoDAO();
            int resultado = oPeriodoDAO.DeletePeriodoContable(IdPeriodoContable, ref mensaje_error);
            if (mensaje_error.Length > 0)
            {
                return mensaje_error;
            }
            return resultado.ToString();
        }
    }
}
