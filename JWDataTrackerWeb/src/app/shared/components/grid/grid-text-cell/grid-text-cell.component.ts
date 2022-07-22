import { Component, Input, OnInit } from '@angular/core';
import { Columns } from 'src/app/shared/models/GridColumns';
import { MidWeekCategories } from 'src/app/shared/models/midWeekSchedule';

@Component({
  selector: 'app-grid-text-cell',
  templateUrl: './grid-text-cell.component.html',
  styleUrls: ['./grid-text-cell.component.scss']
})
export class GridTextCellComponent implements OnInit {
  @Input() column: Columns;
  @Input() dataItem: any;
  midWeekCategories: any = MidWeekCategories;
  constructor() { }

  ngOnInit(): void {
  }

}
