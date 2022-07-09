import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, NavigationEnd } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalStorageKey } from './../../shared/models/constant';
import { AuthService } from './auth.service';
import { CommonService } from './common.service';
import { filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuardService implements CanActivate {
  previousUrl: string;
  currentUrl: string ;
  constructor(public router: Router, private authService: AuthService, private commonService: CommonService) {

   }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const isValid = this.authService.isTokenValid();
    if(!isValid) { 
      this.commonService.clearSession();
      this.commonService.setLocalStorageItems(LocalStorageKey.PREVIOUS_URL,state.url);
      this.router.navigate(['login']); 
      return false;
    }
    // const hasAccess = this.authService.hasAccessOnPage(state.url);
    // if(!hasAccess) this.router.navigate(['']); 
    return true;
  }
  
}