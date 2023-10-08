using IdentityService.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccessLayer.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<UserDbo>? User { get; set; }

        public string DbPath { get; }

        public UserContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "user.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
