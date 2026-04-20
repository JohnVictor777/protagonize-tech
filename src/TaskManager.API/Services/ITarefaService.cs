using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs;
using TaskManager.API.DTOs.Response;

namespace TaskManager.API.Services
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaResponseDTO>> GetAll();
        Task<TarefaResponseDTO?> GetById(Guid id);
        Task<TarefaResponseDTO> Create(TarefaRequestDTO dto);
        Task<bool> Update(Guid id, TarefaRequestDTO dto);
        Task<bool> Delete(Guid id);
    }
}