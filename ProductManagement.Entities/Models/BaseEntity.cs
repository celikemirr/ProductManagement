using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities.Models {
    public class BaseEntity {
        [Key]
        public int Id { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
