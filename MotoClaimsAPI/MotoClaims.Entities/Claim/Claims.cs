using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.Entities.Claim
{
   public class Claims
    {
        public long ClaimId { get; set; }
        public long ClaimTypeId { get; set; }
        public string ClaimTypeName { get; set; }
        public long PolicyId { get; set; }
        public string PolicyNo { get; set; }
        public long MakeId { get; set; }
        public string MakeImage { get; set; }
        public long ModeIld { get; set; }
        public long RepairOption { get; set; }
        public DateTime IncidentDate { get; set; }
        public string AddedByType { get; set; }
        public long AddedById { get; set; }
        public string InitialComments { get; set; }
        public long ClaimStatusId { get; set; }
        public string StatusName { get; set; }
        public string CarNo { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationUrl { get; set; }
        public string InternalNote { get; set; }
        public string ExternalNote { get; set; }
        public string RejectionReason { get; set; }
        public long TenantId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string IdentityNo { get; set; }
        public List<ClaimAccidentCarPhotos> ClaimAccidentCarPhotos { get; set; }
        public List<ClaimDocuments> ClaimDocuments { get; set; }
        public long Count { get; set; }
   }

    public class MyTaskListWithCount
    {
        public IEnumerable<Claims> List { get; set; }
        public long Count { get; set; }
    }

    public class MyTaskWithListName
    {
        public MyTaskListWithCount List { get; set; }
        public string ListName { get; set; }
    }

    public class MBClaims
    {
        public long ClaimId { get; set; }
        public string ClaimName { get; set; }
        public DateTime IncidentDate { get; set; }
        public string StatusName { get; set; }
    }

    public class MBClaim
    {
        public long ClaimId { get; set; }
        public string ClaimName { get; set; }
        public DateTime IncidentDate { get; set; }
        public string StatusName { get; set; }
        public int RepairOption { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MakeName { get; set; }
        public string MakeImage { get; set; }
        public string Year { get; set; }
        public string CarNumber { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public DateTime DrivingLicenseValidity { get; set; }
        public string IdentificationNumber { get; set; }
        public string PlateNumber { get; set; }
        public string ChassisNumber { get; set; }
        public string ColourName { get; set; }
        public long Capacity { get; set; }

    }

    public class AllClaims
    {
        public long ClaimId { get; set; }
        public string PolicyNo { get; set; }
        public string CarNo { get; set; }
        public string ClaimType { get; set; }
        public DateTime IncidentDate { get; set; }
        public  DateTime SubmissionDate { get; set; }
    }

    public class AllClaimsWithCount
    {
        public IEnumerable<AllClaims> AllClaims { get; set; }
        public long Count { get; set; }
    }
}
