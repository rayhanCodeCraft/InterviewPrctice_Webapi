namespace InterviewPrctice_Webapi.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoinDate { get; set; }
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    }
}
