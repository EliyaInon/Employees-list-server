using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.api.Models
{
    public class User
    {
        public Guid id { get; } = Guid.NewGuid();

        [Required (ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last name is required")]
        public string LastName{ get; set; }

        [Required (ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
