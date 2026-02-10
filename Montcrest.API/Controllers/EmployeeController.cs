using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.BLL.DTOs.Leave;
using Montcrest.BLL.Interfaces;
using System.Security.Claims;

namespace Montcrest.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public EmployeeController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        
        private int GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new Exception("UserId not found in token");

            return int.Parse(userIdClaim.Value);
        }

        
        [HttpPost("leave/apply")]
        public async Task<IActionResult> ApplyLeave([FromBody] ApplyLeaveRequestDto dto)
        {
            var userId = GetUserId();

            await _leaveService.ApplyLeaveAsync(userId, dto);

            return Ok(new { message = "Leave applied successfully" });
        }

        
        [HttpGet("leave/my")]
        public async Task<IActionResult> GetMyLeaves()
        {
            var userId = GetUserId();

            var data = await _leaveService.GetMyLeavesAsync(userId);

            return Ok(data);
        }
    }
}
