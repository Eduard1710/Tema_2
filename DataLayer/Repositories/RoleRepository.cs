using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
