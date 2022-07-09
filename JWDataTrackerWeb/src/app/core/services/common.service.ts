import { Component, Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { DialogCloseResult, DialogRef, DialogService, WindowCloseResult, WindowService } from "@progress/kendo-angular-dialog";
import { BehaviorSubject, of } from "rxjs";
import { LocalStorageKey } from "../../shared/models/constant";
import { EncryptionService } from "./encryption.service";

@Injectable({
  providedIn: "root",
})
export class CommonService {
   dialogRef: DialogRef;
   constructor(private encryptionService: EncryptionService,private dialogService: DialogService) {}

  private _isLoadingObs = new BehaviorSubject<boolean>(false);
  isLoadingObs = this._isLoadingObs.asObservable();

  public activeLink:string;
  public fromId:number;

  toggleLoadingScreen(){
    this._isLoadingObs.next(!this._isLoadingObs.value);
  }

  getLocalStorageItemObj<T>(key: string): T {
    var result = this.getLocalStorageItems(key);
    return JSON.parse(result);
  }

  setLocalStorageItems(key: string, value: string): void {
    value = this.encryptionService.encryptData(value);
    localStorage.setItem(key, value);
  }

  getLocalStorageItems(key: string): any {
    if (localStorage.getItem(key) !== null) {
      var value = localStorage.getItem(key);
      value = this.encryptionService.decrypt(value);
      return value;
    }
    return null;
  }

  removeLocalStorageItem(key: string): void {
    if (localStorage.getItem(key) !== null) {
      localStorage.removeItem(key);
    }
  }

  clearSession(): void {
    Object.keys(localStorage).map((key) => {
      localStorage.removeItem(key);
    });
  }

  clearCourseInformation(): void {
    Object.keys(localStorage).map((key) => {
      if(key !== LocalStorageKey.AUTH_TOKEN && key !== LocalStorageKey.CLAIMS && key !== LocalStorageKey.LOGIN_DETAILS){
        localStorage.removeItem(key);
      }
    });
  }

  isCoureDetailsAvailable(): boolean {
    if (localStorage.getItem(LocalStorageKey.STUDENT_COURSE) && localStorage.getItem(LocalStorageKey.COURSE) && localStorage.getItem(LocalStorageKey.MODULES)){
      return true;
    } else {
      return false;
    }
  }

  displayAlert(title: string, component: any,width: number,onWindowClose: any = null) : any{
    if(this.dialogRef) this.dismissDialog();
    this.dialogRef = this.dialogService.open({
      title: title,
      content: component,
      width: width,
    });

    if(onWindowClose){
      this.dialogRef.result.subscribe((result)=>{
        if(result instanceof DialogCloseResult){
          onWindowClose();
        }
      })
    }
    return this.dialogRef.content.instance;
  }

  dismissDialog() {
    if(this.dialogRef){
      this.dialogRef.close();
    }
  }

  displayConfirmation(title: string, content: string, onConfirm: any, onDismiss: any,isYesNo:boolean = true) {
    this.dialogRef = this.dialogService.open({
      title: title,
      content: content,
      actions: isYesNo ? [{ text: "No" }, { text: "Yes", primary: true }] : [{ text: "Confirm", primary: true }],
      width: 450,
      height: 200,
      minWidth: 250,
    });

    this.dialogRef.result.subscribe((result: any) => {
      if (result instanceof DialogCloseResult) {
        if(onDismiss) onDismiss();
      } else {
        if(result.primary){
          if(onConfirm) onConfirm();
        } else {
          if(onDismiss) onDismiss();
          this.dismissDialog();
        }
      }
    });
  }

  imgToFile(dataurl, filename){
    var arr = dataurl.split(','),
    mime = arr[0].match(/:(.*?);/)[1],
    bstr = atob(arr[1]), 
    n = bstr.length, 
    u8arr = new Uint8Array(n);
    while(n--){
        u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, {type:mime});
  }

  addHours(numOfHours, date = new Date()) {
    date.setTime(date.getTime() + numOfHours * 60 * 60 * 1000);
  
    return date;
  }
}