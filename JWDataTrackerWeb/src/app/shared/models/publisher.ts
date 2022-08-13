import { LookUp } from "./lookup";
import { MidWeekScheduleItem } from "./midWeekSchedule";

export class Publisher {
    publisherId: number;
    firstName: string;
    lastName: string;
    congregationId:number;
    isElder: boolean;
    isMs: boolean;
    isRp: boolean;
    isUnBaptized: boolean;
    groupNumber:number;
    fullName:string;
    mostRecentPart: RecentPart;
    recentParts: RecentPart[];

    privilege: number;
    status: number;
    gender: number;

    privilegeLookUp: LookUp;
    statusLookUp: LookUp;
    genderLookUp: LookUp;
}

export class RecentPart{
    date: Date;
    category: string;
    role: string;
    isPartner: Boolean;
    scheduledDateDT: Date;
    part: string;
}