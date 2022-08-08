using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime? ProductionYear { get; set; }
        public string ProductionCountry { get; set; }
    }
}
