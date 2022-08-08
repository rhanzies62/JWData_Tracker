import { DataStateChangeEvent, GridDataResult, PageChangeEvent, PagerSettings } from '@progress/kendo-angular-grid';
import { CompositeFilterDescriptor, SortDescriptor, State } from '@progress/kendo-data-query';
import { Columns } from './GridColumns';

export class PageGrid {
  constructor(
    columns: Columns[],
    pageSize: number = 10,
    defaultSortField: string = '',
    defaultSortFieldDirection: any = 'desc'
  ) {
    this.gridResult = { data: [], total: 0 };

    this.columns = columns;

    this.gridFilter = new GridFilter();
    this.gridFilter.Take = pageSize;
    this.gridFilter.Skip = 0;
    this.gridFilter.Searchs = [];

    this.state = {
      skip: 0,
      take: pageSize,
      filter: { filters: [], logic: 'or' },
      group: [],
      sort: []
    };

    this.pagerSettings = {
        buttonCount: 5,
        info: true,
        type: 'numeric',
        pageSizes: true,
        previousNext: true,
        position: 'bottom'
    }

    if (defaultSortField && defaultSortFieldDirection) {
      this.gridFilter.Field = defaultSortField;
      this.gridFilter.Direction = defaultSortFieldDirection;
      this.sort = [
        {
          field: defaultSortField,
          dir: defaultSortFieldDirection,
        },
      ];
    }
  }
  columns: Columns[];
  totalCount: number;
  gridFilter: GridFilter;
  gridResult: GridDataResult;
  sort: SortDescriptor[];
  pagerSettings: PagerSettings;
  filter: CompositeFilterDescriptor;
  state: State;

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
}

  public pageChange(event: PageChangeEvent) {
    this.gridFilter.Skip = event.skip;
    this.gridFilter.Take = event.take;
  }

  public sortChange(sort: SortDescriptor[]) {
    var column = this.columns.find(c => c.fieldName === sort[0].field);
    this.sort = sort;
    this.gridFilter.Field = column.sortColumn || column.fieldName;
    this.gridFilter.Direction = this.sort[0].dir;
  }

  public filterChange(filters: CompositeFilterDescriptor){
    this.gridFilter.Searchs = [];
    filters.filters.map(f => {
      this.gridFilter.Searchs.push(new GridSearchFilter(f["field"],"like",f["value"],filters.logic))
    });
    this.gridFilter.Skip = 0;
    this.filter = filters;
  }

  public loadData(result: GridResultGeneric<any>) {
    this.gridResult = {
        data: result.data,
        total: result.total
      };
  }
}

export class GridFilter {
  Operator: string;
  Field: string;
  Direction: string;
  _value: string;
  Take: number;
  Skip: number;
  Searchs: GridSearchFilter[];
}

export class GridSearchFilter {
  constructor(
    field: string,
    operator: string,
    value: string,
    appendType: string
  ) {
    this.Field = field;
    this.Operator = operator;
    this.Value = value;
    this.AppendType = appendType;
  }
  Field: string;
  Operator: string;
  Value: string;
  AppendType: string;
}

export class GridResultGeneric<T> {
  data: any;
  total: number;
  isSuccess: boolean;
  message: string;
}
