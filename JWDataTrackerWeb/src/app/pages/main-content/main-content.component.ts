import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.scss']
})
export class MainContentComponent implements OnInit {
  responsivehelper: boolean = false;
  constructor(public authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if(window.location.href.indexOf('localhost') > -1){
      this.responsivehelper = true;
    }
    if(window.location.pathname === "/"){
      this.router.navigate(['dashboard']);
    }
  }

}
