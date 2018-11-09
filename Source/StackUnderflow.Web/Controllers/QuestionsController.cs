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
    public class QuestionsController : Controller
    {
        private readonly QuestionsService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public QuestionsController(QuestionsService service, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: Questions
        public IActionResult Index()
        {
            return View( _service.GetQuestions());
        }

        // GET: Questions/Details/5
        public IActionResult Details(Guid id)
        {
            var question = _service.GetQuestionById(id);

            return View(question);
        }

        // GET: Questions/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Question question)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Question is invalid");
            }

            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            _service.CreateQuestion(question, user);

            return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var question = _service.GetQuestionById(id);

            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] Question question)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Question is not valid.");
            }

            _service.UpdateQuestion(question);

            return View(question);
        }

        // POST: Questions/UpVote/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Questions/UpVote/{id}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult UpVote(string id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Question is not valid.");
            }

            var question = _service.UpVote(id);

            return View(question);
        }

        // POST: Questions/DownVote/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Questions/DownVote/{id}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DownVote(string id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Question is not valid.");
            }

            var question = _service.DownVote(id);

            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var question = _service.GetQuestionById(id);

            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteQuestion(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
