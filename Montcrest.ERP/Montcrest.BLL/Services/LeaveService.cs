using Montcrest.BLL.DTOs.Leave;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Enums;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.BLL.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ILeaveApplicationRepository _leaveRepo;

        public LeaveService(
            IEmployeeRepository employeeRepo,
            ILeaveApplicationRepository leaveRepo)
        {
            _employeeRepo = employeeRepo;
            _leaveRepo = leaveRepo;
        }

        public async Task ApplyLeaveAsync(int userId, ApplyLeaveRequestDto dto)
        {
            var employee = await _employeeRepo.GetByUserIdAsync(userId);

            if (employee == null)
                throw new Exception("Employee not found.");

            if (employee.ManagerId == null)
                throw new Exception("Manager is not assigned to this employee.");

            if (dto.FromDate.Date > dto.ToDate.Date)
                throw new Exception("FromDate cannot be greater than ToDate.");

            if (dto.FromDate.Date < DateTime.UtcNow.Date)
                throw new Exception("Cannot apply leave for past dates.");

            if (string.IsNullOrWhiteSpace(dto.Reason))
                throw new Exception("Reason is required.");

            var leave = new LeaveApplication
            {
                EmployeeId = employee.Id,
                ManagerId = employee.ManagerId,
                LeaveType = dto.LeaveType,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                Reason = dto.Reason,
                Status = LeaveStatus.Pending,
                AppliedOn = DateTime.UtcNow
            };

            await _leaveRepo.AddAsync(leave);
        }

        public async Task<List<LeaveApplicationDto>> GetMyLeavesAsync(int userId)
        {
            var employee = await _employeeRepo.GetByUserIdAsync(userId);

            if (employee == null)
                throw new Exception("Employee not found.");

            var leaves = await _leaveRepo.GetByEmployeeIdAsync(employee.Id);

            return leaves.Select(l => new LeaveApplicationDto
            {
                LeaveId = l.Id,
                EmployeeName = l.Employee.User.FullName,
                ManagerName = l.Manager.User.FullName,
                LeaveType = l.LeaveType,
                FromDate = l.FromDate,
                ToDate = l.ToDate,
                Reason = l.Reason,
                Status = l.Status,
                AppliedOn = l.AppliedOn,
                ManagerRemarks = l.ManagerRemarks
            }).ToList();
        }
    }
}
