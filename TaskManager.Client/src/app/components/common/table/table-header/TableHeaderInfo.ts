
export class TableHeaderInfo<T> {
    constructor(
      public Text: string,
      public ClassName: string,
      public SortingNumber: T = null,
      public Sortable: boolean = true
    ) { }
  }
