using AutoMapper;
using FilmesApi.Data.Dtos.Gerente;
using FilmesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, ReadGerenteDto>()
                .ForMember(gerente => gerente.Cinemas, opts => opts
                .MapFrom(gerente =>  gerente.Cinemas.Select
                (c => new { c.Id, c.Nome, c.Endereco, c.EnderecoId })));
            //Opções para esse mapeamento, vou ocultar as info de gerente do cinema
            //remapeando - o. Veja que deixei a propriedade de cinema alterada
            //Lá no ReadGerenteDto


        }
    }
}
