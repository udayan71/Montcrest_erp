using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.Web.Services;
using Montcrest.Web.ViewModels.Manager;

namespace Montcrest.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly ApiClient _api;

        public ManagerController(ApiClient api)
        {
            _api = api;
        }

        // GET: Manager Dashboard
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: List of Leave Applications
        [HttpGet]
        public async Task<IActionResult> LeaveApplications()
        {
            var leaves = await _api.GetAsync<List<ManagerLeaveApplicationViewModel>>(
                "/api/manager/leave-applications"
            );

            return View(leaves);
        }

        // POST: Approve Leave
        [HttpPost]
        public async Task<IActionResult> ApproveLeave(int leaveId, string remarks)
        {
            var payload = new { remarks };

            await _api.PostAsync(
                $"/api/manager/leave-applications/{leaveId}/approve",
                payload
            );

            TempData["Success"] = "Leave Approved Successfully!";
            return RedirectToAction("LeaveApplications");
        }

        // POST: Reject Leave
        [HttpPost]
        public async Task<IActionResult> RejectLeave(int leaveId, string remarks)
        {
            var payload = new { remarks };

            await _api.PostAsync(
                $"/api/manager/leave-applications/{leaveId}/reject",
                payload
            );

            TempData["Success"] = "Leave Rejected Successfully!";
            return RedirectToAction("LeaveApplications");
        }
    }
}
