import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { ApiResponse } from "src/app/shared/models/apiresponse";
import { CongregationUser } from "src/app/shared/models/congregationuser";
import { LocalStorageKey } from "../../shared/models/constant";
import { CommonService } from "./common.service";
import { routeConfig } from "./routeConfig";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  routeConfig: any = routeConfig;
  constructor(
    private commonService: CommonService,
    private jwtHelper: JwtHelperService
  ) {}

  isTokenValid(): boolean {
    const token = this.getUserToken();
    if (!token) return false;
    const isTokenExpired = this.jwtHelper.isTokenExpired(
      token.authenticationToken
    );
    return !isTokenExpired;
  }

  logout(): void {
    this.commonService.clearSession();
  }

  saveUserToken(userToken: ApiResponse<CongregationUser>): void {
    this.commonService.setLocalStorageItems(
      LocalStorageKey.AUTH_TOKEN,
      userToken.message
    );
    this.commonService.setLocalStorageItems(
      LocalStorageKey.LOGIN_DETAILS,
      JSON.stringify(userToken.data)
    );
  }

  updateUserProfile(user: any) : void {
    this.commonService.setLocalStorageItems(
      LocalStorageKey.LOGIN_DETAILS,
      JSON.stringify(user)
    );
  }

  getUserToken() {
    var token = {authenticationToken:"",claims:""};
    token.authenticationToken = this.commonService.getLocalStorageItems(
      LocalStorageKey.AUTH_TOKEN
    );
    token.claims = this.commonService.getLocalStorageItems(
      LocalStorageKey.CLAIMS
    );
    return token;
  }

  getUserDetails() : CongregationUser {
    var userJson = this.commonService.getLocalStorageItems(
      LocalStorageKey.LOGIN_DETAILS
    );
    return userJson ? JSON.parse(userJson) : { userTypeId: 0 };
  }

  hasAccessOnPage(url: string): boolean {
    const userDetails = this.getUserDetails();
    const routes = this.routeConfig[userDetails.roleId.toString()];
    let hasAccess = false;
    for (var key in routes) {
      if(url.startsWith(key)) hasAccess = true;
    }
    return hasAccess;
  }
}