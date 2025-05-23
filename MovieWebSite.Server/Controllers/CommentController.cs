﻿using Microsoft.AspNetCore.Mvc;
using MovieWebSite.Server.Models;
using MovieWebSite.Server.Repository.IRepository;
using System.Diagnostics;

namespace MovieWebSite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        [HttpGet("{EpisodeId}")]
        public IActionResult GetEpisodeComment(int EpisodeId) {
            var comments = _unitOfWork.CommnentRepository.GetAll(e => e.EpisodeId == EpisodeId);
            return Ok(comments);
        }
        [HttpPost]
        public IActionResult PostComment(Comment comment) {
            var NewComment = new Comment() { 
                CommnentText = comment.CommnentText,
                UserName = comment.UserName,
                EpisodeId = comment.EpisodeId
            };
            try {
                _unitOfWork.CommnentRepository.Add(NewComment);
                _unitOfWork.Save();
                return Ok();
            } catch (Exception ex) {
                Debug.WriteLine("💥Fail to add Comment! ", ex.Message);
                return BadRequest();
            } 
        }
    }
}
