using MotoClaims.DataAccess.Repositories;
using MotoClaims.DataAccess.UOW;
using MotoClaims.Entities.Common;
using MotoClaims.Services.Interfaces;
using MotoClaims.Services.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace MotoClaims.Services
{
    public static class GeneralSettings
    {
        public static string ToXML<T>(T response)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(response.GetType());
            serializer.Serialize(stringwriter, response);
            return stringwriter.ToString();
        }

    
        public static bool CheckNullOrEmpty<T>(T value)
        {
            if (typeof(T) == typeof(string))
                return false;
            else
                return true;
        }
        public static void ActivityLog<T>(T NewActivity = default(T), string Screen = "", string NewActivityEntity = "", string NewActivityPrimaryKeyAttr = "", long NewActivityPrimaryKeyAttrVal = 0, long LoggedInUser = 0, string ActivityName = "", UnitOfWork unitOfWork = null, long tenantId = 0)
        {

            T OldData = default(T);
            bool isNewActivityPerformed = CheckNullOrEmpty(NewActivity);

            //using (Repositories dalSession = new Repositories())
            //{
                //UnitOfWork unitOfWork = dalSession.UnitOfWork;
                CommonRepository myRepository = new CommonRepository(unitOfWork);
                //unitOfWork.Begin();
                try
                {
                    #region Addlog

                    ActivityLog obj = new ActivityLog();
                    obj.EntityReference = NewActivityEntity;
                    obj.UserId = LoggedInUser;
                    obj.ActivityId = ActivityName;
                    obj.EntityReference = Screen + " -> " + obj.EntityReference;


                    var AddLog = myRepository.InsertAuditLog(obj, tenantId);

                    #endregion

                    #region AddlogDetails

                    if (isNewActivityPerformed && ActivityName == CRUD.UPDATE.ToString())
                    {
                        if (!string.IsNullOrEmpty(NewActivityEntity) &&
                            !string.IsNullOrEmpty(NewActivityPrimaryKeyAttr) &&
                            NewActivityPrimaryKeyAttrVal > 0)
                        {
                            OldData = myRepository.GetOldDataById<T>(NewActivityPrimaryKeyAttrVal, NewActivityEntity, NewActivityPrimaryKeyAttr);

                            var NewData = NewActivity;

                            OldData = OldData == null ? NewData : OldData;

                            var CheckActivityDetail = ComparePropertiesTo(OldData, NewData);

                            if (CheckActivityDetail.Count() >= 1)
                            {
                                foreach (var item in CheckActivityDetail)
                                {

                                    item.AuditId = AddLog;
                                    item.ScreenName = Screen;
                                    var AddLogDetail = myRepository.InsertAuditLogDetails(item);
                                }
                            }
                        }
                    }


                    #endregion

                    //unitOfWork.Commit();
                }
                catch (Exception exc)
                {
                    //unitOfWork.Rollback();
                    LogError(exc, "ActivityLog", "", CRUD.INSERT.ToString(), LoggedInUser);
                    throw;
                }
            //}
        }

        public static void LogError(Exception ex, string Method = "", string RequestObject = "", string Activity = "", long LoggedInUser = 0, long tenantId = 0)
        {
            ErrorLogs error = new ErrorLogs();
            error.ActivityId = Activity;
            error.ErrorDetails = ex.InnerException == null ? "" : ex.InnerException.ToString();
            error.StackTrace = ex.StackTrace.ToString();
            error.ErrorMsg = ex.Message.ToString();
            error.Source = ex.Source.ToString();
            error.CreatedBy = LoggedInUser;
            error.Requestobject = RequestObject;
            error.ApiName = Method;

            long result = -1;
            using (Repositories dalSession = new Repositories())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                CommonRepository myRepository = new CommonRepository(unitOfWork);
                unitOfWork.Begin();
                try
                {
                    //Your database code here

                    result = myRepository.InsertErrorLog(error, tenantId);
                    if (result > 0)
                        unitOfWork.Commit();

                }
                catch (Exception exc)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }

        }

        public static List<ActivityDetail> ComparePropertiesTo(this Object a, Object b)
        {
            List<ActivityDetail> ListOfActivity = new List<ActivityDetail>();

            string propName = string.Empty;

            System.Reflection.PropertyInfo[] properties = a.GetType().GetProperties(); // get all the properties of object a

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                propName = propertyName;

                var aValue = a.GetType().GetProperty(propertyName).GetValue(a, null);
                object bValue;

                try // try to get the same property from object b. maybe that property does
                    // not exist! 
                {
                    bValue = b.GetType().GetProperty(propertyName).GetValue(b, null);
                }
                catch
                {
                    continue;
                }

                aValue = aValue == null ? "" : aValue;
                bValue = bValue == null ? "" : bValue;
                //if (aValue == null && bValue == null)
                //    continue;

                //if (aValue == null && bValue != null)
                //    continue;

                //if (aValue != null && bValue == null)
                //    continue;

                // if properties do not match return false
                if (aValue.GetHashCode() != bValue.GetHashCode())
                {
                    //Name of properties which ypu dont want to compare
                    if (propName != "CreatedDate" && propName != "UpdatedDate" && propName != "CreatedBy" && propName != "UpdatedBy" && propName != "AccessGroupIds" && propName != "ProviderLocations" && propName != "ProviderContacts" && propName != "ProviderServiceMapping")
                    {

                        if (bValue.ToString() != "0")
                        {

                            ActivityDetail obj = new ActivityDetail();

                            obj.FieldName = propName;
                            obj.OldValue = aValue.ToString();
                            obj.NewValue = bValue.ToString();

                            ListOfActivity.Add(obj);
                        }
                    }


                    continue;
                }
                else
                    continue;
            }

            return ListOfActivity;
        }

        public enum CRUD
        {
            INSERT,
            EXPORT,
            UPDATE,
            DELETE,
            VIEW,
            UPLOAD,
            PDF
        }

        public static bool Contains(this String str, String substring,
                             StringComparison comp)
        {
            if (substring == null)
                throw new ArgumentNullException("substring",
                                                "substring cannot be null.");
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison",
                                            "comp");

            return str.IndexOf(substring, comp) >= 0;
        }
        private static string key1 = "alert";

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
