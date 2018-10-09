
export class TableHeaderInfo {
    constructor(
      public Text: string,
      public ClassName: string,
      public SortingNumber: number = null,
      public Sortable: boolean = true
    ) { }
  }
