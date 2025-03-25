using Microsoft.EntityFrameworkCore;


namespace todo.Models.Users
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.Name).HasColumnName("name").HasMaxLength(500).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email").HasMaxLength(500).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password").HasMaxLength(300).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.CreationDate).HasColumnName("creation_date").HasColumnType("datetimeoffset").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.IsSysAdmin).HasColumnName("is_sys_admin").HasColumnType("bit").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Removed).HasColumnName("removed").HasColumnType("bit").IsRequired();
        }
    }
}
