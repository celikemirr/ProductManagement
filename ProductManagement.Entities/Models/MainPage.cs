using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities.Models {
    public class MainPage : BaseEntity {

        public string? ImageURL { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
