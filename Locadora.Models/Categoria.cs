using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Categoria
    {
        public static readonly string INSERTCATEGORIA =
            @"INSERT INTO tblCategorias (Nome, Descricao, Diaria) 
            VALUES (@Nome, @Descricao, @Diaria)";

        public static readonly string UPDATECATEGORIA =
            @"UPDATE tblCategorias 
            SET Nome = @Nome, 
            Descricao = @Descricao, 
            Diaria = @Diaria 
            WHERE CategoriaID = @CategoriaID";
        public int CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Diaria { get; private set; }

        public Categoria(string nome, string descricao, decimal diaria)
        {
            Nome = nome;
            Descricao = descricao;
            Diaria = diaria;
        }

        public void setCategoriaID (int categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public override string? ToString()
        {
            return $"Nome: {Nome}\nDescrição: {Descricao}\nDiária: {Diaria:C}\n";
        }
    }
}