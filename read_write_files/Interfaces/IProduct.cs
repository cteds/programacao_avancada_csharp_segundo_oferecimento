using read_write_files.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace read_write_files.Interfaces
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
