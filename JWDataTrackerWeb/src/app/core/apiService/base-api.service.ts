import { HttpClient } from "@angular/common/http";
import { CONTEXT_NAME } from "@angular/compiler/src/render3/view/util";
import { Inject, Injectable } from "@angular/core";
import { AuthService } from "../services/auth.service";


@Injectable({
    providedIn: 'root'
  })
export class BaseApiService {
    baseUrl: string;
    constructor(private http: HttpClient,@Inject("BASE_URL") baseUrl: string,private authService: AuthService) {
      this.baseUrl = baseUrl;
    }

    async get<T>(uri: string, withSecureHeader: boolean = true) : Promise<T> {
        try {
            var headers = { "Content-Type": "application/json", Authorization: "", "Access-Control-Allow-Origin":"*" };
            if(withSecureHeader){
                var token = this.authService.getUserToken();
                headers.Authorization = "bearer " + token.authenticationToken;
            }
            var result = await this.http.get<T>(this.baseUrl + uri, { headers }).toPromise();
            return result;
        } catch (error) {
            console.log(error);
            return null;
        }
    }

    async post<TModel, TResponse>(uri:string, model: TModel, withSecureHeader: boolean = true) : Promise<TResponse> {
        try {
            var headers = { "Content-Type": "application/json", Authorization: "" };
            if(withSecureHeader){
                var token = this.authService.getUserToken();
                headers.Authorization = "bearer " + token.authenticationToken;
            }
            console.log(model);
            var result = await this.http.post<TResponse>(this.baseUrl + uri, JSON.stringify(model), { headers }).toPromise();
            return result;
        } catch (error) {
            console.log(error);
            return null;
        }
    }

    async postUpload<TModel, TResponse>(uri:string, model: TModel, withSecureHeader: boolean = true) : Promise<TResponse> {
        try {
            var headers = { Authorization: "", Claims: "" };
            if(withSecureHeader){
                var token = this.authService.getUserToken();
                headers.Authorization = "bearer " + token.authenticationToken;
            }
            var result = await this.http.post<TResponse>(this.baseUrl + uri, model, { headers }).toPromise();
            return result;
        } catch (error) {
            console.log(error);
            return null;
        }
    }
}