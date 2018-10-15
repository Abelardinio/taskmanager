import { TaskSortingColumn } from './enums/TaskSortingColumn';
import { NumberRange } from './NumberRange';
import { PagingInfo } from './PagingInfo';
import { SortingInfo } from './SortingInfo';

export class TaskFilter {
    constructor(
      public SortingInfo: SortingInfo<TaskSortingColumn>,
      public PagingInfo: PagingInfo,
      public Name?: string,
      public AddedFrom?: Date,
      public AddedTo?: Date,
      public Priority?: NumberRange,
    ) { }
  }
