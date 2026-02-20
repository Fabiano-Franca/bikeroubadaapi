using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Notificacoes;
using MimeKit;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BikeRoubada.Api.Utilities
{
    public class FileHandler : IFileHandler
    {
        
        private readonly IConfiguration _configuration;
        public FileHandler(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
    
        public async Task<FileResult> UploadFile(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fileResult = new FileResult<string>();
            try
            {
                string? path = Path.Combine(_configuration["StorageDirectory"], fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                fileResult.Succeeded = true;
                fileResult.Content = fileName;
                return fileResult;
            } catch(Exception ex)
            {
                fileResult.Succeeded = false;
                fileResult.Error = ex.Message;
            }
            
            return fileResult;
        }


        public async Task<FileResult> DownloadFile(string fileName)
        {
            var fileResult = new FileResult<FileContent>();
            string? path = Path.Combine(_configuration["StorageDirectory"], fileName);
            try
            {
                byte[] bytes = await File.ReadAllBytesAsync(path);
                var type = MimeTypes.GetMimeType(path);
                
                fileResult.Content = new FileContent(fileName, type, bytes);
                fileResult.Succeeded = true;
            } catch(Exception ex)
            {
                fileResult.Succeeded = false;
                fileResult.Error = ex.Message;
            }


            return fileResult;
        }
                

        public string GetPathFile(string fileName)
        {
            var fileResult = new FileResult<FileContent>();
            string? path = Path.Combine(_configuration["StorageDirectory"], fileName);

            return path;
        }

        public FileResult DeleteFile(string fileName)
        {
            string? path = Path.Combine(_configuration["StorageDirectory"], fileName);
            var fileResult = new FileResult();
             try
            {
                File.Delete(path);
                fileResult.Succeeded = true;

            } catch (Exception ex)
            {
                fileResult.Succeeded = false;
                fileResult.Error = ex.Message;
            }

            return fileResult;
        }

 

    }
}
