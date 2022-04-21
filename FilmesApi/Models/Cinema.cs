using FilmesApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
        //Colocando como virtual eu defino que essas são Lazy (olha o startup.cs)
        public virtual Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }
        public virtual Gerente Gerente { get; set; }
        public int GerenteId { get; set; }

        //Relacionando  N p N
        //Um cinema vai ter multiplas sessoes
        [JsonIgnore]
        public virtual List<Sessao> Sessoes { get; set; }

    }
}