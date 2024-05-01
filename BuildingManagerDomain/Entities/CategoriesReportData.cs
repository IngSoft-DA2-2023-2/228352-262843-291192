namespace BuildingManagerDomain.Entities
{
    public struct CategoriesReportData
    {
        public string CategoryName { get; set; }
        public int OpenRequests { get; set; }
        public int CloseRequests { get; set; }
        public int InProgressRequests { get; set; }

        public CategoriesReportData(string categoryName, int openRequests, int closeRequests, int inProgressRequests)
        {
            CategoryName = categoryName;
            OpenRequests = openRequests;
            CloseRequests = closeRequests;
            InProgressRequests = inProgressRequests;
        }
    }
}
