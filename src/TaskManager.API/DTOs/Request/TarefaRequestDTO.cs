using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TaskManager.API.Shared.Enum;

namespace TaskManager.API.DTOs
{
    public class TarefaRequestDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public StatusTarefa Status { get; set; }
    }
}