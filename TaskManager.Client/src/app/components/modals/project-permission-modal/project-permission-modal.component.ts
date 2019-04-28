import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/models/User';
import { ProjectService } from 'src/app/services/ProjectService';
import { Lookup } from 'src/app/models/Lookup';

@Component({
  selector: 'app-project-permission-modal',
  templateUrl: './project-permission-modal.component.html',
  styleUrls: ['./project-permission-modal.component.css']
})
export class ProjectPermissionModalComponent implements OnInit {
  @Input() public set isVisible(value: boolean) {
    if (value) {
      this._projectService.GetLookup().subscribe(projects => this.projectsArray = projects);
    }

    this.isModalWindowVisible = value;
  }

  @Input() public user: User;
  @Output() public isVisibleChange = new EventEmitter();
  public projectsArray: Lookup[];
  public isModalWindowVisible = false;
  public selected: any = {};

  public constructor(private _projectService: ProjectService) { }

  public ngOnInit() {
  }

  public onCancelClick() {
    this.isModalWindowVisible = false;
    this.isVisibleChange.emit();
  }

  public onProjectSelect(project: Lookup) {
    this.selected = project;
  }

  public onOkClick() {
  }
}
