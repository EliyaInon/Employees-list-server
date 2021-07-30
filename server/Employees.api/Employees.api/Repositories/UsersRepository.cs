using Employees.api.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private const string DATABASE_NAME = "Employees_Management";
        private const string COLLECTION_NAME = "users";

        private readonly IMongoCollection<User> usersCollection;
        private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;

        public UsersRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(DATABASE_NAME);
            usersCollection = database.GetCollection<User>(COLLECTION_NAME);
        }

        #region Api

        public IEnumerable<User> GetAllUsers()
        {
            var a = usersCollection.Find(new BsonDocument());

            var g= a.ToList();
            return g;
        }

        public void AddNewUser(User user)
        {
            usersCollection.InsertOne(user);
        }

        public void DeleteUser(string email)
        {
            var filter = filterBuilder.Eq(emp => emp.Email, email);
            usersCollection.DeleteOne(filter);
        }

        public bool IsUserExist(User user)
        {
            var filter = filterBuilder.Eq(userRow => userRow.Email, user.Email);
            var optionalUser = usersCollection.Find(filter).SingleOrDefault();

            return optionalUser != null;
        }

        public User Authenticate(LoginAuthentication login)
        {
            var filter = filterBuilder.And(
                filterBuilder.Eq(emp => emp.Email, login.Email),
                filterBuilder.Eq(emp => emp.Password, login.Password));
            return usersCollection.Find(filter).SingleOrDefault();
        }

        #endregion
    }
}
