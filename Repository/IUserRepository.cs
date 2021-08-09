using CMPApiMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPApiMicroservice.Repository
{
    public interface IUserRepository
    {
        Task <IEnumerable<User>> GetUser();
        Task<User> GetUserById(int Id);
        Task InsertUser(User emp);
        Task DeleteUser(int Id);
        Task UpdateUser(User emp);
        Task Save();
    }
}
