using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoAPI.Models;


namespace ToDoAPI.AppDataContext
{

    // TodoDbContext class inherits from DbContext
    public class TodoDbContext : DbContext
    {

        // DbSettings field to store the connection string
        private readonly ToDoAPI.Models.DbSettings _dbSettings;

        public TodoDbContext(IOptions<ToDoAPI.Models.DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_dbSettings.ConnectionString, ServerVersion.AutoDetect(_dbSettings.ConnectionString));
        }
        // Configuring the model for the Todo entity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .ToTable("Todos")
                .HasKey(x => x.Id);
        }

         // Add DbSet property for the Todo entity
        public DbSet<Todo> Todos { get; set; }
    }
}