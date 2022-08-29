using accessing_db.Models;
using accessing_db.Repositories;

namespace accessing_db
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductRepository _product = new();

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
                        if (_product.ReadAll().Count == 0)
                        {
                            Console.WriteLine("Não há produtos cadastrados.");
                        }
                        else
                        {
                            var productList = _product.ReadAll();

                            Console.WriteLine("\nLista de produtos");
                            foreach (var item in productList)
                            {
                                Console.WriteLine($"{item.IdProduct} - {item.Name} - {item.Description} - R$ {item.Price}");
                            }
                        }
                        break;

                    case "2":
                        Console.WriteLine("\nDigite o código do produto");
                        var id = Console.ReadLine();

                        Console.WriteLine("\nDigite o nome do produto");
                        var name = Console.ReadLine();

                        Console.WriteLine("\nDigite a descrição do produto");
                        var description = Console.ReadLine();

                        Console.WriteLine("\nDigite o preco do produto");
                        var price = Console.ReadLine();

                        Product newProduct = new()
                        {
                            IdProduct = id,
                            Name = name,
                            Description = description,
                            Price = Convert.ToDecimal(price)
                        };

                        _product.Create(newProduct);

                        break;

                    default:
                        break;
                }

            } while (option != "0");
        }
    }
}