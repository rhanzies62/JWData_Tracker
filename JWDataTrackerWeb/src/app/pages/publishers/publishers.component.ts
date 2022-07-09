import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-publishers',
  templateUrl: './publishers.component.html',
  styleUrls: ['./publishers.component.scss']
})
export class PublishersComponent implements OnInit {
  public gridData: any[] = [
      {
          ProductID: 1,
          ProductName: 'Chai',
          UnitPrice: 18,
          Category: {
              CategoryID: 1,
              CategoryName: 'Beverages'
          }
      }
  ];
  constructor() { }

  ngOnInit(): void {
  }

}
