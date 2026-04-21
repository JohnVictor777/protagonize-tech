import { Routes } from '@angular/router';
import { ListComponent } from './features/tarefas/list/list';
import { FormComponent } from './features/tarefas/form/form';

export const routes: Routes = [
  { path: '', component: ListComponent },
  { path: 'criar', component: FormComponent },
  { path: 'editar/:id', component: FormComponent },
  { path: '**', redirectTo: '' }
];