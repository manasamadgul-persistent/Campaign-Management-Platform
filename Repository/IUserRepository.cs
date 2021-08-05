using CMPApiMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPApiMicroservice.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUser();
        User GetUserById(int Id);
        void InsertUser(User emp);
        void DeleteUser(int Id);
        void UpdateUser(User emp);
        void Save();
    }
}
