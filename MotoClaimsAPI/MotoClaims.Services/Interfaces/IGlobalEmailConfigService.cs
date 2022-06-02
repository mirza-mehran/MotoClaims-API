using MotoClaims.Entities;
using MotoClaims.Entities.GlobelSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Services.Interfaces
{
    public interface IGlobalEmailConfigService
    {
        IEnumerable<TimeZoneConfig> GetTimeZone(long tenantId);
        SMTP_Configuration GetSMTP_Configuration(long tenantId);
        string UpdateSMTP_Configuration(SMTP_Configuration model, long tenantId, long userId);

        GlobalEmailConfiguration GetGlobelEmail_Signature(long tenantId);

        string InsertGlobelEmail_Signature(GlobalEmailConfiguration model, long tenantId, long userId, out bool bit);

        string UpdateGlobelEmail_Signature(GlobalEmailConfiguration model, long tenantId, long userId, out bool bit);

    }
}
