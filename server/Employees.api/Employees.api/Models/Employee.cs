using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.api.Models
{
    public class Employee
    {
        #region Props

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Roll Roll { get; set; }
        public DateTimeOffset StartDate { get; private set; } = DateTimeOffset.UtcNow;

        #endregion

        #region Ctors

        // Default ctor
        public Employee() { }

        // Init ctor
        public Employee(string firstName,
                        string lastName,
                        string phoneNumber,
                        string address,
                        Roll roll)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.Roll = roll;
        }

        // Copy ctor
        public Employee(Employee other)
        {
            this.FirstName = other.FirstName;
            this.LastName = other.LastName;
            this.PhoneNumber = other.PhoneNumber;
            this.Address = other.Address;
            this.Roll = other.Roll;
            this.StartDate = other.StartDate;
        }

        #endregion
    }
}
