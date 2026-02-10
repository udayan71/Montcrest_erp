using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.Web.Services;
using Montcrest.Web.ViewModels.Employee;

namespace Montcrest.Web.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApiClient _api;

        public EmployeeController(ApiClient api)
        {
            _api = api;
        }

        // Dashboard
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: Apply Leave Page
        [HttpGet]
        public IActionResult ApplyLeave()
        {
            return View(new ApplyLeaveViewModel());
        }

        // POST: Apply Leave Form Submit
        [HttpPost]
        public async Task<IActionResult> ApplyLeave(ApplyLeaveViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _api.PostAsync("/api/employee/leave/apply", model);

            TempData["Success"] = "Leave application submitted successfully!";
            return RedirectToAction("MyLeaves");
        }

        // GET: Employee Leaves List
        [HttpGet]
        public async Task<IActionResult> MyLeaves()
        {
            var leaves = await _api.GetAsync<List<EmployeeLeaveViewModel>>(
                "/api/employee/leave/my"
            );

            return View(leaves);
        }
    }
}
