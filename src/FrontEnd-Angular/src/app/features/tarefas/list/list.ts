import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TarefaService } from '../../../core/services/tarefa.service';
import { Tarefa, StatusTarefa } from '../../../models/tarefa.model';
import { Observable } from 'rxjs';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './list.html',
  styleUrls: ['./list.css']
})
export class ListComponent implements OnInit {
  tarefas: Tarefa[] = [];

  constructor(
  private tarefaService: TarefaService,
  private cdr: ChangeDetectorRef
) {}

ngOnInit(): void { 
  console.log("LIST COMPONENT CARREGADO");
  this.carregar(); 
}

carregar(): void {
  console.log("Chamando API...");
  
  this.tarefaService.getAll().subscribe({
    next: (data) => {
      console.log("Dados recebidos:", data);
      this.tarefas = data;
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.error('Erro ao carregar tarefas:', err);
    }
  });
}

excluir(id: string): void {
  if (confirm('Deseja excluir?')) {
    (this.tarefaService.delete(id) as Observable<any>).subscribe({
      next: () => {
        this.carregar();
      },
      error: (err: any) => {
        console.error('Erro:', err);
      }
    });
  }
}

  getStatusTexto(status: StatusTarefa): string {
    return status === StatusTarefa.Concluida ? 'Concluída' : 'Pendente';
  }
}