export class Building{
    constructor(
        public id: string,
        public name: string,
        public address: string,
        public manager: string
    ){}
}

export interface Building{
    id: string;
    name: string;
    address: string;
    manager: string;
}