using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ValidacionSUNATDTO
    {
    }
    public class DocumentoConsultaDTO
    {
        public string numRuc;
        public string codComp;
        public string numeroSerie;
        public string numero;
        public string fechaEmision;
        public decimal monto;
    }

    public class ResponseDocumentoConsultaDTO
    {
        public bool success;
        public string message;
        public ResponseDocumentoDataDTO data;
    }

    public class ResponseDocumentoDataDTO
    {
        public string estadoCp;
        public string estadoRuc;
        public string condDomiRuc;
    }
    public class ResponseTokenSunatDTO
    {
        public string access_token;
        public string token_type;
        public int expires_in;
    }
}
