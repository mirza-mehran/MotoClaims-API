using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using MotoClaims.Entities.User;
using MotoClaims.Services.Interfaces;
using MotoClaimsAPI.Models;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security;
using MotoClaims.Entities.JsonObject;
using Newtonsoft.Json.Linq;
using System.Security.Principal;

namespace MotoClaimsAPI.Controllers
{

    [RoutePrefix("api/ForgotPassword")]
    public class ForgotPasswordController : ApiController
    {
        IForgotPasswordService _forgotPassword;
        IUserManagementService userManagementService;

        public ForgotPasswordController( )
        {

        }
        public ForgotPasswordController(IForgotPasswordService forgotPassword, IUserManagementService _userManagementService)
        {
            _forgotPassword = forgotPassword;
            userManagementService = _userManagementService;
        }

        [HttpGet]
        [Route("SendEmail")]
        public HttpResponseMessage GetEmail(string Email,string Url)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            tenantId = 2;

            var resultData = _forgotPassword.GetEmails(Email, tenantId);
            var UserNameObj = resultData.Where(x => x.Email == Email).FirstOrDefault();
            UserProfile ina = new UserProfile();
            
            bool emailVerify = resultData.Where(x => x.Email == Email).Any();
            if (emailVerify == true)
            {
                GetToken(UserNameObj.UserName,Email,Url);
                return ResponseMessages<UserProfile>("Send Email Successfully", string.Empty, (int)HttpStatusCode.OK, null);
            }

            return ResponseMessages<UserProfile>("Invalid Email", string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        string jwt_token;
        [NonAction]
        public void GetToken(string UserName, string Email, string Url)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
            long userId = Convert.ToInt64(claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value);
            tenantId = 2;

            UserProfile user = userManagementService.GetUserLoginDetails(UserName);
            var Roles = userManagementService.GetRoles(tenantId);
            string RoleName = Roles.Where(x => x.RoleId == user.RoleId).Select(x => x.RoleName).FirstOrDefault().ToString();

            if (user != null && user.UserId > 0)
            {
                if (user.Status == 1)
                {
                    string key = ConfigurationManager.AppSettings["token.secret"];  //Secret key which will be used later during validation    
                    var issuer = ConfigurationManager.AppSettings["issuer"];  //normally this will be your site URL    
                    var tokenExpiry = DateTime.Now.AddMinutes(3);
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var accessGroups = JsonConvert.SerializeObject(user.AccessGroupIds.Select(x => x.AccessGroupId), Formatting.None);
                    var userPermissions = JsonConvert.SerializeObject(user.UserModules, Formatting.None);
                    //Create a List of Claims   
                    var permClaims = new List<Claim>();
                    permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    permClaims.Add(new Claim("UserId", user.UserId.ToString()));
                    permClaims.Add(new Claim("FirstName", user.FirstName));
                    permClaims.Add(new Claim("LastName", user.LastName));
                    permClaims.Add(new Claim("Username", user.UserName));
                    permClaims.Add(new Claim("Email", user.Email));
                    permClaims.Add(new Claim("RoleId", user.RoleId.ToString()));
                    permClaims.Add(new Claim("RoleName", RoleName));
                    permClaims.Add(new Claim("TenantId", user.TenantId.ToString()));
                    permClaims.Add(new Claim("UserAccessGroups", accessGroups));
                    permClaims.Add(new Claim("UserPermissions", userPermissions));
                    if (user.ImageUrl == null)
                    {
                        permClaims.Add(new Claim("ImageUrl", string.Empty));
                    }
                    else
                    {
                        permClaims.Add(new Claim("ImageUrl", user.ImageUrl));
                    }


                    //Create Security Token object by giving required parameters    
                    var token = new JwtSecurityToken(issuer,
                                        issuer,
                                        permClaims,
                                        expires: DateTime.Now.AddMinutes(1),
                                        signingCredentials: credentials);
                     jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

                    PostEmail(jwt_token,Email,Url);
                }
            }
        }

        [NonAction]
        public void PostEmail(string jwt_token,string Email, string Url)
        {
            string SMTP_Host = "smtp.gmail.com", Auth_User = "testmotoclaim@gmail.com", Auth_Password = "Admin123@";
            int Port = 587; bool SSL = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(Auth_User);
            mail.To.Add(Email);
            mail.Subject = "Motor Claims";

            string FilePath = HttpContext.Current.Server.MapPath("~/Content/ForgotPasswordTemplate.cshtml");
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = Regex.Replace(MailText, "Token", jwt_token).Replace("Url", Url);
            mail.Body = MailText;
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Host = SMTP_Host;
            client.Port = Port;
            client.EnableSsl = SSL;
            client.UseDefaultCredentials = false;
            var credential = new System.Net.NetworkCredential(Auth_User, Auth_Password);
            client.Credentials = credential;
            client.Send(mail);  
        }


