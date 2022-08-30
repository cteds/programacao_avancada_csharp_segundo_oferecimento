using parallel_program.Models;
using parallel_program.Repositories;

namespace parallel_program
{
    internal class Program
    {
        private const string path = "database/log.txt";

        static void Main(string[] args)
        {
            ProductRepository _product = new();

            _product.LoadData();

            FileStream file = File.OpenWrite(path);

            LogRepository _log = new(file);

            User user = new User()
            {
                IdUser = Guid.NewGuid(),
                Name = "John Doe",
                JobTitle = "Developer"
            };

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

                            //Console.writeline("\nlista de produtos");
                            //foreach (var item in productList)
                            //{
                            //    Console.WriteLine($"{item.IdProduct} - {item.Name} - {item.Description} - r$ {item.Price} utilizando a Thread: {Thread.CurrentThread.ManagedThreadId} às {DateTimeOffset.Now}");
                            //}

                            Parallel.ForEach(productList, product =>
                            {
                                Console.WriteLine($"\nO usuário {user.Name} está exibindo o produto: \n{product.Name}\n utilizando a Thread: {Thread.CurrentThread.ManagedThreadId} às {DateTimeOffset.Now}");

                                _log.RegisterAccess(user);
                            });

                        }
                        break;

                        file.Close();

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