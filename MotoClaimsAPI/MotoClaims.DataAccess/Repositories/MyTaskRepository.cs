using Dapper;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
   public class MyTaskRepository
    {
        public MyTaskRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;

        public IEnumerable<Claims> GetClaimsUserId(long StatusId, long tenentId, long userId)
        {
            DynamicParameters dbParam = new DynamicParameters();
            dbParam.Add("Operation", "GetClaims");
            dbParam.Add("TenantId", tenentId);
            dbParam.Add("UserId", userId);
            dbParam.Add("StatusId", StatusId);
            return unitOfWork.Connection.Query<Claims>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParam);
        }

        public List<MyTaskWithListName> GetClaimsByUserId(long Id, long tenentId, long userId)
        {
            List<MyTaskWithListName> myTaskModelList = new List<MyTaskWithListName>();
            
            for (int i = 0; i <= 7; i++)
            {
                if (i == 0)
                {
                    /// INITIAL
                    var list = GetClaimsUserId( 6,  tenentId,  userId);
                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "INITIAL";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 1)
                {
                    /// APPROVED
                    var list = GetClaimsUserId(11, tenentId, userId);
                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "APPROVED";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 2)
                {
                    /// REJECTED
                    var list = GetClaimsUserId(19, tenentId, userId);
                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "REJECTED";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 3)
                {
                    /// APPEAL 
                    var list = GetClaimsUserId(14, tenentId, userId);

                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "APPEAL";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 4)
                {
                    /// ASSESSED BY GARAGE/AGENCY 
                    var list = GetClaimsUserId(15, tenentId, userId);

                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "ASSESSED BY GARAGE/AGENCY";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 5)
                {
                    /// REVISION DURING REPAIR
                    var list = GetClaimsUserId(16, tenentId, userId);

                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "REVISION DURING REPAIR";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 6)
                {
                    /// SCHEDULED CALLS
                    var list = GetClaimsUserId(17, tenentId, userId);

                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "SCHEDULED CALLS";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 7)
                {
                    /// REPLACEMENT CARS
                    var list = GetClaimsUserId(18, tenentId, userId);

                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "REPLACEMENT CARS";

                    myTaskModelList.Add(myTaskModelList1);
                }
            }


            return myTaskModelList;
        }

        public List<MyTaskWithListName> GetPendingTaskClaimsByUserId(long Id, long tenentId, long userId)
        {
            List<MyTaskWithListName> myTaskModelList = new List<MyTaskWithListName>();

            for (int i = 0; i <= 3; i++)
            {
                if (i == 0)
                {
                    /// SCHEDULED CALLS
                    var list = GetClaimsUserId(17, tenentId, userId);

                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "SCHEDULED CALLS";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 1)
                {
                    /// ASSIGN TO GARAGE/AGENCY
                    var list = GetClaimsUserId(25, tenentId, userId);
                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "ASSIGN TO GARAGE/AGENCY";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 2)
                {
                    /// ASSIGN TO SURVEYOUR
                    var list = GetClaimsUserId(27, tenentId, userId);
                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "ASSIGN TO SURVEYOUR";

                    myTaskModelList.Add(myTaskModelList1);
                }

                if (i == 3)
                {
                    /// ASSIGN TO REPLACEMENT CAR 
                    var list = GetClaimsUserId(26, tenentId, userId);

                    MyTaskListWithCount claimOffice = new MyTaskListWithCount();
                    claimOffice.List = list;
                    claimOffice.Count = list.Count();

                    MyTaskWithListName myTaskModelList1 = new MyTaskWithListName();
                    myTaskModelList1.List = claimOffice;
                    myTaskModelList1.ListName = "ASSIGN TO REPLACEMENT CAR";

                    myTaskModelList.Add(myTaskModelList1);
                }
            }

            return myTaskModelList;
        }

    }
}
