using Microsoft.EntityFrameworkCore;

namespace FitnessCenter.Models
{
    public class FitnessCenterContext : DbContext
    {
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subscribtion> Subscribtions { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public FitnessCenterContext(DbContextOptions<FitnessCenterContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
