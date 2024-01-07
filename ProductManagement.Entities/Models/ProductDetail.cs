using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Entities.Models {
    public class ProductDetail {
        public int Id { get; set; }

        public string Description { get; set; }
        public int NumberOfViews { get; set; }

        public bool? IsBestSeller { get; set; }

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

    }
}
