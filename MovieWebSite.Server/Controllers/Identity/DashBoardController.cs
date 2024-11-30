using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieWebSite.Server.Repository;
using MovieWebSite.Server.Repository.IRepository;
using Server.Model.AuthModels;
using Server.Model.DTO;


namespace MovieWebSite.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWorks;
        private readonly StripeSettingDTO _settings;

        public DashBoardController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWorks, IOptions<StripeSettingDTO> settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWorks = unitOfWorks;
            _settings = settings.Value;
        }

        [HttpGet("subscription-status")]
        public IActionResult GetSubscriptionStatusData()
        {
            try
            {
                var subscriptionData = _userManager.Users
                    .GroupBy(u => u.SubscriptionStatus == null ? "Not Subscribed" : u.SubscriptionStatus)
                    .Select(g => new { Status = g.Key, Count = g.Count() })
                    .ToList();

                return Ok(subscriptionData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching subscription status data: {ex.Message}");
            }
        }

        [HttpGet("content-popularity")]
        public IActionResult GetContentPopularity()
        {
            try
            {
                if (_unitOfWorks.UserFilmRepository == null)
                {
                    return StatusCode(500, "UserFilmRepository is not initialized");
                }

                var userFilms = _unitOfWorks.UserFilmRepository.GetAll(includeProperty: "Film");
                if (userFilms == null)
                {
                    return StatusCode(500, "Failed to retrieve user films");
                }

                var popularContent = userFilms
                    .AsQueryable()
                    .GroupBy(uf => uf.FilmId)
                    .Select(g => new
                    {
                        FilmId = g.Key,
                        Title = g.Select(uf => uf.Film != null ? uf.Film.Title : "Unknown").FirstOrDefault() ?? "Unknown",
                        ViewCount = g.Count(uf => uf.ViewedOn.HasValue),
                        AverageRating = g.Average(uf => uf.Rating.HasValue ? uf.Rating.Value : 0)
                    })
                    .OrderByDescending(x => x.ViewCount)
                    .Take(10)
                    .ToList();

                return Ok(popularContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching content popularity: {ex.Message}");
            }
        }

        [HttpGet("user-demographics")]
        public IActionResult GetUserDemographics()
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var demographics = _userManager.Users
                    .Where(u => u.Dob != default)
                    .GroupBy(u => (currentYear - u.Dob.Year) / 10 * 10)
                    .Select(g => new
                    {
                        AgeGroup = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(x => x.AgeGroup)
                    .AsEnumerable()
                    .Select(x => new
                    {
                        AgeGroup = $"{x.AgeGroup}-{x.AgeGroup + 9}",
                        x.Count
                    })
                    .ToList();

                return Ok(demographics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching user demographics: {ex.Message}");
            }
        }

        [HttpGet("genre-popularity")]
        public IActionResult GetGenrePopularity()
        {
            try
            {
                if (_unitOfWorks.CategoryFilmRepository == null)
                {
                    return StatusCode(500, "CategoryFilmRepository is not initialized");
                }

                var categoryFilms = _unitOfWorks.CategoryFilmRepository.GetAll(includeProperty: "Category");
                if (categoryFilms == null)
                {
                    return StatusCode(500, "Failed to retrieve category films");
                }

                var filmIds = categoryFilms.Select(cf => cf.FilmId).Distinct().ToList();
                var films = _unitOfWorks.FilmRepository.GetAll(f => filmIds.Contains(f.Id), includeProperty: "UserFilms");

                var genrePopularity = categoryFilms
                    .GroupBy(cf => cf.Category != null ? cf.Category.Name : "Unknown")
                    .Select(g => new {
                        Genre = g.Key,
                        ViewCount = g.Sum(cf => {
                            var film = films.FirstOrDefault(f => f.Id == cf.FilmId);
                            return film != null ? film.UserFilms.Count(uf => uf.ViewedOn.HasValue) : 0;
                        })
                    })
                    .OrderByDescending(x => x.ViewCount)
                    .ToList();

                return Ok(genrePopularity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching genre popularity: {ex.Message}");
            }
        }
    }
}