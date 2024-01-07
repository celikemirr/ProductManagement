using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Entities.Models {
    public class User : BaseEntity {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
