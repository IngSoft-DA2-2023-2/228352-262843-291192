namespace BuildingManagerDomain.Entities
{
    public struct ApartmentsReportData
    {
        public int ApartmentFloor { get; set; }
        public int ApartmentNumber { get; set; }
        public ApartmentsReportData(int apartmentFloor, int apartmentNumber)
        {
            ApartmentFloor = apartmentFloor;
            ApartmentNumber = apartmentNumber;
        }
    }
}
