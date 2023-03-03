using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PersonnelInfo:BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BasicSalary { get; set; }
        public int Allowance { get; set; }
        public int Transportation { get; set; }
        public int TotalSallary { get; set; }
        public DateTime SalaryDate { get; set; }
    }

}