        [NonAction]
        public HttpResponseMessage GetPrincipalFromExpiredToke(updatePasswordUser model) 
        {
            var Key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["token.secret"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(model.Code, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                // throw new SecurityTokenException("Invalid token");
                 return ResponseMessages<ClaimsPrincipal>("Invalid token", string.Empty, (int)HttpStatusCode.BadRequest, null);
            }
            else
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);
                if (!string.IsNullOrEmpty(model.password))
                {
                    model.password = Common.GetSHA256Hash(model.password);
                }

                string errMessage = null;
                var resultData = _forgotPassword.UpdateUserAccount(model, tenantId, out errMessage);
            }
            return ResponseMessages<UserProfile>("ResetPassword Successfully", string.Empty, (int)HttpStatusCode.BadRequest, null);
        }

        //public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        //{
        //   // string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIzMGVjODAxYy00OGNlLTRkMTctOTgzYS1jYTVmMmZjZThjM2MiLCJVc2VySWQiOiIyMDA1OCIsIkZpcnN0TmFtZSI6IkphaGFuIiwiTGFzdE5hbWUiOiJKYWhhbiIsIlVzZXJuYW1lIjoiemFpYiIsIkVtYWlsIjoiamVoYW54YWliQGdtYWlsLmNvbSIsIlJvbGVJZCI6IjEiLCJSb2xlTmFtZSI6IkFkbWluIiwiVGVuYW50SWQiOiIyIiwiVXNlckFjY2Vzc0dyb3VwcyI6IlsxXSIsIlVzZXJQZXJtaXNzaW9ucyI6Ilt7XCJNb2R1bGVJZFwiOjEsXCJNb2R1bGVTeXN0ZW1OYW1lXCI6XCJBRFwiLFwiTW9kdWxlQWN0aW9uc1wiOlt7XCJBY3Rpb25JZFwiOjEsXCJBY3Rpb25OYW1lXCI6XCJWSUVXXCJ9XX0se1wiTW9kdWxlSWRcIjoyLFwiTW9kdWxlU3lzdGVtTmFtZVwiOlwiQVBSXCIsXCJNb2R1bGVBY3Rpb25zXCI6W3tcIkFjdGlvbklkXCI6MSxcIkFjdGlvbk5hbWVcIjpcIlZJRVdcIn0se1wiQWN0aW9uSWRcIjoyLFwiQWN0aW9uTmFtZVwiOlwiSU5TRVJUXCJ9LHtcIkFjdGlvbklkXCI6MyxcIkFjdGlvbk5hbWVcIjpcIlVQREFURVwifSx7XCJBY3Rpb25JZFwiOjQsXCJBY3Rpb25OYW1lXCI6XCJERUxFVEVcIn0se1wiQWN0aW9uSWRcIjo1LFwiQWN0aW9uTmFtZVwiOlwiRVhQT1JUXCJ9LHtcIkFjdGlvbklkXCI6NixcIkFjdGlvbk5hbWVcIjpcIklNUE9SVFwifV19LHtcIk1vZHVsZUlkXCI6MyxcIk1vZHVsZVN5c3RlbU5hbWVcIjpcIkFQT1wiLFwiTW9kdWxlQWN0aW9uc1wiOlt7XCJBY3Rpb25JZFwiOjEsXCJBY3Rpb25OYW1lXCI6XCJWSUVXXCJ9LHtcIkFjdGlvbklkXCI6MixcIkFjdGlvbk5hbWVcIjpcIklOU0VSVFwifSx7XCJBY3Rpb25JZFwiOjMsXCJBY3Rpb25OYW1lXCI6XCJVUERBVEVcIn0se1wiQWN0aW9uSWRcIjo0LFwiQWN0aW9uTmFtZVwiOlwiREVMRVRFXCJ9XX0se1wiTW9kdWxlSWRcIjo0LFwiTW9kdWxlU3lzdGVtTmFtZVwiOlwiQVZQXCIsXCJNb2R1bGVBY3Rpb25zXCI6W3tcIkFjdGlvbklkXCI6MSxcIkFjdGlvbk5hbWVcIjpcIlZJRVdcIn0se1wiQWN0aW9uSWRcIjoyLFwiQWN0aW9uTmFtZVwiOlwiSU5TRVJUXCJ9LHtcIkFjdGlvbklkXCI6MyxcIkFjdGlvbk5hbWVcIjpcIlVQREFURVwifSx7XCJBY3Rpb25JZFwiOjQsXCJBY3Rpb25OYW1lXCI6XCJERUxFVEVcIn1dfSx7XCJNb2R1bGVJZFwiOjUsXCJNb2R1bGVTeXN0ZW1OYW1lXCI6XCJBVU1cIixcIk1vZHVsZUFjdGlvbnNcIjpbe1wiQWN0aW9uSWRcIjoxLFwiQWN0aW9uTmFtZVwiOlwiVklFV1wifSx7XCJBY3Rpb25JZFwiOjIsXCJBY3Rpb25OYW1lXCI6XCJJTlNFUlRcIn0se1wiQWN0aW9uSWRcIjozLFwiQWN0aW9uTmFtZVwiOlwiVVBEQVRFXCJ9LHtcIkFjdGlvbklkXCI6NCxcIkFjdGlvbk5hbWVcIjpcIkRFTEVURVwifV19LHtcIk1vZHVsZUlkXCI6NixcIk1vZHVsZVN5c3RlbU5hbWVcIjpcIkFTQ1wiLFwiTW9kdWxlQWN0aW9uc1wiOlt7XCJBY3Rpb25JZFwiOjEsXCJBY3Rpb25OYW1lXCI6XCJWSUVXXCJ9LHtcIkFjdGlvbklkXCI6MixcIkFjdGlvbk5hbWVcIjpcIklOU0VSVFwifSx7XCJBY3Rpb25JZFwiOjMsXCJBY3Rpb25OYW1lXCI6XCJVUERBVEVcIn0se1wiQWN0aW9uSWRcIjo0LFwiQWN0aW9uTmFtZVwiOlwiREVMRVRFXCJ9XX0se1wiTW9kdWxlSWRcIjo3LFwiTW9kdWxlU3lzdGVtTmFtZVwiOlwiQVJNXCIsXCJNb2R1bGVBY3Rpb25zXCI6W3tcIkFjdGlvbklkXCI6MSxcIkFjdGlvbk5hbWVcIjpcIlZJRVdcIn0se1wiQWN0aW9uSWRcIjoyLFwiQWN0aW9uTmFtZVwiOlwiSU5TRVJUXCJ9LHtcIkFjdGlvbklkXCI6MyxcIkFjdGlvbk5hbWVcIjpcIlVQREFURVwifSx7XCJBY3Rpb25JZFwiOjQsXCJBY3Rpb25OYW1lXCI6XCJERUxFVEVcIn1dfSx7XCJNb2R1bGVJZFwiOjgsXCJNb2R1bGVTeXN0ZW1OYW1lXCI6XCJBR01cIixcIk1vZHVsZUFjdGlvbnNcIjpbe1wiQWN0aW9uSWRcIjoxLFwiQWN0aW9uTmFtZVwiOlwiVklFV1wifSx7XCJBY3Rpb25JZFwiOjIsXCJBY3Rpb25OYW1lXCI6XCJJTlNFUlRcIn0se1wiQWN0aW9uSWRcIjozLFwiQWN0aW9uTmFtZVwiOlwiVVBEQVRFXCJ9LHtcIkFjdGlvbklkXCI6NCxcIkFjdGlvbk5hbWVcIjpcIkRFTEVURVwifV19LHtcIk1vZHVsZUlkXCI6OSxcIk1vZHVsZVN5c3RlbU5hbWVcIjpcIlBHXCIsXCJNb2R1bGVBY3Rpb25zXCI6W3tcIkFjdGlvbklkXCI6MSxcIkFjdGlvbk5hbWVcIjpcIlZJRVdcIn0se1wiQWN0aW9uSWRcIjoyLFwiQWN0aW9uTmFtZVwiOlwiSU5TRVJUXCJ9LHtcIkFjdGlvbklkXCI6MyxcIkFjdGlvbk5hbWVcIjpcIlVQREFURVwifSx7XCJBY3Rpb25JZFwiOjQsXCJBY3Rpb25OYW1lXCI6XCJERUxFVEVcIn1dfSx7XCJNb2R1bGVJZFwiOjEwLFwiTW9kdWxlU3lzdGVtTmFtZVwiOlwiUEFcIixcIk1vZHVsZUFjdGlvbnNcIjpbe1wiQWN0aW9uSWRcIjoxLFwiQWN0aW9uTmFtZVwiOlwiVklFV1wifSx7XCJBY3Rpb25JZFwiOjIsXCJBY3Rpb25OYW1lXCI6XCJJTlNFUlRcIn0se1wiQWN0aW9uSWRcIjozLFwiQWN0aW9uTmFtZVwiOlwiVVBEQVRFXCJ9LHtcIkFjdGlvbklkXCI6NCxcIkFjdGlvbk5hbWVcIjpcIkRFTEVURVwifV19LHtcIk1vZHVsZUlkXCI6MTEsXCJNb2R1bGVTeXN0ZW1OYW1lXCI6XCJQQ0FcIixcIk1vZHVsZUFjdGlvbnNcIjpbe1wiQWN0aW9uSWRcIjoxLFwiQWN0aW9uTmFtZVwiOlwiVklFV1wifSx7XCJBY3Rpb25JZFwiOjIsXCJBY3Rpb25OYW1lXCI6XCJJTlNFUlRcIn0se1wiQWN0aW9uSWRcIjozLFwiQWN0aW9uTmFtZVwiOlwiVVBEQVRFXCJ9LHtcIkFjdGlvbklkXCI6NCxcIkFjdGlvbk5hbWVcIjpcIkRFTEVURVwifV19LHtcIk1vZHVsZUlkXCI6MTIsXCJNb2R1bGVTeXN0ZW1OYW1lXCI6XCJQU1wiLFwiTW9kdWxlQWN0aW9uc1wiOlt7XCJBY3Rpb25JZFwiOjEsXCJBY3Rpb25OYW1lXCI6XCJWSUVXXCJ9LHtcIkFjdGlvbklkXCI6MixcIkFjdGlvbk5hbWVcIjpcIklOU0VSVFwifSx7XCJBY3Rpb25JZFwiOjMsXCJBY3Rpb25OYW1lXCI6XCJVUERBVEVcIn0se1wiQWN0aW9uSWRcIjo0LFwiQWN0aW9uTmFtZVwiOlwiREVMRVRFXCJ9XX1dIiwiSW1hZ2VVcmwiOiIvVXBsb2Fkcy8xNjQ5NDExODcwMjMwOTM2MjAxXzI5NDQ0NDA0MzkxNTczNTVfODY3OTAxODgwMjQwNzk5MDI0X24uanBlZyIsImV4cCI6MTY1MTA3MDM0OCwiaXNzIjoiaHR0cDovL2NsYWltb3RvLmNvbSIsImF1ZCI6Imh0dHA6Ly9jbGFpbW90by5jb20ifQ.77oqZ2xpVmUQscm-MGWpiv3mJtFkkGZx_x5Kf0KUEro";
        //    var Key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["token.secret"]);

        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        ValidateLifetime = false,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Key),
        //        ClockSkew = TimeSpan.Zero
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        //    JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        throw new SecurityTokenException("Invalid token");
        //    }
        //    else
        //    {
        //        var identity = User.Identity as ClaimsIdentity;
        //        IEnumerable<Claim> claims = identity.Claims;
        //        long tenantId = Convert.ToInt64(claims.Where(p => p.Type == "TenantId").FirstOrDefault()?.Value);

        //        //string errMessage = null;
        //        //var resultData = _forgotPassword.UpdateUserAccount(model, tenantId, out errMessage);
        //        //return ResponseMessages<UserProfile>("Invalid Email", string.Empty, (int)HttpStatusCode.BadRequest, null);
        //    }
        //    return principal;
        //}

        //[HttpGet]
        //[Route("Verify")]
        [HttpPost]
        [Route("ResetPassword")]
        public HttpResponseMessage GetPrincipalFromExpiredToken(updatePasswordUser model)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(model.Code, validationParameters, out validatedToken);

                if (!string.IsNullOrEmpty(model.password))
                {
                    model.password = Common.GetSHA256Hash(model.password);
                }

                string errMessage = null;
                var resultData = _forgotPassword.UpdateUserAccount(model,Convert.ToInt64(model.TenantId), out errMessage);
            }
            catch (Exception ex)
            {
                IPrincipal principal = null;
                return ResponseMessages<ClaimsPrincipal>("Invalid token", string.Empty, (int)HttpStatusCode.BadRequest, null);
            }
            return ResponseMessages<UserProfile>("ResetPassword Successfully", string.Empty, (int)HttpStatusCode.OK, null);
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            var Key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["token.secret"]);
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = ConfigurationManager.AppSettings["issuer"], //some string, normally web url,  
                ValidAudience = ConfigurationManager.AppSettings["issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["token.secret"]))
            };
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
