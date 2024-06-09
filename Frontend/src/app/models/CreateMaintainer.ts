export class CreateMaintainer {
    constructor(
        public name: string,
        public lastname: string,
        public email: string,
        public password: string
    ) { }
}

export interface CreateMaintainer {
    name: string,
    lastname: string,
    email: string,
    password: string
}


