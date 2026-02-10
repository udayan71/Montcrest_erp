using Montcrest.BLL.DTOs.Manager;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Enums;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.BLL.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepo;
        private readonly ILeaveApplicationRepository _leaveRepo;

        public ManagerService(IManagerRepository managerRepo,
                              ILeaveApplicationRepository leaveRepo)
        {
            _managerRepo = managerRepo;
            _leaveRepo = leaveRepo;
        }

        public async Task<List<ManagerLeaveApplicationDto>> GetLeaveApplicationsAsync(int managerUserId)
        {
            var manager = await _managerRepo.GetByUserIdAsync(managerUserId);

            if (manager == null)
                throw new Exception("Manager profile not found.");

            var leaves = await _leaveRepo.GetByManagerIdAsync(manager.Id);

            return leaves.Select(l => new ManagerLeaveApplicationDto
            {
                LeaveId = l.Id,
                EmployeeId = l.EmployeeId,
                EmployeeName = l.Employee.User.FullName,
                EmployeeEmail = l.Employee.User.Email,
                LeaveType = l.LeaveType,
                FromDate = l.FromDate,
                ToDate = l.ToDate,
                Reason = l.Reason,
                Status = l.Status,
                AppliedOn = l.AppliedOn,
                ManagerRemarks = l.ManagerRemarks
            }).ToList();
        }

        public async Task ApproveLeaveAsync(int managerUserId, int leaveId, string remarks)
        {
            var manager = await _managerRepo.GetByUserIdAsync(managerUserId);

            if (manager == null)
                throw new Exception("Manager profile not found.");

            var leave = await _leaveRepo.GetByIdAsync(leaveId);

            if (leave == null)
                throw new Exception("Leave application not found.");

            if (leave.ManagerId != manager.Id)
                throw new Exception("You are not authorized to review this leave request.");

            if (leave.Status != LeaveStatus.Pending)
                throw new Exception("Only pending leave can be approved.");

            leave.Status = LeaveStatus.Approved;
            leave.ManagerRemarks = remarks;
            leave.ReviewedOn = DateTime.UtcNow;

            await _leaveRepo.UpdateAsync(leave);
        }

        public async Task RejectLeaveAsync(int managerUserId, int leaveId, string remarks)
        {
            var manager = await _managerRepo.GetByUserIdAsync(managerUserId);

            if (manager == null)
                throw new Exception("Manager profile not found.");

            var leave = await _leaveRepo.GetByIdAsync(leaveId);

            if (leave == null)
                throw new Exception("Leave application not found.");

            if (leave.ManagerId != manager.Id)
                throw new Exception("You are not authorized to review this leave request.");

            if (leave.Status != LeaveStatus.Pending)
                throw new Exception("Only pending leave can be rejected.");

            leave.Status = LeaveStatus.Rejected;
            leave.ManagerRemarks = remarks;
            leave.ReviewedOn = DateTime.UtcNow;

            await _leaveRepo.UpdateAsync(leave);
        }
    }
}
