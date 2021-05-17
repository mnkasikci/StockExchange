using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExhangeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ItemsController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public async Task<List<ItemTypeModel>> GetItemTypes()
        {
            ItemTypeData itd = new ItemTypeData(_config);
            return await itd.GetItemTypes();
        }
    }
}

