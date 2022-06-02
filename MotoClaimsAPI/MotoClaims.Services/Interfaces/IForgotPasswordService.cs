using MotoClaims.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IForgotPasswordService
   {
        IEnumerable<UserProfiles> GetEmails(string Email, long tenantId);
        bool UpdateUserAccount(updatePasswordUser model, long tenentId, out string errMessage);

    }
}
