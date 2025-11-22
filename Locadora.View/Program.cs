using Locadora.Controller;
using Locadora.Models;
using Locadora.Models.Enums;

namespace Locadora.View
{
    internal class Program
    {
        static void Main()
        {
            #region Cliente e Documento Testes
            //ClienteController clienteController = new();

            //CREATE - CREATE
            // Cliente cliente = new("Ben Tennyson", "ben10@supremo.com");
            //Documento documento = new("CPF", "2022", new DateOnly(2020, 1, 1), new DateOnly(2030, 1, 1));

            //try
            //{
            //    clienteController.AdicionarCliente(cliente, documento);
            //    Console.WriteLine("Cliente adicionado com sucesso.");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //CREATE - READ Many
            //try
            //{
            //    var listaDeClientes = clienteController.ListarTodosClientes();
            //    Console.WriteLine("Lista de clientes");
            //    Console.WriteLine();
            //    foreach (var c in listaDeClientes)
            //    {
            //        Console.WriteLine(c);
            //        Console.WriteLine();
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //CREATE - READ One
            //try
            //{
            //    Console.WriteLine(clienteController.BuscarClientePorEmail("ben10@supremo.com"));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //CREATE - UPDATE
            // try
            //{
            //    clienteController.AtualizarTelefoneCliente("11211-1111", "joao.silva@email.com");
            //    Console.WriteLine(clienteController.BuscarClientePorEmail("joao.silva@email.com"));
            //    Console.WriteLine("Atualização efetuada com sucesso.");
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}

            //Documento documento = new("CPF", "2022", new DateOnly(2020, 1, 1), new DateOnly(2030, 1, 1));

            //try
            //{
            //    clienteController.AtualizarDocumentoCliente("ben10@supremo.com", documento);
            //    Console.WriteLine(clienteController.BuscarClientePorEmail("ben10@supremo.com"));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //CREATE - DELETE
            //try
            //{
            //    clienteController.DeletarCliente("");
            //    Console.WriteLine("Cliente deletado com sucesso");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            #endregion


            //var categoriaController = new CategoriaController();

            //var categoria = new Categoria("Pesado", "Veículo utilitário esportivo", 2500.00m);
            //categoriaController.AdicionarCategoria(categoria);

            //categoria = new Categoria("Voador", "Carro de passeio com porta-malas separado", 2000.00m);
            //categoriaController.AdicionarCategoria(categoria);


            var veiculoController = new VeiculoController();
            try
            {
                //var veiculo = new Veiculo(1, "XYZ-9876", "Ford", "Mustang", 2022, EStatusVeiculo.Disponivel.ToString());
                //veiculoController.AdicionarVeiculo(veiculo);
                //var veiculos = veiculoController.ListarTodosVeiculos();

                //foreach (var item in veiculos)
                //{
                //    Console.WriteLine(item);
                //    Console.WriteLine();
                //}

                // veiculoController.BuscarVeiculoPlaca("XYZ-9876");
                //veiculoController.DeletarVeiculo(123);

                //segundo jeito
                //var veiculo = veiculoController.BuscarVeiculoPlaca("XYZ-9876");
                // veiculoController.DeletarVeiculo(veiculo.VeiculoID);
                Console.WriteLine(veiculoController.BuscarVeiculoPlaca("MNO7890"));
                veiculoController.AtualizarStatusVeiculo(EStatusVeiculo.Manutencao.ToString(), "MNO7890");
                Console.WriteLine(veiculoController.BuscarVeiculoPlaca("MNO7890"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);









            }
        }
    }
}