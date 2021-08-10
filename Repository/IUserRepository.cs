using CMPApiMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPApiMicroservice.Repository
{
    public interface IUserRepository
    {
        Task <IEnumerable<User>> getUser();
        Task<User> getUserById(int Id);
        Task addUser(User emp);
        Task deleteUser(int Id);
        Task updateUser(User emp);
        Task Save();
    }
}
