namespace BikeRoubada.Api.Utilities
{
    public interface IFileHandler
    {
        Task<FileResult> UploadFile(IFormFile file);
        Task<FileResult> DownloadFile(string FileName);
        string GetPathFile(string FileName);
        FileResult DeleteFile(string fileName);
    }
}
