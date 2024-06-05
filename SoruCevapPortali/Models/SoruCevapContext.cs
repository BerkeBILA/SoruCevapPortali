using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SoruCevapPortali.Models
{
    public class SoruCevapContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public SoruCevapContext(DbContextOptions<SoruCevapContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

       
        }

        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }


    }
}
