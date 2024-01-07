using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Entities.Models {
    public class About : BaseEntity{
        public string? ImageURL { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
