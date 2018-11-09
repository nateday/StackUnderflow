using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StackUnderflow.Business;
using StackUnderflow.Data;
using StackUnderflow.Data.Entities;

namespace StackUnderflow.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CommentsService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentsController(CommentsService service, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: Comments
        public IActionResult Index()
        {
            return View(_service.GetComments());
        }

        // GET: Comments/Details/5
        public IActionResult Details(Guid id)
        {
            var comment = _service.GetCommentById(id);
            return View(comment);
        }

        // GET: Comments/Create
        [Authorize]
        public IActionResult Create(string id)
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Comments/Create/{id}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string id, [FromForm] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Comment is not valid");
            }

            comment.ResponseId = new Guid(id);
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var newComment = _service.CreateComment(comment, user);

            return View(newComment);
        }

        // GET: Comments/Edit/5
        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var comment = _service.GetCommentById(id);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [FromForm] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Comment is not valid.");
            }

            var updatedComment = _service.UpdateComment(comment);

            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var comment = _service.GetCommentById(id);

            return View(comment);
        }

        // POST: Comments/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteComment(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
