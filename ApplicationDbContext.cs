using api.Entidades;
using Microsoft.EntityFrameworkCore;

namespace api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Las tablas intermedias no tienen un Id propio: su clave es la combinación de las dos llaves.
            modelBuilder.Entity<PeliculaGenero>().HasKey(x => new { x.PeliculaId, x.GeneroId });
            modelBuilder.Entity<PeliculaCine>().HasKey(x => new { x.PeliculaId, x.CineId });
            modelBuilder.Entity<PeliculaActor>().HasKey(x => new { x.PeliculaId, x.ActorId });
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<Productora> Productoras { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculaGenero> PeliculasGeneros { get; set; }
        public DbSet<PeliculaCine> PeliculasCines { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
    }
}
