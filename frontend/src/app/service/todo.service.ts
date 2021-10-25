import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToDo } from '../model/models';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  url: string = 'http://localhost:5000/api/todos';

  constructor(private http: HttpClient) { }

  loadTodos(): Observable<ToDo[]>{
    return this.http.get<ToDo[]>(this.url);
  }

  addTodo(toDo: ToDo){
    const httpOptions = {
      headers: new HttpHeaders({'Content-Type':'application/json'})
    };
    return this.http.post(this.url, JSON.stringify(toDo), httpOptions);
  }

  update(toDo: ToDo){
    const httpOptions = {
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    };
    return this.http.put(`${this.url}/${toDo.id}`, JSON.stringify(toDo), httpOptions);
  }

  delete(toDo: ToDo){
    console.log(toDo);
    return this.http.delete(`${this.url}/${toDo.id}`);
  }
}
