using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Data.Dtos.Gerente
{
    public class ReadGerenteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //Não quero mostrar a informação endereços dos cinemas, senão vai ficar redundante 
       //Quero trazer um obj Cinema anonimo, então vou trocar de List Para Object
        public Object Cinemas { get; set; }

    }
}
