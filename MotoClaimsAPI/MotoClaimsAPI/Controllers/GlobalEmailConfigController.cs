using MotoClaims.Entities;
using MotoClaims.Entities.GlobelSetting;
using MotoClaims.Services.Interfaces;
using MotoClaimsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MotoClaimsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/EmailTemplate")]
    public class GlobalEmailConfigController : ApiController
    {
        IGlobalEmailConfigService _globalEmailConfigService;
        private object upload;

        public GlobalEmailConfigController()
        {

        }

        public GlobalEmailConfigController(IGlobalEmailConfigService globalEmailConfigService)
        {
            _globalEmailConfigService = globalEmailConfigService;
        }

        /// <summary>
        /// Get All TimeZone
        /// </summary>
        /// <remarks>
        ///
        /// Get an array of all TimeZone
        ///     
        /// </remarks>
        ///
        [Route("TimeZone")]
        [HttpGet]
        public HttpResponseMessage GetTimeZone()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);

            var resultData = _globalEmailConfigService.GetTimeZone(tenentId);
            if (resultData.Count() == 0)
            {
                return ResponseMessages<IEnumerable<TimeZoneConfig>>("TimeZone not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<IEnumerable<TimeZoneConfig>>("Get TimeZone list Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Get GetSMTP_Configuration
        /// </summary>
        /// <remarks>
        ///
        /// Get signle SMTP_Configuration by providing Tenant ID
        ///     
        /// </remarks>
        /// 
        [Route("SMTP_Configuration")]
        [HttpGet]
        public HttpResponseMessage GetSMTP_Configuration()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);

            var resultData = _globalEmailConfigService.GetSMTP_Configuration(tenentId);
            if (resultData.SMTPConfig_ID == 0)
            {
                return ResponseMessages<SMTP_Configuration>("SMTP_Configuration not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<SMTP_Configuration>("Get SMTP_Configuration Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }


        /// <summary>
        /// Update SMTP_Configuration
        /// </summary>
        /// <remarks>
        ///
        /// Update an existing SMTP_Configuration
        ///     
        /// </remarks>
        [Route("Put_SMTP_Configuration")]
        [HttpPut]
        public HttpResponseMessage PutSMTP_Configuration(SMTP_Configuration model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            var resultData = _globalEmailConfigService.UpdateSMTP_Configuration(model, tenentId, userId);
            if (resultData == null)
            {
                return ResponseMessages<SMTP_Configuration>("Error While Updating SMTP_Configuration!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<SMTP_Configuration>("SMTP_Configuration Updating Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Get GlobelEmail_Signature
        /// </summary>
        /// <remarks>
        ///
        /// Get signle GlobelEmail_Signature by providing Tenant ID
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("GlobelEmail_Signature")]
        public HttpResponseMessage GetGlobelEmail_Signature()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);

            var resultData = _globalEmailConfigService.GetGlobelEmail_Signature(tenentId);
            if (resultData.GlobalEmail_ID == 0)
            {
                return ResponseMessages<GlobalEmailConfiguration>("GlobelEmail_Signature not found", "Empty Record", (int)HttpStatusCode.OK, resultData);
            }
            return ResponseMessages<GlobalEmailConfiguration>("Get GlobelEmail_Signature Successfully", string.Empty, (int)HttpStatusCode.OK, resultData);

        }

        /// <summary>
        /// Add GlobelEmail_Signature
        /// </summary>
        /// <remarks>
        ///
        /// Insert new GlobelEmail_Signature
        ///     
        /// </remarks>
        [Route("Post_GlobelEmail_Signature")]
        [HttpPost]
        public HttpResponseMessage PostGlobelEmail_Signature(GlobalEmailConfiguration model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            bool bit ;
            var resultData = _globalEmailConfigService.InsertGlobelEmail_Signature(model, tenentId, userId, out  bit);
            if (bit  == false)
            {
                return ResponseMessages<GlobalEmailConfiguration>(resultData, "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<GlobalEmailConfiguration>("GlobelEmail_Signature Saved Successfully.", string.Empty, (int)HttpStatusCode.OK, null);
        }

        /// <summary>
        /// Update GlobelEmail_Signature
        /// </summary>
        /// <remarks>
        ///
        /// Update an existing GlobelEmail_Signature
        ///     
        /// </remarks>
        [Route("Put_GlobelEmail_Signature")]
        [HttpPut]
        public HttpResponseMessage PutGlobelEmail_Signature(GlobalEmailConfiguration model)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);

            bool bit;
            var resultData = _globalEmailConfigService.UpdateGlobelEmail_Signature(model, tenentId, userId, out bit);
            if (bit == false)
            {
                return ResponseMessages<GlobalEmailConfiguration>("Error While Updating GlobelEmail_Signature!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<GlobalEmailConfiguration>("GlobelEmail_Signature Updating Successfully.", string.Empty, (int)HttpStatusCode.OK, null);

        }

        [Route("SendEmail")]
        [HttpGet]
        public HttpResponseMessage SendEmail(string SMTP_Host,string Auth_User,string Auth_Password,int Port,bool SSL,string ToText)
        {

            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenentId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            bool bit = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Auth_User);
                mail.To.Add(ToText);
                mail.Subject = "Motor Claims";
                string FilePath = HttpContext.Current.Server.MapPath("~/Content/MailSignature.html");
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                mail.Body = MailText;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                SmtpClient client = new SmtpClient();
                client.Host = SMTP_Host;
                client.Port = Port;
                client.EnableSsl = SSL;
                client.UseDefaultCredentials = false;
                var credentials = new System.Net.NetworkCredential(Auth_User, Auth_Password);
                client.Credentials = credentials;
                client.Send(mail);
                bit = true;
            }
            catch (Exception ex)
            {
                bit = false;
            }

            if (bit == false)
            {
                return ResponseMessages<GlobalEmailConfiguration>("Error While Send Email!", "Ensure that the data included in the request are correct", (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<GlobalEmailConfiguration>("Email Send Successfully", string.Empty, (int)HttpStatusCode.OK, null);
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
