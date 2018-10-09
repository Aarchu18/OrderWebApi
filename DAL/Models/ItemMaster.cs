using System;
using System.Collections.Generic;

namespace OrderManagementApi.Models
{
    public partial class ItemMaster
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCategory { get; set; }
        public int ItemQuantity { get; set; }
    }
}
