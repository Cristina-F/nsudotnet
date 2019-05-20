namespace Lab4_ef.Models {
    public class Project {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int Bonus { get; set; }
        public int ? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}