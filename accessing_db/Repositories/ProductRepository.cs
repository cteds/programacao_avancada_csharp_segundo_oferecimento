using accessing_db.Interfaces;
using accessing_db.Models;
using System.Data.SqlClient;

namespace accessing_db.Repositories
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
    }
}
