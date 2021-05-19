using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
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
        [HttpGet]
        [Route("Pending")]
        [Authorize(Roles = "Admin")]
        public async Task<List<PendingItemModel>> GetAllPendingItems()
        {
            ItemTypeData data = new ItemTypeData(_config);
            return await data.GetAllPendingItems();

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AuthorizePendingItem(PendingItemModel pendingItem)
        {

            ItemTypeData data = new ItemTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await data.AuthorizePendingItem(pendingItem.PendingId, userID);
            return Ok();
        }
        [HttpPost]
        [Route("Refuse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RefusePendingItem(PendingItemModel pendingItem)
        {

            ItemTypeData data = new ItemTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await data.RefusePendingItem(pendingItem.PendingId, userID);
            return Ok();
        }
        [HttpGet]
        [Route("Inventory")]
        public async Task<List<UserItemModel>> GetUserItems()
        {
            ItemTypeData data = new ItemTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await data.GetUserItems(userID);
        }
        [HttpPost]
        [Route("SellOffers")]
        public async Task <IActionResult> CreateSellOffer(OfferModel offer)
        {
            if (offer.Amount <= 0 || offer.UnitPrice <= 0) return BadRequest();

            ItemTypeData data = new ItemTypeData(_config);
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            offer.OffererID = userID;
            
            var UserItems = await data.GetUserItems(userID);

            if (UserItems.Find(p => p.ItemTypeId == offer.ItemTypeID)?.Amount >= offer.Amount)
            {
                await data.CreateSellOffer(offer);
                return Ok();
            }
            else return BadRequest();

            // auto check for buy offers

        }

    }
}

