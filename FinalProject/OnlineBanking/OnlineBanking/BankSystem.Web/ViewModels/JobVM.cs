namespace BankSystem.Web.ViewModels
{
    public class JobVM
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobTime { get; set; }
        public List<JobRequirementVM>? JobRequirementList { get; set; }
    }
}
