using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Product;
using MotoClaims.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Services
{
    public class ProductService : IProductService
    {
        public string DeleteProduct(long Id, long tenentId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProductRepository myRepository = new ProductRepository(unitOfWork);
                    GeneralSettings.ActivityLog(Id, "Delete Product", "Products", "Id", Id, userId, GeneralSettings.CRUD.DELETE.ToString(), unitOfWork, tenentId);

                    ErrorMessage = myRepository.DeleteProduct(Id,  tenentId,  userId, out bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Delete Product", GeneralSettings.ToXML(Id), GeneralSettings.CRUD.DELETE.ToString(), userId, tenentId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }
        public ProductByID GetProduct(long Id, long tenentId, long userId)
        {
            ProductByID UserProfiles = new ProductByID();
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                //unitOfWork.Begin();
                try
                {
                    ProductRepository myRepository = new ProductRepository(unitOfWork);
                    UserProfiles = myRepository.GetProduct(Id, tenentId, userId);
                    //  unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return UserProfiles;
        }
        public IEnumerable<ProductList> GetProducts(long tenentId, long userId)
        {
            IEnumerable<ProductList> obj = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProductRepository myRepository = new ProductRepository(unitOfWork);
                    obj = myRepository.GetProducts( tenentId, userId);
                    // unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    //unitOfWork.Rollback();
                    //GeneralSettings.LogError(ex, "ReUsers_GetAll", "", GeneralSettings.CRUD.VIEW.ToString(), HttpContext.Current.Session["LogInUser"].ToString());
                    //throw;
                }
            }
            return obj;
        }
        public string InsertProduct(ProductAdd model, long tenentId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProductRepository myRepository = new ProductRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Add Product", "Products", "Id", model.Id, userId, GeneralSettings.CRUD.INSERT.ToString(), unitOfWork, tenentId);
                    ErrorMessage = myRepository.InsertProduct(model, tenentId, userId,out bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Add Product", GeneralSettings.ToXML(model), GeneralSettings.CRUD.INSERT.ToString(), userId, tenentId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }
        public string UpdateProduct(ProductAdd model, long tenentId, long userId, out bool bit)
        {
            string ErrorMessage = null;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    ProductRepository myRepository = new ProductRepository(unitOfWork);
                    GeneralSettings.ActivityLog(model, "Edit Product", "Products", "Id", model.Id, userId, GeneralSettings.CRUD.UPDATE.ToString(), unitOfWork, tenentId);

                    ErrorMessage = myRepository.UpdateProduct(model, tenentId, userId, out bit);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    GeneralSettings.LogError(ex, "Edit Product", GeneralSettings.ToXML(model), GeneralSettings.CRUD.UPDATE.ToString(), userId, tenentId);
                    bit = false;
                }
            }
            return ErrorMessage;
        }
    }
}
