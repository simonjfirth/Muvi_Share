using MovieSoft.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MovieSoft.DAL
{
    public class MovieContext : DbContext
    {
        public DbSet<SoftMovie> SoftMovies { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
    }
}