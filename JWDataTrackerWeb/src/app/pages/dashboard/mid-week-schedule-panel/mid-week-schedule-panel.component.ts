import { Component, OnInit } from '@angular/core';
import { MidWeekScheduleApiservice } from 'src/app/core/apiService/mid-week-schedule-api.service';
import { CommonService } from 'src/app/core/services/common.service';
import { AddEditMidWeekScheduleComponent } from 'src/app/shared/components/add-edit-mid-week-schedule/add-edit-mid-week-schedule.component';
import { MidWeekCategories, MidWeekSchedule, MidWeekScheduleItem } from 'src/app/shared/models/midWeekSchedule';

@Component({
  selector: 'app-mid-week-schedule-panel',
  templateUrl: './mid-week-schedule-panel.component.html',
  styleUrls: ['./mid-week-schedule-panel.component.scss']
})
export class MidWeekSchedulePanelComponent implements OnInit {
  currentMidWeekSchedule: MidWeekSchedule;
  midWeekCategories: any = MidWeekCategories;
  categoryArrangement: string[] = [ MidWeekCategories.TREASUREFROMGODSWORD,MidWeekCategories.APPLYYOURSELFTOTHEFIELDMINISTRY,MidWeekCategories.LIVINGASACHRISTIAN,MidWeekCategories.ATTENDANTS ];
  constructor(private midWeekScheduleApiService: MidWeekScheduleApiservice, private commonService: CommonService) { }

  async ngOnInit(): Promise<void> {
    this.commonService.toggleLoadingScreen();
    var schedules = await this.midWeekScheduleApiService.List(0,1);
    this.commonService.toggleLoadingScreen();
    if(schedules.data){
      this.currentMidWeekSchedule = schedules.data[0];
    }
  }

  filterByCategory(category:string = MidWeekCategories.OPENING): MidWeekScheduleItem[] {
    return this.currentMidWeekSchedule.midWeekScheduleItems.filter(i => i.category === category)
  }

  openMidWeekModal = () => {
    var component = this.commonService.displayAlert("Add/Edit Mid Week Schedule",AddEditMidWeekScheduleComponent,this.commonService.getModalWidth(), async ()=> {
      var schedules = await this.midWeekScheduleApiService.List(0,1);
      if(schedules.data){
        this.currentMidWeekSchedule = schedules.data[0];
      }
    });
    component.scheduleDate = new Date(this.currentMidWeekSchedule.scheduledDate);
  }
}
