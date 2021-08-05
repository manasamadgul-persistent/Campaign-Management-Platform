using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPApiMicroservice.DBContext;
using CMPApiMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace CMPApiMicroservice.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DBEntities _userContext;

        public UserRepository(DBEntities dbContext)
        {
            _userContext = dbContext;
        }
        public IEnumerable<User> GetUser()
        {
            return _userContext.Users.ToList();
        }
        public void InsertUser(User _user)
        {
            _userContext.Add(_user);
            Save();
        }
        public void DeleteUser(int Id)
        {
            var product = _userContext.Users.Find(Id);
            _userContext.Users.Remove(product);
            Save();
        }
        public User GetUserById(int Id)
        {
            return _userContext.Users.Find(Id);
        }
        public void Save()
        {
            _userContext.SaveChanges();
        }
        public void UpdateUser(User _user)
        {
            _userContext.Entry(_user).State = EntityState.Modified;
            Save();
        }
    }
}
