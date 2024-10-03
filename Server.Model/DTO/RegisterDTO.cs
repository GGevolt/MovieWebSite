using System.ComponentModel.DataAnnotations;

namespace Server.Model.DTO
{
    public class RegisterDTO
    {
        [MaxLength(100)]
        public required string FullName { get; set; }
        public required string Gender { get; set; }
        public DateOnly Dob { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [DataType(DataType.Password)]
        public required string PasswordHash { get; set; }
        public required string UserName { get; set; }
        public required string EmailConfirnUrl { get; set; }
    }
}
