using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.DataAccess
{
    public class DBContext : IdentityDbContext<UserEntity>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Add DbSets for User.
            base.OnModelCreating(builder);
        }
        
    }
}
