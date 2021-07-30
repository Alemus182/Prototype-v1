using System;

namespace Application.Dtos.Properties
{
    public class FindPropertiesResponse
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string CodeInternal { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string NameOwner { get; set; }
        public string AddressOwner { get; set; }
        public DateTime BirthdayOwner { get; set; }
    }
}
