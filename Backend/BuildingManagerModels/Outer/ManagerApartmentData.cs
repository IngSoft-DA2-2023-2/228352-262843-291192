using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class ManagerApartmentData
    {

        public int? Floor { get; set; }
        public int? Number { get; set; }

        public ManagerApartmentData(Apartment apartment)
        {
            Floor = apartment.Floor;
            Number = apartment.Number;
        }
    }
}
