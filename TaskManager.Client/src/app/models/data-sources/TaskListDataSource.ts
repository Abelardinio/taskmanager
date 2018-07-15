import { Observable } from "../../../../node_modules/rxjs";
import { Task } from "../Task";
import { DataSource } from '@angular/cdk/table';

export class TaskListDataSource extends DataSource<Task> {


  constructor(private taskList: Observable<Task[]>) {
    super();
  }

  connect(): Observable<Task[]> {
    return this.taskList;
  }

  disconnect() {
  }
}