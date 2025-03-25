using Microsoft.EntityFrameworkCore;


namespace todo.Models.Tasks
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>().ToTable("tasks");
            modelBuilder.Entity<Task>().Property(t => t.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Task>().Property(t => t.Name).HasColumnName("name").HasMaxLength(500).IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.Description).HasColumnName("description").HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.IdUser).HasColumnName("id_user").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.CreateDate).HasColumnName("create_date").HasColumnType("datetimeoffset").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.Completed).HasColumnName("is_completed").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.Removed).HasColumnName("removed").HasColumnType("bit").IsRequired();

            modelBuilder.Entity<Task>().HasQueryFilter(t => !t.Removed);
        }
    }
}
