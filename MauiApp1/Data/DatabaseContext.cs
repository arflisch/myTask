using MyTask.Model;
//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace MyTask.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TaskItem>  Tasks { get; set; }
       
        public string DbPath { get; }

        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            DbPath = System.IO.Path.Join(path, "dbtasks.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var task = modelBuilder.Entity<TaskItem>();
            task.HasKey(x => x.Id);
            task.Property(x => x.Name).IsRequired().HasMaxLength(50);
            task.Property(x => x.Description).HasMaxLength(512);
        }

    }
}
*/