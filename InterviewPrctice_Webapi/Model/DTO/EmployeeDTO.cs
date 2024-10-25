namespace InterviewPrctice_Webapi.Model.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoinDate { get; set; }
        public List<ExperienceDTO> Experiences { get; set; } = new List<ExperienceDTO>();
    }
}
