namespace Montcrest.API.DTOs.JobApplications
{
    public class ApplyJobRequestDto
    {
        //public string Token { get; set; }
        public int TechnologyId { get; set; }
        public IFormFile Resume { get; set; } = null!;
    }
}
