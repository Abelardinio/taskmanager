import { Component, OnInit, Input, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-timer',
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
    this.interval = setInterval(() => this._setTimeLeft(), 1000);
  }

  private _setTimeLeft() {
    const now = new Date().getTime();
    const distance = Date.parse(this.completionDate) - now;

    if (distance < 0) {
      clearInterval(this.interval);
      this.timeLeft = this.expiredPlaceholder;
      return;
    }

    const days = Math.floor(distance / (1000 * 60 * 60 * 24));
    const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((distance % (1000 * 60)) / 1000);

    this.timeLeft = days + 'd ' + hours + 'h '
      + minutes + 'm ' + seconds + 's ';
  }
}
