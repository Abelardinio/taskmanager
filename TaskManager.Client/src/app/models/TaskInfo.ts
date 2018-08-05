export class TaskInfo {
  constructor(
    public name: string,
    public description: string,
    public priority: number,
    public timeToComplete: TimeSpan
  ) { }
}

export class TimeSpan {
  constructor(
    public weeks: number,
    public days: number,
    public hours: number
  ) { }
}
