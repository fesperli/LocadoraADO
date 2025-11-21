using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;


namespace Locadora.Controller
{
    public class CategoriaController
    {
        public void AdicionarCategoria(Categoria categoria)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {
                using SqlCommand command = new(Categoria.INSERTCATEGORIA, connection);

                command.Parameters.AddWithValue("@Nome", categoria.Nome);
                command.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Diaria", categoria.Diaria);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao adicionar categoria: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao adicionar categoria: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public string BuscarNomeCategoriaPorId(int id)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            try
            {
                var command = new SqlCommand(Categoria.SELECTNOMECATEGORIAPORID, connection);

                command.Parameters.AddWithValue("@Id", id);

                string nomecategoria = string.Empty;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    nomecategoria = reader["Nome"].ToString() ?? string.Empty;

                }
                    return nomecategoria;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar categoria " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar categoria " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Categoria> ListarTodasCategorias()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {
                var command = new SqlCommand(Categoria.SELECTNOMECATEGORIAPORID, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Categoria> listaCategoria = new List<Categoria>();

                while (reader.Read())
                {
                    var categoria = new Categoria(
                                                    reader["Nome"].ToString(),
                                                    reader["Descricao"] != DBNull.Value ?
                                                    reader["Descricao"].ToString() : null,
                                                    Convert.ToDecimal(reader["Diaria"])
                                                 );
                    categoria.setCategoriaID(Convert.ToInt32(reader["CategoriaID"]));

                    listaCategoria.Add(categoria);
                }
                return listaCategoria;
            }
            catch (SqlException ex)
            {
                throw new Exception("Não foi possível listar todas categorias " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao listar todas categorias " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void AtualizarCategoria(Categoria categoria, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                using SqlCommand command = new(Categoria.UPDATECATEGORIA, connection, transaction);

                command.Parameters.AddWithValue("@CategoriaID", categoria.CategoriaId);
                command.Parameters.AddWithValue("@Nome", categoria.Nome);
                command.Parameters.AddWithValue("@Descricao", categoria.Descricao);
                command.Parameters.AddWithValue("@Diaria", categoria.Diaria);

                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Erro ao alterar categoria: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Erro inesperado ao alterar categoria: " + e.Message);
            }
        }
    }
}
