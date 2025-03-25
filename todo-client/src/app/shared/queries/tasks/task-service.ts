import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITask } from './models/task';
import { API_URL } from '../../../app.config';


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = API_URL + '/tasks/';

  constructor(private http: HttpClient) {}

  getTasksByUserId(userId: number): Observable<ITask[]> {
    return this.http.get<ITask[]>(`${this.apiUrl}${userId}`);
  }

  // Método para excluir uma tarefa
  deleteTask(taskId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}${taskId}`);
  }

  // Método para atualizar uma tarefa
  updateTask(taskId: number, task: ITask): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}${taskId}`, task);
  }
  
  // Método para adicionar uma nova tarefa
  addTask(task: ITask): Observable<void> {
    return this.http.post<void>(this.apiUrl, task);
  }
  
}
