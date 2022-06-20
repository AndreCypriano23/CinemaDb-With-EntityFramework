using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Services
{
    public class CinemaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return _mapper.Map<ReadCinemaDto>(cinema);

        }

        public List<ReadCinemaDto> RecuperaCinemas(string nomeDoFilme)
        {
            //A partir do nome do filme eu retorno apenas os cinemas que existem uma sessao que está exibindo esse filme
            List<Cinema> cinemas = _context.Cinemas.ToList();
            if (cinemas == null)
            {
                return null;
            }
            //Exibindo apenas as sessoes que possuam o titulo do filme que estou passando
            if (!string.IsNullOrEmpty(nomeDoFilme))
            {
                //efetuo uma consulta, antes usei o linq
                //Mas agora vou fazer uma pesquisa mais complexa
                IEnumerable<Cinema> query = from cinema in cinemas // a partir de um cinema qualquer da minha lista de cinemas
                                            where cinema.Sessoes.Any(sessao =>
                                            sessao.Filme.Titulo == nomeDoFilme)
                                            select cinema;
                cinemas = query.ToList();
            }
            //Mapear agora
            return _mapper.Map<List<ReadCinemaDto>>(cinemas);

        }

    
        public ReadCinemaDto RecuperaCinemasPorId(int id)
        {
            Cinema Cinema = _context.Cinemas.FirstOrDefault(Cinema => Cinema.Id == id);
            if (Cinema != null)
            {
                ReadCinemaDto CinemaDto = _mapper.Map<ReadCinemaDto>(Cinema);
                return CinemaDto;
            }
            return null;
        }

        public Result AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }

            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();

            return Result.Ok();
        }


        public  Result DeletaCinema(int id)
        {
            Cinema Cinema = _context.Cinemas.FirstOrDefault(Cinema => Cinema.Id == id);
            if (Cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }
            _context.Remove(Cinema);
            _context.SaveChanges();

            return Result.Ok();
        }


    }
}
