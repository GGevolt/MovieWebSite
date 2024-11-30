using Microsoft.AspNetCore.Identity;
using Server.Model.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Model.AuthModels
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateOnly Dob { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime LastLogin { get; set; } = DateTime.Now;
        public string? CustomerId { get; set; }
        public string? SubscriptionId { get; set; }
        public string? PriceId { get; set; }
        public string? SubscriptionStatus { get; set; }
        public DateTime? SubscriptionStartPeriod { get; set; }
        public DateTime? SubscriptionEndPeriod { get; set; }
        public virtual ICollection<UserFilm> UserFilms { get; set; }
    }
}
