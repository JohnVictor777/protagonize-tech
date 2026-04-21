import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TarefaService } from '../../../core/services/tarefa.service';
import { TarefaRequest, StatusTarefa } from '../../../models/tarefa.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './form.html',
  styleUrls: ['./form.css']
})
export class FormComponent implements OnInit {
  tarefa: TarefaRequest = { titulo: '', descricao: '', status: StatusTarefa.Pendente };
  id: string | null = null;

  constructor(
    private service: TarefaService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.service.getById(this.id).subscribe(data => {
        this.tarefa = { titulo: data.titulo, descricao: data.descricao, status: data.status };
      });
    }
  }

salvar(): void {
  const acao = this.id 
    ? this.service.update(this.id, this.tarefa) 
    : this.service.create(this.tarefa);

  (acao as Observable<any>).subscribe({
    next: () => {
      this.router.navigate(['/']);
    },
    error: (err: any) => { 
      console.error('Erro:', err);
    }
  });
}

}