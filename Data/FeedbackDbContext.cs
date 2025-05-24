using Microsoft.EntityFrameworkCore;
using prctica3.Models;

namespace prctica3.Data
{
    public class FeedbackDbContext : DbContext
    {
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
        {
        }

        public DbSet<Feedback> Feedbacks => Set<Feedback>();
    }
}
