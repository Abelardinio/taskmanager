import { TaskStatus } from './enums/TaskStatus';


export class Task {
  constructor(
    public Id: number,
    public Name: string,
    public Added: Date,
    public Description: string,
    public Priority: number,
    public Status: TaskStatus,
    public AssignedUserId: number
  ) { }
}
