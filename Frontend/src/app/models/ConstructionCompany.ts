export class ConstructionCompany{
  constructor(
      public id: string,
      public name: string
  ){}
}

export interface ConstructionCompany{
  id: string;
  name: string;
}