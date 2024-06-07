using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;

namespace BuildingManagerIImporters
{
    public interface IImporter
    {
        List<Building> Import(string data, Guid companyId);
        string Name { get; }
    }
}