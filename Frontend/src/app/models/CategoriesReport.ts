export interface CategoriesReport {
    data: CategoriesReportData[];
}

export interface CategoriesReportData {
    openRequests: number,
    closeRequests: number,
    inProgressRequests: number,
    categoryName: string,
}