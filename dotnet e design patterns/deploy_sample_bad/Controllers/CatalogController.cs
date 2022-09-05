using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace deploy_sample.Controllers
{
    public class Product
    {
        public string IdProduct { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }

    public class User
    {
        public string IdUser { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly string stringConexao = "Server=MP\\SQLEXPRESS; Initial Catalog=Catalog; User id=sa; pwd=cteds2022;";
        //private readonly string stringConexao = "Data source=MP\\SQLEXPRESS; Initial Catalog=Catalog; integrated security=true;";


        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            try
            {
                List<Product> listProducts = new();

                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string querySelectAll = "SELECT IdProduct, Name, Description, Price FROM Products";

                    con.Open();

                    SqlDataReader rdr;

                    using (SqlCommand cmd = new(querySelectAll, con))
                    {
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            Product product = new()
                            {
                                IdProduct = rdr[0].ToString(),

                                Name = rdr["Name"].ToString(),

                                Description = rdr["Description"].ToString(),

                                Price = Convert.ToDecimal(rdr["Price"])
                            };

                            listProducts.Add(product);
                        }
                    }
                }

                return Ok(listProducts);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost("products")]
        public IActionResult PostProduct(Product newProduct)
        {
            try
            {
                using (SqlConnection con = new(stringConexao))
                {
                    string queryInsert = "INSERT INTO Products (IdProduct, Name, Description, Price) VALUES (@IdProduct, @Name, @Description, @Price)";

                    using (SqlCommand cmd = new(queryInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@IdProduct", newProduct.IdProduct);
                        cmd.Parameters.AddWithValue("@Name", newProduct.Name);
                        cmd.Parameters.AddWithValue("@Description", newProduct.Description);
                        cmd.Parameters.AddWithValue("@Price", newProduct.Price);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            try
            {
                List<User> listUsers = new();

                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string querySelectAll = "SELECT IdUser, Name, Email FROM Users";

                    con.Open();

                    SqlDataReader rdr;

                    using (SqlCommand cmd = new(querySelectAll, con))
                    {
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            User user = new()
                            {
                                IdUser = rdr[0].ToString(),

                                Name = rdr["Name"].ToString(),

                                Email = rdr["Email"].ToString()
                            };

                            listUsers.Add(user);
                        }
                    }
                }

                return Ok(listUsers);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost("users")]
        public IActionResult PostUser(User newUser)
        {
            try
            {
                using (SqlConnection con = new(stringConexao))
                {
                    string queryInsert = "INSERT INTO Users (IdUser, [Name], Email, [Password]) VALUES (@IdUser, @Name, @Email, @Password)";

                    using (SqlCommand cmd = new(queryInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@IdUser", newUser.IdUser);
                        cmd.Parameters.AddWithValue("@Name", newUser.Name);
                        cmd.Parameters.AddWithValue("@Email", newUser.Email);
                        cmd.Parameters.AddWithValue("@Password", newUser.Password);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                return StatusCode(201);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
