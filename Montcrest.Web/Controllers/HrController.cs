using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.Web.Services;
using Montcrest.Web.ViewModels.Hr;

namespace Montcrest.Web.Controllers
{
    [Authorize(Roles = "HR")]
    public class HrController : Controller
    {
        private readonly ApiClient _api;
        private readonly IConfiguration _config;
        public HrController(ApiClient api, IConfiguration config)
        {
            _api = api;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Technologies()
        {
            var technologies = await _api.GetAsync<List<HrTechnologyViewModel>>(
                "/api/hr/technologies");

            return View(technologies);
        }

        [HttpGet]
        public async Task<IActionResult> Applicants(int technologyId)
        {
            var applicants = await _api.GetAsync<List<HrApplicantViewModel>>(
                $"/api/hr/applications/{technologyId}");

            var apiBaseUrl = _config["ApiSettings:BaseUrl"]!.TrimEnd('/');

            foreach (var a in applicants)
            {
                if (!string.IsNullOrEmpty(a.Resume))
                {
                    // If already absolute, leave it
                    if (!a.Resume.StartsWith("http"))
                    {
                        a.Resume = $"{apiBaseUrl}{a.Resume}";
                    }
                }
            }

            return View(applicants);
        }



        [HttpGet]
        public async Task<IActionResult> Review(int applicationId)
        {
            var model = await _api.GetAsync<HrApplicantReviewViewModel>(
                $"/api/hr/review/{applicationId}");

            var apiBaseUrl = _config["ApiSettings:BaseUrl"]!.TrimEnd('/');

            if (!string.IsNullOrEmpty(model.ResumePath) &&
                !model.ResumePath.StartsWith("http"))
            {
                model.ResumePath = $"{apiBaseUrl}/{model.ResumePath.TrimStart('/')}";
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Approve(int applicationId,int technologyId, string remarks)
        {
            var payload = new { remarks };

            await _api.PostAsync(
                $"/api/hr/review/{applicationId}",
                payload);

            TempData["Success"] = "Application reviewed successfully.";
            return RedirectToAction("Applicants", new { technologyId });
        }

        
        [HttpPost]
        public async Task<IActionResult> Reject(int applicationId, string remarks)
        {
            var payload = new { remarks };

            await _api.PostAsync(
                $"/api/hr/reject/{applicationId}",
                payload);

            TempData["Error"] = "Application rejected.";
            return RedirectToAction("Review", new { applicationId });
        }


        [HttpPost]
        public async Task<IActionResult> SyncScore(int applicationId, int technologyId)
        {
            try
            {
                await _api.PostAsync(
                    $"/api/hr/sync-score/{applicationId}",
                    new { });

                TempData["Success"] = "Score synced successfully. Candidate result updated!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Applicants", new { technologyId });
        }


        [HttpGet]
        public async Task<IActionResult> ConductExam(int applicationId)
        {
            var review = await _api.GetAsync<HrApplicantReviewViewModel>(
                $"/api/hr/review/{applicationId}");

            var model = new ConductExamViewModel
            {
                ApplicationId = review.ApplicationId,
                CandidateName = review.CandidateName,
                Email = review.Email,
                TechnologyName = review.TechnologyName,
                TechnologyId = review.TechnologyId
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConductExam(ConductExamViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var payload = new { examLink = model.ExamLink };

            await _api.PostAsync(
                $"/api/hr/send-exam/{model.ApplicationId}",
                payload);

            TempData["Success"] = "Exam link sent successfully to candidate.";

            return RedirectToAction("Applicants", new { technologyId = model.TechnologyId });
        }


    }
}
