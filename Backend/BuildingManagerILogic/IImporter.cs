using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IImporter
    {
        List<Building> Import(string path, Guid companyId);
        string Name { get; }
    }
}
