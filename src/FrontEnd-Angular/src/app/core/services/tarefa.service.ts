import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tarefa, TarefaRequest } from '../../models/tarefa.model';

@Injectable({
  providedIn: 'root'
} )
export class TarefaService {
  private apiUrl = 'http://localhost:5014/api/Tarefas'; 

  constructor(private http: HttpClient ) {}

  getAll(): Observable<Tarefa[]> {
    return this.http.get<Tarefa[]>(this.apiUrl );
  }

  getById(id: string): Observable<Tarefa> {
    return this.http.get<Tarefa>(`${this.apiUrl}/${id}` );
  }

  create(tarefa: TarefaRequest): Observable<Tarefa> {
    return this.http.post<Tarefa>(this.apiUrl, tarefa );
  }

  update(id: string, tarefa: TarefaRequest): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, tarefa );
  }

  delete(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}` );
  }
}
