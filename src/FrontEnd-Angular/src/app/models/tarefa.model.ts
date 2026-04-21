export enum StatusTarefa {
  Pendente = 0,
  Concluida = 1
}

export interface Tarefa {
  id: string; // Guid do back-end
  titulo: string;
  descricao: string;
  status: StatusTarefa;
  dataCriacao: string;
}

export interface TarefaRequest {
  titulo: string;
  descricao: string;
  status: StatusTarefa;
}