﻿using Microsoft.AspNetCore.Mvc;
using Server.Model.Models;
using MovieWebSite.Server.Repository.IRepository;

namespace MovieWebSite.Server.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_unitOfWork.CategoryRepository.GetAll());
        }

        [HttpPost]
        public IActionResult CreateUpdateCategory([FromBody] Category category)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
                _unitOfWork.CategoryRepository.Remove(category);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}