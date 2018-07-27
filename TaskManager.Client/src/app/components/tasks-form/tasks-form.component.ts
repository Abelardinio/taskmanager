import { Component, OnInit, OnDestroy } from '@angular/core';
import { TaskService } from '../../services/TaskService';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { TaskFilter, TakeType } from '../../models/TaskFilter';
export { TaskStatus } from '../../models/enums/TaskStatus';
import { finalize} from 'rxjs/operators';
import { Location } from '@angular/common';
import { Task } from '../../models/Task';
import { TaskListDataSource } from '../../models/data-sources/TaskListDataSource';
import { TaskStatus } from '../../models/enums/TaskStatus';

@Component({
  selector: 'app-tasks-form',
  templateUrl: './tasks-form.component.html',
  styleUrls: ['./tasks-form.component.css'],
  host: { 'class': 'flex-column flexible' }
})
export class TasksFormComponent implements OnInit, OnDestroy {
  tasks: Task[] = [];
  subject = new BehaviorSubject(this.tasks);
  dataSource: TaskListDataSource = new TaskListDataSource(this.subject.asObservable());
  displayedColumns = ['Name', 'Priority', 'Added', 'TimeToComplete', 'Action'];
  selectedTask = {};
  tableTopIsLoading: boolean;
  tableBottomIsLoading: boolean;
  tableIsInitialized: boolean;
  fetchDataInterval;
  constructor(
    private _taskService: TaskService,
    private route: ActivatedRoute,
    private location: Location) {

  }

  ngOnInit() {
    let id = Number(this.route.snapshot.paramMap.get('id'));
    let taketype = TakeType.None;

    if (id > 0) {
      taketype = TakeType.BeforeAndAfter
    }

    this._taskService.Get(new TaskFilter(id, 30, taketype)).subscribe(
      data => {
        this.tasks.push(...(<Task[]>data));
        this.subject.next(this.tasks);

        if (id !== 0) {
          this._selectTask(this._getTask(id));
          
          setTimeout(() => {          
            document.getElementsByClassName( 'selected' )[ 0 ].scrollIntoView({block: "end", behavior: "smooth"});
            setTimeout(() => {          
              this.tableIsInitialized = true;
            }, 300);
          }, 300);
        }else{
          this.tableIsInitialized = true;
        }
      });

      this.fetchDataInterval = setInterval(()=>{
          if (this.tasks.length<30){
            this._loadNextTasks();
          }
      }, 10000);
  }

  ngOnDestroy(){
    clearInterval(this.fetchDataInterval);
  }

  onRowSelect(task: Task, event) {
    if (event.target.className === "btn") return;
    this._selectTask(task);
  }

  onCompleteButtonClick(element) {
    if (element.IsLoading) return;
    element.IsLoading = true;
    
    this._taskService.Complete(element.Id)
      .pipe(finalize(() => { element.IsLoading = false; }))
      .subscribe(
        () => { element.Status = TaskStatus.Completed; });
  }

  onRemoveButtonClick(element) {
    if (element.IsLoading) return;
    element.IsLoading = true;

    this._taskService.Delete(element.Id)
      .pipe(finalize(() => { element.IsLoading = false; }))
      .subscribe(
        () => {
          element.Status = TaskStatus.Removed;
          this.tasks.splice(this.tasks.indexOf(element), 1);
          this.subject.next(this.tasks); this._selectTask(null);
        });
  }

  onTableScroll(e) {
    if (!this.tableIsInitialized) return;
    const tableViewHeight = e.target.offsetHeight // viewport: ~500px
    const tableScrollHeight = e.target.scrollHeight // length of all table
    const scrollLocation = e.target.scrollTop; // how far user scrolled

    // If the user has scrolled within 200px of the bottom, add more data
    const limit = 500;
    const topLimit = tableScrollHeight - tableViewHeight - limit;
    const bottomLimit = limit;
    if (scrollLocation > topLimit) {
      if (!this.tableTopIsLoading) {
        this.tableTopIsLoading = true;        
        this._loadNextTasks();
      }
    }

    if (scrollLocation < bottomLimit) {
      if (!this.tableBottomIsLoading) {
        this.tableBottomIsLoading = true;        
        this._loadPreviousTasks();
      }
    }
  }  

  private _loadPreviousTasks() {
    this._taskService.Get(new TaskFilter(this.tasks[0].Id, 20, TakeType.Before)).subscribe(
      data => {
        this.tasks.unshift(...(<Task[]>data));
        this.tasks.splice(this.tasks.length - (<Task[]>data).length, (<Task[]>data).length);

        this.subject.next(this.tasks);
        let timeout = (<Task[]>data).length === 0 ? 3000 : 100;
        setTimeout(() => {
          this.tableBottomIsLoading = false;
        }, timeout);
      });
  }

  private _loadNextTasks() {
    this._taskService.Get(new TaskFilter(this.tasks[this.tasks.length - 1].Id, 20, TakeType.After)).subscribe(
      data => {
        this.tasks.push(...(<Task[]>data));
        this.tasks.splice(0, (<Task[]>data).length);

        this.subject.next(this.tasks);
        let timeout = (<Task[]>data).length === 0 ? 3000 : 100;
        setTimeout(() => {
          this.tableTopIsLoading = false;
        }, timeout);
      });
  }

  

  private _selectTask(task: Task) {
    if (task) {
      this.selectedTask = task;
      this.location.go("tasks/" + task.Id);
    }
    else {
      this.selectedTask = {};
      this.location.go("tasks");
    }
  }

  private _getTask(taskId: number): Task {
    return this.tasks.filter(x => x.Id === taskId)[0];
  }
}


