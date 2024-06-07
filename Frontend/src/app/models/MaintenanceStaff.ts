import { User } from "./User"
import { UserRole } from "./UserRole"

export interface MaintenanceStaff extends User{
    Role: UserRole.MAINTENANCE
}

export interface BasicMaintainerStaff{
    id: string,
    name: string
}