using FilmesAPI_Alura.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI_Alura.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts)
        {
        }

        public DbSet<Filme> Filmes { get; set; }
    }
}
