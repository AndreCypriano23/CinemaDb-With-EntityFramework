using FilmesApi.Models;
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

            builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente)
                .WithMany(gerente => gerente.Cinemas)
                .HasForeignKey(cinema => cinema.GerenteId);

            builder.Entity<Sessao>()
               .HasOne(sessao => sessao.Filme) //sessao vai ter um filme
               .WithMany(filme => filme.Sessoes) //um filme terá 0 ou muitas sessoes
               .HasForeignKey(sessao => sessao.FilmeId);


            builder.Entity<Sessao>()
               .HasOne(sessao => sessao.Cinema) 
               .WithMany(cinema => cinema.Sessoes) 
               .HasForeignKey(sessao => sessao.CinemaId);

        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; } 

    }

}