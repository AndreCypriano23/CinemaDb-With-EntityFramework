using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        //Colocando como virtual eu defino que essas são Lazy (olha o startup.cs)
        [JsonIgnore]//para evitar o ciclo infitido de cinema chama endereço e vice-versa
        public virtual Cinema Cinema { get; set; }
        //AQUI UM CINEMA NAO PRECISA exixtir antes, é o endereço que precisa estar cadastrado
        //Antes de cadastrar um cinema


    }
}
