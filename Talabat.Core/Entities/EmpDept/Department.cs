using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.EmpDept
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        // Navigational Property [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
