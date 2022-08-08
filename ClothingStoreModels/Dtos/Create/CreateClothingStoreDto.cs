using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos
{
    public class CreateClothingStoreDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        public string Headquaters { get; set; }
        [Column(TypeName = "decimal(17,2)")]
        public decimal? Incame { get; set; }
        [Required]
        public string OwnerContactEmail { get; set; }
        [Required]
        public string OwnerContactNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        public string ApartamentNumber { get; set; }
        public string PostalCode { get; set; }
    }
}
