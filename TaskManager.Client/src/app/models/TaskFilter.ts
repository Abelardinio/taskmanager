import { TaskSortingColumn } from './enums/TaskSortingColumn';
import { NumberRange } from './NumberRange';
import { PagingInfo } from './PagingInfo';
import { SortingInfo } from './SortingInfo';
import { BaseFilter } from './BaseFilter';

export class TaskFilter extends BaseFilter<TaskSortingColumn> {
  constructor(
    public SortingInfo: SortingInfo<TaskSortingColumn>,
    public PagingInfo: PagingInfo,
    public Name?: string,
    public AddedFrom?: Date,
    public AddedTo?: Date,
    public Priority?: NumberRange
  ) {
    super(SortingInfo, PagingInfo);
  }
}
