using System.Collections.Generic;

namespace Lab4_ef.Models {
    public class Employee {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}