export interface MaintenancesReport{
    datas: Array<MaintenancesReportData>;
}

export interface MaintenancesReportData{
    openRequests: number,
    closeRequests: number, 
    inProgressRequests: number,
    averageClosingTime: number,
    maintainerName: string,
}