using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.BLL.DTOs.Hr;
using Montcrest.BLL.Interfaces;

namespace Montcrest.API.Controllers
{
    [Route("api/hr")]
    [ApiController]
    [Authorize(Roles = "HR")]
    public class HrController : ControllerBase
    {
        private readonly IHrService _hrService;
        private readonly IRecruitmentWorkflowService _workflowService;

        public HrController(IHrService hrService, IRecruitmentWorkflowService workflowService)
        {
            _hrService = hrService;
            _workflowService = workflowService;
        }

        

        [HttpGet("technologies")]
        public async Task<IActionResult> GetTechnologies()
        {
            var data = await _hrService.GetTechnologiesAsync();
            return Ok(data);
        }

        [HttpGet("applications/{technologyId}")]
        public async Task<IActionResult> GetApplicationsByTechnology(int technologyId)
        {
            var data = await _hrService.GetApplicantsByTechnologyAsync(technologyId);
            return Ok(data);
        }


        [HttpGet("review/{applicationId}")]
        public async Task<IActionResult> GetApplicationForReview(int applicationId)
        {
            var data = await _workflowService.GetApplicationForReviewAsync(applicationId);
            return Ok(data);
        }

        [HttpPost("review/{applicationId}")]
        public async Task<IActionResult> ReviewApplication(int applicationId, [FromBody] ReviewApplicationRequestDto dto)
        {
            await _workflowService.ReviewApplicationAsync(applicationId, dto.Remarks);
            return Ok(new { message = "Application reviewed successfully" });
        }

        [HttpPost("reject/{applicationId}")]
        public async Task<IActionResult> RejectApplication(int applicationId, [FromBody] RejectApplicationRequestDto dto)
        {
            await _workflowService.RejectApplicationAsync(applicationId, dto.Remarks);
            return Ok(new { message = "Application rejected successfully" });
        }

        

        [HttpPost("send-exam/{applicationId}")]
        public async Task<IActionResult> SendExam(int applicationId, [FromBody] SendExamRequestDto dto)
        {
            await _workflowService.SendExamAsync(applicationId, dto.ExamLink);
            return Ok(new { message = "Exam link sent successfully" });
        }

        [HttpPost("sync-score/{applicationId}")]
        public async Task<IActionResult> SyncScore(int applicationId)
        {
            await _workflowService.SyncExamScoreAsync(applicationId);
            return Ok(new { message = "Score synced successfully" });
        }
    }
}
