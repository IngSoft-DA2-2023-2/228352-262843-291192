import { RequestState } from "./RequestState";

export interface ManagerRequest {
    id: string,
    description: string,
    state: RequestState,
    categoryId: string,
    maintainerStaffId: string,
    buildingId: string,
    managerId: string,
    apartmentFloor: string,
    apartmentNumber: string,
    maintainerStaffName: string,
    attendedAt: number,
    completedAt: number,
    cost: number,
    buildingName: string,
    categoryName: string,
}

