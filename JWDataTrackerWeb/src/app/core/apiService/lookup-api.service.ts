import { Injectable } from "@angular/core";
import { LookUp } from "src/app/shared/models/lookup";
import { BaseApiService } from "./base-api.service";

@Injectable({
    providedIn: 'root'
  })
  export class LookUpApiService {
    constructor(private baseApiService : BaseApiService) {
        
    }

    async loadGenderLookUp() : Promise<LookUp[]> {
        var result = await this.baseApiService.get<LookUp[]>(`LookUp/ListLookUpByCode?code=Gender`);
        return result;
    }

    async loadStatusLookUp() : Promise<LookUp[]> {
        var result = await this.baseApiService.get<LookUp[]>(`LookUp/ListLookUpByCode?code=Status`);
        return result;
    }

    async loadMidWeekCategoryLookUp() : Promise<LookUp[]> {
        var result = await this.baseApiService.get<LookUp[]>(`LookUp/ListLookUpByCode?code=MidWeekCategory`);
        return result;
    }

    async loadMidWeekRoleLookUp() : Promise<LookUp[]> {
        var result = await this.baseApiService.get<LookUp[]>(`LookUp/ListLookUpByCode?code=MidWeekRole`);
        return result;
    }

    async loadPrivilegeLookUp() : Promise<LookUp[]> {
        var result = await this.baseApiService.get<LookUp[]>(`LookUp/ListLookUpByCode?code=Privilege`);
        return result;
    }
  }