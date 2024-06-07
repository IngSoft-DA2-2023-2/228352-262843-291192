import { Category } from "./Category";
import { MaintenanceStaff } from "./MaintenanceStaff";
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
    maintenanceStaff: MaintenanceStaff,
    category: Category,
    attendedAt: number,
    completedAt: number,
    cost: number 
}

