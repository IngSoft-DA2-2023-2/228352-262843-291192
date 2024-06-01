export class Building{
    constructor(
        public name: string,
        public address: string,
    ){}
}

export interface Building{
    name: string;
    address: string;
}