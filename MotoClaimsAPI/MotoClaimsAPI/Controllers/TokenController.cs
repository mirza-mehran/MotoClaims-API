using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using MotoClaims.Services.Interfaces;
using MotoClaims.Entities.User;
using System.Security.Cryptography;
using System.Linq;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using MotoClaimsAPI.Models;
using System.Net;

namespace MotoClaimsAPI.Controllers
{
    public class TokenController : ApiController
    {
        IUserManagementService userManagementService;
        public TokenController()
        {

        }
        public TokenController(IUserManagementService _userManagementService)
        {
            userManagementService = _userManagementService;
        }

        /// <summary>
        /// Get Token
        /// </summary>
        /// <remarks>
        ///
        /// Provide UserName and Password to get token for user login
        ///     
        /// </remarks>
        [HttpPost]
        public HttpResponseMessage GetToken(LoginRequest obj)
        {
            long tenantId = 0;
            UserProfile user = userManagementService.GetUserLoginDetails(obj.UserName);
            var Roles = userManagementService.GetRoles(tenantId);
            string RoleName = Roles.Where(x => x.RoleId == user.RoleId).Select(x=> x.RoleName).FirstOrDefault().ToString();

                if (user != null && user.UserId > 0)
                {
                    bool passwordMatched;
                    passwordMatched = Common.VerifySHA256Hash(obj.Password, user.Password);
                    if (passwordMatched)
                    {
                        if (user.Status == 1)
                        {
                            string key = ConfigurationManager.AppSettings["token.secret"];  //Secret key which will be used later during validation    
                            var issuer = ConfigurationManager.AppSettings["issuer"];  //normally this will be your site URL    
                            var tokenExpiry = Convert.ToDouble(ConfigurationManager.AppSettings["token.expiry"]);
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
                            if (user.MobileNo == null)
                            {
                                permClaims.Add(new Claim("MobileNo", string.Empty));
                            }
                            else
                            {
                                permClaims.Add(new Claim("MobileNo", user.MobileNo));
                            }
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
                                                expires: DateTime.Now.AddHours(tokenExpiry),
                                                signingCredentials: credentials);
                                string jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                                JWTToken jwt = new JWTToken();
                                jwt.JWT_Token = jwt_token;
                                //return Ok(jwt_token);
                            return ResponseMessages<JWTToken>("Get Token Successfully", string.Empty, (int)HttpStatusCode.OK, jwt);
                        }
                        return ResponseMessages<UserProfile>("Inactive User", string.Empty, (int)HttpStatusCode.BadRequest, null);
                    }
                    else
                    {
                       // return BadRequest("Incorrect Password!");
                        return ResponseMessages<UserProfile>("Incorrect Password!", string.Empty, (int)HttpStatusCode.BadRequest, null);
                    }
                }
                else
                {
                    //return BadRequest("User Name Not Exist!");
                    return ResponseMessages<UserProfile>("User Name Not Exist!", string.Empty, (int)HttpStatusCode.BadRequest, null);
                }
          
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
