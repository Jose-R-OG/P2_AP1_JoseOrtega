using Microsoft.EntityFrameworkCore;
using P2_AP1_JoseOrtega.Models;

namespace P2_AP1_JoseOrtega.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Modelo> modelos { get; set; }
    }
}
