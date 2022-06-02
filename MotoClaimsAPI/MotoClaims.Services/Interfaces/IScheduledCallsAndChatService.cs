using MotoClaims.Entities.ScheduledCallsAndChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
   public interface IScheduledCallsAndChatService
   {
        string InsertScheduledCalls(ScheduledCalls obj, long tenentId, long userId);
        ConfigScheduledCallTimeSlots GetConfigScheduledCallTimeSlots(long tenentId, long userId);
        IEnumerable<ScheduledCallExceptionSlots> GetScheduledCallExceptionSlots(long tenentId, long userId);
       // string InsertScheduledCallsAssigned(ScheduledCallsAssigned obj, long tenentId, long userId);
        IEnumerable<ScheduledCallsList> GetScheduledCalls(long tenentId, long userId);
        string UpdateScheduledCalls(ScheduledCalls obj, long tenentId, long userId);
         string CancelCall(long SC_Id, long tenentId, long userId);

        IEnumerable<ScheduledCalls> GetMBScheduledCalls(long civilId, long tenantId, long userId);

   }
}
