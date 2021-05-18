using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
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
    public class ItemsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;

        public ItemsController(IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<List<ItemTypeModel>> GetItemTypes()
        {
            ItemTypeData itd = new ItemTypeData(_config);
            return await itd.GetItemTypes();
        }
        [HttpPost]
        [Route("Pending")]
        public async Task<IActionResult> AddPendingItem(AddPendingItemModel aPIM)
        {
            ItemTypeData data = new ItemTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await data.AddItemToAuthorize(aPIM, userID);
            

            return Ok();
        }
    }
}

