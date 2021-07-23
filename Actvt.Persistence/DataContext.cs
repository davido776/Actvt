using Actvt.Domain;
using Microsoft.EntityFrameworkCore;

namespace Actvt.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Activity> Activities { get; set; }
    }
}