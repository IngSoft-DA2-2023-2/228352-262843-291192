using System;
using System.Collections.Generic;

namespace JsonImporter
{
    public class JsonBuilding
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var other = (JsonBuilding)obj;
            return Name == other.Name ;
        }
    }
}
