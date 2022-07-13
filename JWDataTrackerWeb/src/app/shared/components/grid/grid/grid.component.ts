import { Component, Input, OnInit, Output,EventEmitter } from '@angular/core';
import { PageChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor } from '@progress/kendo-data-query';
import { PageGrid } from 'src/app/shared/models/GridFilter';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit {
  @Input() pageGrid: PageGrid;
  @Input() onLoadGrid: any;
  @Input() gridTitle: string;
  @Input() addButtonTitle: string;
  @Input() onAdd: any;
  @Output() onReload = new EventEmitter<string>();
  
  isLoading: boolean = false;
  constructor() { }

  async ngOnInit(): Promise<void> {
    await this.loadDataGrid();
  }

  async pageChange(event: PageChangeEvent): Promise<void> {
    this.pageGrid.pageChange(event);
    await this.loadDataGrid();
  }

  async sortChange(sort: SortDescriptor[]): Promise<void> {
    this.pageGrid.sortChange(sort);
    await this.loadDataGrid();
  }

  async filterChange(filter: any): Promise<void> {
    this.pageGrid.filterChange(filter);
    await this.loadDataGrid();
  }

  onAddClick() {
    if(this.onAdd) this.onAdd();
  }

  public async loadDataGrid() : Promise<void> {
    var result = await this.onLoadGrid(this.pageGrid.gridFilter);
    this.pageGrid.loadData(result);
  }
}