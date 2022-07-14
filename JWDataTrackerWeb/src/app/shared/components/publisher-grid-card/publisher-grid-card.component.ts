import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-publisher-grid-card',
  templateUrl: './publisher-grid-card.component.html',
  styleUrls: ['./publisher-grid-card.component.scss']
})
export class PublisherGridCardComponent implements OnInit {
  @Input() dataItem: any;
  constructor() { }

  ngOnInit(): void {
  }

}
