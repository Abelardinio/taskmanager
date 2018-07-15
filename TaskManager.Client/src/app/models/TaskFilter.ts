export class TaskFilter {
    constructor(
      public taskId: number,
      public count: number,
      public type: TakeType,
    ) { }
    }

export enum TakeType{
  None = 0,
  Before = 1,
  After = 2,
  BeforeAndAfter = 3
}