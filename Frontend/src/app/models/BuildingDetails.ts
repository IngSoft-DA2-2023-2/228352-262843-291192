import { Apartment } from "./Apartment";

export class BuildingDetails{
  constructor(
      public name: string,
      public address: string,
      public location: string,
      public commonExpenses: number,
      public manager: string,
      public constructionCompany: string,
      public apartments: Apartment[]
  ){}
}

export interface BuildingDetails{
  name: string;
  address: string;
  location: string;
  commonExpenses: number;
  manager: string;
  constructionCompany: string;
  apartments: Apartment[];
}