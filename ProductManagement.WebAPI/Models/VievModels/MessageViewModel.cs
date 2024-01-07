using System.ComponentModel.DataAnnotations;

namespace ProductManagement.WebAPI.Models.VievModels
{
    public class MessageCreateViewModel
    {
        [Required(ErrorMessage = "Bu alan gerekli")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Bu alan gerekli")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Bu alan gerekli")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Bu alan gerekli")]
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Bu alan gerekli")]
        public string Email { get; set; }
    }

    public class MessageUpdateViewModel
    {
        public int Id { get; set; }
        public bool status { get; set; }

    }
}
