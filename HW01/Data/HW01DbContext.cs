using HW01.Models;
using Microsoft.EntityFrameworkCore;

namespace HW01.Data
{
    public class HW01DbContext : DbContext
    {
        public HW01DbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
    }
}
