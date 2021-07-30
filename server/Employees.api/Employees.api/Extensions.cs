using Employees.api.Dtos;
using Employees.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.api
{
    public static class Extensions
    {
        public static EmployeesDto AsDto(this Employee employee)
        {
            return new EmployeesDto(employee.Id, employee.StartDate)
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Address = employee.Address,
                Roll = employee.Roll,
            };
        }

        public static UserDto AsDto(this User user)
        {
            return new UserDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
