using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.API.DTOs;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _service;
        private readonly ILogger<TarefasController> _logger;

        public TarefasController(ITarefaService service, ILogger<TarefasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _service.GetAll();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("HTTP GET /tarefas/{Id}", id);

            var tarefa = await _service.GetById(id);

            if (tarefa == null)
            {
                _logger.LogWarning("HTTP 404 - tarefa {Id} não encontrada", id);
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TarefaRequestDTO dto)
        {
            _logger.LogInformation("HTTP POST /tarefas - criando tarefa");

            var tarefa = await _service.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TarefaRequestDTO dto)
        {
            var updated = await _service.Update(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("HTTP DELETE /tarefas/{Id}", id);

            var result = await _service.Delete(id);

            if (!result)
            {
                _logger.LogWarning("HTTP 404 ao deletar tarefa {Id}", id);
                return NotFound();
            }

            return NoContent();
        }
    }

}
