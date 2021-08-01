using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.api.Settings
{
    public class MongoDbSettings
    {
        const string DATABASE_NAME = "Employees_Management";

        public IMongoClient Client
        {
            get
            {
                var settings = MongoClientSettings.FromConnectionString(
                $"mongodb+srv://EmployeesManagementDB:agcwtJe7rX6kGPhD@cluster0.d7n7w.mongodb.net/{DATABASE_NAME}?retryWrites=true&w=majority");

                return new MongoClient(settings);
            }
        }
        public IMongoDatabase Database
        {
            get
            {
                return Client.GetDatabase(DATABASE_NAME);

            }
        }
    }
}
