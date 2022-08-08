using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos.Dispaly
{
    public class ClothingStoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string Headquaters { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? Incame { get; set; }
        public string OwnerContactEmail { get; set; }
        public string OwnerContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public virtual List<StoreReviewDto> StoreReviews { get; set; }
        public virtual List<ProductDto> Products { get; set; }
    }
}
