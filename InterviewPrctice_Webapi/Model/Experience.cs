using System.Text.Json.Serialization;

namespace InterviewPrctice_Webapi.Model
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

        // Foreign key relationship
        public int EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
