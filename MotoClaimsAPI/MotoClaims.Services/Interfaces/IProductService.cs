using MotoClaims.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductList> GetProducts(long tenentId, long userId);
        ProductByID GetProduct(long Id, long tenentId, long userId);
        string InsertProduct(ProductAdd model, long tenentId, long userId, out bool bit);
        string DeleteProduct(long Id, long tenentId, long userId, out bool bit);
        string UpdateProduct(ProductAdd model, long tenentId, long userId, out bool bit);

    }
}
