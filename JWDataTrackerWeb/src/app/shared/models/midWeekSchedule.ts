import { Publisher } from "./publisher";

export class MidWeekSchedule {
    midWeekScheduleId: number;
    scheduledDate: Date;
    scheduleDT: Date;
    attendance: number;
    midWeekScheduleItems: MidWeekScheduleItem[];
}

export class MidWeekScheduleItem {
    midWeekScheduleItemId: number;
    role: string;
    publisherId: number;
    partnerPublisherId: number;
    hallNumber: string;
    midWeekScheduleId: number;
    category: string;
    withPartner: boolean;
    isForElderMs: boolean;
    publisher: Publisher;
    partnerPublisher: Publisher;
    publisherName: string;
    partnerName: string;
}

export class Role {
    name: string;
    withPartner: boolean;
    isForElderMs: boolean;
}

export class MidWeekCategoryRole {
    category: string;
    roles: Role[];
}

export enum MidWeekCategories {
    TREASUREFROMGODSWORD = "TREASUREFROMGODSWORD",
    APPLYYOURSELFTOTHEFIELDMINISTRY= "APPLYYOURSELFTOTHEFIELDMINISTRY",
    LIVINGASACHRISTIAN = "LIVINGASACHRISTIAN",
    ATTENDANTS = "ATTENDANTS",
    OPENING = "OPENING"
}

export enum MidWeekScheduleRoles {
    Chairman = "Chairman",
    OpeningPrayer = "Opening Prayer",
    ClosingPrayer = "Closing Prayer",
    InstructionalTalk = "Instructional Talk",
    SpiritualGem = "Spiritual Gem",
    BibleReading = "Bible Reading",
    VideoDiscussion = "Video Discussion",
    FirstPart = "First Part",
    SecondPart = "Second Part",
    ThirPart = "Third Part",
    CBSConductor = "CBS Conductor",
    CBSReader = "CBS Reader",
    AudioVideo = "Audio Video",
    Zoom = "Zoom",
    MicRoving = "Mic Roving"
}
    
export const MidWeekCategoryRoles: MidWeekCategoryRole[] = [
    {
        category: MidWeekCategories.OPENING,
        roles: [
            {name: MidWeekScheduleRoles.Chairman, withPartner: false, isForElderMs: true},
            {name: MidWeekScheduleRoles.OpeningPrayer, withPartner: false, isForElderMs: true},
            {name: MidWeekScheduleRoles.ClosingPrayer, withPartner: false, isForElderMs: true}
        ]
    },
    {
        category: MidWeekCategories.TREASUREFROMGODSWORD,
        roles: [
            {name: MidWeekScheduleRoles.InstructionalTalk, withPartner: false, isForElderMs: true},
            {name: MidWeekScheduleRoles.SpiritualGem, withPartner: false, isForElderMs: true},
            {name: MidWeekScheduleRoles.BibleReading, withPartner: false, isForElderMs: false}
        ]
    },
    {
        category: MidWeekCategories.APPLYYOURSELFTOTHEFIELDMINISTRY,
        roles: [
            {name: MidWeekScheduleRoles.VideoDiscussion, withPartner: false, isForElderMs: true},
            {name: MidWeekScheduleRoles.FirstPart, withPartner: true, isForElderMs: false},
            {name: MidWeekScheduleRoles.SecondPart, withPartner: true, isForElderMs: false},
            {name: MidWeekScheduleRoles.ThirPart, withPartner: true, isForElderMs: false}
        ]
    },
    {
        category: MidWeekCategories.LIVINGASACHRISTIAN,
        roles: [
            {name: MidWeekScheduleRoles.FirstPart, withPartner: false, isForElderMs: true},
            {name: MidWeekScheduleRoles.SecondPart, withPartner: false, isForElderMs: true},
            {name: MidWeekScheduleRoles.CBSConductor, withPartner: false, isForElderMs: true},
            {name: MidWeekScheduleRoles.CBSReader, withPartner: false, isForElderMs: false}
        ]
    },
    {
        category: MidWeekCategories.ATTENDANTS,
        roles: [
            {name: MidWeekScheduleRoles.AudioVideo, withPartner: true, isForElderMs: false},
            {name: MidWeekScheduleRoles.Zoom, withPartner: true, isForElderMs: false},
            {name: MidWeekScheduleRoles.MicRoving, withPartner: true, isForElderMs: false}
        ]
    }
];

