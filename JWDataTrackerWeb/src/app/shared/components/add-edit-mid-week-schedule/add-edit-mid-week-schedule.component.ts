import { ThrowStmt } from '@angular/compiler';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { GridComponent } from '@progress/kendo-angular-grid';
import { MidWeekScheduleApiservice } from 'src/app/core/apiService/mid-week-schedule-api.service';
import { PublisherApiService } from 'src/app/core/apiService/publisher-api.service';
import { CommonService } from 'src/app/core/services/common.service';
import { PublisherRecentPartColumns } from '../../models/GridColumns';
import { GridFilter, GridResultGeneric, PageGrid } from '../../models/GridFilter';
import { MidWeekCategories, MidWeekCategoryRoles, MidWeekSchedule, MidWeekScheduleItem, MidWeekScheduleRoles } from '../../models/midWeekSchedule';
import { Publisher, RecentPart } from '../../models/publisher';

@Component({
  selector: 'app-add-edit-mid-week-schedule',
  templateUrl: './add-edit-mid-week-schedule.component.html',
  styleUrls: ['./add-edit-mid-week-schedule.component.scss']
})
export class AddEditMidWeekScheduleComponent implements OnInit {
  public publisherRecentPartPageGrid : PageGrid;
  @ViewChild(GridComponent) publisherGrid: GridComponent;
  @Input() scheduleDate: Date;
  message: string = "";
  midWeekSchedule: MidWeekSchedule;
  categoryArrangement: string[] = [ MidWeekCategories.OPENING,MidWeekCategories.TREASUREFROMGODSWORD,MidWeekCategories.APPLYYOURSELFTOTHEFIELDMINISTRY,MidWeekCategories.LIVINGASACHRISTIAN,MidWeekCategories.ATTENDANTS ];
  currentCategory: string = MidWeekCategories.OPENING;
  midWeekCategories: any = MidWeekCategories;
  publishers: Publisher[] = [];
  selectedPublisher: Publisher;

  sourceElderMs: Publisher[] = [];
  filteredElderMs: Publisher[] = [];

  sourcePublisher: Publisher[] = [];
  filteredPublisher: Publisher[] = [];

  sourceAttendants: Publisher[] = [];
  filteredAttendants: Publisher[] = [];
  
  constructor(private publisherApiService: PublisherApiService,private midWeekScheduleApiService: MidWeekScheduleApiservice,private commonService: CommonService) { }

  async ngOnInit(): Promise<void> {
    this.publisherRecentPartPageGrid = new PageGrid(PublisherRecentPartColumns,4,'scheduledDate','desc');
    if(this.scheduleDate){
      await this.loadScheduleByDate(this.scheduleDate);
    } else {
      this.buildMidWeekSchedule();  
    }
    
    this.publishers = await this.publisherApiService.List();
    this.sourcePublisher = this.publishers.filter(i => !i.isElder);
    this.sourceElderMs = this.publishers.filter(i => i.isElder || i.isMs);
    this.sourceAttendants = this.publishers;
    this.handleFilter("");
    this.handleFilterForElderMs("");
    this.handleFilterForAttendants("");
  }

  async loadScheduleByDate(date: Date){
    var result = await this.midWeekScheduleApiService.GetMidWeekScheduleByDate(date);
    if(result){
      result.scheduledDate = new Date(result.scheduleDT);
      result.midWeekScheduleItems.map(mwsi => {
        if((mwsi.category === MidWeekCategories.APPLYYOURSELFTOTHEFIELDMINISTRY && mwsi.role !== MidWeekScheduleRoles.VideoDiscussion) ||
            mwsi.category === MidWeekCategories.ATTENDANTS) {
            mwsi.withPartner = true;
        }

        if(mwsi.category === MidWeekCategories.OPENING ||
           (mwsi.category === MidWeekCategories.TREASUREFROMGODSWORD && mwsi.role !== MidWeekScheduleRoles.BibleReading) ||
           (mwsi.category === MidWeekCategories.APPLYYOURSELFTOTHEFIELDMINISTRY && mwsi.role === MidWeekScheduleRoles.VideoDiscussion) ||
           (mwsi.category === MidWeekCategories.LIVINGASACHRISTIAN && mwsi.role !== MidWeekScheduleRoles.CBSReader)) {
           mwsi.isForElderMs = true;
        }
      });
      this.midWeekSchedule = result;
    } 
  }

  async onScheduleDateChange() {
    await this.loadScheduleByDate(this.midWeekSchedule.scheduledDate);
  }

  changeCategory(nextPrevious: number){
    var index = this.categoryArrangement.indexOf(this.currentCategory) + nextPrevious;
    if(index > -1 && index < this.categoryArrangement.length)
      this.currentCategory = this.categoryArrangement[index];
  }

  clearValue(midWeekScheduleItem : MidWeekScheduleItem, obj: string){
    midWeekScheduleItem[obj] = null;
  }

  loadSchedulesByCategory(){
    return this.midWeekSchedule.midWeekScheduleItems.filter(mwsi => mwsi.category === this.currentCategory);
  }

