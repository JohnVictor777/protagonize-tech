using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManager.API.Models;
using TaskManager.API.Repositories;
using TaskManager.API.Services;

namespace TaskManager.Tests.Services
{
    public class TarefaServiceTests
    {
        private readonly Mock<ITarefaRepository> _repositoryMock;
        private readonly Mock<ILogger<TarefaService>> _loggerMock;
        private readonly TarefaService _service;

        public TarefaServiceTests()
        {
            // dependências
            _repositoryMock = new Mock<ITarefaRepository>();
            _loggerMock = new Mock<ILogger<TarefaService>>();

            _service = new TarefaService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetById_DeveRetornarTarefa_QuandoIdExistir()
        {
            // Arrange (Preparação)
            var idExistente = Guid.NewGuid();
            var tarefaFake = new Tarefa { Id = idExistente, Titulo = "Teste", Status = 0 };

            _repositoryMock.Setup(repo => repo.GetById(idExistente))
                           .ReturnsAsync(tarefaFake);

            // Act (Ação)
            var resultado = await _service.GetById(idExistente);

            // Assert (Verificação)
            Assert.NotNull(resultado);
            Assert.Equal("Teste", resultado.Titulo);
        }
    }
}