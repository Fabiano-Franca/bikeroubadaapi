namespace BikeRoubada.Api.Utilities
{
    public class FileResult
    {
        public bool Succeeded { get; set; }
        public string? Error { get; set; }

    }

    public class FileResult<T> : FileResult where T: class
    {
        public T? Content { get; set; }
        
    }

    public class FileContent
    {
        
        public FileContent(string fileName, string type, byte[] bytes)
        {
            FileName = fileName;
            Type = type;
            Bytes = bytes;
        }

        public string? FileName { get; set; }
        public string? Type { get; set; }
        public byte[]? Bytes { get; set; }
    }
}
