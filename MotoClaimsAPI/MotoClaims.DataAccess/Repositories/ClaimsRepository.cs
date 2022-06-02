using Dapper;
using MotoClaims.DataAccess.Generics;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities;
using MotoClaims.Entities.AuthorityMatrix;
using MotoClaims.Entities.Benefits;
using MotoClaims.Entities.Claim;
using MotoClaims.Entities.JsonObject;
using MotoClaims.Entities.Policy;
using MotoClaims.Entities.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MotoClaims.DataAccess.Repositories
{
  public  class ClaimsRepository
    {
        public ClaimsRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork = null;
        DBGenerics dBGenerics = new DBGenerics();
        long Claim_Id;
        public long InsertClaims(Claims obj, long tenantId, long userId, out bool bit)
        {
            bit = false;
            if (obj != null)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("ClaimTypeId", obj.ClaimTypeId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("MakeId", obj.MakeId);
                dbParams.Add("ModeIld", obj.ModeIld);
                dbParams.Add("RepairOption", obj.RepairOption);
                dbParams.Add("IncidentDate", obj.IncidentDate);
                dbParams.Add("AddedByType", obj.AddedByType);
                dbParams.Add("AddedById", obj.AddedById);
                dbParams.Add("InitialComments", obj.InitialComments);
                dbParams.Add("ClaimStatusId", 13);
                dbParams.Add("CarNo", obj.CarNo);
                dbParams.Add("SubmissionDate", obj.SubmissionDate);
                dbParams.Add("Region", obj.Region);
                dbParams.Add("Area", obj.Area);
                dbParams.Add("Location", obj.Location);
                dbParams.Add("Latitude", obj.Latitude);
                dbParams.Add("Longitude", obj.Longitude);
                dbParams.Add("LocationUrl", obj.LocationUrl);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                dbParams.Add("tempId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                var PolicyDetail = GetPolicyByID(obj.PolicyId,tenantId, userId);
                var AuthoritysMatrixs  = GetAuthoritysMatrixs(tenantId, userId);

                foreach (var item in AuthoritysMatrixs)
                {
                    AuthorityMatrixs AM = new AuthorityMatrixs();
                    AM.AM_Assign_ID = item.AM_Assign_ID;
                    AM.AM_Assign_Name = item.AM_Assign_Name;
                    AM.AM_Assign_YearFrom = item.AM_Assign_YearFrom;
                    AM.AM_Assign_YearTo = item.AM_Assign_YearTo;
                    
                    string AMMakes = item.AM_Assign_MakeID;
                    var ArrayMakes = AMMakes.Replace("{","[{").Replace("}","}]");
                    
                    List<JsonObjectModels> Makes = JsonConvert.DeserializeObject<List<JsonObjectModels>>(ArrayMakes);
                    AM.Makes = Makes;
                    
                    string AMModel = item.AM_Assign_ModelID;
                    List<JsonObjectModels> Models = JsonConvert.DeserializeObject<List<JsonObjectModels>>(AMModel);
                    AM.Models = Models;
               
                    bool verifyMake = AM.Makes.Where(x => x.value == PolicyDetail.MakeId || x.value == 0).Any();
                    bool verifyModel = AM.Models.Where(x => x.value == PolicyDetail.ModelId || x.value == 0).Any();
                    
                    if (verifyMake == true && verifyModel == true && PolicyDetail.IsAgencyRepair == item.AM_Assign_RepairOption &&  Convert.ToInt64(PolicyDetail.Year) >= AM.AM_Assign_YearFrom && Convert.ToInt64(PolicyDetail.Year) <= AM.AM_Assign_YearTo )
                    {
                        dbParams.Add("AssignTo_Id", item.AM_Assign_ToUser);
                        var affectedrows = unitOfWork.Connection.Execute("usp_ModifyClaims", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                        Claim_Id = dbParams.Get<long>("tempId");

                        for (int i = 0; i < 2; i++)
                        {
                            DynamicParameters dbParam = new DynamicParameters();
                            dbParam.Add("Operation", "Insert");
                            dbParam.Add("TenantId", tenantId);
                            dbParam.Add("Claim_Id", Claim_Id);
                            if (i == 0)
                            {
                                dbParam.Add("Status_Id", 13);
                            }
                            else
                            {
                                dbParam.Add("Status_Id", 6);
                            }
                            dbParams.Add("CreatedBy", userId);
                            dbParams.Add("UpdatedBy", userId);
                            dbParams.Add("UpdatedDate", DateTime.Now);
                            dbParams.Add("IsDeleted", false);
                            dbParams.Add("IsActive", true);
                            unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                        }

                        if (affectedrows != 0)
                        {
                            bit = true;
                        }
                        else
                        {
                            Claim_Id = 0;
                        }

                        break;
                    }

                    else if (verifyMake == true && verifyModel == true && PolicyDetail.IsAgencyRepair == item.AM_Assign_RepairOption &&  AM.AM_Assign_YearFrom == 0 && AM.AM_Assign_YearTo == 0)
                    {
                        dbParams.Add("AssignTo_Id", item.AM_Assign_ToUser);
                        var affectedrows = unitOfWork.Connection.Execute("usp_ModifyClaims", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                        Claim_Id = dbParams.Get<long>("tempId");

                        for (int i = 0; i < 2; i++)
                        {
                            DynamicParameters dbParam = new DynamicParameters();
                            dbParam.Add("Operation", "Insert");
                            dbParam.Add("TenantId", tenantId);
                            dbParam.Add("Claim_Id", Claim_Id);
                            if (i == 0)
                            {
                                dbParam.Add("Status_Id", 13);
                            }
                            else
                            {
                                dbParam.Add("Status_Id", 6);
                            }
                            dbParams.Add("CreatedBy", userId);
                            dbParams.Add("UpdatedBy", userId);
                            dbParams.Add("UpdatedDate", DateTime.Now);
                            dbParams.Add("IsDeleted", false);
                            dbParams.Add("IsActive", true);
                            unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                        }

                        if (affectedrows != 0)
                        {
                            bit = true;
                        }
                        else
                        {
                            Claim_Id = 0;
                        }

                        break;
                    }
                }
            }
            return Claim_Id;
        }
        public string InsertClaimsImage(ClaimAccidentCarPhotos obj, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (obj.ClaimId > 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("MakeId", obj.MakeId);
                dbParams.Add("ModeIld", obj.ModelId);
                dbParams.Add("AccidentCarPhotoId", obj.AccidentCarPhotoId);
                dbParams.Add("Path", obj.Path);
                dbParams.Add("ClaimAttachmentId", obj.ClaimAttachmentId);
                dbParams.Add("ClaimPhotoTypeId", obj.ClaimPhotoTypeId);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                var affectedrows2 = unitOfWork.Connection.Execute("usp_ModifyClaimsPhotos", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows2 != 0)
                {
                    ErrorMessage = "Image Successfully Save";
                }
                else
                {
                    ErrorMessage = "Image not Successfully Save";
                }
            }
            else
            {
                return ErrorMessage = "Image not Successfully Save";
            }
            return ErrorMessage;
        }
        public string InsertClaimsDocument(ClaimDocuments obj, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (obj.ClaimId > 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("MakeId", obj.MakeId);
                dbParams.Add("ModelId", obj.ModelId);
                dbParams.Add("DocumentTypeId", obj.DocumentTypeId);
                dbParams.Add("Path", obj.Path);
                dbParams.Add("ClaimAttachmentId", obj.ClaimAttachmentId);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                var affectedrows2 = unitOfWork.Connection.Execute("usp_ModifyClaimsDocuments", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows2 != 0)
                {
                    ErrorMessage = "Attachment Successfully Save";
                }
                else
                {
                    ErrorMessage = "Attachment not Successfully Save";
                }
            }
            else
            {
                return ErrorMessage = "Attachment not Successfully Save";
            }
            return ErrorMessage;
        }

        
        public long UpdateClaims(Claims obj, long tenantId, long userId, out bool bit)
        {
            bit = false;

            if (obj != null)
            {
                DynamicParameters Params = new DynamicParameters();
                Params.Add("Operation", "Delete");
                Params.Add("TenantId", tenantId);
                Params.Add("ClaimId", obj.ClaimId);
                unitOfWork.Connection.Query<Claims>("usp_ModifyClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: Params);

                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);

                dbParams.Add("ClaimTypeId", obj.ClaimTypeId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("MakeId", obj.MakeId);
                dbParams.Add("ModeIld", obj.ModeIld);
                dbParams.Add("RepairOption", obj.RepairOption);
                dbParams.Add("IncidentDate", obj.IncidentDate);
                dbParams.Add("AddedByType", obj.AddedByType);
                dbParams.Add("AddedById", obj.AddedById);
                dbParams.Add("InitialComments", obj.InitialComments);
                dbParams.Add("ClaimStatusId", 6);
                dbParams.Add("CarNo", obj.CarNo);
                dbParams.Add("SubmissionDate", obj.SubmissionDate);
                dbParams.Add("Region", obj.Region);
                dbParams.Add("Area", obj.Area);
                dbParams.Add("Location", obj.Location);
                dbParams.Add("Latitude", obj.Latitude);
                dbParams.Add("Longitude", obj.Longitude);
                dbParams.Add("LocationUrl", obj.LocationUrl);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                dbParams.Add("tempId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyClaims", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                Claim_Id = dbParams.Get<long>("tempId");
                if (affectedrows != 0)
                {
                    bit = true;
                }
                else
                {
                    Claim_Id = 0;
                }
            }
            return Claim_Id;
        }
        public string UpdateClaimsImage(ClaimAccidentCarPhotos obj, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (obj.ClaimId > 0)
            {
                DynamicParameters Params = new DynamicParameters();
                Params.Add("Operation", "Delete");
                Params.Add("TenantId", tenantId);
                Params.Add("ClaimId", obj.ClaimId);
                unitOfWork.Connection.Query<Claims>("usp_ModifyClaimsPhotos", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: Params);


                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("MakeId", obj.MakeId);
                dbParams.Add("ModeIld", obj.ModelId);
                dbParams.Add("AccidentCarPhotoId", obj.AccidentCarPhotoId);
                dbParams.Add("Path", obj.Path);
                dbParams.Add("ClaimAttachmentId", obj.ClaimAttachmentId);
                dbParams.Add("ClaimPhotoTypeId", obj.ClaimPhotoTypeId);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                var affectedrows2 = unitOfWork.Connection.Execute("usp_ModifyClaimsPhotos", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows2 != 0)
                {
                    ErrorMessage = "Image Successfully Update";
                }
                else
                {
                    ErrorMessage = "Image not Successfully Update";
                }
            }
            else
            {
                return ErrorMessage = "Image not Successfully Update";
            }
            return ErrorMessage;
        }
        public string UpdateClaimsDocument(ClaimDocuments obj, long tenantId, long userId)
        {
            string ErrorMessage = null;

            if (obj.ClaimId > 0)
            {
                //DynamicParameters Params = new DynamicParameters();
                //Params.Add("Operation", "Delete");
                //Params.Add("TenantId", tenantId);
                //Params.Add("ClaimId", obj.ClaimId);
                //unitOfWork.Connection.Query<Claims>("usp_ModifyClaimsDocuments", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: Params);


                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Update");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("CD_Id", obj.CD_Id);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("PolicyId", obj.PolicyId);
                dbParams.Add("MakeId", obj.MakeId);
                dbParams.Add("ModelId", obj.ModelId);
                dbParams.Add("DocumentTypeId", obj.DocumentTypeId);
                dbParams.Add("Path", obj.Path);
                dbParams.Add("ClaimAttachmentId", obj.ClaimAttachmentId);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                var affectedrows2 = unitOfWork.Connection.Execute("usp_ModifyClaimsDocuments", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                if (affectedrows2 != 0)
                {
                    ErrorMessage = "Attachment Successfully Update";
                }
                else
                {
                    ErrorMessage = "Attachment not Successfully Update";
                }
            }
            else
            {
                return ErrorMessage = "Attachment not Successfully Update";
            }
            return ErrorMessage;
        }


        public IEnumerable<Claims> GetClaimsByPolicyId(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetClaimsByPolicyId");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("PolicyId", Id);
            var result = unitOfWork.Connection.Query<Claims>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        //public IEnumerable<Claims> GetClaimsById(long Id, long userId, long tenantId)
        //{
        //    DynamicParameters dbParams = new DynamicParameters();
        //    dbParams.Add("Operation", "GetClaimsById");
        //    dbParams.Add("TenantId", tenantId);
        //    dbParams.Add("ClaimId", Id);

        //    var orderDictionary = new Dictionary<long, Claims>();
        //    var list = unitOfWork.Connection.Query<Claims, ClaimAccidentCarPhotos, ClaimDocuments, Claims >(
        //    "usp_GetClaims",
        //    (claims, claimAccidentCarPhotos, claimDocuments) =>
        //    {
        //        Claims claimsEntry;

        //        if (!orderDictionary.TryGetValue(claims.ClaimId, out claimsEntry))
        //        {
        //            claimsEntry = claims;
        //            claimsEntry.ClaimAccidentCarPhotos = new List<ClaimAccidentCarPhotos>();
        //            claimsEntry.ClaimDocuments = new List<ClaimDocuments>();

        //            orderDictionary.Add(claimsEntry.ClaimId, claimsEntry);
        //        }

        //        claimsEntry.ClaimAccidentCarPhotos.Add(claimAccidentCarPhotos);
        //        claimsEntry.ClaimDocuments.Add(claimDocuments);

        //        return claimsEntry;
        //    }, dbParams, commandType: System.Data.CommandType.StoredProcedure,
        //    splitOn: "ClaimId")
        //    .Distinct()
        //    .ToList();

        //    return list;
        //}

        public Claims GetClaimsById(long id, long tenantId)
        {
            Claims claims = new Claims();
            var dbParams = new DynamicParameters();
            dbParams.Add("@ClaimId", id);
            dbParams.Add("@TenantId", tenantId);

            for (int i = 0; i <= 2; i++)
            {
                if (i == 0)
                {
                    dbParams.Add("@Operation", "GetClaim");
                    var result = unitOfWork.Connection.Query<Claims>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    claims = result.First();
                }
                else if (i == 1)
                {
                    dbParams.Add("@Operation", "GetClaimAccidentCarPhotos");
                    var result = unitOfWork.Connection.Query<ClaimAccidentCarPhotos>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    claims.ClaimAccidentCarPhotos = result.ToList();
                }
                else if (i == 2)
                {
                    dbParams.Add("@Operation", "GetClaimDocuments");
                    var result = unitOfWork.Connection.Query<ClaimDocuments>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
                    claims.ClaimDocuments = result.ToList();
                }
            }

            return claims;
        }

        public IEnumerable<ClaimType> GetClaimsType( long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetClaimsType");
            dbParams.Add("TenantId", tenentId);
            var result = unitOfWork.Connection.Query<ClaimType>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public ClaimActions GetClaimAction(long ClaimId, long RoleId, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetAdminGroup");
            dbParams.Add("RoleId", RoleId);
            dbParams.Add("ClaimId", ClaimId);
            return unitOfWork.Connection.QueryFirstOrDefault<ClaimActions>("usp_GetClaimActions", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public AllClaimsWithCount GetAllClaim(long PageIndex, long PageSize, string SearchText, string SearchOption, string SortType, string SortName,long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("userId", userId);
            dbParams.Add("PageIndex", PageIndex);
            dbParams.Add("PageSize", PageSize);
            dbParams.Add("SearchText", SearchText);
            dbParams.Add("SearchOption", SearchOption);
            dbParams.Add("SortType", SortType);
            dbParams.Add("SortName", SortName);
            dbParams.Add("TotalRecord", dbType:DbType.Int64,direction:ParameterDirection.InputOutput);

            var list = unitOfWork.Connection.Query<AllClaims>("usp_GetAllClaimsByPageIndex", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            long count = dbParams.Get<long>("TotalRecord");

            AllClaimsWithCount allClaimsWithCount = new AllClaimsWithCount();
            allClaimsWithCount.AllClaims = list;
            allClaimsWithCount.Count = count;

            return allClaimsWithCount;
        }

        public IEnumerable<AuthorityMatrixs> GetAuthoritysMatrixs(long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetAuthoritysMatrixs");
            dbParams.Add("TenantId", tenentId);
            return unitOfWork.Connection.Query<AuthorityMatrixs>("usp_GetAuthorityMatrix", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public Policy GetPolicyByID(long id, long tenentId, long userId)
        {
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            var sql = "usp_GetPolicies";
            var param = new DynamicParameters();
            param.Add("Id", id);
            param.Add("TenantId", tenentId);
            param.Add("Operation", "GetPolicyById");
            param.Add("tempPolicyId", null);

            var benefitDictionary = new Dictionary<int, Policy>();
            var list = unitOfWork.Connection.Query<Policy, Benefit, Policy>(
                sql,
                (productByID, benefits) =>
                {
                    Policy productEntry;
                    if (!benefitDictionary.TryGetValue(productByID.Id, out productEntry))
                    {
                        productEntry = productByID;
                        productEntry.Benefits = new List<Benefit>();
                        benefitDictionary.Add(productEntry.Id, productEntry);
                    }
                    productEntry.Benefits.Add(benefits);
                    return productEntry;
                }, param, transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure,
                splitOn: "Id")
            .ToList();

            var result = list.Find(p => p.Id == id);
            return result;
        }

        public IEnumerable<UserProfile> GetAllUserProfiles(long id, long roleId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Id", id);
            dbParams.Add("RoleId", roleId);
            dbParams.Add("TenantId", tenantId);
            return unitOfWork.Connection.Query<UserProfile>("usp_GetUserProfiles", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<Policy> GetPolicies(string civilId,long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetPolicies");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("IdentityNo", civilId);
            var result = unitOfWork.Connection.Query<Policy>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
            return result;
        }

        public IEnumerable<ClaimRejectionReason> GetClaimRejectionReason(long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetClaimRejectionReason");
            dbParams.Add("TenantId", tenentId);
            return unitOfWork.Connection.Query<ClaimRejectionReason>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<Status_Master> GetClaimStatus_Master(long Id, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetClaimStatus_Master");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("ClaimId", Id);
            return unitOfWork.Connection.Query<Status_Master>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<Status_Master> GetClaimAllStatus_Master(long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetClaimAllStatus_Master");
            dbParams.Add("TenantId", tenentId);
            return unitOfWork.Connection.Query<Status_Master>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public string UpdateStatusRejectApprove(Claims obj, long tenentId, long userId)
        {
            string message = null;
            if (obj.ClaimId != 0)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "UpdateStatusRejectApprove");
                dbParams.Add("TenantId", tenentId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("ClaimStatusId", obj.ClaimStatusId);
                dbParams.Add("InternalNote", obj.InternalNote);
                dbParams.Add("ExternalNote", obj.ExternalNote);
                dbParams.Add("RejectionReason", obj.RejectionReason);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", 0);
                dbParams.Add("IsActive", 1);
                dbParams.Add("tempId",null);

                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);

                DynamicParameters dbParam = new DynamicParameters();
                dbParam.Add("Operation", "Insert");
                dbParam.Add("TenantId", tenentId);
                dbParam.Add("Claim_Id", obj.ClaimId);
                dbParam.Add("Status_Id", obj.ClaimStatusId);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                ///FCM NOTIFICATION
                var resultData = GetUserFCMTokenByID(userId, tenentId, obj.ClaimStatusId);
                dBGenerics.SendMessage(resultData.FCM_Token,"Claimoto", resultData.StatusName);

                if (affectedrows != 0)
                {
                    message = "Claim Status has Successfully Update!";
                }
                else
                {
                    message = "Claim Status has not Successfully Update!";
                }
            }
            else
            {
                message = "Ensure that the Claim Id included in the request is correct";
            }
            
            return message;
        }

        public string InsertClaimAppeal(ClaimAppeal obj, long tenantId,long userId)
        {
            string message = null;
            if (obj.ClaimId != 0 && obj.AppealComment != null)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);
                dbParams.Add("ClaimId", obj.ClaimId);
                dbParams.Add("AppealComment", obj.AppealComment);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyClaimAppeal", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                DynamicParameters dbParam = new DynamicParameters();
                dbParam.Add("Operation", "Insert");
                dbParam.Add("TenantId", tenantId);
                dbParam.Add("Claim_Id", obj.ClaimId);
                dbParam.Add("Status_Id", 14);
                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);
                unitOfWork.Connection.Execute("usp_ModifyClaimsStatusMapping", dbParam, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

                ///FCM NOTIFICATION
                var resultData = GetUserFCMTokenByID(userId, tenantId, 14);
                dBGenerics.SendMessage(resultData.FCM_Token, "Claimoto", resultData.StatusName);

                if (affectedrows != 0)
                {
                    message = "ClaimAppeal has Successfully Save!";
                }
                else
                {
                    message = "ClaimAppeal has not Successfully Save!";
                }

            }
            else
            {
                message = "Ensure that the data included in the request is correct";
            }

            return message;
        }

        public string InsertClaimsLeaveMessage(ClaimLeaveMessage obj, long tenantId, long userId, out bool bit)
        {
            bit = false;
            string message = null;

            if (obj != null)
            {
                DynamicParameters dbParams = new DynamicParameters();
                dbParams.Add("Operation", "Insert");
                dbParams.Add("TenantId", tenantId);

                dbParams.Add("Claim_Id", obj.Claim_Id);
                dbParams.Add("MSG", obj.MSG);

                dbParams.Add("CreatedBy", userId);
                dbParams.Add("UpdatedBy", userId);
                dbParams.Add("UpdatedDate", DateTime.Now);
                dbParams.Add("IsDeleted", false);
                dbParams.Add("IsActive", true);

                var affectedrows = unitOfWork.Connection.Execute("usp_ModifyClaimLeaveMessage", dbParams, transaction: unitOfWork.Transaction, commandType: CommandType.StoredProcedure);


                if (affectedrows != 0)
                {
                    message = "ClaimsLeaveMessage has Successfully Save!";
                    bit = true;
                }
                else
                {
                    message = "ClaimsLeaveMessage has not Successfully Save!";
                }
            }
            else
            {
                message = "Ensure that the data included in the request is correct";
            }

            return message;
        }

        public IEnumerable<UserProfiles> GetUserProfiles(long id, long roleId, long tenantId, string SearchText)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetUserProfile");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("SearchText", SearchText);

            return unitOfWork.Connection.Query<UserProfiles>("usp_GetUserProfiles", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<MBClaims> GetMBClaims(long CivilId, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetMBClaims");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("IdentityNo", CivilId);
            return unitOfWork.Connection.Query<MBClaims>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public MBClaim GetMBClaim(long ClaimId, long tenentId, long userId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetMBClaimById");
            dbParams.Add("TenantId", tenentId);
            dbParams.Add("ClaimId", ClaimId);
            return unitOfWork.Connection.QueryFirstOrDefault<MBClaim>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public IEnumerable<ClaimAccidentCarPhotos> GetMBClaimAccidentCarPhotosByClaimId(long ClaimId, long tenantId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetMBClaimAccidentCarPhotosByClaimId");
            dbParams.Add("TenantId", tenantId);
            dbParams.Add("ClaimId", ClaimId);
            return unitOfWork.Connection.Query<ClaimAccidentCarPhotos>("usp_GetClaims", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

        public FCMToken GetUserFCMTokenByID(long userProfileId, long tenantId,long StatusId)
        {
            DynamicParameters dbParams = new DynamicParameters();
            dbParams.Add("Operation", "GetFCMToken");
            dbParams.Add("userId", userProfileId);
            dbParams.Add("TenentId", tenantId);
            dbParams.Add("StatusId", StatusId);
            return unitOfWork.Connection.QueryFirstOrDefault<FCMToken>("usp_ModifyFCMToken", transaction: unitOfWork.Transaction, commandType: System.Data.CommandType.StoredProcedure, param: dbParams);
        }

  }
}
