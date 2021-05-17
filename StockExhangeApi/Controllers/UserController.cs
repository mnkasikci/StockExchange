using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
using StockExhangeApi.Data;
using System;
using System.Collections.Generic;
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

        public UserController(ApplicationDbContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet]
        public UserModel GetByID()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserData data = new UserData(_config);
            return data.GetUserById(userID);
        }

    }
}
