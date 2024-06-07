export interface BuildingsReport{
    datas: Array<BuildingsReportData>;
}

export interface BuildingsReportData{
    buildingId: string,
    buildingName: string, 
    openRequests: number,
    closeRequests: number,
    inProgressRequests: number,
}