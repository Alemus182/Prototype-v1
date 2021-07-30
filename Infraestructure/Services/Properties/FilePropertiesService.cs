using Application.Interfaces.Infraestructure.Services.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infraestructure.Services.SIIC
{
    public class FilePropertiesService : IFilePropertiesService
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<FilePropertiesService> _logger;

        public FilePropertiesService(IConfiguration configuration, ILogger<FilePropertiesService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Tuple<bool,string>> LoadPhotoOwner(int IdOwner, string data, string filename)
        {
            bool isvalid = true;
            string message = string.Empty;
            byte[] content = Array.Empty<byte>();

            string DirectoryPath = Path.Combine(_configuration["FileSettings:pathOwnerPhotos"],"owners", IdOwner.ToString());

            if (!Directory.Exists(DirectoryPath))
                 Directory.CreateDirectory(DirectoryPath);

            try
            {
                content = Convert.FromBase64String(data);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message);
                isvalid = false;
                message = "Image with format invalid";
            }

            if (isvalid)
            {
                try
                {
                   await File.WriteAllBytesAsync($"{DirectoryPath}/{filename}", content);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    isvalid = false;
                    message = "error create file";
                }
            }

            return new Tuple<bool, string> (isvalid, message);
        }

        public async Task<Tuple<bool, string>> LoadPhotoProperty(int IdPropertyImage, string data, string filename)
        {
            bool isvalid = true;
            string message = string.Empty;
            byte[] content = Array.Empty<byte>();

            string DirectoryPath = Path.Combine(_configuration["FileSettings:pathOwnerPhotos"],"Properties", IdPropertyImage.ToString(),"Images");

            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            try
            {
                content = Convert.FromBase64String(data);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message);
                isvalid = false;
                message = "Image with format invalid";
            }

            if (isvalid)
            {
                try
                {
                    await File.WriteAllBytesAsync($"{DirectoryPath}/{filename}", content);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    isvalid = false;
                    message = "error create file";
                }
            }

            return new Tuple<bool, string>(isvalid, message);
        }
    }
}
