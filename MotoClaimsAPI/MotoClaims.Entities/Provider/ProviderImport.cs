using MotoClaims.Entities.Policy;
using MotoClaims.Entities.VehicleParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Provider
{
   public class ProviderImport
    {
        public long ProviderTypeId { get; set; }
        public string Name { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public long ServiceTypeId { get; set; }
        public long ProviderServiceId { get; set; }

        public string BranchName { get; set; }
        public long CountryId { get; set; }
        public string StreetAddress { get; set; }
        public long CityId { get; set; }
        public string Url { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public long AreaId { get; set; }

        public List<ProviderContact> ProviderContacts { get; set; }
        public List<ProviderLocation> ProviderLocations { get; set; }
        public List<ProviderServiceMapping> ProviderServiceMapping { get; set; }

        public bool HasError { get; set; }
        public List<ErrorList> ErrorList { get; set; }
    }
}
