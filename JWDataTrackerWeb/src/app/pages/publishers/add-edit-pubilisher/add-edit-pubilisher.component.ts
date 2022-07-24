import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { PublisherApiService } from 'src/app/core/apiService/publisher-api.service';
import { CommonService } from 'src/app/core/services/common.service';
import { PublisherRecentPartColumns } from 'src/app/shared/models/GridColumns';
import { GridFilter, GridResultGeneric, PageGrid } from 'src/app/shared/models/GridFilter';
import { Publisher, RecentPart } from 'src/app/shared/models/publisher';

@Component({
  selector: 'app-add-edit-pubilisher',
  templateUrl: './add-edit-pubilisher.component.html',
  styleUrls: ['./add-edit-pubilisher.component.scss']
})
export class AddEditPubilisherComponent implements OnInit {
  public publisherRecentPartPageGrid : PageGrid;
  @Input() id: number = 0;
  publisherForm: FormGroup;
  message: string;
  constructor(private formBuilder: FormBuilder,private commonService: CommonService,private publisherApiService: PublisherApiService) { }

  async ngOnInit(): Promise<void> {
    this.publisherRecentPartPageGrid = new PageGrid(PublisherRecentPartColumns,4,'scheduledDate','desc');
    let publisher: Publisher;
    if(this.id) publisher = await this.publisherApiService.Get(this.id);
    
    this.publisherForm = this.formBuilder.group({
      publisherId:  new FormControl(this.id || 0),
      firstName: new FormControl(this.id ? publisher.firstName : '',[Validators.required]),
      lastName: new FormControl(this.id ? publisher.lastName : '',[Validators.required]),
      groupNumber: new FormControl(this.id ? publisher.groupNumber : '',[Validators.required]),
      congregationId: new FormControl(1),
      isElder: new FormControl(this.id ? publisher.isElder : false),
      isMs: new FormControl(this.id ? publisher.isMs : false),
      isRp: new FormControl(this.id ? publisher.isRp : false),
      isUnBaptized: new FormControl(this.id ? publisher.isUnBaptized :false),
    });
  }

  async submit() {
    if(this.publisherForm.valid){
      this.commonService.toggleLoadingScreen();
      var result = await this.publisherApiService.addedit(this.publisherForm.value);
      this.commonService.toggleLoadingScreen();
      if(result.isSuccess){
        this.commonService.dismissDialog();
      } else {
        this.message = result.message;
      }
    } else {
      this.publisherForm.markAllAsTouched();
    }
  }

  async delete() {
    if(confirm("Are you sure you want to delete")){
      this.commonService.toggleLoadingScreen();
      var result = await this.publisherApiService.Delete(this.id);
      this.commonService.toggleLoadingScreen();
      if(result.isSuccess){
        this.commonService.dismissDialog();
      } else {
        this.message = result.message;
      }
    }
  }

  loadDataGrid = async (gridFilter: GridFilter) : Promise<GridResultGeneric<RecentPart>> => {
    var result = await this.publisherApiService.ListPublisherRecentParts(gridFilter,this.id);
    return result;
  }
}
