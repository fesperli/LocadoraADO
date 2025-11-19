using Locadora.Controller;
using Locadora.Models;


//Cliente cliente = new Cliente("Ted", "aquele@urso.com");

//Documento documento = new Documento(
//    1,
//    "RG",
//    "123456789",
//    new DateTime(2020, 1, 15),
//    new DateTime(2030, 1, 15)
//);

//Console.WriteLine(cliente);
//Console.WriteLine("---------------------------------------------");

var clienteController = new ClienteController();
//try
//{
//    clienteController.AdicionarCliente(cliente);
//}
//catch (Exception ex)
//{
//    Console.Write(ex.Message);
//}

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

clienteController.AtualizarTelefoneCliente("99999-9999", "x@urso.com");
Console.WriteLine(clienteController.BuscaClientePorEmail("x@urso.com"));


