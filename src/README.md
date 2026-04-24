# Desafio Técnico – Gerenciamento de Tarefas

Este projeto consiste em uma aplicação web simples para cadastro e gerenciamento de tarefas, desenvolvida como parte de um desafio técnico. O objetivo é demonstrar conhecimentos em desenvolvimento web, integração entre front-end e back-end, lógica de programação e **qualidade de software através de testes unitários**.

## Tecnologias Utilizadas

*   **Front-end**: Angular 17+
*   **Back-end**: ASP.NET Core Web API (.NET 8)
*   **Banco de Dados**: SQL Server
*   **ORM**: Entity Framework Core
*   **Comunicação**: API REST (JSON)
*   **Testes Unitários**: xUnit & Moq (Back-end)

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

*   `FrontEnd-Angular`: Aplicação front-end desenvolvida com Angular.
*   `TaskManager.API`: API back-end desenvolvida com ASP.NET Core.
*   `TaskManager.Tests`: Projeto de testes unitários para o back-end.

## Entidade Principal: Tarefa

A aplicação gerencia a entidade `Tarefa` com os seguintes campos:

| Campo       | Tipo         | Descrição                               |
| :---------- | :----------- | :-------------------------------------- |
| `Id`        | `Guid`       | Identificador único (gerado automaticamente) |
| `Titulo`    | `string`     | Título da tarefa                        |
| `Descricao` | `string`     | Descrição detalhada da tarefa           |
| `Status`    | `Enum`       | Estado da tarefa (`Pendente` / `Concluída`) |
| `DataCriacao` | `DateTime`   | Data e hora de criação da tarefa        |

## Funcionalidades

### Back-end (API)
*   **CRUD Completo**: Endpoints para listar, buscar por ID, criar, atualizar e excluir tarefas.
*   **Autenticação JWT**: Sistema de login e registro de usuários para proteção dos endpoints.
*   **Logging**: Implementação de logs estruturados com **Serilog** (Console, Arquivo e SQL Server).
*   **Testes Unitários**: Cobertura de lógica de negócio no `TarefaService` utilizando **xUnit** e **Moq**.

### Front-end (Angular)
*   **Interface Intuitiva**: Telas para listagem, criação e edição de tarefas.
*   **Consumo de API**: Integração completa via `HttpClient`.
*   **Feedback ao Usuário**: Mensagens de sucesso/erro e confirmações de exclusão.

## Pré-requisitos

*   [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Node.js LTS](https://nodejs.org/)
*   [Angular CLI](https://angular.io/cli)
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Configuração e Execução

### 1. Banco de Dados
Ajuste a connection string no arquivo `TaskManager.API/appsettings.json` se necessário. Em seguida, aplique as migrações:
```bash
cd TaskManager.API
dotnet ef database update
```

### 2. Executando o Back-end
```bash
dotnet run
```
A API estará disponível em `http://localhost:5014`. O **Swagger** pode ser acessado em `/swagger`.

### 3. Executando o Front-end
```bash
cd FrontEnd-Angular
npm install
npm serve
```
Acesse `http://localhost:4200`.

### 4. Executando os Teste Unitários
**Back-end:**
```bash
cd TaskManager.Tests
dotnet test
```

## Diferenciais Implementados
*   **Testes Unitários**: Garantia de qualidade na lógica de serviços.
*   **Arquitetura Limpa**: Separação em camadas (Controller, Service, Repository, DTO).
*   **Logs Estruturados**: Rastreabilidade de erros e operações.
*   **Documentação Completa**: Instruções claras para execução e testes.

## Licença
Este projeto está licenciado sob a licença MIT.
