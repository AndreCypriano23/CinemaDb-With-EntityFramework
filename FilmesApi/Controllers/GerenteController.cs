using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.Gerente;
using FilmesApi.Models;
using FilmesApi.Services;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {

        private GerenteService _gerenteService;

        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto dto)
        {

            ReadGerenteDto readDto = _gerenteService.AdicionaGerente(dto);

            return CreatedAtAction(nameof(RecuperaGerentePorId), new { Id = readDto.Id }, readDto);

        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGerentePorId(int id)
        {
            ReadGerenteDto readDto =  _gerenteService.RecuperaGerentePorId(id);
            if (readDto != null) return Ok();

            return NotFound();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaGerente(int id)
        {
            Result resultado =  _gerenteService.DeletaGerente(id);
            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }


    }
}
