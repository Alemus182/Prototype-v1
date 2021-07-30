using Application.Dtos.Properties;
using Application.Services.Properties.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Infraestructure.Data
{
    public interface IPropertiesRepository
    {
        Task<int> CreateProperty(CreatePropertyRequest reqest);
        Task<Tuple<string, string>> CreatePropertyWithOwner (CreatePropertyWithOwnerRequest reqest);
        Task<int> DeleteProperty(int IdProperty, bool isWithOner);
        Task<int> AddImageProperty(AddImagePropertyRequest reqest);
        Task<int> DeleteImageProperty(int IdImageProperty);
        Task<int> ChangePriceProperty(ChangePricePropertyRequest request);
        Task<IEnumerable<FindPropertiesResponse>> FindPropertiesByFilters(FindPropertyByFiltersRequest request);
    }
}
