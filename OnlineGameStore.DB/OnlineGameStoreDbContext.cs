using Microsoft.EntityFrameworkCore;
using OnlineGameStore.DB.Entities;

namespace OnlineGameStore.DB
{
    public class OnlineGameStoreDbContext : DbContext
    {
        private readonly string _connectionString;

        #region DbSets
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        #endregion

        public OnlineGameStoreDbContext(string connectionString)
        {
            _connectionString = connectionString;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
