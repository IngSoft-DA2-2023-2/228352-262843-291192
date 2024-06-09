using BuildingManagerDomain.Entities;
using System.Collections.Generic;
using System.Linq;

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ListImportersResponse other = (ListImportersResponse)obj;
            return Importers.SequenceEqual(other.Importers);
        }

    }
}
