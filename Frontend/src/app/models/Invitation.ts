import { UserRole } from "../enums/UserRole";

export interface InvitationList {
    invitations: Invitation[];
}

export interface Invitation {
    id?: string;
    email: string;
    name: string;
    deadline: number;
    role: UserRole;
  }