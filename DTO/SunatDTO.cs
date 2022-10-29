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
        public string NOMBRE { get; set; } = "ANDES SYSTEMS E.I.R.L.";
        public string RUC_EMIS { get; set; } = "20100370426";
        public string NOMBRE_EMIS { get; set; } = "CONCYSSA S A";
        public string MOT_TRAS { get; set; } = "01";
        public string MOT_TRAS_DES { get; set; } = "VENTA INTERNA";
        public string PESO { get; set; } = "4";
        public string BULTOS { get; set; } = "1";
        public string TIPO_TRANS { get; set; } = "01";
        public string FCH_INICIO { get; set; } = "2022-10-23";
        public string RUC_TRANS { get; set; } = "20513233605";
        public string NOM_TRANS { get; set; } = "ANDES SYSTEMS";
        public string PLACA { get; set; } = " C8Z-908";
        public string LIC_TRANS { get; set; } = "73073584";
        public string UBIGEO_LLE { get; set; } = "070101";
        public string PUNTO_LLE { get; set; } = "PUNTO DE PARTID";
        public string UBIGEO_PAR { get; set; } = "070102";
        public string PUNTO_PAR { get; set; } = "PUNTO DE LLEGADA";
        public string PUERTO { get; set; } = "puerto";
        public string PDF { get; set; } = "SI";
        public string ENVIO { get; set; } = "2";
        public string WSDL { get; set; } = "3";

        public List<ITEMS> ITEMS { get; set; } = new List<ITEMS>();
    }

    public class ITEMS{
        public string CODIGO { get; set; } = "01";
        public string DESCRIPCION { get; set; }
        public string TIPOUNI { get; set; }

        public decimal CANTIDAD { get; set; }

    }

    public class ResultadoGRDTO
    {
        public string Message { get; set; } = "";
        public string Result { get; set; } = "";
        public string FilesMessage { get; set; } = "";
        public List<AnexoDTO> DetalleAnexo { get; set; }
    }

    public class ResponseDTO
    {
        public Boolean Success { get; set; }
        public List<information> information { get; set; }

    }

    public class information
    {
        public string message { get; set; }
        public string pdf { get; set; }
        public string xml { get; set; }

    }
}
