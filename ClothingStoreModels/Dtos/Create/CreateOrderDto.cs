using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos.Create
{
    public class CreateOrderDto
    {
        public string Description { get; set; }
        public DateTime CreatedOrderDate { get; set; } = DateTime.Now;
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal ProductPrice { get; set; }
        [Required]
        public string ProductSize { get; set; }
        [Required]
        public string ProductGender { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        public bool IsBought { get; set; } = false;
    }
}
