using Employees.api.Models;
using System;
using System.Collections.Generic;

namespace Employees.api.Repositories
{
    public interface IUsersRepository
    {
        public IEnumerable<User> GetAllUsers();
        public void AddNewUser(User user);
        public void DeleteUser(string email);
        public bool IsUserExist(User user);
        public User Authenticate(LoginAuthentication login);
    }
}
