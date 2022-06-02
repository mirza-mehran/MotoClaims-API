using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Provider
{
  public class Provider
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProviderContact> ProviderContacts { get; set; }
        public IEnumerable<ProviderLocation> ProviderLocations { get; set; }
    }

    public class ModelProvider
    {

        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
    }
    public class ModelProviderWithTotalRecord
    {
        public IEnumerable<ModelProvider> ModelProvider { get; set; }
        public long TotalRecord { get; set; }
    }
}
