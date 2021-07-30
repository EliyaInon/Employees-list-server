using Employees.api.Models;
using System;
using System.Collections.Generic;

namespace Employees.api.Repositories
{
    public interface IEmployeesRepository
    {
        public IEnumerable<Employee> GetAllEmployees();

        public Employee GetEmployee(Guid id);

        public void AddEmployee(Employee newEmployee);

        public void EditEmployee(Employee employee);

        public void DeleteEmployee(Guid id);
    }
}
