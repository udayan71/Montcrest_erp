using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.Web.DTOs;
using Montcrest.Web.Services;
using Montcrest.Web.ViewModels;
using Montcrest.Web.ViewModels.Candidate;
using Montcrest.Web.ViewModels.Hr;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Montcrest.Web.Controllers
{
    public class CandidateController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiClient _api;
        private readonly IConfiguration _config;


        public CandidateController(IHttpClientFactory httpClientFactory,ApiClient api,IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _api = api;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var client = _httpClientFactory.CreateClient("MontcrestAPI");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("/api/technology");

            if (!response.IsSuccessStatusCode)
                return View(new List<TechnologyDto>());

            var json = await response.Content.ReadAsStringAsync();
            var technologies = JsonSerializer.Deserialize<List<TechnologyDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(technologies);
        }

        [HttpGet]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> Apply(int id)
        {
            var tech = await _api.GetAsync<TechnologyDto>($"/api/technology/{id}");

            var model = new ApplyJobViewModel
            {
                TechnologyId = tech.Id,
                TechnologyName = tech.Name
            };

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> Apply(ApplyJobViewModel model)
        {
            Console.WriteLine("MVC Apply POST hit");

            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("MontcrestAPI");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            Console.WriteLine(model.Resume == null
    ? "Resume is NULL in MVC"
    : $"Resume received: {model.Resume.FileName}, Size: {model.Resume.Length}");


            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.TechnologyId.ToString()), "TechnologyId");
            formData.Add(
                new StreamContent(model.Resume.OpenReadStream()),
                "Resume",
                model.Resume.FileName
            );

            var response = await client.PostAsync(
                "/api/jobapplication/apply",
                formData
            );

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to apply for job";
                return View(model);
            }

            TempData["Success"] = "Job application submitted successfully";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Review(int applicationId)
        {
            var model = await _api.GetAsync<CandidateReviewViewModel>(
                $"/api/candidate/applications/review/{applicationId}");

            // Fix resume URL (same logic you used in HR)
            var apiBaseUrl = _config["ApiSettings:BaseUrl"]!.TrimEnd('/');

            if (!string.IsNullOrEmpty(model.ResumePath) &&
                !model.ResumePath.StartsWith("http"))
            {
                model.ResumePath = $"{apiBaseUrl}/{model.ResumePath.TrimStart('/')}";
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Available()
        {
            var technologies = await _api.GetAsync<List<TechnologyDto>>(
                "/api/technology");

            var applications = await _api.GetAsync<List<MyApplicationDto>>(
                "/api/candidate/applications");

            var model = technologies.Select(t =>
            {
                var app = applications.FirstOrDefault(a => a.TechnologyId == t.Id);

                return new AvailableTechnologyViewModel
                {
                    TechnologyId = t.Id,
                    Name = t.Name,
                    HasApplied = app != null,
                    ApplicationId = app?.ApplicationId
                };
            }).ToList();

            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> MyApplications()
        {
            var applications = await _api.GetAsync<List<MyApplicationViewModel>>(
                "/api/candidate/applications");

            return View(applications);
        }


    }
}
