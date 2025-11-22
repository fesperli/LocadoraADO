using Locadora.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Locadora.Models
{
    public class Locacao
    {

        public Guid LocacaoId { get; private set; }
        public int ClienteId { get; private set; }
        public string ClienteNome { get; private set; }
        public int VeiculoId { get; private set; }
        public string VeiculoNome { get; private set; }
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public decimal ValorDiaria { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal? Multa { get; private set; } = 0.00m;
        public EStatusLocacao StatusLocacao { get; private set; } = EStatusLocacao.Ativa;
        public List<LocacaoFuncionario> LocacaoFuncionarios { get; private set; } = [];


        public readonly static string sp_AdicionarLocacao =
        "EXEC sp_AdicionarLocacao @ClienteId, @VeiculoId, @DataDevolucaoPrevista, @DataDevolucaoReal, @ValorDiaria, @ValorTotal, @Multa, @Status; SELECT SCOPE_IDENTITY();";

        public readonly static string sp_AtualizarLocacao =
            "EXEC sp_AtualizarLocacao @idLocacao, @DataDevolucaoReal, @SWtatus, @Multa";

        public readonly static string sp_BuscarLocacaoId =
            "EXEC sp_BuscarLocacaoId @idLocacao";

        public readonly static string sp_BuscarLocacao =
               "EXEC sp_BuscarLocacao";

        public readonly static string sp_CancelarLocacao =
            "EXEC sp_CancelarLocacao @idLocacao, @Status";

        public void SetVeiculoNome(string nome)
            => VeiculoNome = nome;

        public void SetClienteNome(string nome)
            => ClienteNome = nome;

        public void SetMulta(decimal multa)
            => Multa = multa;

        public void SetStatus(EStatusLocacao status)
            => StatusLocacao = status;

        public void SetDataLocacao(DateTime dataLocacao)
            => DataLocacao = dataLocacao;

        public void SetDataDevolucaoReal(DateTime dataDevolucaoReal)
            => DataDevolucaoReal = dataDevolucaoReal;

        public Locacao(int clienteID, int veiculoID, decimal valorDiaria, int diasLocacao)
        {
            ClienteId = clienteID;
            VeiculoId = veiculoID;
            DataLocacao = DateTime.Now;
            ValorDiaria = valorDiaria;
            ValorTotal = valorDiaria * diasLocacao;
            DataDevolucaoPrevista = DateTime.Now.AddDays(diasLocacao);
            StatusLocacao = EStatusLocacao.Ativa;
        }

        public override string ToString()
        {
            return $"Cliente: {ClienteNome}\n" +
                   $"Veiculo: {VeiculoNome}\n" +
                   $"Data locacao: {DataLocacao}\n" +
                   $"Data prevista: {DataDevolucaoPrevista}\n" +
                   $"{(DataDevolucaoReal is null ? "" : $"Data devolucao real: {DataDevolucaoReal}\n")}" +
                   $"{(Multa is null ? "" : $"Multa: {Multa}\n")}" +
                   $"Valor diaria: {ValorDiaria}\n" +
                   $"Valor total: {ValorTotal}\n" +
                   $"Status: {StatusLocacao}\n";
        }
    }
}
