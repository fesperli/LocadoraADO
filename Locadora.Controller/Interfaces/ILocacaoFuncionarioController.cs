using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Controller.Interfaces
{
    public interface ILocacaoFuncionarioController
    {
        void AdicionarLocacaoFuncionario(LocacaoFuncionario relacionamento);
        void RemoverLocacaoFuncionario(int locacaoFuncionarioId);
        List<LocacaoFuncionario> ListarFuncionariosDaLocacao(Guid locacaoId);
        List<LocacaoFuncionario> ListarLocacoesDoFuncionario(int funcionarioId);
    }
}
