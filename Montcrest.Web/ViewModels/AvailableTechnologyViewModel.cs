namespace Montcrest.Web.ViewModels
{

    public class AvailableTechnologyViewModel
    {
        public int TechnologyId { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool HasApplied { get; set; }
        public int? ApplicationId { get; set; }
    }

}