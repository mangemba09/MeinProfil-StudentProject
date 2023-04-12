namespace WebMeinProfilMvc.Models
{
    public class EditView
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public IFormFile ImagePath { get; set; }
    }
}
