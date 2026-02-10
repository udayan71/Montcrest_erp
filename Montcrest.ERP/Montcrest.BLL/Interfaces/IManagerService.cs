using Montcrest.BLL.DTOs.Manager;

namespace Montcrest.BLL.Interfaces
{
    public interface IManagerService
    {
        Task<List<ManagerLeaveApplicationDto>> GetLeaveApplicationsAsync(int managerUserId);

        Task ApproveLeaveAsync(int managerUserId, int leaveId, string remarks);

        Task RejectLeaveAsync(int managerUserId, int leaveId, string remarks);
    }
}
