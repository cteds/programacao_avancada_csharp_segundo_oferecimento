using accessing_db.Models;

namespace accessing_db.Interfaces
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
