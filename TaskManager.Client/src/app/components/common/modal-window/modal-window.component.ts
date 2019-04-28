import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-modal-window',
  templateUrl: './modal-window.component.html',
  styleUrls: ['./modal-window.component.css']
})
export class ModalWindowComponent implements OnInit {
  @Input() public headerText: string;
  @Input() public okButtonText = 'Ok';
  @Input() public cancelButtonText = 'Cancel';
  @Input() public okButtonDisabled = false;
  @Input() public cancelButtonDisabled = false;
  @Input() public isLoading = false;
  @Output() public okClick = new EventEmitter<any>();
  @Output() public cancelClick = new EventEmitter<any>();
  public constructor() { }

  public ngOnInit() {
  }

  public okButtonClick() {
    this.okClick.emit();
  }

  public cancelButtonClick() {
    this.cancelClick.emit();
  }
}
