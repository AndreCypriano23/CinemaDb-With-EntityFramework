using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.Sessao;
using FilmesApi.Models;
using FilmesApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {

        private SessaoService _sessaoService; 
    
        public SessaoController(SessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessaoDto SessaoDto)
        {
            ReadSessaoDto sessaoDto = _sessaoService.AdicionaSessao(SessaoDto);

            return CreatedAtAction(nameof(RecuperaSessaoPorId), new { Id = sessaoDto.Id }, sessaoDto);
        }



        [HttpGet("{id}")]
        public IActionResult RecuperaSessaoPorId(int id)
        {
            ReadSessaoDto sessaoDto = _sessaoService.RecuperaSessaoPorId(id);

            if (sessaoDto != null) return Ok();
          
            return NotFound();

        }

    }

}
