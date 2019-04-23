import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-project-permission-modal',
  templateUrl: './project-permission-modal.component.html',
  styleUrls: ['./project-permission-modal.component.css']
})
export class ProjectPermissionModalComponent implements OnInit {
  @Input() public isVisible = false;
  @Output() public isVisibleChange = new EventEmitter();
  public constructor() { }

  public ngOnInit() {
  }

  public onCancelClick() {
    this.isVisible = false;
    this.isVisibleChange.emit();
  }

  public onOkClick() {
  }
}
