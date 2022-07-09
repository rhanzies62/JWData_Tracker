import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { CongregationUser } from 'src/app/shared/models/congregationuser';

@Component({
  selector: 'app-base-module',
  templateUrl: './base-module.component.html',
  styleUrls: ['./base-module.component.scss']
})
export class BaseModuleComponent implements OnInit {

  public loggedInDetail: CongregationUser;
  constructor(public authService: AuthService) {}

  ngOnInit(): void {
    this.loggedInDetail = this.authService.getUserDetails();
  }


}
