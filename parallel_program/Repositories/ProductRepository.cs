using Bogus;
using parallel_program.Interfaces;
using parallel_program.Models;
using System.Data.SqlClient;

namespace parallel_program.Repositories
{
    internal class ProductRepository : IProduct
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string stringConexao = "Server=MP\\SQLEXPRESS; Initial Catalog=Catalog; User id=sa; pwd=cteds2022;";
        //private readonly string stringConexao = "Data source=MP\\SQLEXPRESS; Initial Catalog=Catalog; integrated security=true;";

        public void Create(Product newProduct)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // SQL Injection
                //string queryInsert = $"INSERT INTO Products (IdProduct, Name, Description, Price) VALUES ('{newProduct.IdProduct}', '{newProduct.Name}', '{newProduct.Description}', {newProduct.Price})";
                
                string queryInsert = "INSERT INTO Products (IdProduct, Name, Description, Price) VALUES (@IdProduct, @Name, @Description, @Price)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@IdProduct", newProduct.IdProduct);
                    cmd.Parameters.AddWithValue("@Name", newProduct.Name);
                    cmd.Parameters.AddWithValue("@Description", newProduct.Description);
                    cmd.Parameters.AddWithValue("@Price", newProduct.Price);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string idProduct)
        {
            throw new NotImplementedException();
        }

        public List<Product> ReadAll()
        {
            List<Product> listProducts = new();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT * FROM Products";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Product product = new()
                        {
                            IdProduct =         rdr["IdProduct"].ToString(),
                            Name =              rdr[1].ToString(),
                            Description =       rdr[2].ToString(),
                            Price =             Convert.ToDecimal(rdr[3])
                        };

                        listProducts.Add(product);
                    }
                }
            }

            return listProducts;
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public void LoadData()
        {
            Faker<Product> ProductFaker() => new Faker<Product>()
                .RuleFor(d => d.IdProduct, f => f.Random.Guid().ToString())
                .RuleFor(d => d.Name, f => f.Commerce.ProductName())
                .RuleFor(d => d.Description, f => f.Commerce.ProductDescription())
                .RuleFor(d => d.Price, f => Convert.ToDecimal(f.Commerce.Price()));

            List<Product> products = ProductFaker().Generate(15000);

            using (SqlConnection con = new(stringConexao))
            {
                string queryInsert = "INSERT INTO Products (IdProduct, Name, Description, Price) VALUES (@IdProduct, @Name, @Description, @Price)";

                con.Open();

                foreach (var item in products)
                {
                    using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@IdProduct", item.IdProduct);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Description", item.Description);
                        cmd.Parameters.AddWithValue("@Price", item.Price);


                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
