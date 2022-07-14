import { Injectable } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { BaseApiService } from "./base-api.service";
import { Authentication } from "../../shared/models/authentication"
import { ApiResponse } from "src/app/shared/models/apiresponse";
import { CongregationUser } from "src/app/shared/models/congregationuser";
import { Publisher } from "src/app/shared/models/publisher";
import { GridFilter, GridResultGeneric } from "src/app/shared/models/GridFilter";
import { PublisherGrid } from "src/app/shared/models/PublisherGrid";
@Injectable({
    providedIn: 'root'
  })
export class PublisherApiService {

    constructor(private baseApiService : BaseApiService, private authService: AuthService) {
        
    }

    async addedit(model: Publisher): Promise<ApiResponse<CongregationUser>> {
        var result = await this.baseApiService.post<Publisher,ApiResponse<any>>('publisher/AddEdit',model);
        return result;
    }

    async ListPublishers(filter: GridFilter): Promise<GridResultGeneric<PublisherGrid>> {
      var result = await this.baseApiService.post<GridFilter,GridResultGeneric<PublisherGrid>>(`publisher/ListPublishers`,filter);
      return result;
    }

    async Get(id: number): Promise<Publisher> {
      var result = await this.baseApiService.get<Publisher>(`publisher/get?publisherId=${id}`);
      return result;
    }

}