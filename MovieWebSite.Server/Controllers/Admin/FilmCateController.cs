using Microsoft.AspNetCore.Mvc;
using Server.Model.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmCateController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        [HttpGet("{id}")]
        public IActionResult GetFilmCate(int id)
        {
            try
            {
                List<Category> filmCate = new List<Category>();
                var cateIds = _unitOfWork.CategoryFilmRepository.GetAll().Where(cf => cf.FilmId == id).Select(cf => cf.CategoryId);
                foreach (var cateId in cateIds)
                {
                    filmCate.Add(_unitOfWork.CategoryRepository.Get(c => c.Id == cateId));
                }
                return Ok(filmCate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
