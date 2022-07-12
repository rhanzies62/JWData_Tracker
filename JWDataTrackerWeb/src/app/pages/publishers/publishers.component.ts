import { Component, OnInit } from '@angular/core';
import { CommonService } from 'src/app/core/services/common.service';
import { AddEditPubilisherComponent } from './add-edit-pubilisher/add-edit-pubilisher.component';

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
  constructor(private commonService: CommonService) { }

  ngOnInit(): void {
  }

  openPublisherModal(){
    this.commonService.displayAlert("Add/Edit Publisher",AddEditPubilisherComponent,this.commonService.getModalWidth(),()=> {});
  }

}
