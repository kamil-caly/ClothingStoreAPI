using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos.Dispaly
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string Size { get; set; }
        public DateTime? ProductionYear { get; set; }
        public string ProductionCountry { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual List<ProductReviewDto> ProductReviews { get; set; }
    }
}
