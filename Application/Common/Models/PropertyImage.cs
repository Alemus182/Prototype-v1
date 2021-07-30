using System;
using System.Collections.Generic;

#nullable disable

namespace Infraestructure.Models
{
    public partial class PropertyImage
    {
        public int IdPropoertyImage { get; set; }
        public int IdProperty { get; set; }
        public string FileImage { get; set; }
        public bool Enabled { get; set; }

        public virtual Property IdPropertyNavigation { get; set; }
    }
}
