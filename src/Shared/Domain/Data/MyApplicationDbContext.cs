namespace Domain.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class MyApplicationDbContext : IdentityDbContext
    {
        public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options)
            : base(options) { }

        
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestAssignment> TestAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<TestAssignment>()
                .HasKey(ta => ta.AssignmentId);

            
            modelBuilder.Entity<Test>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<TestAssignment>()
                .HasOne(ta => ta.User) 
                .WithMany()
                .HasForeignKey(ta => ta.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
