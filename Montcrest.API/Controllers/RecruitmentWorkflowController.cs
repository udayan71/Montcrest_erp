//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Montcrest.BLL.DTOs.Hr;
//using Montcrest.BLL.Interfaces;

//namespace Montcrest.API.Controllers
//{
//    [ApiController]
//    [Route("api/hr/workflow")]
//    [Authorize(Roles = "HR")]
//    public class RecruitmentWorkflowController : ControllerBase
//    {
//        private readonly IRecruitmentWorkflowService _service;

//        public RecruitmentWorkflowController(IRecruitmentWorkflowService service)
//        {
//            _service = service;
//        }

        
//        [HttpGet("review/{applicationId:int}")]
//        public async Task<IActionResult> GetForReview(int applicationId)
//        {
//            var result = await _service.GetApplicationForReviewAsync(applicationId);
//            return Ok(result);
//        }

       
//        [HttpPost("review/{applicationId:int}")]
//        public async Task<IActionResult> Review(int applicationId, [FromBody] ReviewApplicationRequestDto dto)
//        {
//            await _service.ReviewApplicationAsync(applicationId, dto.Remarks);
//            return Ok(new { message = "Application reviewed successfully" });
//        }

       
//        [HttpPost("send-exam/{applicationId:int}")]
//        public async Task<IActionResult> SendExam(int applicationId, [FromBody] SendExamRequestDto dto)
//        {
//            await _service.SendExamAsync(applicationId, dto.ExamLink);
//            return Ok(new { message = "Exam link sent successfully" });
//        }

        
//        [HttpPost("submit-score/{applicationId:int}")]
//        public async Task<IActionResult> SubmitScore(int applicationId, [FromBody] SubmitScoreRequestDto dto)
//        {
//            await _service.SubmitExamResultAsync(applicationId, dto.Score);
//            return Ok(new { message = "Exam score submitted successfully" });
//        }

        
//        [HttpPost("reject/{applicationId:int}")]
//        public async Task<IActionResult> Reject(int applicationId, [FromBody] RejectApplicationRequestDto dto)
//        {
//            await _service.RejectApplicationAsync(applicationId, dto.Remarks);
//            return Ok(new { message = "Candidate rejected" });
//        }
//    }
//}
