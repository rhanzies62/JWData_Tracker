import { Injectable } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { BaseApiService } from "./base-api.service";
import { Authentication } from "../../shared/models/authentication"
import { ApiResponse } from "src/app/shared/models/apiresponse";
import { CongregationUser } from "src/app/shared/models/congregationuser";
@Injectable({
    providedIn: 'root'
  })
export class AuthenticateApiService {

    constructor(private baseApiService : BaseApiService, private authService: AuthService) {
        
    }

    async Login(model: Authentication): Promise<ApiResponse<CongregationUser>> {
        var result = await this.baseApiService.post<Authentication,ApiResponse<CongregationUser>>('authenticate',model,false);
        if(result.isSuccess){
            this.authService.saveUserToken(result);
        }
        return result;
      }

}