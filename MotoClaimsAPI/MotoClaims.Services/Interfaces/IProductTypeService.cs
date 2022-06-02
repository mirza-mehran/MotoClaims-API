using MotoClaims.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IProductTypeService
    {
        IEnumerable<ProductType> GetProductTypeList(long tenentId, long userId);
    }
}
