import { Owner } from "./Owner";

export class Apartment {
  constructor(
    public number: number,
    public floor: number,
    public rooms: number,
    public bathrooms: number,
    public hasTerrace: boolean,
    public owner: Owner
  ){}
}

export interface Apartment {
  number: number;
  floor: number;
  rooms: number;
  bathrooms: number;
  hasTerrace: boolean;
  owner: Owner;
}