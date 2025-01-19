using Microsoft.EntityFrameworkCore;
using hecotoBackend.Models;

namespace hecotoBackend.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
    }
}