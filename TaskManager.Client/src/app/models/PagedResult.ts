export class PagedResult<T> {
    constructor(
      public Items: T[],
      public PagesCount: number
    ) { }
  }
