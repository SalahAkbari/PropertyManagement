using Microsoft.EntityFrameworkCore;
using Property.Domain.Entities;

namespace Property.DataAccess.DbContext
{
    public class SqlDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Domain.Entities.Property> Properties { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }
    }
}
