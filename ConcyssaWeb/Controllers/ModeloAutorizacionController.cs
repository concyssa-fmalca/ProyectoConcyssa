using DAO;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcyssaWeb.Controllers
{
    public class ModeloAutorizacionController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }

        public string ObtenerModeloAutorizacion()
        {
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = oModeloAutorizacionDAO.ObtenerModeloAutorizacion(IdSociedad.ToString(),BaseDatos);
            if (lstModeloAutorizacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstModeloAutorizacionDTO);
            }
            else
            {
                return "error";
            }
        }


        public int UpdateInsertModeloAutorizacion(ModeloAutorizacionDTO modeloAutorizacionDTO)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));

            int resultado = oModeloAutorizacionDAO.UpdateInsertModeloAutorizacion(modeloAutorizacionDTO,IdSociedad.ToString(),BaseDatos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }


        public string ObtenerDatosxID(int IdModeloAutorizacion)
        {
            string valida = "";
            valida = validarEmpresaActual();
            if (valida != "")
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = oModeloAutorizacionDAO.ObtenerDatosxID(IdModeloAutorizacion,BaseDatos);

            if (lstModeloAutorizacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstModeloAutorizacionDTO);
            }
            else
            {
                return "error";
            }

        }


        public int EliminarModeloAutorizacionDetalleEtapa(int IdModeloAutorizacionEtapa)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int resultado = oModeloAutorizacionDAO.EliminarModeloAutorizacionDetalleEtapa(IdModeloAutorizacionEtapa,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarModeloAutorizacionDetalleAutor(int IdModeloAutorizacionAutor)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int resultado = oModeloAutorizacionDAO.EliminarModeloAutorizacionDetalleAutor(IdModeloAutorizacionAutor,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public int EliminarModeloAutorizacionDetalleCondicion(int IdModeloAutorizacionCondicion)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int resultado = oModeloAutorizacionDAO.EliminarModeloAutorizacionDetalleCondicion(IdModeloAutorizacionCondicion,BaseDatos);
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

        public int validarEmpresaActualUpdateInsert()
        {
            int rpta = 0;
            int IdSociedad = Convert.ToInt32(HttpContext.Session.GetInt32("IdSociedad"));
            if (IdSociedad == null)
            {
                return -999;
            }
            return rpta;
        }

        
        public int EliminarModeloAutorizacion(int IdModeloAutorizacion)
        {
            int valida = 0;
            valida = validarEmpresaActualUpdateInsert();
            if (valida != 0)
            {
                return valida;
            }

            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int resultado = oModeloAutorizacionDAO.EliminarModeloAutorizacion(IdModeloAutorizacion,BaseDatos);
            if (resultado == 0)
            {
                resultado = 1;
            }else if(resultado == 1)
            {
                return  2;
            }

            return 0;
        }

        public string ValidarAutoresxTipoDocumento(int IdAutor, int IdTipoDocumento)
        {
            string BaseDatos = String.IsNullOrEmpty(HttpContext.Session.GetString("BaseDatos")) ? "" : HttpContext.Session.GetString("BaseDatos")!;
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = oModeloAutorizacionDAO.ValidarAutoresxTipoDocumento(IdAutor, IdTipoDocumento, BaseDatos);
            if (lstModeloAutorizacionDTO.Count == 0)
            {
                return "ok";
            }
            else
            {
                string mensaje = "El Usuario ya es autor de los Siguientes Modelos: </br>";
                for (int i = 0; i < lstModeloAutorizacionDTO.Count; i++)
                {
                    mensaje += lstModeloAutorizacionDTO[i].DescripcionModelo +"</br>";
                }
                return mensaje;
            }

        }

    }
}
