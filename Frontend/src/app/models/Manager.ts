import { UserRole } from "./UserRole";

export class Manager {

  constructor(
    public token: string,
    public id: string,
    public role: UserRole,
    public email: string,
    public name: string,
    public lastname: string,
  ){}
}

export interface Manager {
  token: string;
  id: string;
  role: UserRole;
  email: string;
  name: string;
  lastname: string;
}