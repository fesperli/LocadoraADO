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

        public Guid LocacaoID { get; private set; }
        public int ClienteID { get; private set; }
        public int VeiculoID { get; private set; }
        public DateTime DataLocacao { get; private set; }
        public DateTime DataDevolucaoPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }

        public decimal ValorDiaria { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal Multa { get; private set; }
        public ESatusLocacao  Status{ get; private set; }

        public Locacao(int clienteID, int veiculoID, decimal valorDiaria, int diasLocacao)
        {
            ClienteID = clienteID;
            VeiculoID = veiculoID;
            DataLocacao = DateTime.Now;
            ValorDiaria = valorDiaria;
            ValorTotal = valorDiaria * diasLocacao;
            DataDevolucaoPrevista = DateTime.Now.AddDays(diasLocacao);
            Status = ESatusLocacao.Ativa;

        }
        //TODO: definir os valores de cliente e veiculo como nome e modelo respectivamente
        public override string ToString()
        {
            return $"Locação ID: {LocacaoID}\n" +
                $"Cliente ID: {ClienteID}\n" +
                $"Veículo ID: {VeiculoID}\n" +
                $"Data de Locação: {DataLocacao}\n" +
                $"Data de Devolução Prevista: {DataDevolucaoPrevista}\n" +
                $"Data de Devolução Real: {DataDevolucaoReal}\n" +
                $"Valor da Diária: {ValorDiaria:C}\n" +
                $"Valor Total: {ValorTotal:C}\n" +
                $"Multa: {Multa:C}\n" +
                $"Status: {Status}\n"; 
        }

    }
}
