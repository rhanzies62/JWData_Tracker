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
}

export class RecentPart{
    date: Date;
    part: MidWeekScheduleItem
}