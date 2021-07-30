using Employees.api.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.api.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private const string DATABASE_NAME = "Employees_Management";
        private const string COLLECTION_NAME = "employees-list";

        private readonly IMongoCollection<Employee> employeesCollection;
        private readonly FilterDefinitionBuilder<Employee> filterBuilder = Builders<Employee>.Filter;

        public EmployeesRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(DATABASE_NAME);
            employeesCollection = database.GetCollection<Employee>(COLLECTION_NAME);
        }

        #region Api

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeesCollection.Find(new BsonDocument()).ToList();
        }
        public Employee GetEmployee(Guid id)
        {
            var filter = filterBuilder.Eq(emp => emp.Id, id);
            return employeesCollection.Find(filter).SingleOrDefault();
        }
        public void AddEmployee(Employee newEmployee)
        {
            employeesCollection.InsertOne(newEmployee);
        }
        public void DeleteEmployee(Guid id)
        {
            var filter = filterBuilder.Eq(emp => emp.Id, id);
            employeesCollection.DeleteOne(filter);
        }
        public void EditEmployee(Employee employee)
        {
            var filter = filterBuilder.Eq(emp => emp.Id, employee.Id);
            employeesCollection.ReplaceOne(filter, employee);
        }

        #endregion
    }
}
