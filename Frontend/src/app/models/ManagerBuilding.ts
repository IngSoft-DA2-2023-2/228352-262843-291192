export interface ManagerBuildings {
    buildings: ManagerBuilding[]
}

export interface ManagerBuilding {
    id: string,
    name: string,
    apartments: ManagerApartment[]
}

export interface ManagerApartment {
    floor: number,
    number: number
}