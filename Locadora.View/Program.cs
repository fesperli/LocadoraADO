using Locadora.Controller;
using Locadora.Models;


Cliente cliente = new Cliente("Ben10", "omni@trix.com");

Documento documento = new Documento("RG", "123456789", new DateTime(2021, 1, 1), new DateTime(2030, 1, 1));


//Console.WriteLine(cliente);
//Console.WriteLine("---------------------------------------------");

var clienteController = new ClienteController();
try
{
    clienteController.AdicionarCliente(cliente, documento);
}
catch (Exception ex)
{
    Console.Write(ex.Message);
}

//try
//{
//    var listadeClientes = clienteController.ListarTodosClientes();

//    foreach (var clientedaLista in listadeClientes)
//    {
//        Console.WriteLine(clientedaLista);
//    }
//}
//catch (Exception ex)
//{
//    Console.Write(ex.Message);
//}

//clienteController.AtualizarTelefoneCliente("99999-9999", "x@urso.com");
//Console.WriteLine(clienteController.BuscaClientePorEmail("x@urso.com"));

//try
//{
//    clienteController.DeletarCLientePorEmail("a@a.com");
//} catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}


