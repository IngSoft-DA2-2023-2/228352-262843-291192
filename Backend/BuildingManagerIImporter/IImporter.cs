using System;
using System.Collections.Generic;

namespace BuildingManagerIImporter
{
    public interface IImporter
    {
        List<Building> Import(string data, Guid companyId);
        string Name { get; }
    }
}