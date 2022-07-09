import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { CommonService } from 'src/app/core/services/common.service';
import { BaseModuleComponent } from 'src/app/pages/base-module/base-module.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent extends BaseModuleComponent implements OnInit {
  fullname: string;
  constructor(private commonService: CommonService, public authService: AuthService) {
    super(authService);
   }

  ngOnInit(): void {
    super.ngOnInit();
    this.fullname = `${this.loggedInDetail.firstName} ${this.loggedInDetail.lastName}`;
  }

  logout(){
    this.commonService.clearSession();
    window.location.reload();
  }

}
