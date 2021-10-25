import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ToDo } from '../model/models';
import { TodoService } from '../service/todo.service';

@Component({
  selector: 'todo-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  todos: ToDo[];
  todo: ToDo;

  @Output() deleteItemMessage = new EventEmitter();
  @Output() itemStatusChangeMessage = new EventEmitter();
  @Output() addItemMessage = new EventEmitter();

  constructor(private todoService: TodoService) {}

  refreshTodos(){
    this.todoService.loadTodos().subscribe(x => {
      this.todos = x;
      console.log("refreshTodos -> todos: ", this.todos);
    });
  }

  ngOnInit(): void {
    this.refreshTodos();
  }

  onAddItem(task) {
    if (task.value === "")
      return;

    this.todo = { description: task.value, isDone: false };
    task.value = "";

    this.addItemMessage.emit(task.value + " is added successfully...");
    this.todoService.addTodo(this.todo).subscribe(_ => this.refreshTodos());
    this.refreshTodos();
  }

  onStateChange(todo: ToDo) {
    todo.isDone = !todo.isDone;
    this.todoService.update(todo).subscribe(_ => this.refreshTodos());
    let msg = todo.isDone
    ? todo.description + ' is completed successfully...'
    : todo.description + ' is not completed yet...';
    this.itemStatusChangeMessage.emit(msg);
  }

  onDelete(todo: ToDo) {
    console.log("todo: ", todo);
    this.todoService.delete(todo).subscribe(_ => this.refreshTodos());
    this.deleteItemMessage.emit(todo.description + ' is deleted successfully...');
  }

}
