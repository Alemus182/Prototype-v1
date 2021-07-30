using Application.Common;
using Application.Dtos.Properties;
using Application.Interfaces.Infraestructure.Data;
using Application.Interfaces.Infraestructure.Data.Base;
using Application.Services.Properties.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class PropertiesRepository : IPropertiesRepository
    {
        private readonly ISprocRepository _SprocRepository;

        public PropertiesRepository(ISprocRepository SprocRepository)
        {
            _SprocRepository = SprocRepository;
        }

        public async Task<int> AddImageProperty(AddImagePropertyRequest request)
        {
            List<ParameterStored> parametros = new List<ParameterStored>();

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdProperty",
                ParameterValue = request.IdProperty,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Int32
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@File",
                ParameterValue = request.File,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.String
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdPropertyImage",
                ParameterValue = 0,
                Direction = System.Data.ParameterDirection.InputOutput,
                Type = System.Data.DbType.Int32
            });

            return
                await _SprocRepository.
                ExecuteSpSingleWithOutPutAsync("[dbo].[AddImageProperty]", parametros, "IdPropertyImage", System.Threading.CancellationToken.None);
        }

        public async Task<int> DeleteImageProperty(int IdImageProperty)
        {
            List<ParameterStored> parametros = new List<ParameterStored>();

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdPropertyImage",
                ParameterValue = IdImageProperty,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Int32
            });

            return
                await _SprocRepository.
                ExecuteSpSingleAsync("[dbo].[DeleteImageProperty]", parametros, System.Threading.CancellationToken.None);
        }

        public async Task<int> CreateProperty(CreatePropertyRequest request)
        {
            List<ParameterStored> parametros = new List<ParameterStored>();

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@Name",
                ParameterValue = request.Name,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.String
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@Address",
                ParameterValue = request.Address,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.String
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdOwner",
                ParameterValue = request.IdOwner,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Int32
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@Price",
                ParameterValue = request.Price,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Decimal
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@Year",
                ParameterValue = request.Year,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Int16
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdProperty",
                ParameterValue = 0,
                Direction = System.Data.ParameterDirection.InputOutput,
                Type = System.Data.DbType.Int32
            });

            return
                await _SprocRepository.
                ExecuteSpSingleWithOutPutAsync("[dbo].[CreateProperty]", parametros,"IdProperty", System.Threading.CancellationToken.None);
        }

        public async Task<Tuple<string,string>> CreatePropertyWithOwner(CreatePropertyWithOwnerRequest request)
        {
            List<ParameterStored> parametros = new List<ParameterStored>();

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@Name",
                ParameterValue = request.Name,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.String
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@Address",
                ParameterValue = request.Address,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.String
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@Price",
                ParameterValue = request.Price,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Decimal
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@Year",
                ParameterValue = request.Year,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Int16
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@AddressOwner",
                ParameterValue = request.AddressOwner,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.String
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@BirthdayOwner",
                ParameterValue = request.BirthdayOwner,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Date
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@NameOwner",
                ParameterValue = request.NameOwner,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.String
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@PhotoOwner",
                ParameterValue = request.PhotoOwner,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.String
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdProperty",
                ParameterValue = 0,
                Direction = System.Data.ParameterDirection.InputOutput,
                Type = System.Data.DbType.Int32
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdOwner",
                ParameterValue = 0,
                Direction = System.Data.ParameterDirection.InputOutput,
                Type = System.Data.DbType.Int32
            });

            List<Tuple<string, string>> outParameters = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Int","IdProperty"),
                new Tuple<string, string>("Int","IdOwner"),

            };  

            var result =
                await _SprocRepository.
                ExecuteSpSingleWithOutPutAsync
                ("[dbo].[CreatePropertyWithOwner]", parametros, outParameters, System.Threading.CancellationToken.None);

            return new Tuple<string, string>(result[0], result[1]);
        }

        public async Task<int> DeleteProperty(int IdProperty, bool IsWithOwner)
        {
            List<ParameterStored> parametros = new List<ParameterStored>();

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdProperty",
                ParameterValue = IdProperty,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Int32
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IsWithOwner",
                ParameterValue = IsWithOwner,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Boolean
            });

            return
             await _SprocRepository.
             ExecuteSpSingleAsync("[dbo].[DeleteProperty]", parametros, System.Threading.CancellationToken.None);
        }

        public async Task<int> ChangePriceProperty(ChangePricePropertyRequest request)
        {
            List<ParameterStored> parametros = new List<ParameterStored>();

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@IdProperty",
                ParameterValue = request.IdProperty,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Int32
            });

            parametros.Add(new ParameterStored()
            {
                ParameterName = "@NewPrice",
                ParameterValue = request.NewPrice,
                Direction = System.Data.ParameterDirection.Input,
                Type = System.Data.DbType.Decimal
            });

            return
             await _SprocRepository.
             ExecuteSpSingleAsync("[dbo].[ChangePriceProperty]", parametros, System.Threading.CancellationToken.None);
        }

        public async Task<IEnumerable<FindPropertiesResponse>> FindPropertiesByFilters(FindPropertyByFiltersRequest request)
        {
            List<ParameterStored> parametros = new List<ParameterStored>();

            if (!string.IsNullOrEmpty(request.Address))
            {
                parametros.Add(new ParameterStored()
                {
                    ParameterName = "@Address",
                    ParameterValue = request.Address,
                    Direction = System.Data.ParameterDirection.Input,
                    Type = System.Data.DbType.String
                });
            }

            if (! string.IsNullOrEmpty(request.Name))
            {
                parametros.Add(new ParameterStored()
                {
                    ParameterName = "@Name",
                    ParameterValue = request.Name,
                    Direction = System.Data.ParameterDirection.Input,
                    Type = System.Data.DbType.String
                });
            }

            if (request.PriceHigherOrEqual.HasValue)
            {
                parametros.Add(new ParameterStored()
                {
                    ParameterName = "@PriceHigher",
                    ParameterValue = request.PriceHigherOrEqual,
                    Direction = System.Data.ParameterDirection.Input,
                    Type = System.Data.DbType.Decimal
                });
            }

            if (request.PriceLessOrEqual.HasValue)
            {
                parametros.Add(new ParameterStored()
                {
                    ParameterName = "@PriceLess",
                    ParameterValue = request.PriceLessOrEqual,
                    Direction = System.Data.ParameterDirection.Input,
                    Type = System.Data.DbType.Decimal
                });
            }

            if (request.YearHigherOrEqual.HasValue)
            {
                parametros.Add(new ParameterStored()
                {
                    ParameterName = "@YearHigher",
                    ParameterValue = request.YearHigherOrEqual,
                    Direction = System.Data.ParameterDirection.Input,
                    Type = System.Data.DbType.Int16
                });
            }

            if (request.YearLessOrEqual.HasValue)
            {
                parametros.Add(new ParameterStored()
                {
                    ParameterName = "@YearLess",
                    ParameterValue = request.YearLessOrEqual,
                    Direction = System.Data.ParameterDirection.Input,
                    Type = System.Data.DbType.Int16
                });
            }


            if (!string.IsNullOrEmpty(request.OwnerName))
            {
                parametros.Add(new ParameterStored()
                {
                    ParameterName = "@OwnerName",
                    ParameterValue = request.OwnerName,
                    Direction = System.Data.ParameterDirection.Input,
                    Type = System.Data.DbType.String
                });
            }

            return
             await _SprocRepository.
             ExecuteSpAsync<FindPropertiesResponse>("[dbo].[FindPropertiesByFilters]", parametros, System.Threading.CancellationToken.None);
        }
    }
}
