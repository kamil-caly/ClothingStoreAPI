using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos.Dispaly
{
    public class OrderDto
    {
        public string Description { get; set; }
        public DateTime CreatedOrderDate { get; set; } = DateTime.Now;
        public string ProductName { get; set; }        
        public decimal ProductPrice { get; set; }       
        public string ProductSize { get; set; }      
        public string ProductGender { get; set; }       
        public int ProductQuantity { get; set; }
        public bool IsBought { get; set; } = false;
    }
}
