using CMPApiMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPApiMicroservice.DBContext
{
    public class DBEntities : DbContext
    {
        public DBEntities(DbContextOptions<DBEntities> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
