import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { CommonService } from './core/services/common.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'JWDataTrackerWeb';
  responsivehelper: boolean = false;
  isLoading: boolean = false;
  subscription: Subscription;
  constructor(private commonService: CommonService) {
    this.subscription = this.commonService.isLoadingObs.subscribe(value => {
      this.isLoading = value;
    });

    
    
  }
}
