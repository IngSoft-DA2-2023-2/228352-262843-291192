namespace BuildingManagerIImporter.Exceptions
{
    public class NotCorrectImporterException : Exception
    {
        public NotCorrectImporterException(Exception e, string message) : base(message)
        {

        }
    }
}
