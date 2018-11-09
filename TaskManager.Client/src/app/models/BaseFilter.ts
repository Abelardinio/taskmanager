import { PagingInfo } from './PagingInfo';
import { SortingInfo } from './SortingInfo';

export class BaseFilter<T> {
    constructor(
      public SortingInfo: SortingInfo<T>,
      public PagingInfo: PagingInfo
    ) { }
  }
