import { SortingOrder } from './enums/SortingOrder';
import { TaskSortingColumn } from './enums/TaskSortingColumn';

export class TaskFilter {
    constructor(
      public SortingOrder: SortingOrder = 0,
      public SortingColumn: TaskSortingColumn = 0,
      public Name: string
    ) { }
  }
