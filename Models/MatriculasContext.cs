using Microsoft.EntityFrameworkCore;
using MiIT.Models;

namespace MiIT.Data
{
    public class MatriculasContext : DbContext
    {
        public MatriculasContext(DbContextOptions<MatriculasContext> options) : base(options)
        {
        }

        public DbSet<Curso> Curso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().ToTable("Curso");
        }
    }
}
