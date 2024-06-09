export class CreateRequest {
    constructor(
        description: string,
        categoryId: string,
        buildingId: string,
        apartmentNumber: number,
        apartmentFloor: number,
        managerId: string
    ) { }
}

export interface CreateRequest {
    description: string,
    categoryId: string,
    buildingId: string,
    apartmentNumber: number,
    apartmentFloor: number,
    managerId: string
}


