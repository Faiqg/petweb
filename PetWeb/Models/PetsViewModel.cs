using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetWeb.Models
{
    public class PetsViewModel
    {
        public string OwnerName { get; set; }
        public string OwnerGender { get; set; }
        public int OwnerAge { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string City { get; set; }
    }
}
