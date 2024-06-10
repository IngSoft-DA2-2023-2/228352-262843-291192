import { UserRole } from "../enums/UserRole";

export interface Invitation {
    id?: string;
    email: string;
    name: string;
    deadline: number;
    role: UserRole;
}

export interface InvitationItem {
    id?: string;
    email: string;
    name: string;
    deadline: number;
    role: UserRole;
    editable: boolean;
}