export class Columns {
    fieldName: string;
    title: string;
    type: string;
    width: number;
    isVisible: boolean;
    sortColumn: string;
    filterable: boolean;
}
export const PublisherColumns:Columns[] = [
    { fieldName: 'publisherId', title: 'Publisher ID', type: 'text', width: 50,isVisible:false, sortColumn: "", filterable: false },
    { fieldName: 'congregationId', title: 'Congregation Id', type: 'text', width: 50,isVisible:false, sortColumn: "", filterable: false },
    { fieldName: 'groupNumber', title: 'FS Group #', type: 'text', width: 20,isVisible:true, sortColumn: "", filterable: false },
    { fieldName: 'firstName', title: 'First Name', type: 'text', width: 50,isVisible:true, sortColumn: "", filterable: true },
    { fieldName: 'lastName', title: 'Last Name', type: 'text', width: 50,isVisible:true, sortColumn: "", filterable: true },
    { fieldName: 'isElderBool', title: 'Is Elder', type: 'crosscheck', width: 15,isVisible:true, sortColumn: "isElder", filterable: false },
    { fieldName: 'isMsBool', title: 'Is MS', type: 'crosscheck', width: 15,isVisible:true, sortColumn: "isMs", filterable: false },
    { fieldName: 'isRpBool', title: 'Is RP', type: 'crosscheck', width: 15,isVisible:true, sortColumn: "isRp", filterable: false },
    { fieldName: 'isUnBaptizedBool', title: 'Is Unbaptize', type: 'crosscheck', width: 20,isVisible:true, sortColumn: "isUnBaptized", filterable: false },
    { fieldName: 'createdDateDt', title: 'Created Date', type: 'date', width: 25,isVisible:true, sortColumn: "createdDate", filterable: false },
];

export const PublisherRecentPartColumns: Columns[] = [
    { fieldName: 'scheduledDateDT', title: 'Date', type: 'date', width: 40,isVisible:true, sortColumn: "scheduledDate", filterable: false },
    { fieldName: 'category', title: 'Category', type: 'text', width: 70,isVisible:true, sortColumn: "", filterable: false },
    { fieldName: 'part', title: 'Role', type: 'text', width: 50,isVisible:true, sortColumn: "", filterable: false },
]
