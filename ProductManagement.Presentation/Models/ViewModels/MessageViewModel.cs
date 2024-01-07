namespace ProductManagement.Presentation.Models.ViewModels
{
    public class MessageViewModel
    {
        public string fullName { get; set; }
        public string subject { get; set; }
        public string details { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int id { get; set; }
        public bool status { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class MessageCreateViewModel
    {
        public string fullName { get; set; }
        public string subject { get; set; }
        public string details { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

}

