using Dio.series.Interfaces;
using Dio.series.web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dio.series.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SerieController : Controller
    {

        private readonly iRepositorio<Serie> repositorio;

        public SerieController(iRepositorio<Serie> repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet("")]
        public IActionResult Lista()
        {
            return Ok(repositorio.Lista().Select(s => new SerieModel(s)));
        }

        [HttpPut("{id}")]
        public IActionResult Atualiza(int id,[FromBody] SerieModel model)
        {
            repositorio.Atualiza(id, model.ToSerie());
            return NoContent();
        }

        [HttpPost]
        public IActionResult Cria([FromBody] SerieModel serie)
        {
            serie.Id = repositorio.ProximoId();
            repositorio.Insere(serie.ToSerie());
            return Created("",serie);
        }

        [HttpDelete("{id}")]
        public IActionResult Deleta(int id)
        {
            repositorio.Exclui(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Consulta(int id)
        {
            SerieModel serie =new SerieModel(repositorio.RetornaPorId(id));
            return Ok(serie);
        }
    }
}
