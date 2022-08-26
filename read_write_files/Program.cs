using read_write_files.Models;

namespace read_write_files
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product = new();

            string option;

            do
            {
                Console.WriteLine("\nEscolha uma das opções abaixo:\n");
                Console.WriteLine("1 - Listar produtos");
                Console.WriteLine("2 - Cadastrar produto");
                Console.WriteLine("3 - Editar produto");
                Console.WriteLine("4 - Excluir produto");
                Console.WriteLine("0 - Sair da aplicação\n");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        if (product.ReadAll().Count == 0)
                        {
                            Console.WriteLine("Não há produtos cadastrados.");
                        }
                        else
                        {
                            var productList = product.ReadAll();

                            Console.WriteLine("\nLista de produtos");
                            foreach (var item in productList)
                            {
                                Console.WriteLine($"{item.IdProduct} - {item.Name} - {item.Description} - {item.Price}");
                            }
                        }
                        break;

                    default:
                        break;
                }

            } while (option != "0");
        }
    }
}