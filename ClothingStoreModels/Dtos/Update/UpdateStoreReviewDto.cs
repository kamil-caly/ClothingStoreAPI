using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreModels.Dtos.Update
{
    public class UpdateStoreReviewDto
    {
        public string Comment { get; set; }
        [Range(1, 5)]
        [Required]
        public int Rating { get; set; }
    }
}
