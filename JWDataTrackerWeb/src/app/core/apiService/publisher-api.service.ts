import { Inject, Injectable } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { BaseApiService } from "./base-api.service";
import { Authentication } from "../../shared/models/authentication"
import { ApiResponse } from "src/app/shared/models/apiresponse";
import { CongregationUser } from "src/app/shared/models/congregationuser";
import { Publisher } from "src/app/shared/models/publisher";
import { GridFilter, GridResultGeneric } from "src/app/shared/models/GridFilter";
import { PublisherGrid } from "src/app/shared/models/PublisherGrid";
import { HttpClient } from "@angular/common/http";
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

    async Delete(id: number): Promise<ApiResponse<boolean>> {
      var result = await this.baseApiService.get<ApiResponse<boolean>>(`publisher/delete?publisherId=${id}`);
      return result;
    }
    
    async List(): Promise<Publisher[]> {
      var result = await this.baseApiService.get<Publisher[]>(`publisher/list`);
      return result;
    }

    async ListPublisherRecentParts(filter: GridFilter,publisherId): Promise<GridResultGeneric<PublisherGrid>> {
      var result = await this.baseApiService.post<GridFilter,GridResultGeneric<PublisherGrid>>(`publisher/ListPublisherRecentParts?publisherId=${publisherId}`,filter);
      return result;
    }

    async ListPublisherGrid(request: string): Promise<any> {
      var result = await this.baseApiService.get<any>(`publisher/ListPublisherGrid?${request}`,true,'application/x-www-form-urlencoded');
      return result;
    }

}