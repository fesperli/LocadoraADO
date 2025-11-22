using Locadora.Controller.Interfaces;
using Locadora.Models;
using Locadora.Models.Enum;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Databases;

namespace Locadora.Controller
{
    public class LocacaoController : ILocacaoController
    {
        public void AdicionarLocacao(Locacao locacao)
        {
            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            {
                connection.Open();

                using var transaction = connection.BeginTransaction();
                {
                    try
                    {
                        var command = new SqlCommand(Locacao.sp_AdicionarLocacao, connection, transaction);

                        command.Parameters.AddWithValue("@ClienteId", locacao.ClienteId);
                        command.Parameters.AddWithValue("@VeiculoId", locacao.VeiculoId);
                        command.Parameters.AddWithValue("@DataDevolucaoPrevista", locacao.DataDevolucaoPrevista);
                        command.Parameters.AddWithValue("@DataDevolucaoReal",
                            locacao.DataDevolucaoReal ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ValorDiaria", locacao.ValorDiaria);
                        command.Parameters.AddWithValue("@ValorTotal", locacao.ValorTotal);
                        command.Parameters.AddWithValue("@Multa", locacao.Multa);
                        command.Parameters.AddWithValue("@Status", locacao.StatusLocacao);

                        command.ExecuteNonQuery();
                        transaction.Commit();

                        Console.WriteLine("Locacao adicionada com sucesso!");
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao adicionar cliente: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro inesperado ao adicionar cliente: " + ex.Message);
                    }
                }
            }
        }

        public void AtualizarLocacao(int idLocacao, DateTime dataDevolucaoReal, EStatusLocacao status)
        {
            var locacao = BuscarLocacaoId(idLocacao);
            var dataDevolucao = dataDevolucaoReal.Day - locacao.DataDevolucaoPrevista.Day;

            if (dataDevolucao > 0)
                locacao.SetMulta(dataDevolucao * locacao.ValorDiaria);

            locacao.SetStatus(status);

            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            {
                connection.Open();

                using var transaction = connection.BeginTransaction();
                {
                    try
                    {
                        var command = new SqlCommand(Locacao.sp_AtualizarLocacao, connection, transaction);
                        command.Parameters.AddWithValue("@idLocacao", idLocacao);
                        command.Parameters.AddWithValue("@DataDevolucaoReal", dataDevolucaoReal);
                        command.Parameters.AddWithValue("@Status", locacao.StatusLocacao);
                        command.Parameters.AddWithValue("@Multa", locacao.Multa);

                        command.ExecuteNonQuery();
                        transaction.Commit();

                        Console.WriteLine("Locacao atualizada com sucesso!");
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao atualizar locacao: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro inesperado ao atualizar a locacao: " + ex.Message);
                    }
                }
            }
        }

        public Locacao BuscarLocacaoId(int locacaoId)
        {
            Locacao locacao = null;
            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            {
                connection.Open();
                try
                {
                    var command = new SqlCommand(Locacao.sp_BuscarLocacaoId, connection);
                    command.Parameters.AddWithValue("@idLocacao", locacaoId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var dataPrevista = reader.GetDateTime(3).Day - reader.GetDateTime(2).Day;

                        locacao = new Locacao(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetDecimal(5),
                            dataPrevista
                        );

                        if (!reader.IsDBNull(4))
                            locacao.SetDataDevolucaoReal((DateTime)reader.GetValue(4));

                        locacao.SetStatus(Enum.Parse<EStatusLocacao>(reader.GetString(8)));
                        locacao.SetDataLocacao(reader.GetDateTime(2));
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao localizar locacao: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao localizar locacao: " + ex.Message);
                }
            }
            return locacao ?? throw new Exception("Nao foi possivel localizar a locacao!");
        }

        public List<Locacao> ListarLocacao()
        {
            var locacoes = new List<Locacao>();
            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(Locacao.sp_BuscarLocacao, connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var dataPrevista = reader.GetDateTime(3).Day - reader.GetDateTime(2).Day;

                        var locacao = new Locacao(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetDecimal(5),
                            dataPrevista
                        );

                        if (!reader.IsDBNull(4))
                            locacao.SetDataDevolucaoReal((DateTime)reader.GetValue(4));

                        locacao.SetDataLocacao(reader.GetDateTime(2));
                        locacao.SetStatus(Enum.Parse<EStatusLocacao>(reader.GetString(8)));
                        locacoes.Add(locacao);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar locacoes: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao listar locacoes: " + ex.Message);
                }

                return locacoes ?? throw new Exception("Nao foi possivel localizar a locacao!");
            }
        }

        public void CancelarLocacao(int locacaoId)
        {
            var locacao = this.BuscarLocacaoId(locacaoId) ?? 
                throw new Exception("Locação não foi encontrada");

            if (locacao.StatusLocacao == EStatusLocacao.Cancelada)
                throw new Exception("Locação já está cancelada");

            using var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            {
                connection.Open();

                using var transaction = connection.BeginTransaction();
                {
                    try
                    {
                        var command = new SqlCommand(Locacao.sp_CancelarLocacao, connection, transaction);
                        command.Parameters.AddWithValue("@idLocacao", locacaoId);
                        command.Parameters.AddWithValue("@Status", EStatusLocacao.Cancelada);

                        command.ExecuteNonQuery();
                        transaction.Commit();

                        Console.WriteLine("Locacao cancelada com sucesso!");
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao Cancelar Locacao: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro inesperado ao Cancelar Locacao: " + ex.Message);
                    }
                }
            }
        }
    }
}
