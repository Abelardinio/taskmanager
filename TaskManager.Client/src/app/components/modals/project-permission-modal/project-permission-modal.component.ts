import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/models/User';
import { ProjectService } from 'src/app/services/ProjectService';
import { Lookup } from 'src/app/models/Lookup';
import { PermissionService } from 'src/app/services/PermissionService';
import { PermissionEditor } from './permission-editor';
import { PermissionEditingModel } from './permission-editing-model';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-project-permission-modal',
  templateUrl: './project-permission-modal.component.html',
  styleUrls: ['./project-permission-modal.component.css']
})
export class ProjectPermissionModalComponent implements OnInit {
  @Input() public set isVisible(value: boolean) {
    if (value) {
      this._permissionService.GetLookup().subscribe(permissions => this._permissionEditor = new PermissionEditor(permissions));

      this._projectService.GetLookup().subscribe(projects => {
        this.projectsArray = projects;
        this.onProjectSelect(projects[0]);
      });
    }

    this.isModalWindowVisible = value;
  }

  @Input() public user: User;
  @Output() public isVisibleChange = new EventEmitter();
  public projectsArray: Lookup[];
  public permissionsArray: PermissionEditingModel[] = [];
  public isModalWindowVisible = false;
  public selected: any = {};
  public isModelChanged = false;
  public isLoading = false;
  private _permissionEditor: PermissionEditor;

  public constructor(private _projectService: ProjectService,
    private _permissionService: PermissionService) { }

  public ngOnInit() {
  }

  public onCancelClick() {
    this._closeModalWindow();
  }

  public onProjectSelect(project: Lookup) {
    const projectId = project.Id,
      editingModel = this._permissionEditor.get(projectId);

    if (project.Id !== this.selected.Id) {
      this.selected = project;

      if (editingModel) {
        this.permissionsArray = editingModel;
      } else {
        this._permissionService.Get(this.user.Id, projectId).subscribe(permissions =>
          this.permissionsArray = this._permissionEditor.add(projectId, permissions));
      }
    }
  }

  public onOkClick() {
    const changes = this._permissionEditor.getChanges();

    if (changes.Permissions.length !== 0) {
      this.isLoading = true;
      this._permissionService.Update(this.user.Id, changes)
      .pipe(finalize(() => { this.isLoading = false; })).subscribe(() => this._closeModalWindow());
    }
  }

  public onPermissionUpdate() {
    this.isModelChanged = this._permissionEditor.getChanges().Permissions.length !== 0;
  }

  private _closeModalWindow() {
    this.selected = {};
    this.isModelChanged = false;
    this.isModalWindowVisible = false;
    this.permissionsArray = [];
    this.isVisibleChange.emit();
  }
}
