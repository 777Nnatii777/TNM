using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class MyApplicationDbContext : IdentityDbContext
    {
        public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options)
            : base(options) { }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<TestAssignment> TestAssignments { get; set; }

        public DbSet<TestAdded> TestAddeds { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<TestAssignment>()
                .HasKey(ta => ta.AssignmentId);

            modelBuilder.Entity<Test>()
                .HasMany(t => t.Questions)
                .WithOne(q => q.Test)
                .HasForeignKey(q => q.TestId);

           
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);

            modelBuilder.Entity<TestAdded>()
    .HasOne(ta => ta.Test)
    .WithMany() 
    .HasForeignKey(ta => ta.TestId)
    .OnDelete(DeleteBehavior.Cascade); 


            modelBuilder.Entity<Question>()
                .Property(q => q.Type)
                .HasConversion<int>();

            modelBuilder.Entity<Answer>()
       .Property(a => a.IsCorrect)
       .HasConversion<int>() 
       .IsRequired(); 
        }
    }
}
