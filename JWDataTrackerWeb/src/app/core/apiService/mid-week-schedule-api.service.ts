import { Injectable } from "@angular/core";
import { ApiResponse } from "src/app/shared/models/apiresponse";
import { MidWeekSchedule } from "src/app/shared/models/midWeekSchedule";
import { AuthService } from "../services/auth.service";
import { BaseApiService } from "./base-api.service";

@Injectable({
    providedIn: 'root'
  })
export class MidWeekScheduleApiservice {

    constructor(private baseApiService : BaseApiService, private authService: AuthService) {
        
    }

    async addedit(model: MidWeekSchedule): Promise<ApiResponse<boolean>> {
        var result = await this.baseApiService.post<MidWeekSchedule,ApiResponse<boolean>>('MidWeekSchedule/AddEdit',model);
        return result;
    }

    async GetMidWeekScheduleByDate(date: Date): Promise<MidWeekSchedule> {
        var result = await this.baseApiService.get<MidWeekSchedule>(`MidWeekSchedule/GetMidWeekScheduleByDate?date=${date.toJSON()}`);
        return result;
    }

    async List(): Promise<MidWeekSchedule[]> {
        var result = await this.baseApiService.get<MidWeekSchedule[]>(`MidWeekSchedule/List`);
        return result;
    }
}