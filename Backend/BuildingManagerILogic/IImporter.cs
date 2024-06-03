using BuildingManagerDomain.Entities;
using System.Collections.Generic;

namespace BuildingManagerILogic
{
    public interface IImporter
    {
        List<Building> Import(string path);
        string Name { get; }
    }
}
