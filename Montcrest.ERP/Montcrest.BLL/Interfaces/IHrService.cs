
using Montcrest.BLL.DTOs.Hr;

public interface IHrService
{
    Task<List<HrTechnologySummaryDto>> GetTechnologiesAsync();
    Task<List<HrApplicantDto>> GetApplicantsByTechnologyAsync(int technologyId);
}
