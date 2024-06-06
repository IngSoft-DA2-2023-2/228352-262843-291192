export class Building{
    constructor(
        public name: string,
        public address: string,
        public manager: string
    ){}
}

export interface Building{
    name: string;
    address: string;
    manager: string;
}