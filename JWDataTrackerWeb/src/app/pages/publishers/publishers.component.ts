import { Component, OnInit, ViewChild } from '@angular/core';
import { PageChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor } from '@progress/kendo-data-query';
import { PublisherApiService } from 'src/app/core/apiService/publisher-api.service';
import { CommonService } from 'src/app/core/services/common.service';
import { GridComponent } from 'src/app/shared/components/grid/grid/grid.component';
import { PublisherColumns } from 'src/app/shared/models/GridColumns';
import { GridFilter, GridResultGeneric, PageGrid } from 'src/app/shared/models/GridFilter';
import { PublisherGrid } from 'src/app/shared/models/PublisherGrid';
import { AddEditPubilisherComponent } from './add-edit-pubilisher/add-edit-pubilisher.component';

@Component({
  selector: 'app-publishers',
  templateUrl: './publishers.component.html',
  styleUrls: ['./publishers.component.scss']
})
export class PublishersComponent implements OnInit {
  public publisherPageGrid : PageGrid;
  @ViewChild(GridComponent) publisherGrid: GridComponent;

  constructor(private commonService: CommonService,private publisherApiService: PublisherApiService) {
      this.publisherPageGrid = new PageGrid(PublisherColumns,10,'publisherId','desc');
   }

  async ngOnInit(): Promise<void> {
  }

  openPublisherModal = (dataItem: any = null) => {
    var component = this.commonService.displayAlert("Add/Edit Publisher",AddEditPubilisherComponent,this.commonService.getModalWidth(),()=> {
      this.publisherGrid.loadDataGrid();
    });

    if(dataItem){
      component.id = dataItem.publisherId;
    }
  }


  loadDataGrid = async (request: string) : Promise<GridResultGeneric<PublisherGrid>> => {
    var result = await this.publisherApiService.ListPublisherGrid(request);
    return result;
  }

}
