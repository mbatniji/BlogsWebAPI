using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogWebAPI.CustomModels;
using BlogWebAPI.Enum;
using BlogWebAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BlogWebAPI.API
{
    [Route("api/v1/[controller]")]
    public class AuthController: Controller
    {
        private readonly ILogger<AuthController> _logger;
        protected readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthController(
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        return UnprocessableEntity(
                        new APIResponse
                        {
                            status = false,
                            statuscode = (int)API_StatusCode.Error,
                            message = $"The user {model.Email} not found!"
                        });
                    }

                    if (await _signInManager.UserManager.CheckPasswordAsync(user, model.Password))
                    {
                        var claim = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim("Email", user.Email),
                            new Claim("UserId", user.Id)
                            };
                        var signinKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                        var expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);
                        var _token = new JwtSecurityToken(
                                     issuer: _configuration["Jwt:Site"],
                                     audience: _configuration["Jwt:Site"],
                                     expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                                     signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
                                     claims: claim
                                   );
                        return Ok(
                        new APIResponse
                        {
                            status = true,
                            statuscode = (int)API_StatusCode.Success,
                            items = new List<APIUsers> { new APIUsers
                                  {
                                      Email = user.UserName,
                                      Id = user.Id,
                                      access_token = new JwtSecurityTokenHandler().WriteToken(_token),
                                      expiration = _token.ValidTo
                                  }
                            }
                        });

                    }
                    else
                    {
                        return UnprocessableEntity(
                           new APIResponse
                           {
                               status = false,
                               statuscode = (int)API_StatusCode.Error,
                               message = $"The user {model.Email} not found!"


                           });
                    }
                }
                else
                {
                    return UnprocessableEntity(

                         new APIResponse
                         {
                             status = false,
                             statuscode = (int)API_StatusCode.Error,
                             message = "Model State Error",
                             errorlist = ModelState.Where(e => e.Value.Errors.Count > 0).Select(e => new { Name = e.Key, Message = e.Value.Errors.First().ErrorMessage }).ToList()


                         });

                }



            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return BadRequest(
                  new APIResponse
                  {
                      status = false,
                      statuscode = (int)API_StatusCode.Exception,
                      message = $"Exception"
                  });


        }
    }
}