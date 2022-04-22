using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
  

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto CinemaDto)
        {
            Cinema Cinema = _mapper.Map<Cinema>(CinemaDto);
            _context.Cinemas.Add(Cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = Cinema.Id }, Cinema);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            //A partir do nome do filme eu retorno apenas os cinemas que existem uma sessao que está exibindo esse filme
            List<Cinema> cinemas = _context.Cinemas.ToList();
            if(cinemas == null)
            {   
                return NotFound();
            }
            //Exibindo apenas as sessoes que possuam o titulo do filme que estou passando
            if(!string.IsNullOrEmpty(nomeDoFilme))
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
            List<ReadCinemaDto> readDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);

            return Ok(readDto);
    
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            Cinema Cinema = _context.Cinemas.FirstOrDefault(Cinema => Cinema.Id == id);
            if(Cinema != null)
            {
                ReadCinemaDto CinemaDto = _mapper.Map<ReadCinemaDto>(Cinema);
                return Ok(CinemaDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto CinemaDto)
        {
            Cinema Cinema = _context.Cinemas.FirstOrDefault(Cinema => Cinema.Id == id);
            if(Cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(CinemaDto, Cinema);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Cinema Cinema = _context.Cinemas.FirstOrDefault(Cinema => Cinema.Id == id);
            if (Cinema == null)
            {
                return NotFound();
            }
            _context.Remove(Cinema);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