  handleFilter(value) {
    this.filteredPublisher = this.sourcePublisher.filter(
      (s) => s.fullName.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }

  handleFilterForElderMs(value) {
    this.filteredElderMs = this.sourceElderMs.filter(
      (s) => s.fullName.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }

  handleFilterForAttendants(value) {
    this.filteredAttendants = this.sourceAttendants.filter(
      (s) => s.fullName.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }

  onModelChange(schedule: MidWeekScheduleItem,isPartner: boolean = false) {
    if(schedule.isForElderMs){
        var list = this.midWeekSchedule.midWeekScheduleItems.filter(i => i.isForElderMs);
        this.sourceElderMs = this.publishers.filter(i => i.isElder || i.isMs);
        list.map(item => {
          if(item.publisher){
            if(item.role === MidWeekScheduleRoles.Chairman || item.role === MidWeekScheduleRoles.OpeningPrayer || item.role === MidWeekScheduleRoles.ClosingPrayer || item.role === MidWeekScheduleRoles.CBSConductor) { }
            else this.sourceElderMs = this.sourceElderMs.filter(i => i.publisherId !== item.publisher.publisherId);
          } 
        });
        this.handleFilterForElderMs("");
    } else {
      var list = this.midWeekSchedule.midWeekScheduleItems.filter(i => !i.isForElderMs && i.category === MidWeekCategories.APPLYYOURSELFTOTHEFIELDMINISTRY);
      this.sourcePublisher = this.publishers.filter(i => !i.isElder);
      console.log("BEFORE:",this.sourcePublisher);
      list.map(item => {
        if(item.partnerPublisher)
          this.sourcePublisher = this.sourcePublisher.filter(i => i.publisherId !== item.partnerPublisher.publisherId);
        if(item.publisher)
          this.sourcePublisher = this.sourcePublisher.filter(i => i.publisherId !== item.publisher.publisherId); 
      });
      console.log("AFTER:",this.sourcePublisher);
      this.handleFilter("");
    }
  }

  onModelChangeForAttendant(schedule: MidWeekScheduleItem) {
      var list = this.midWeekSchedule.midWeekScheduleItems.filter(i => i.category === MidWeekCategories.ATTENDANTS);
      this.sourceAttendants = this.publishers;
      list.map(item => {
        if(item.partnerPublisher)
          this.sourceAttendants = this.sourceAttendants.filter(i => i.publisherId !== item.partnerPublisher.publisherId);
        if(item.publisher)
          this.sourceAttendants = this.sourceAttendants.filter(i => i.publisherId !== item.publisher.publisherId); 
      });
      this.handleFilterForAttendants("");
  }

  private buildMidWeekSchedule(){
    this.midWeekSchedule = {
      attendance: 0,
      midWeekScheduleId: 0,
      midWeekScheduleItems: [],
      scheduledDate: !this.midWeekSchedule ? new Date() : this.midWeekSchedule.scheduledDate,
      scheduleDT: null
    };

    var midWeekCategoryRoles = MidWeekCategoryRoles;
    midWeekCategoryRoles.map(mwcr => {
      mwcr.roles.map(r => {
        this.midWeekSchedule.midWeekScheduleItems.push({
          category: mwcr.category,  
          hallNumber: "Main",
          midWeekScheduleId: 0,
          midWeekScheduleItemId: 0,
          partnerPublisherId: null,
          publisherId: null,
          role: r.name,
          withPartner: r.withPartner,
          isForElderMs: r.isForElderMs,
          publisher: null,
          partnerPublisher: null,
          partnerName: "",
          publisherName: ""
        });
      })
    });
  }

  async submit(){
    this.commonService.toggleLoadingScreen();
    this.midWeekSchedule.midWeekScheduleItems.map(mwsi => {
      mwsi.publisherId = mwsi.publisher ? mwsi.publisher.publisherId : 0;
      mwsi.publisherName = mwsi.publisher ? mwsi.publisher.fullName : "";

      mwsi.partnerPublisherId = mwsi.partnerPublisher ? mwsi.partnerPublisher.publisherId : 0;
      mwsi.partnerName = mwsi.partnerPublisher ? mwsi.partnerPublisher.fullName : "";
    });
    var result = await this.midWeekScheduleApiService.addedit(this.midWeekSchedule);
    this.commonService.toggleLoadingScreen();
    if(!result.isSuccess) this.message = result.message;
    else this.commonService.dismissDialog();
  }

  loadPublisherRecentParts(publisherId: number){
    this.selectedPublisher = null;
    setTimeout(() => {
      this.selectedPublisher = this.publishers.find(i => i.publisherId === publisherId);
    }, 200);
  }

  loadDataGrid = async (gridFilter: GridFilter) : Promise<GridResultGeneric<RecentPart>> => {
    var result = await this.publisherApiService.ListPublisherRecentParts(gridFilter,this.selectedPublisher.publisherId);
    return result;
  }
}
