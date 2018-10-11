import { SortingOrder } from './enums/SortingOrder';
import { TaskSortingColumn } from './enums/TaskSortingColumn';
import { NumberRange } from './NumberRange';

export class TaskFilter {
    constructor(
      public SortingOrder: SortingOrder = 0,
      public SortingColumn: TaskSortingColumn = 0,
      public Name?: string,
      public AddedFrom?: Date,
      public AddedTo?: Date,
      public Priority?: NumberRange
    ) { }
  }
