using System;
using System.Collections.Generic;

namespace OrderManagementApi.Models
{
    public partial class OrderMaster
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public int ItemId { get; set; }
       

        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
    public partial class OrderList
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public int ItemId { get; set; }
        public string ClientName { get; set; }
        public string ItemName { get; set; }

        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
