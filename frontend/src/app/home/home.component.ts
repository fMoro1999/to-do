import { TodoService } from './../service/todo.service';
import { ToDo } from './../model/models';
import { Component } from "@angular/core";

@Component({
  selector: "todo-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent {
  alert: string;

  constructor(private todoService: TodoService){}

  onAddItem(todo: ToDo) {
    
  }

  onUpdateItem(oldValue: string, newValue: string) {
    if (oldValue != null && oldValue !== newValue) {
      this.alert = oldValue + " is updated to " + newValue + " sucessfully...";
    } else if (oldValue != null && oldValue === newValue) {
      this.alert = oldValue + " is not updated...";
    }
  }

  onItemMessage(mgs) {
    this.alert = mgs;
  }
}
