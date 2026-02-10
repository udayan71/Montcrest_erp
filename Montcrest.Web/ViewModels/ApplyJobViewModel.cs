using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Montcrest.Web.ViewModels
{
    public class ApplyJobViewModel
    {
        public int TechnologyId { get; set; }

        public string TechnologyName { get; set; }

        [Required]
        public IFormFile Resume { get; set; } = null!;
    }
}
