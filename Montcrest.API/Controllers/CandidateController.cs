using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Models;
using System.Security.Claims;

[Authorize(Roles = "Candidate")]
[ApiController]
[Route("api/candidate")]
public class CandidateController : ControllerBase
{
    private readonly IJobApplicationService _jobService;

    public CandidateController(IJobApplicationService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet("applications/review/{applicationId}")]
    public async Task<IActionResult> ReviewApplication(int applicationId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        var result = await _jobService.GetReviewAsync(applicationId, userId);

        if (result == null)
            return NotFound("Application not found");

        return Ok(result);
    }

    [HttpGet("applications")]
    public async Task<IActionResult> GetMyApplications()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var apps = await _jobService.GetApplicationsByUserAsync(userId);

        return Ok(apps.Select(a => new
        {
            ApplicationId = a.Id,                
            TechnologyName = a.Technology!.Name, 
            Status = a.Status.ToString()
        }));
    }


}
