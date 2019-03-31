import { TaskSortingColumn } from './enums/TaskSortingColumn';
import { NumberRange } from './NumberRange';
import { PagingInfo } from './PagingInfo';
import { SortingInfo } from './SortingInfo';
import { BaseSortableFilter } from './BaseFilter';

export class TaskFilter extends BaseSortableFilter<TaskSortingColumn> {
  constructor(
    public SortingInfo: SortingInfo<TaskSortingColumn>,
    public PagingInfo: PagingInfo,
    public Name?: string,
    public AddedFrom?: Date,
    public AddedTo?: Date,
    public Priority?: NumberRange,
    public ProjectId?: number,
    public FeatureId?: number
  ) {
    super(SortingInfo, PagingInfo);
  }
}
