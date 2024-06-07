using System;
using System.Collections.Generic;

namespace BuildingManagerIImporter
{
    public class ImporterBuilding
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Manager { get; set; }
        public string Location { get; set; }
        public long CommonExpenses { get; set; }
        public List<ImporterApartment> Apartments { get; set; }
    }
}
