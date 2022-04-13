using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Algumas definições na hora de iniciar, como os modelos serão iniciados
            //RELACIONAMENTO DE 1 PARA 1 E O ENDEREÇO DEVE EXISTIR ANTES
            builder.Entity<Endereco>()
                .HasOne(endereco => endereco.Cinema)//Endereço que tem um cinema
                .WithOne(cinema => cinema.Endereco)//Cinema que tem um endereço
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);
                //ESTOU EXPLICITANDO QUE O MEU ENDERECO TEM UM CINEMA
        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}