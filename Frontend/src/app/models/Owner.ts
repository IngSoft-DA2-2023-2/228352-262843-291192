export class Owner {
  constructor(
    public name: string,
    public lastName: string,
    public email: string
  ){}
}

export interface Owner {
  name: string,
  lastName: string,
  email: string
}