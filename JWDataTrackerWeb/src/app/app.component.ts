import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'JWDataTrackerWeb';
  responsivehelper: boolean = false;
  /**
   *
   */
  constructor() {
    if(window.location.href.indexOf('localhost') > -1){
      this.responsivehelper = true;
    }
    
  }
}
