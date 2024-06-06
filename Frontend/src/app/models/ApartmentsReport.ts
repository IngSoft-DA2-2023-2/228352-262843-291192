export interface ApartmentsReport{
    data: Array<ApartmentsReportData>;
}

export interface ApartmentsReportData{
    apartmentFloor: number,
    apartmentNumber: number,
    ownerName: string,
    openRequests: number,
    closeRequests: number, 
    inProgressRequests: number,
}