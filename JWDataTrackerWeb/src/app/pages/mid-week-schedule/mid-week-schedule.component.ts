import { Component, OnInit } from '@angular/core';
import { CommonService } from 'src/app/core/services/common.service';
import { AddEditMidWeekScheduleComponent } from 'src/app/shared/components/add-edit-mid-week-schedule/add-edit-mid-week-schedule.component';

@Component({
  selector: 'app-mid-week-schedule',
  templateUrl: './mid-week-schedule.component.html',
  styleUrls: ['./mid-week-schedule.component.scss']
})
export class MidWeekScheduleComponent implements OnInit {

  constructor(private commonService: CommonService) { }

  ngOnInit(): void {
  }

  openMidWeekModal = (dataItem: any = null) => {
    var component = this.commonService.displayAlert("Add/Edit Mid Week Schedule",AddEditMidWeekScheduleComponent,this.commonService.getModalWidth(),()=> {
    });

    if(dataItem){
      component.id = dataItem.publisherId;
    }
  }


}
