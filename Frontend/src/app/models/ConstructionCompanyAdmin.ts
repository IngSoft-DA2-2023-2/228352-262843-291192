export class ConstructionCompanyAdmin {

  constructor(
    public email: string,
    public name: string,
    public lastName: string,
    public password: string,
  ){}
}

export interface ConstructionCompanyAdmin {
  email: string;
  name: string;
  lastName: string;
  password: string;
}