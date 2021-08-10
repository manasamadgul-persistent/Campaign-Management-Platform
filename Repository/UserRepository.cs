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
        public async Task<IEnumerable<User>> getUser()
        {
            List<User> datalist = new List<User>();
            var UData = _userContext.Users.ToList();
            foreach(var item in UData)
            {
                datalist.Add(new User
                {
                    Id = item.Id,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    Age = item.Age,
                    Address = item.Address,
                    State = Convert.ToInt32(item.State).ToStateEnum()
                });
            }
            return datalist;
        }
        public async Task addUser(User _user)
        {
            var result = await _userContext.AddAsync(_user);
            await Save();
        }
        public async Task deleteUser(int Id)
        {
            var id = await _userContext.Users.FindAsync(Id);
            _userContext.Users.Remove(id);
            await Save();
        }
        public async Task<User> getUserById(int Id)
        {
             return await _userContext.Users.FindAsync(Id);
        }
        public async Task Save()
        {
            await _userContext.SaveChangesAsync();
        }
        public async Task updateUser(User _user)
        {
            _userContext.Entry(_user).State = EntityState.Modified;
            await Save();
        }
    }
}
