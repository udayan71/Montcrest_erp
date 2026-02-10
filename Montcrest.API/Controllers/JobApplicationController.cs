using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.API.DTOs.JobApplications;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Models;
using System.Security.Claims;

namespace Montcrest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Candidate")] // ✔ Use literal string
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _service;
        private readonly IWebHostEnvironment _env;

        public JobApplicationController(
            IJobApplicationService service,
            IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        [HttpPost("apply")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Apply([FromForm] ApplyJobRequestDto applyJobRequest)
        {
            
            if (applyJobRequest.Resume == null || applyJobRequest.Resume.Length == 0)
                return BadRequest("Resume file is required");

            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
            var extension = Path.GetExtension(applyJobRequest.Resume.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Resume should be in PDF or Word format");

            if (applyJobRequest.Resume.Length > 15 * 1024 * 1024)
                return BadRequest("File size cannot exceed 15 MB");

            
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );


            var fileName = $"{Guid.NewGuid()}{extension}";

            var uploadPath = Path.Combine(
                _env.WebRootPath,
                "uploads",
                "resumes",
                $"user-{userId}",
                $"tech-{applyJobRequest.TechnologyId}"
            );

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await applyJobRequest.Resume.CopyToAsync(stream);
            }

            
            var relativePath = Path.Combine(
                "uploads",
                "resumes",
                $"user-{userId}",
                $"tech-{applyJobRequest.TechnologyId}",
                fileName
            ).Replace("\\", "/");

            
            await _service.ApplyAsync(
                userId,
                applyJobRequest.TechnologyId,
                relativePath
            );

            return Ok(new { message = "Job application submitted successfully" });
        }
    }
}
