using System;
using System.Collections.Generic;

namespace OrderManagementApi.Models
{
    public partial class ClientMaster
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public long ClientContact { get; set; }
        public string ClientAddress { get; set; }
    }
}
