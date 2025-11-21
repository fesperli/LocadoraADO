using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locadora.Models;
using Microsoft.Data.SqlClient;


namespace Locadora.Controller
{ 
    public class CategoriaController
    {
        public void AdicionarCategoria(Categoria categoria, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                using SqlCommand command = new(Categoria.INSERTCATEGORIA, connection, transaction);

                command.Parameters.AddWithValue("@CategoriaID", categoria.CategoriaId);
                command.Parameters.AddWithValue("@Nome", categoria.Nome);
                command.Parameters.AddWithValue("@Descricao", categoria.Descricao);
                command.Parameters.AddWithValue("@Diaria", categoria.Diaria);

                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Erro ao adicionar categoria: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Erro inesperado ao adicionar categoria: " + e.Message);
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
