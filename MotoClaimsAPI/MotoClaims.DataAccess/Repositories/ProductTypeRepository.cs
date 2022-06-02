using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Product;

namespace MotoClaims.DataAccess.Repositories
{
    public class ProductTypeRepository 
    {
        public ProductTypeRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;


        public IEnumerable<ProductType> GetProductTypeList(long tenentId, long userId)
        {
            return unitOfWork.Connection.Query<ProductType>("usp_GetProductTypeDropdownList", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
