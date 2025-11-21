using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Veiculo
    {
        public static readonly string SELECTALLVEICULOS =
       @"SELECT v.*, c.*
        FROM tblVeiculos v
        LEFT JOIN tblCategorias c 
        ON v.CategoriaID = c.CategoriaID";
        public int VeiculoID { get; private set; }
        public int CategoriaID { get; private set; }
        public string? Placa { get; private set; }
        public string? Marca { get; private set; }
        public string? Modelo { get; private set; }
        public int Ano { get; private set; }
        public string? StatusVeiculo { get; private set; }

        public Veiculo(int categoriaID, string placa,
                        string marca, string modelo,
                        int ano, string statusVeiculo)
        {
            CategoriaID = categoriaID;
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            StatusVeiculo = statusVeiculo;
        }

        public void setVeiculoID(int veiculoID)
        {
            VeiculoID = veiculoID;
        }

        public void setStatusVeiculo(string statusVeiculo)
        {
            StatusVeiculo = statusVeiculo;
        }
        public override string? ToString()
        {
            return $"Placa: {Placa}\nMarca: {Marca}\nModelo: {Modelo}\n" +
                $"Ano: {Ano}\nStatus: {StatusVeiculo}\n";
        }
    }
}