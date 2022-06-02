using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.GlobelSetting
{
   public class GlobalEmailConfiguration
    {
        public long GlobalEmail_ID { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string Email_Signature { get; set; }
    }
}
