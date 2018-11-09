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
    public class ResponsesController : Controller
    {
        private readonly ResponsesService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public ResponsesController(ResponsesService service, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: Responses
        public IActionResult Index()
        {
            return View(_service.GetResponses());
        }

        // GET: Responses/Details/5
        public IActionResult Details(Guid id)
        {
            var response = _service.GetResponseById(id);

            return View(response);
        }

        // GET: Responses/Create
        [Authorize]
        public IActionResult Create(string id)
        {
            ViewBag.QuestionId = id;
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Responses/Create/{id}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string id, [FromForm] Response response)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Response is not valid");
            }

            response.QuestionId = new Guid(id);

            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            var newResponse = _service.CreateResponse(response, user);

            return View(newResponse);
        }

        // GET: Responses/Edit/5
        [Authorize]
        public IActionResult Edit(Guid id)
        {

            var response = _service.GetResponseById(id);

            return View(response);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] Response response)
        {

            if (!ModelState.IsValid)
            {
                throw new Exception("Response is not valid.");
            }

            var updatedResponse = _service.UpdateResponse(response);

            return View(updatedResponse);
        }

        // GET: Responses/Delete/5
        [Authorize]
        public IActionResult Delete(Guid id)
        {

            var response = _service.GetResponseById(id);

            return View(response);
        }

        // POST: Responses/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteResponse(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
