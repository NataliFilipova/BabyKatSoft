﻿using BabyKat.Core.Contracts;
using BabyKat.Core.Models.Articlesss;
using BabyKat.Core.Models.Commentt;
using BabyKat.Core.Models.Productt;
using BabyKat.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BabyKat.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        public CommentController(ICommentService _commentService)
        {
            this.commentService = _commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add(int articleId)
        {
            var model = new CommentModel()
            {
                ArticleId = articleId,
            };
                
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CommentModel model)
        {
           
                await commentService.AddComment(model);

                return RedirectToAction("All", "Article");
            
        }

        public async Task<IActionResult> Remove(int commentId)
        {
            int articleId = await commentService.DeleteComment(commentId);
            return RedirectToAction("Read","Article", new {articleId = articleId});
        }
    }
}
