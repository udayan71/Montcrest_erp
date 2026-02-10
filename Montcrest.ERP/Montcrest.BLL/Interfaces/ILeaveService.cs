using Montcrest.BLL.DTOs.Leave;

namespace Montcrest.BLL.Interfaces
{
    public interface ILeaveService
    {
        Task ApplyLeaveAsync(int userId, ApplyLeaveRequestDto dto);

        Task<List<LeaveApplicationDto>> GetMyLeavesAsync(int userId);
    }
}
