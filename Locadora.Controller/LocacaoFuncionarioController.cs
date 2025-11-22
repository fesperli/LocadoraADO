using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Databases;

namespace Locadora.Controller
{
    public class LocacaoFuncionarioController : ILocacaoFuncionarioController
    {
        public void AdicionarLocacaoFuncionario(LocacaoFuncionario relacionamento)
        {
            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using var command = new SqlCommand(LocacaoFuncionario.INSERT, connection);
            command.Parameters.AddWithValue("@LocacaoID", relacionamento.LocacaoId);
            command.Parameters.AddWithValue("@FuncionarioID", relacionamento.FuncionarioID);

            command.ExecuteNonQuery();
        }
        public void RemoverLocacaoFuncionario(int locacaoFuncionarioId)
        {
            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using var command = new SqlCommand(LocacaoFuncionario.DELETE, connection);
            command.Parameters.AddWithValue("@LocacaoFuncionarioID", locacaoFuncionarioId);

            command.ExecuteNonQuery();
        }
        public List<LocacaoFuncionario> ListarFuncionariosDaLocacao(Guid locacaoId)
        {
            var result = new List<LocacaoFuncionario>();
            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using var command = new SqlCommand(LocacaoFuncionario.SELECT_BY_LOCACAO, connection);
            command.Parameters.AddWithValue("@LocacaoID", locacaoId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var lf = new LocacaoFuncionario(
                    (Guid)reader["LocacaoID"],
                    (int)reader["FuncionarioID"]
                );
                lf.SetLocacaoFuncionarioID((int)reader["LocacaoFuncionarioID"]);
                //inserir funcionario
                result.Add(lf);
            }
            return result;
        }
        public List<LocacaoFuncionario> ListarLocacoesDoFuncionario(int funcionarioId)
        {
            var result = new List<LocacaoFuncionario>();
            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using var command = new SqlCommand(LocacaoFuncionario.SELECT_BY_FUNCIONARIO, connection);
            command.Parameters.AddWithValue("@FuncionarioID", funcionarioId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var lf = new LocacaoFuncionario(
                    (Guid)reader["LocacaoID"],
                    funcionarioId
                );
                lf.SetLocacaoFuncionarioID((int)reader["LocacaoFuncionarioID"]);
                //inserir locacao
                result.Add(lf);
            }
            return result;
        }
    }
}
