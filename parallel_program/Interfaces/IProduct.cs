using parallel_program.Models;

namespace parallel_program.Interfaces
{
    public interface IProduct
    {
        //Create Read Update Delete - CRUD

        List<Product> ReadAll();

        void Create(Product newProduct);

        void Update(Product product);

        void Delete(string idProduct);
    }
}
