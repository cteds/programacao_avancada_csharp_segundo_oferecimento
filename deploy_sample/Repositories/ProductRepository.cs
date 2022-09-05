using deploy_sample.Interfaces;
using deploy_sample.Models;
using System.Data.SqlClient;

namespace deploy_sample.Repositories
{
    /// <summary>
    /// Repositório responsável pela manipulação da entidade produto
    /// </summary>
    internal class ProductRepository : IProduct
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros
        /// Data Source = Nome do servidor
        /// initial catalog = Nome do banco de dados
        /// user Id=sa; pwd=cteds2022 = Faz a autenticação com o usuário do SQL Server, passando o logon e a senha
        /// integrated security=true = Faz a autenticação com o usuário do sistema
        /// </summary>
        private readonly string stringConexao = "Server=labsoft.pcs.usp.br; Initial Catalog=db_prof; User id=prof; pwd=16nM10_okv;";
        //private readonly string stringConexao = "Server=MP\\SQLEXPRESS; Initial Catalog=Catalog; User id=sa; pwd=cteds2022;";
        //private readonly string stringConexao = "Data source=MP\\SQLEXPRESS; Initial Catalog=Catalog; integrated security=true;";

        /// <summary>
        /// Cadastra um novo produto
        /// </summary>
        /// <param name="product">Objeto com os dados que serão cadastrados</param>
        public void Create(Product product)
        {
            // Declara a conexão passando a string de conexão como parâmetro
            using (SqlConnection con = new(stringConexao))
            {
                // Declara a query que será executada
                //                       INSERT INTO Products (IdProduct, Name, Description, Price) VALUES ('','','',0)DROP TABLE Products--
                //string queryInsert = $"INSERT INTO Products (IdProduct, Name, Description, Price) VALUES ('{product.IdProduct}', '{product.Name}', '{product.Description}', {product.Price})";
                string queryInsert = "INSERT INTO Products (IdProduct, Name, Description, Price) VALUES (@IdProduct, @Name, @Description, @Price)";

                // Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                using (SqlCommand cmd = new(queryInsert, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Exclui um produto existente
        /// </summary>
        /// <param name="idProduct">Id do produto que será deletado</param>
        public void Delete(string idProduct)
        {
            // Declara a SqlConnection con passando a string de conexão como parâmetro
            using (SqlConnection con = new(stringConexao))
            {
                // Declara a query a ser executada passando o valor como parâmetro
                string queryDelete = "DELETE FROM Products WHERE IdProduct = @IdProduct";

                // Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                using (SqlCommand cmd = new(queryDelete, con))
                {
                    // Define o valor recebido no método como o valor do parâmetro
                    cmd.Parameters.AddWithValue("@IdProduct", idProduct);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lista todos os produtos
        /// </summary>
        /// <returns>Uma lista de produtos</returns>
        public List<Product> ReadAll()
        {
            // Cria uma lista onde serão armazenados os dados
            List<Product> listProducts = new();

            // Declara a SqlConnection con passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdProduct, Name, Description, Price FROM Products";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader rdr para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                using (SqlCommand cmd = new(querySelectAll, con))
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto genero do tipo Product
                        Product product = new()
                        {
                            // Atribui à propriedade o valor da primeira coluna da tabela do banco de dados
                            IdProduct = rdr[0].ToString(),

                            // Atribui à propriedade nome o valor da coluna "Name" da tabela do banco de dados
                            Name = rdr["Name"].ToString(),

                            Description = rdr["Description"].ToString(),

                            Price = Convert.ToDecimal(rdr["Price"])
                        };

                        // Adiciona o objeto criado à lista
                        listProducts.Add(product);
                    }
                }
            }

            // Retorna a lista
            return listProducts;
        }

        /// <summary>
        /// Edita um produto existente
        /// </summary>
        /// <param name="product">Objeto com os novos dados</param>
        public void Update(Product product)
        {
            // Declara a SqlConnection con passando a string de conexão como parâmetro
            using (SqlConnection con = new(stringConexao))
            {
                // Declara a query a ser executada
                string queryUpdateBody = "UPDATE Products SET IdProduct = @IdProduct, Name = @Name, Description = @Description, Price = @Price WHERE IdProduct = @IdProduct";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new(queryUpdateBody, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
