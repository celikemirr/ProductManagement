using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Entities.Models {
    public class Contact : BaseEntity {
        public string? ImageURL { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
