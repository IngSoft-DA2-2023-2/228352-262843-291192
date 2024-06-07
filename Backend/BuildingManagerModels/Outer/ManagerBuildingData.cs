using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class ManagerBuildingData
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public ManagerBuildingData(Building building)
        {
            Id = building.Id;
            Name = building.Name;
        }
    }
}
