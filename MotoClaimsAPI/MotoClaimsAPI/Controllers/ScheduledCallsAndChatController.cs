using MotoClaims.Entities.ScheduledCallsAndChat;
using MotoClaims.Services.Interfaces;
using MotoClaimsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/ScheduledCallsAndChat")]
    public class ScheduledCallsAndChatController : ApiController
    {
        IScheduledCallsAndChatService _iScheduledCallsAndChatService;

        public ScheduledCallsAndChatController()
        {

        }

        public ScheduledCallsAndChatController(IScheduledCallsAndChatService iScheduledCallsAndChatService)
        {
            _iScheduledCallsAndChatService = iScheduledCallsAndChatService;
        }


        /// <summary>
        /// Add new ScheduledCalls
        /// </summary>
        /// <remarks>
        ///
        /// Insert new ScheduledCalls
        ///     
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage PostScheduledCalls([FromBody]ScheduledCalls obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            
            //if (ModelState.IsValid)
            //{
                string resultData = _iScheduledCallsAndChatService.InsertScheduledCalls(obj,tenantId,userId);

                if (resultData == null)
                {
                    return ResponseMessages<ScheduledCalls>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<ScheduledCalls>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            //}
            //string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            //return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        /// <summary>
        /// Get Split DayTiming
        /// </summary>
        /// <remarks>
        ///
        /// Get Split DayTiming
        ///     
        /// </remarks>
        [Route("SplitDayTiming")]
        [HttpGet]
        public HttpResponseMessage GetSplitDayTiming(string Day,DateTime dateTime)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            IEnumerable<ScheduledCallExceptionSlots> resultData1 = _iScheduledCallsAndChatService.GetScheduledCallExceptionSlots(tenantId, userId);
            ConfigScheduledCallTimeSlots resultData = _iScheduledCallsAndChatService.GetConfigScheduledCallTimeSlots(tenantId, userId);

            List<string> breakIntervals = new List<string>();
            // DateTime date = DateTime.ParseExact(dateTime, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);

            ScheduledCallExceptionSlots data = resultData1.Where(x => x.Date == dateTime).FirstOrDefault();
            if (data == null)
            {
                if (Day != resultData.WeekDay1 && Day != resultData.WeekDay2)
                {
                    string breakTime = string.Empty;
                    string IfromTime = resultData.DayStartTime.ToString();
                    string ItoTime = resultData.DayEndTime.ToString();

                    DateTime fromTime = DateTime.ParseExact(IfromTime, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime toTime = DateTime.ParseExact(ItoTime, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                    while (fromTime < toTime)
                    {
                        DateTime timeInterval1, timeInterval2;
                        timeInterval1 = fromTime;

                        fromTime = fromTime.AddHours(1);
                        timeInterval2 = fromTime;

                        if (fromTime < toTime)
                        {
                            breakIntervals.Add(timeInterval1.ToLongTimeString() + " To " + timeInterval2.ToLongTimeString());
                        }
                        else
                        {
                            breakIntervals.Add(timeInterval1.ToLongTimeString() + " To " + toTime.ToLongTimeString());
                        }
                    }
                    return ResponseMessages<List<string>>("Get Timing Slots list Successfully", " ", (int)HttpStatusCode.OK, breakIntervals);
                }
                else
                {
                    return ResponseMessages<List<string>>("It's off day", " ", (int)HttpStatusCode.OK, breakIntervals);
                }
            }
            if (data.Date == dateTime && data.LimitOfSlots != 0)
            {
                if (data.StartTime != TimeSpan.MinValue && data.EndTime != TimeSpan.MinValue)
                {
                    string breakTime = string.Empty;
                    string IfromTime = data.StartTime.ToString();
                    string ItoTime   = data.EndTime.ToString();

                    DateTime fromTime = DateTime.ParseExact(IfromTime, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime toTime = DateTime.ParseExact(ItoTime, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                    while (fromTime < toTime)
                    {
                        DateTime timeInterval1, timeInterval2;
                        timeInterval1 = fromTime;

                        fromTime = fromTime.AddHours(1);
                        timeInterval2 = fromTime;

                        if (fromTime < toTime)
                        {
                            breakIntervals.Add(timeInterval1.ToLongTimeString() + " To " + timeInterval2.ToLongTimeString());
                        }
                        else
                        {
                            breakIntervals.Add(timeInterval1.ToLongTimeString() + " To " + toTime.ToLongTimeString());
                        }
                    }

                    return ResponseMessages<List<string>>("Get Timing Slots list Successfully", " ", (int)HttpStatusCode.OK, breakIntervals);
                }
            }
            else if (data.Date == dateTime && data.LimitOfSlots == 0)
            {
                return ResponseMessages<List<string>>("It's off day", " ", (int)HttpStatusCode.OK, breakIntervals);
            }
      
            
            return ResponseMessages<List<string>>("Get Timing Slots list Successfully"," ", (int)HttpStatusCode.OK, breakIntervals);
        }

        /// <summary>
        /// Get Split WithInterval
        /// </summary>
        /// <remarks>
        ///
        /// Get Split WithInterval
        ///     
        /// </remarks>
        [Route("SplitWithInterval")]
        [HttpGet]
        public HttpResponseMessage GetSplitWithInterval(DateTime IfromTime, DateTime ItoTime)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            string breakTime = string.Empty;
            List<string> breakIntervals = new List<string>();

            ConfigScheduledCallTimeSlots resultData = _iScheduledCallsAndChatService.GetConfigScheduledCallTimeSlots(tenantId, userId);

            while (IfromTime < ItoTime)
            {
                DateTime timeInterval1, timeInterval2;
                timeInterval1 = IfromTime;

                IfromTime = IfromTime.AddMinutes(resultData.IntervalGap);
                timeInterval2 = IfromTime;

                if (IfromTime < ItoTime)
                {
                    breakIntervals.Add(timeInterval1.ToLongTimeString() + " to " + timeInterval2.ToLongTimeString());
                }
                else
                {
                    breakIntervals.Add(timeInterval1.ToLongTimeString() + " to " + ItoTime.ToLongTimeString());
                }
            }
            return ResponseMessages<List<string>>("BreakIntervals Successfully", " ", (int)HttpStatusCode.OK, breakIntervals);
        }

        /// <summary>
        /// Get ScheduledCalls
        /// </summary>
        /// <remarks>
        ///
        /// Get Array ScheduledCalls
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("ScheduledCalls")]
        public HttpResponseMessage GetScheduledCalls() 
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _iScheduledCallsAndChatService.GetScheduledCalls(tenantId, userId);

            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ScheduledCallsList>>("ScheduledCalls not found", "", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<ScheduledCallsList>>("Get ScheduledCalls list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);
        }

        /// <summary>
        /// Get ScheduledCalls For Mobile
        /// </summary>
        /// <remarks>
        ///
        /// Get Array ScheduledCalls
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("Mobile/SchduleCalls")]
        public HttpResponseMessage GetMBScheduledCalls(long CivilId)
        {
            var Identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = Identity.Claims;

            long tenantId =Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
             
            var result = _iScheduledCallsAndChatService.GetMBScheduledCalls(CivilId,tenantId,userId);
            if (result.Count() == 0)
            {
                return ResponseMessages<IEnumerable<ScheduledCalls>>("ScheduledCalls not found", "",(int)HttpStatusCode.OK,result);
            }
            return ResponseMessages<IEnumerable<ScheduledCalls>>("Get ScheduledCalls list Successfully",string.Empty,(int)HttpStatusCode.OK,result);
        }

        /// <summary>
        /// Add new ScheduledCallsAssigned
        /// </summary>
        /// <remarks>
        ///
        /// Insert new ScheduledCallsAssigned
        ///     
        /// </remarks>
        //[Route("ScheduledCallsAssigned")]
        //[HttpPost]
        //public HttpResponseMessage PostScheduledCallsAssigned([FromBody]ScheduledCallsAssigned obj)
        //{
        //    var identity = User.Identity as ClaimsIdentity;
        //    IEnumerable<Claim> claims = identity.Claims;
        //    long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
        //    long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
        //    //tenentId = 2;

        //    if (ModelState.IsValid)
        //    {
        //        string resultData = _iScheduledCallsAndChatService.InsertScheduledCallsAssigned(obj, tenantId, userId);
        //        if (resultData == null)
        //        {
        //            return ResponseMessages<ScheduledCallsAssigned>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
        //        }
        //        return ResponseMessages<ScheduledCallsAssigned>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
        //    }
        //    string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

        //    return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        //}

        /// <summary>
        /// Update ScheduledCalls
        /// </summary>
        /// <remarks>
        ///
        /// Update ScheduledCalls by providing SC_Id 
        ///     
        /// </remarks>
        /// 
        [Route("ScheduledCallsReAssigned")]
        [HttpPut]
        public HttpResponseMessage PutScheduledCalls([FromBody]ScheduledCalls obj)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            if (ModelState.IsValid)
            {
                string resultData = _iScheduledCallsAndChatService.UpdateScheduledCalls(obj, tenantId, userId);

                if (resultData == null)
                {
                    return ResponseMessages<ScheduledCalls>("Ensure that the data included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<ScheduledCalls>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            }
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());

            return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        /// <summary>
        ///  Cancel ScheduledCall
        /// </summary>
        /// <remarks>
        ///
        ///  Cancel ScheduledCall by providing SC_Id 
        ///     
        /// </remarks>
        /// 
        [Route("CancelScheduledCall")]
        [HttpPut]
        public HttpResponseMessage PutCancelCall(long SC_Id)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            //if (ModelState.IsValid)
            //{
                string resultData = _iScheduledCallsAndChatService.CancelCall(SC_Id, tenantId, userId);
                if (resultData == null)
                {
                    return ResponseMessages<ScheduledCalls>("Ensure that the SC_Id included in the request are correct", " ", (int)HttpStatusCode.BadRequest, null);
                }
                return ResponseMessages<ScheduledCalls>(resultData, string.Empty, (int)HttpStatusCode.OK, null);
            //}
            //string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
           // return ResponseMessages<ModelStateDictionary>(validationErrors, string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        [NonAction]
        public HttpResponseMessage ResponseMessages<T>(string Message, string Detail, int Status, dynamic resultData)
        {

            var postUser = new ExceptionError<T>
            {
                timestamp = DateTime.Now.ToString(),
                message = Message,
                detail = Detail,
                status = Status,
                data = resultData
            };

            var jsonString = JsonConvert.SerializeObject(postUser);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return response;
        }


    }
}
