using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Internal.Models;
using StockExchangeDataManager.Library.Models;
using StockExhangeApi.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockExhangeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegistrationModel userRegistrationData)
        {
            UserSqlData data = new UserSqlData(_config);


            var userByID = await _userManager.FindByNameAsync(userRegistrationData.UserName);

            if (userByID != null)
            {
                var result = new BadRequestObjectResult(new { message = "Another User with this name already exists", currentDate = DateTime.Now });
                return result;
            }

            var userByEmail = await _userManager.FindByEmailAsync(userRegistrationData.EmailAddress);

            if (userByEmail != null)
            {
                var result = new BadRequestObjectResult(new { message = "Another User with this e-mail address already exists", currentDate = DateTime.Now });
                return result;
            }

            if (!await data.TCIDNumberNotExist(userRegistrationData.TCIDNumber))
            {
                var result = new BadRequestObjectResult(new { message = "Another User with this TC ID Number already exists", currentDate = DateTime.Now });
                return result;
            }

            IdentityUser newUser = new IdentityUser()
            {
                Email = userRegistrationData.EmailAddress,
                EmailConfirmed = true,
                UserName = userRegistrationData.UserName,
            };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, userRegistrationData.Password);
            if (identityResult.Succeeded)
            {
                var newIDData = await _userManager.FindByNameAsync(newUser.UserName);
                var newID = newIDData.Id;

                UserDataModel userDataModel = new UserDataModel(userRegistrationData, newID);
                SqlUserModel sqlUserModel = new SqlUserModel(userDataModel);

                try
                {
                    await data.SaveUserToSqlDB(sqlUserModel);
                    return Ok();
                }
                catch (Exception ex)
                {
                    var removeIdentityResult = await _userManager.DeleteAsync(newUser);

                    if (removeIdentityResult.Succeeded)
                    {
                        var couldntCreateSql = new BadRequestObjectResult(new
                        {
                            message = "Couldn't create the user, please try again"
                            ,
                            currentDate = DateTime.Now
                        });
                    }
                    else
                    {
                        var fatalErrorResult = new BadRequestObjectResult(new
                        {
                            message = "Couldn't create the user, but can not remove the data you entered either. Please contact an admin\n" +
                            String.Join(Environment.NewLine, from e in removeIdentityResult.Errors select e.Description)
                            ,
                            currentDate = DateTime.Now
                        });
                        return fatalErrorResult;
                    }
                }



            }
            var errorResult = new BadRequestObjectResult(new { message = String.Join(Environment.NewLine, from e in identityResult.Errors select e.Description), currentDate = DateTime.Now });
            return errorResult;
        }


        [HttpGet]
        public async Task<UserDataModel> GetByID()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            string userUsername = User.FindFirstValue(ClaimTypes.Name);


            UserSqlData data = new UserSqlData(_config);

            //var a = _context.Roles.ToList();
            //var b = _context.UserRoles.ToList(); 


            UserDataModel um = new UserDataModel(await data.GetUserById(userID), userUsername, userEmail);
            return um;

        }

    }
}
