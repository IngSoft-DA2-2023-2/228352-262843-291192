using System.Collections.Generic;

namespace BuildingManagerModels.Outer
{
    public class ListImportersResponse
    {
        public List<string> Importers { get; set; }

        public ListImportersResponse(List<string> importers)
        {
            Importers = new List<string>();
            foreach (var importer in importers)
            {
                Importers.Add(importer);
            }
        }
    }
}
