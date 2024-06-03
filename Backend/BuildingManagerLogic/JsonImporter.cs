using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BuildingManagerLogic
{
    public class JsonImporter : IImporter
    {
        public string Name => "DefaultJson";

        public List<Building> Import(string path)
        {
            var jsonContent = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var buildingInfos = JsonSerializer.Deserialize<List<BuildingInfo>>(jsonContent, options);

            // Convierte los datos deserializados en objetos de tipo Building
            var buildings = new List<Building>();
            foreach (var buildingInfo in buildingInfos)
            {
                var building = new Building
                {
                    Id = Guid.NewGuid(),
                    Name = buildingInfo.Nombre,
                    Address = $"{buildingInfo.Direccion.CallePrincipal} {buildingInfo.Direccion.NumeroPuerta}",
                    Location = $"{buildingInfo.Gps.Latitud}, {buildingInfo.Gps.Longitud}",
                    ConstructionCompanyId = Guid.NewGuid(), // Debes proporcionar el ID de la compañía de construcción
                    CommonExpenses = buildingInfo.GastosComunes,
                    Apartments = buildingInfo.Departamentos.Select(dep => new Apartment
                    {
                        Floor = dep.Piso,
                        Number = dep.NumeroPuerta,
                        Rooms = dep.Habitaciones,
                        Bathrooms = dep.Baños,
                        Owner = new Owner { Email = dep.PropietarioEmail },
                        BuildingId = null, // Debes establecer esto si deseas registrar la relación con el edificio
                        HasTerrace = dep.ConTerraza
                    }).ToList()
                };
                buildings.Add(building);
            }

            return buildings;
        }
        private class BuildingInfo
        {
            public string Nombre { get; set; }
            public Direccion Direccion { get; set; }
            public string Encargado { get; set; }
            public Gps Gps { get; set; }
            public decimal GastosComunes { get; set; }
            public List<Departamento> Departamentos { get; set; }
        }

        private class Direccion
        {
            public string CallePrincipal { get; set; }
            public int NumeroPuerta { get; set; }
            public string CalleSecundaria { get; set; }
        }

        private class Gps
        {
            public decimal Latitud { get; set; }
            public decimal Longitud { get; set; }
        }

        private class Departamento
        {
            public int Piso { get; set; }
            public int NumeroPuerta { get; set; }
            public int Habitaciones { get; set; }
            public bool ConTerraza { get; set; }
            public int Baños { get; set; }
            public string PropietarioEmail { get; set; }
        }
    }
}
