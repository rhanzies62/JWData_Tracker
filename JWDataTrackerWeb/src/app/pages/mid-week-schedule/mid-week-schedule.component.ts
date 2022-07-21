import { Component, HostListener, OnInit } from '@angular/core';
import { PageChangeEvent } from '@progress/kendo-angular-grid';
import { MidWeekScheduleApiservice } from 'src/app/core/apiService/mid-week-schedule-api.service';
import { CommonService } from 'src/app/core/services/common.service';
import { AddEditMidWeekScheduleComponent } from 'src/app/shared/components/add-edit-mid-week-schedule/add-edit-mid-week-schedule.component';
import { GridResultGeneric } from 'src/app/shared/models/GridFilter';
import { MidWeekCategories, MidWeekSchedule, MidWeekScheduleItem } from 'src/app/shared/models/midWeekSchedule';

@Component({
  selector: 'app-mid-week-schedule',
  templateUrl: './mid-week-schedule.component.html',
  styleUrls: ['./mid-week-schedule.component.scss']
})
export class MidWeekScheduleComponent implements OnInit {
  skip: number = 0;
  pageSize: number = 3;
  schedules: GridResultGeneric<MidWeekSchedule[]>;
  midWeekCategories: any = MidWeekCategories;
  categoryArrangement: string[] = [ MidWeekCategories.OPENING,MidWeekCategories.TREASUREFROMGODSWORD,MidWeekCategories.APPLYYOURSELFTOTHEFIELDMINISTRY,MidWeekCategories.LIVINGASACHRISTIAN,MidWeekCategories.ATTENDANTS ];
  constructor(private commonService: CommonService,private midWeekScheduleApiService: MidWeekScheduleApiservice) { }

  async ngOnInit(): Promise<void> {
    await this.loadSchedules();
  }

  openMidWeekModal = (dataItem: any = null) => {
    var component = this.commonService.displayAlert("Add/Edit Mid Week Schedule",AddEditMidWeekScheduleComponent,this.commonService.getModalWidth(), async ()=> {
      this.schedules = await this.midWeekScheduleApiService.List(this.skip,this.pageSize);
    });

    if(dataItem){
      component.scheduleDate = new Date(dataItem.scheduledDate);
    }
  }

  filterByCategory(category:string, scheduleItem: MidWeekSchedule): MidWeekScheduleItem[] {
    return scheduleItem.midWeekScheduleItems.filter(i => i.category === category)
  }

  async pageChange(event: PageChangeEvent): Promise<void> {
    this.skip = event.skip;
    this.pageSize = event.take;
    this.schedules = await this.midWeekScheduleApiService.List(this.skip,this.pageSize);
  }

  @HostListener('window:resize', ['$event'])
  async onResize(event) {
    await this.loadSchedules();
  }

  async loadSchedules() {
    if (window.innerWidth <= 767){
      this.pageSize = 1;
      this.skip = 0;
      this.schedules = await this.midWeekScheduleApiService.List(this.skip,this.pageSize);
    }
    else if(window.innerWidth > 767 && window.innerWidth < 991){
      this.pageSize = 2;
      this.skip = 0;
      this.schedules = await this.midWeekScheduleApiService.List(this.skip,this.pageSize);
    }
    else if(window.innerWidth > 991){
      this.pageSize = 3;
      this.skip = 0;
      this.schedules = await this.midWeekScheduleApiService.List(this.skip,this.pageSize);
    } 
  }

}
