using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.api.Dtos;
using Employees.api.Models;
using Employees.api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Employees.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository repository;

        public EmployeesController(IEmployeesRepository _repository)
        {
            this.repository = _repository;
        }

        // Get /employees
        [HttpGet]
        [Authorize]
        public IEnumerable<EmployeesDto> AllEmployees()
        {
            return repository.GetAllEmployees().Select(emp => emp.AsDto());
        }

        // Get /employees/{id}
        [HttpGet("{id}")]
        [Authorize]
        public EmployeesDto GetEmployee(Guid id)
        {
            return repository.GetEmployee(id).AsDto();
        }

        // Post /employees
        [HttpPost]
        [Authorize]
        public ActionResult<EmployeesDto> AddEmployee(AddEmployeeDto employeeDto)
        {
            Employee newEmployee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                PhoneNumber = employeeDto.PhoneNumber,
                Address = employeeDto.Address,
                Roll = employeeDto.Roll
            };

            repository.AddEmployee(newEmployee);

            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.Id}, newEmployee.AsDto());
        }

        // Put /employees/{id}
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult UpdateEmployee(Guid id, UpdateEmployeeDto employeeDto)
        {
            var findEmployee = repository.GetEmployee(id);

            if (findEmployee is null)
            {
                return NotFound();
            }

            findEmployee.FirstName = employeeDto.FirstName;
            findEmployee.LastName = employeeDto.LastName;
            findEmployee.PhoneNumber = employeeDto.PhoneNumber;
            findEmployee.Address = employeeDto.Address;
            findEmployee.Roll = employeeDto.Roll;

            repository.EditEmployee(findEmployee);

            return Ok();
        }

        // Delete /employees/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult RemoveEmployee(Guid id)
        {
            var findEmployee = repository.GetEmployee(id);

            if (findEmployee is null)
            {
                return NotFound();
            }

            repository.DeleteEmployee(id);

            return Ok();
        }
    }
}
