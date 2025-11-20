using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Documento

    {
        public static readonly string INSERTDOCUMENTO = "INSERT INTO tblDocumentos (ClienteID, TipoDocumento, Numero, DataEmissao, DataValidade) " +
            "VALUES (@ClienteID, @TipoDocumento, @Numero, @DataEmissao, @DataValidade)";


        public int DocumentoID { get; private set; }
        public int ClienteID { get; private set; }
        public string TipoDocumento { get; private set; }
        public string Numero { get; private set; }
        public DateTime DataEmissao { get; private set; }
        public DateTime DataValidade { get; private set; }

        public Documento (string tipoDocumento, string numero, DateTime dataEmissao, DateTime dataValidade)
        {
            TipoDocumento = tipoDocumento;
            Numero = numero;
            DataEmissao = dataEmissao;
            DataValidade = dataValidade;
        }

        public void setClienteID(int clienteID)
        {
            ClienteID = clienteID;
        }


        public override string ToString()
        {
            return $"TipoDocumento: {TipoDocumento}\nNumero: {Numero}\nDataEmissao: {DataEmissao}\nDataValidade: {DataValidade}\n";
        }
    }
}
