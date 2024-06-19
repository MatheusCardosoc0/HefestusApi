using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories;
using HefestusApi.Repositories.Interfaces;
using HefestusApi.Services.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<ServiceResponse<IEnumerable<CityDto>>> GetAllCitiesAsync(string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<CityDto>>();
            try
            {
                var cities = await _cityRepository.GetAllCitiesAsync(SystemLocationId);
                var cityDtos = cities.Select(c => new CityDto { Id = c.Id, Name = c.Name, IBGENumber = c.IBGENumber, State = c.State });

                response.Data = cityDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as cidades: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<City>> GetCityByIdAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<City>();
            try
            {
                var city = await _cityRepository.GetCityByIdAsync(SystemLocationId, id);
                if (city == null)
                {
                    response.Success = false;
                    response.Message = "Cidade não encontrada.";
                    return response;
                }
                
                response.Data = city;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a cidade: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchCityByNameAsync(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var cities = await _cityRepository.SearchCityByNameAsync(searchTerm.ToLower(), SystemLocationId);

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = cities.Select(c => new CitySimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = cities.Cast<object>().ToList();
                }
                else
                {
                    throw new ArgumentException("Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao processar a busca: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<City>> CreateCityAsync(CityRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<City>();
            try
            {
                var city = new City
                {
                    Name = request.Name,
                    IBGENumber = request.IBGENumber,
                    State = request.State
                };

                await _cityRepository.AddCityAsync(city);

                response.Data = city;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a cidade: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateCityAsync(int id, CityRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var city = await _cityRepository.GetCityByIdAsync(SystemLocationId, id);
                if (city == null)
                {
                    response.Success = false;
                    response.Message = $"Cidade com o ID {id} não foi encontrada.";
                    return response;
                }

                city.Name = request.Name;
                city.IBGENumber = request.IBGENumber;
                city.State = request.State;

                bool updateResult = await _cityRepository.UpdateCityAsync(city);
                if (!updateResult)
                {
                    throw new Exception("A atualização da cidade falhou por uma razão desconhecida.");
                }

                response.Data = true; 
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a cidade: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteCityAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var city = await _cityRepository.GetCityByIdAsync(SystemLocationId, id);

                if (city == null)
                {
                    response.Success = false;
                    response.Message = $"Cidade com o ID {id} não existe.";
                    return response;
                }

                if (city.Persons.Any())
                {
                    response.Success = false;
                    response.Message = $"Cidade não pode ser excluida, pois está relacionado a pessoas.";
                    return response;
                }

                bool deleted = await _cityRepository.DeleteCityAsync(city);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a cidade devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a cidade: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
