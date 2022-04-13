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
        public IEnumerable<Cinema> RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            return _context.Cinemas;
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
