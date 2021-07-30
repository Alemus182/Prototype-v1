using System;
using System.Collections.Generic;

#nullable disable

namespace Infraestructure.Models
{
    public partial class PropertyHistory
    {
        public int IdPropertyHistory { get; set; }
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public DateTime Updated { get; set; }
        public string Action { get; set; }
    }
}
