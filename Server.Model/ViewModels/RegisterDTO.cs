using System.ComponentModel.DataAnnotations;

namespace Server.Model.ViewModels
{
    public class RegisterDTO
    {
        [MaxLength(40)]
        public string FirstName { get; set; }
        [MaxLength(40)]
        public string LastName { get; set; }
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public string UserName { get; set; }

    }
}
