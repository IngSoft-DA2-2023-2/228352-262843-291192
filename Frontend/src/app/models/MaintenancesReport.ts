export interface MaintenancesReport{
    datas: Array<MaintenancesReportData>;
}

export interface MaintenancesReportData{
    openRequests: string,
    closeRequests: string, 
    inProgressRequests: number,
    averageClosingTime: number,
    maintainerName: number,
}