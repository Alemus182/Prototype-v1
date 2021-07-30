using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Infraestructure.Services.Properties
{
    public interface IFilePropertiesService
    {
        Task<Tuple<bool,string>> LoadPhotoOwner(int IdOwner, string data, string fileName);

        Task<Tuple<bool, string>> LoadPhotoProperty(int IdPropertyImage, string data, string fileName);
    }
}
