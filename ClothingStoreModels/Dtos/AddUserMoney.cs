using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos
{
    public class AddUserMoney
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Money { get; set; }
    }
}
