import { Apartment } from "./Apartment";

export class BuildingDetails{
  constructor(
      public id: string,
      public name: string,
      public address: string,
      public location: string,
      public commonExpenses: number,
      public managerId: string,
      public manager: string,
      public constructionCompanyId: string,
      public constructionCompany: string,
      public apartments: Apartment[]
  ){}
}

export interface BuildingDetails{
  id: string;
  name: string;
  address: string;
  location: string;
  commonExpenses: number;
  managerId: string;
  manager: string;
  constructionCompanyId: string;
  constructionCompany: string;
  apartments: Apartment[];
}