using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class MyApplicationDbContext : IdentityDbContext
{
    public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Result> Results { get; set; }

    public DbSet<TestAssignment> TestAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tests)
            .WithOne(t => t.Creator)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId);

        modelBuilder.Entity<Result>()
            .HasOne(r => r.User)
            .WithMany(u => u.Results)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Result>()
            .HasOne(r => r.Test)
            .WithMany()
            .HasForeignKey(r => r.TestId);

        modelBuilder.Entity<TestAssignment>()
        .HasKey(ta => ta.AssignmentId);

        base.OnModelCreating(modelBuilder);
    }
}
