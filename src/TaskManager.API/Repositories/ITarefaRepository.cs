using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.Repositories
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetAll();
        Task<Tarefa?> GetById(Guid id);
        Task Add(Tarefa tarefa);
        Task Update(Tarefa tarefa);
        Task Delete(Guid id);
    }
}