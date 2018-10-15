import { SortingOrder } from './enums/SortingOrder';

export class SortingInfo<T> {
    constructor(
      public Order: SortingOrder,
      public Column: T
    ) { }
  }
