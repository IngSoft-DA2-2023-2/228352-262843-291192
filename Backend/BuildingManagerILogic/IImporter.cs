using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;

namespace BuildingManagerILogic
{
    public interface IImporter
    {
        List<Building> Import(string path, Guid companyId);
        string Name { get; }
    }
}
