using read_write_files.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace read_write_files.Models
{
    public class Product : Base, IProduct
    {
        public string IdProduct { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        private const string path = "database/products.csv";

        public Product()
        {
            CreateFolderFile(path);
        }

        private static string PrepareLineCSV(Product product)
        {
            return $"{product.IdProduct};{product.Name};{product.Description};{product.Price}";
        }

        public List<Product> ReadAll()
        {
            List<Product> products = new();

            string[] lines = File.ReadAllLines(path);

            foreach (var item in lines)
            {
                string[] line = item.Split(";");

                Product product = new()
                {
                    IdProduct = line[0],
                    Name = line[1],
                    Description = line[2],
                    Price = Convert.ToDecimal( line[3] )
                };

                products.Add(product);
            }

            return products;
        }

        public void Create(Product newProduct)
        {
            string[] line = { PrepareLineCSV(newProduct) };
            File.AppendAllLines(path, line);
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(string idProduct)
        {
            throw new NotImplementedException();
        }
    }
}
