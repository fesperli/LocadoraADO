using Locadora.Models;
using Locadora.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Controller.Interfaces
{
    public interface ILocacaoController
    {
        public void AdicionarLocacao(Locacao locacao);

        public void AtualizarLocacao(int idLocacao, DateTime dataDevolucaoReal, EStatusLocacao status);

        public Locacao BuscarLocacaoId(int locacaoId);

        public List<Locacao> ListarLocacao();

        public void CancelarLocacao(int locacaoId);
    }
}
