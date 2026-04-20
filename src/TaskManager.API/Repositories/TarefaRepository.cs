using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Models;

namespace TaskManager.API.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {

        private readonly TaskManagerDbContext _context;

        public TarefaRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarefa>> GetAll()
        {
            return await _context.Tarefas.ToListAsync<Tarefa>();
        }

        public async Task<Tarefa?> GetById(Guid id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa?> GetById(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task Add(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
        }
    }
}