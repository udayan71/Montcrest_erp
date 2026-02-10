using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montcrest.BLL.DTOs.Manager;
using Montcrest.BLL.Interfaces;
using System.Security.Claims;

namespace Montcrest.API.Controllers
{
    [Route("api/manager")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpGet("leave-applications")]
        public async Task<IActionResult> GetLeaveApplications()
        {
            var userId = GetUserId();
            var data = await _managerService.GetLeaveApplicationsAsync(userId);
            return Ok(data);
        }

        [HttpPost("leave-applications/{leaveId}/approve")]
        public async Task<IActionResult> ApproveLeave(int leaveId, [FromBody] ReviewLeaveRequestDto dto)
        {
            var userId = GetUserId();

            await _managerService.ApproveLeaveAsync(userId, leaveId, dto.Remarks);

            return Ok(new { message = "Leave approved successfully" });
        }

        [HttpPost("leave-applications/{leaveId}/reject")]
        public async Task<IActionResult> RejectLeave(int leaveId, [FromBody] ReviewLeaveRequestDto dto)
        {
            var userId = GetUserId();

            await _managerService.RejectLeaveAsync(userId, leaveId, dto.Remarks);

            return Ok(new { message = "Leave rejected successfully" });
        }
    }
}
