using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos.Dispaly
{
    public class BasketDto
    {
        public int CreatedById { get; set; }
        public virtual List<OrderDto> Orders { get; set; }
    }
}
