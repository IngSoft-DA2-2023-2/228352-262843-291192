using BuildingManagerDomain.Entities;
using BuildingManagerModels.Outer;
using System.Collections.Generic;

namespace BuildingManagerILogic
{
    public interface IImporterLogic
    {
        List<Building> ImportData(string importerName, string path);
        ListImportersResponse ListImporters();
    }
}