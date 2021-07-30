using Employees.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.api.Dtos
{
    public class EmployeesDto
    {      
        public EmployeesDto(Guid id,
                            DateTimeOffset startDate)
        {
            this.Id = id;
            this.StartDate = startDate;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Roll Roll { get; set; }
        public DateTimeOffset StartDate { get; private set; }
    }
}
