using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GRSunatDTO
    {
        public string N_DOC { get; set; } = "T001-000002";
        public string TIPO_DOC { get; set; } = "09";
        public string FECHA { get; set; } = "2022-10-23";
        public string RUC { get; set; } = "20513233605";
        public string TIPO_RUC { get; set; } = "6";
        public string O_COMPRA { get; set; } = "-";
        public string NOMBRE { get; set; } = "ANDES SYSTEMS E.I.R.L.";
        public string DIRECCION { get; set; } = "";
        public string RUC_EMIS { get; set; } = "20100370426";
        public string NOMBRE_EMIS { get; set; } = "CONCYSSA S A";
        public string MOT_TRAS { get; set; } = "01";
        public string MOT_TRAS_DES { get; set; } = "VENTA INTERNA";
        public decimal PESO_BRUTO { get; set; } = 0;
        public string UNI_PESO_BRUTO { get; set; } = "KGM";
        public decimal BULTOS { get; set; } = 0;
        public string TIPO_TRANS { get; set; } = "01";
        public string FCH_INICIO { get; set; } = "2022-10-23";
        public string RUC_TRANS { get; set; } = "20513233605";
        public string NOM_TRANS { get; set; } = "ANDES SYSTEMS";
        public string PLACA_PRINCIPAL { get; set; } = " C8Z-908";
        public string MARCA_PRINCIPAL { get; set; } = "";
        public string UBIGEO_LLE { get; set; } = "070101";
        public string PUNTO_LLE { get; set; } = "PUNTO DE PARTID";
        public string RUC_LLE { get; set; } = "";
        public string COD_LLE { get; set; } = "0000";
        public string UBIGEO_PAR { get; set; } = "070102";
        public string PUNTO_PAR { get; set; } = "PUNTO DE LLEGADA";
        public string RUC_PAR { get; set; } = "";
        public string COD_PAR { get; set; } = "0000";
        public string PDF { get; set; } = "SI";
        public string OBS1 { get; set; } = "";
        public string OBS2 { get; set; } = "";

        public List<ITEMS> ITEMS { get; set; } = new List<ITEMS>();
        public List<CONDUCTORES> CONDUCTORES { get; set; } = new List<CONDUCTORES>();
        public List<INDICADORES> INDICADORES { get; set; } = new List<INDICADORES>();
        public List<DOCS_RELA> DOCS_RELA { get; set; } = new List<DOCS_RELA>();
    }

    public class ITEMS{
        public string CODIGO { get; set; } = "01";
        public string DESCRIPCION { get; set; }
        public string TIPOUNI { get; set; }

        public decimal CANTIDAD { get; set; }

    }

    public class CONDUCTORES
    {
        public string TIPO { get; set; }
        public string RUC { get; set; }
        public string TIPO_RUC { get; set; }
        public string LICENCIA { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
    }

    public class DOCS_RELA
    {
        public string TIPO_RUC { get; set; }
        public string RUC { get; set; }
        public string DOCUMENTO { get; set; }
        public string TIPO { get; set; }
        public string DESCRIP { get; set; }
    }

    public class INDICADORES
    {
        public string CODIGO { get; set; }
    }

    public class ResultadoGRDTO
    {
        public Boolean Success { get; set; }
        public string Message { get; set; } = "";
        public string Ticket { get; set; } = "";
        public string Result { get; set; } = "";
        public string FilesMessage { get; set; } = "";
        public List<AnexoDTO> DetalleAnexo { get; set; }
    }

    public class ResponseDTO
    {
        public Boolean Success { get; set; }
        public string information { get; set; }
        public string registro { get; set; }

        public ErrorTicket error { get; set; }
        //public List<information> information { get; set; }

    }

    public class information
    {
        public string message { get; set; }
        public string pdf { get; set; }
        public string xml { get; set; }

    }



    public class ResultadoTicket
    {
        public Boolean Success { get; set; }
        public string information { get; set; } = "";
        public string message { get; set; } = "";
        public string qrbarcode { get; set; } = "";
        public string xml { get; set; } = "";
        public string pdf { get; set; } = "";
        public string cdr { get; set; } = "";

    }

    public class ResultadoTicketError
    {
        public Boolean Success { get; set; }
        public string information { get; set; } = "";

        public ErrorTicket error { get; set; }


    }

    public class ErrorTicket
    {
        public string numError { get; set; } = "";
        public string desError { get; set; } = "";
    }
}
