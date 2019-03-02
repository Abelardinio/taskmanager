import { PagingInfo } from './PagingInfo';
import { SortingInfo } from './SortingInfo';


export class BaseFilter {
  constructor(
    public PagingInfo: PagingInfo
  ) { }
}

export class BaseSortableFilter<T> extends BaseFilter {
    constructor(
      public SortingInfo: SortingInfo<T>,
      public PagingInfo: PagingInfo
    ) { super(PagingInfo) }
  }
