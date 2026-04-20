using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs;
using TaskManager.API.DTOs.Response;
using TaskManager.API.Models;
using TaskManager.API.Repositories;

namespace TaskManager.API.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;
        private readonly ILogger<TarefaService> _logger;

        public TarefaService(ITarefaRepository repository, ILogger<TarefaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<TarefaResponseDTO>> GetAll()
        {
            var tarefas = await _repository.GetAll();

            return tarefas.Select(t => new TarefaResponseDTO
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                DataCriacao = t.DataCriacao
            });
        }

        public async Task<TarefaResponseDTO?> GetById(Guid id)
        {
            _logger.LogInformation("Buscando tarefa com ID {TarefaId}", id);

            var tarefa = await _repository.GetById(id);

            if (tarefa == null)
            {
                _logger.LogWarning("Tarefa com ID {TarefaId} não encontrada", id);
                return null;
            }

            _logger.LogInformation("Tarefa encontrada com ID {TarefaId}", id);

            return new TarefaResponseDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                DataCriacao = tarefa.DataCriacao
            };
        }

        public async Task<TarefaResponseDTO> Create(TarefaRequestDTO dto)
        {
            _logger.LogInformation("Criando nova tarefa: {Titulo}", dto.Titulo);

            var tarefa = new Tarefa
            {
                Id = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Status = dto.Status,
                DataCriacao = DateTime.UtcNow
            };

            await _repository.Add(tarefa);

            _logger.LogInformation("Tarefa criada com sucesso. ID: {TarefaId}", tarefa.Id);

            return new TarefaResponseDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                DataCriacao = tarefa.DataCriacao
            };
        }

        public async Task<bool> Update(Guid id, TarefaRequestDTO dto)
        {
            var tarefa = await _repository.GetById(id);

            if (tarefa == null)
                return false;

            tarefa.Titulo = dto.Titulo;
            tarefa.Descricao = dto.Descricao;
            tarefa.Status = dto.Status;

            await _repository.Update(tarefa);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            _logger.LogInformation("Tentando deletar tarefa {TarefaId}", id);

            var tarefa = await _repository.GetById(id);

            if (tarefa == null)
            {
                _logger.LogWarning("Tentativa de deletar tarefa inexistente {TarefaId}", id);
                return false;
            }

            await _repository.Delete(id);
            _logger.LogInformation("Tarefa {TarefaId} deletada com sucesso", id);

            return true;
        }
    }
}