using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Entities.Models {
    public class Product : BaseEntity {
        public string Name { get; set; }
        public float Price { get; set; } 
        public string? ImageURL { get; set; }

        public virtual ProductDetail? ProductDetail { get; set; }
    }
}
