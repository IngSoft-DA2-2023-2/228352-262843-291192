using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class ManagerBuildingData
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ManagerApartmentData> Apartments { get; set; }

        public ManagerBuildingData(Building building)
        {
            Id = building.Id;
            Name = building.Name;
            Apartments = new List<ManagerApartmentData>();
            foreach (var apartment in building.Apartments)
            {
                Apartments.Add(new ManagerApartmentData(apartment));
            }
        }
    }
}
