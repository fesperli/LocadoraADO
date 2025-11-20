using Utils.Databases;
using Microsoft.Data.SqlClient;
using Locadora.Models;
using System.Transactions;


namespace Locadora.Controller
{
    public class ClienteController
    {
        public void AdicionarCliente(Cliente cliente, Documento documento)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Cliente.INSERTCLIENTE, connection, transaction);

                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);

                    int clienteId = Convert.ToInt32(command.ExecuteScalar());

                    cliente.setClienteID(clienteId);

                    var DocumentoController = new DocumentoController();


                    documento.setClienteID(clienteId);

                    DocumentoController.AdicionarDocumento(documento, connection, transaction);

                    transaction.Commit();
                    }
                catch (SqlException ex)
                {
                    throw new Exception("Erro de SQL ao adicionar cliente " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao adicionar cliente " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Cliente> ListarTodosClientes()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {

                SqlCommand command = new SqlCommand(Cliente.SELECTALLCLIENTES, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Cliente> clientes = new List<Cliente>();

                while (reader.Read())
                {
                    var cliente = new Cliente(
                        reader["Nome"].ToString(),
                        reader["Email"].ToString(),
                        reader["Telefone"] != DBNull.Value ?
                        reader["Telefone"].ToString() : null
                    );

                    cliente.setClienteID(Convert.ToInt32(reader["ClienteID"]));

                    clientes.Add(cliente);
                }
                return clientes;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro de SQL ao listar clientes: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar clientes: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }



        }

        public Cliente BuscaClientePorEmail(string email)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(Cliente.SELECTCLIENTEPOREMAIL, connection);

                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var cliente = new Cliente(
                        reader["Nome"].ToString(),
                        reader["Email"].ToString(),
                        reader["Telefone"] != DBNull.Value ?
                        reader["Telefone"].ToString() : null
                    );

                    cliente.setClienteID(Convert.ToInt32(reader["ClienteID"]));
                    return cliente;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro de SQL ao buscar cliente por email: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar cliente por email: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void AtualizarTelefoneCliente(string telefone, string email)
        {
            // buscar o cliente
            // atualizar a propriedade telefone
            // salvar no banco

            var clienteEncontrado = BuscaClientePorEmail(email);

            if (clienteEncontrado is null)
                throw new Exception("Não existe cliente com esse email cadastrado!");

            clienteEncontrado.setTelefone(telefone);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(Cliente.UPDATEFONECLIENTE, connection);
                command.Parameters.AddWithValue("@Telefone", clienteEncontrado.Telefone);
                command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteID);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro de SQL ao atualizar telefone do cliente: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar telefone do cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeletarCLientePorEmail(string email) {

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

                var clienteEncontrado = BuscaClientePorEmail(email);
                connection.Open();

                if (clienteEncontrado is null) {
                    throw new Exception("Nao existe cliente com esse email cadastrado");
                    return;
                }
            try
            {
                SqlCommand command = new SqlCommand(Cliente.DELETECLIENTE, connection);
                command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteID);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro no BD ao deletar no cliente" + ex.Message);
            }
            catch (Exception ex) {
                throw new Exception("Erro ao deletar cliente" + ex.Message);
                    }
            finally {
                connection.Close();
            }
        

    } 
        }
}
//colocar os usings corretos e fazer o delete por id depois