using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_unitOfWork.CategoryRepository.GetAll());
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult CreateUpdateCategory([FromBody] Category category)
        {
            if (category.Id == 0)
            {
                _unitOfWork.CategoryRepository.Add(category);
            }
            else
            {
                _unitOfWork.CategoryRepository.Update(category);
            }
            _unitOfWork.Save();
            return Ok();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
            _unitOfWork.CategoryRepository.Remove(category);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
