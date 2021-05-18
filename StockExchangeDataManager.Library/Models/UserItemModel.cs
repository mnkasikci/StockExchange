using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDataManager.Library.Models
{
	public class UserItemModel
	{
		public int ItemId { get; set; }
		public int ItemTypeId { get; set; }
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string 	ItemTypeName { get; set; }
		public int Amount { get; set; }
	}
}
