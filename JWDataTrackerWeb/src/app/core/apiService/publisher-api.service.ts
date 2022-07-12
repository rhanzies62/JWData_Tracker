import { Injectable } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { BaseApiService } from "./base-api.service";
import { Authentication } from "../../shared/models/authentication"
import { ApiResponse } from "src/app/shared/models/apiresponse";
import { CongregationUser } from "src/app/shared/models/congregationuser";
import { Publisher } from "src/app/shared/models/publisher";
@Injectable({
    providedIn: 'root'
  })
export class PublisherApiService {

    constructor(private baseApiService : BaseApiService, private authService: AuthService) {
        
    }

    async addedit(model: Publisher): Promise<ApiResponse<CongregationUser>> {
        var result = await this.baseApiService.post<Publisher,ApiResponse<any>>('publisher',model);
        return result;
      }

}