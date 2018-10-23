import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Utils } from '../../../common/utils';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css']
})

export class TimerComponent implements OnInit, OnDestroy {
  constructor() { }

  @Input()
  public completionDate: string;

  @Input()
  public expiredPlaceholder: string;

  public timeLeft;
  public interval;

  public ngOnInit() {
    this.timeLeft = this.completionDate;
    this.initTimer();
    this._setTimeLeft();
  }

  public ngOnDestroy(): void {
    clearInterval(this.interval);
  }

  private initTimer() {
    this.interval = setInterval(() => this._setTimeLeft(), 1000);
  }

  private _setTimeLeft() {

    var dateTimeRange = Utils.getDateTimeRange(new Date(), new Date(this.completionDate))

    if (!dateTimeRange) {
      clearInterval(this.interval);
      this.timeLeft = this.expiredPlaceholder;
      return;
    }

    this.timeLeft = dateTimeRange.days + 'd ' + dateTimeRange.hours + 'h '
      + dateTimeRange.minutes + 'm ' + dateTimeRange.seconds + 's ';
  }
}
