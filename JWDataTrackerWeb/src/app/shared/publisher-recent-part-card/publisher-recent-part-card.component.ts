import { Component, Input, OnInit } from '@angular/core';
import { MidWeekCategories } from '../models/midWeekSchedule';
import { RecentPart } from '../models/publisher';

@Component({
  selector: 'app-publisher-recent-part-card',
  templateUrl: './publisher-recent-part-card.component.html',
  styleUrls: ['./publisher-recent-part-card.component.scss']
})
export class PublisherRecentPartCardComponent implements OnInit {
  @Input() dataItem: RecentPart;
  midWeekCategories: any = MidWeekCategories;
  constructor() { }

  ngOnInit(): void {
  }

}
