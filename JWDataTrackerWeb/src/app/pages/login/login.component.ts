import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AuthenticateApiService } from 'src/app/core/apiService/authenticate-api.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { CommonService } from 'src/app/core/services/common.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  message: string = "";
  constructor(private formBuilder: FormBuilder, private authenticateApiService: AuthenticateApiService,private commonService: CommonService,private authService: AuthService,private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: new FormControl('',[Validators.required]),
      password: new FormControl('',[Validators.required])
    });

    if(this.authService.isTokenValid()){
      this.router.navigate(['']);
    }
  }

  async submit() {
    this.commonService.toggleLoadingScreen();
    var result = await this.authenticateApiService.Login(this.loginForm.value);

    if(result.isSuccess){
      window.location.reload();
    } else {
      this.commonService.toggleLoadingScreen();
      this.message = result.message;
    }
  }
}
