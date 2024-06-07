import { UserRole } from "./UserRole";

export class User {

  constructor(
    public token: string,
    public userId: string,
    public role: UserRole,
    public email: string,
    public name: string,
    public lastname: string,
  ){}
}

export interface Usuario {
  token: string;
  userId: string;
  role: UserRole;
  email: string;
  name: string;
  lastname: string;
}