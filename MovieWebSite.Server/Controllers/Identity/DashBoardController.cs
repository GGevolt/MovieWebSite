using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieWebSite.Server.Repository;
using Server.Model.AuthModels;
using Server.Model.DTO;
using System;
using System.Linq;

namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UnitOfWorks _unitOfWorks;
        private readonly StripeSettingDTO _settings;

        public DashboardController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UnitOfWorks unitOfWorks, IOptions<StripeSettingDTO> settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWorks = unitOfWorks;
            _settings = settings.Value;
        }

        [HttpGet("subscription-status")]
        public IActionResult GetSubscriptionStatusData()
        {
            var subscriptionData = _userManager.Users
                .GroupBy(u => u.SubscriptionStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();

            return Ok(subscriptionData);
        }

        [HttpGet("subscription-trend")]
        public IActionResult GetSubscriptionTrend()
        {
            var trendData = _userManager.Users
                .Where(u => u.SubscriptionStartPeriod.HasValue)
                .GroupBy(u => new {
                    Year = u.SubscriptionStartPeriod.Value.Year,
                    Month = u.SubscriptionStartPeriod.Value.Month,
                    PlanType = u.PriceId == _settings.ProPriceId ? "Pro" : "Premium"
                })
                .Select(g => new {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    PlanType = g.Key.PlanType,
                    Count = g.Count(),
                    MonthlyRevenue = g.Key.PlanType == "Pro" ? g.Count() * 19.99m : g.Count() * 180m / 12
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Ok(trendData);
        }

        [HttpGet("content-popularity")]
        public IActionResult GetContentPopularity()
        {
            var popularContent = _unitOfWorks.UserFilmRepository.GetAll()
                .GroupBy(uf => uf.FilmId)
                .Select(g => new {
                    FilmId = g.Key,
                    Title = g.First().Film.Title,
                    ViewCount = g.Count(uf => uf.ViewedOn.HasValue),
                    AverageRating = g.Average(uf => uf.Rating ?? 0)
                })
                .OrderByDescending(x => x.ViewCount)
                .Take(10)
                .ToList();

            return Ok(popularContent);
        }

        [HttpGet("user-demographics")]
        public IActionResult GetUserDemographics()
        {
            var currentYear = DateTime.Now.Year;
            var demographics = _userManager.Users
                .GroupBy(u => (currentYear - u.Dob.Year) / 10 * 10)
                .Select(g => new {
                    AgeGroup = $"{g.Key}-{g.Key + 9}",
                    Count = g.Count()
                })
                .OrderBy(x => x.AgeGroup)
                .ToList();

            return Ok(demographics);
        }

        [HttpGet("genre-popularity")]
        public IActionResult GetGenrePopularity()
        {
            var genrePopularity = _unitOfWorks.CategoryFilmRepository.GetAll()
                .GroupBy(cf => cf.Category.Name)
                .Select(g => new {
                    Genre = g.Key,
                    FilmCount = g.Count(),
                    ViewCount = g.Sum(cf => cf.Film.UserFilms.Count(uf => uf.ViewedOn.HasValue))
                })
                .OrderByDescending(x => x.ViewCount)
                .ToList();

            return Ok(genrePopularity);
        }

        [HttpGet("user-retention")]
        public IActionResult GetUserRetention()
        {
            var retentionData = _userManager.Users
                .GroupBy(u => new { Year = u.CreatedDate.Year, Month = u.CreatedDate.Month })
                .Select(g => new {
                    CohortDate = new DateTime(g.Key.Year, g.Key.Month, 1),
                    TotalUsers = g.Count(),
                    ActiveUsers = g.Count(u => u.LastLogin >= DateTime.Now.AddDays(-30))
                })
                .OrderBy(x => x.CohortDate)
                .ToList();

            return Ok(retentionData);
        }
    }
}