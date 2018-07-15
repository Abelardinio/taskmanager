import { Component, OnInit, Input, OnDestroy } from '@angular/core';

@Component({
  selector: 'timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css']
})

export class TimerComponent implements OnInit, OnDestroy {
  constructor() { }

  @Input()
  completionDate: string;

  @Input()
  expiredPlaceholder: string;

  timeLeft;
  interval;

  ngOnInit() {
    this.timeLeft = this.completionDate;
    this.initTimer();
    this._setTimeLeft();
  }

  ngOnDestroy(): void {
    clearInterval(this.interval);
  }

  initTimer() {
    this.interval = setInterval(()=>this._setTimeLeft(), 1000);
  }

  private _setTimeLeft() {
    var now = new Date().getTime();
    var distance = Date.parse(this.completionDate) - now;
      
    if (distance < 0) {
      clearInterval(this.interval);
      this.timeLeft = this.expiredPlaceholder;
      return;
    }

    var days = Math.floor(distance / (1000 * 60 * 60 * 24));
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    this.timeLeft = days + "d " + hours + "h "
      + minutes + "m " + seconds + "s ";
  }
}