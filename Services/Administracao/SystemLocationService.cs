using AutoMapper;
using BCrypt.Net;
using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Administracao.Interfaces;
using HefestusApi.Services.Administracao.Interfaces;
using HefestusApi.Utilities.functions;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Services.Administracao
{
    public class SystemLocationService: ISystemLocationService

    {
        private readonly ISystemLocationRepository _systemLocationRepository;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public SystemLocationService(ISystemLocationRepository systemLocationRepository, IMapper mapper, TokenService tokenService)
        {
            _systemLocationRepository = systemLocationRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ServiceResponse<Dictionary<string, string>>> AuthSystemLocationsAsync(RequiredCampsForAuthentication requiredCampsForAuthentication)
        {
            var response = new ServiceResponse<Dictionary<string, string>>();
            try
            {
                if(requiredCampsForAuthentication == null)
                {
                    response.Success = false;
                    response.Message = "Informe os campos para se autenticar";
                    return response;
                }

                var systemLocation = await _systemLocationRepository.SearchSystemLocationByNameCompareAsync(requiredCampsForAuthentication.Name);

                if(systemLocation == null)
                {
                    response.Success = false;
                    response.Message = "Não foi encontrado uma empresa com esse nome";
                    return response;
                }

                var token = _tokenService.GenerateTokenSystemLocation(systemLocation);

                response.Success = true;
                response.Data = new Dictionary<string, string> { { "token", token } };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar authenticar: " + ex;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<Dictionary<string, string>>> ValidateTokenAsync(string token)
        {
            var response = new ServiceResponse<Dictionary<string, string>>();
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    response.Success = false;
                    response.Message = "";
                    return response;
                }

                var systemLocationName = _tokenService.ValidateTokenSystemLocation(token);

                if (systemLocationName == null)
                {
                    response.Success = false;
                    response.Message = "";
                    return response;
                }

                response.Success = true;
                response.Data = systemLocationName;


            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar authenticar: " + ex;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<SystemLocationDto>>> GetAllSystemLocationsAsync()
        {
            var response = new ServiceResponse<IEnumerable<SystemLocationDto>>();
            try
            {
                var systemLocations = await _systemLocationRepository.GetAllSystemLocationsAsync();

                var systemLocationDtos = _mapper.Map<IEnumerable<SystemLocationDto>>(systemLocations);

                response.Data = systemLocationDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as locals: " + ex;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetSystemLocationByIdAsync(string detailLevel, string systemLocationId)
        {
            var response = new ServiceResponse<object>();
            try
            {
                var systemLocation = await _systemLocationRepository.GetSystemLocationByIdAsync(systemLocationId);
                if (systemLocation == null)
                {
                    response.Success = false;
                    response.Message = "Localização do sistema não encontrada.";
                    return response;
                }

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = _mapper.Map<SystemLocationSimpleSearchDataDto>(systemLocation);
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = systemLocation;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a localização do sistema: " + ex.Message;
                return response;
            }

            response.Success = true;
            return response;
        }



        public async Task<ServiceResponse<IEnumerable<object>>> SearchSystemLocationByNameAsync(string searchTerm, string detailLevel)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var systemLocations = await _systemLocationRepository.SearchSystemLocationByNameAsync(searchTerm.ToLower());

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = _mapper.Map<IEnumerable<SystemLocationSimpleSearchDataDto>>(systemLocations).Cast<object>().ToList();
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = _mapper.Map<IEnumerable<SystemLocationDto>>(systemLocations).Cast<object>().ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao processar a busca: " + ex;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<SystemLocation>> CreateSystemLocationAsync(SystemLocationRequestDataDto request)
        {
            var response = new ServiceResponse<SystemLocation>();
            try
            {
                var exisitingSystemLocation = await _systemLocationRepository.GetSystemLocationByNameAsync(request.Name);

                if (exisitingSystemLocation != null)
                {
                    response.Success = false;
                    response.Message = $"Já existe o local {request.Name} com esse nome!";
                    return response;
                }

                var hashedPasswordSystemLocation = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var systemLocation = new SystemLocation
                {
                    Name = request.Name,
                    Description = request.Description,
                    Password = request.Password
                };

                var personGroup = new PersonGroup
                {
                    Name = "funcionarios",
                    SystemLocationId = systemLocation.Id
                };

                var city = new City
                {
                    Name = "Aparecida de Goiânia",
                    IBGENumber = 999999,
                    State = "GO",
                    SystemLocationId = systemLocation.Id
                };


                var personAdmin = new Person
                {
                    Name = "ADMIN",
                    Razao = "ADMIN",
                    PersonType = "juridica",
                    ICMSContributor = false,
                    Gender = "",
                    Email = "test@gmail.com",
                    Description = "",
                    Habilities = "",
                    CPF = "99999999",
                    IBGE = "",
                    InscricaoEstadual = "",
                    IsBlocked = false,
                    CEP = "74932630",
                    Address = "",
                    Age = 0,
                    CityId = city.Id,
                    City = city,
                    BirthDate = new DateTime(),
                    SystemLocationId = systemLocation.Id,
                    Phone = "",
                    MaritalStatus = "",
                    PersonGroup = new List<PersonGroup> { personGroup },
                    UrlImage = ""
                };

                var password = "123";

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new User
                {
                    Name = "ADMIN",
                    Password = hashedPassword,
                    PersonId = personAdmin.Id,
                    Person = personAdmin,
                    SystemLocationId = systemLocation.Id
                };

                await _systemLocationRepository.AddSystemLocationAsync(systemLocation, personAdmin, user, personGroup, city);

                response.Data = systemLocation;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a local: {ex}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateSystemLocationAsync(SystemLocationRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var systemLocationToUpdate = await _systemLocationRepository.GetSystemLocationByIdAsync(SystemLocationId);

                if (systemLocationToUpdate == null)
                {
                    response.Success = false;
                    response.Message = $"Empresa com o ID {SystemLocationId} não encontrado.";
                    return response;
                }

                var hashedPasswordSystemLocation = BCrypt.Net.BCrypt.HashPassword(request.Password);

                systemLocationToUpdate.Description = request.Description;
                systemLocationToUpdate.Password = request.Password;

                bool updateResult = await _systemLocationRepository.UpdateSystemLocationAsync(systemLocationToUpdate);
                if (!updateResult)
                {
                    throw new Exception("A atualização do local falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a local: {ex}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteSystemLocationAsync(string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var systemLocation = await _systemLocationRepository.GetSystemLocationByIdAsync(SystemLocationId);

                if (systemLocation == null)
                {
                    response.Success = false;
                    response.Message = $"Empresa com o ID {SystemLocationId} não existe.";
                    return response;
                }

                bool deleted = await _systemLocationRepository.DeleteSystemLocationAsync(systemLocation);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a local devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a local: " + ex;
            }

            return response;
        }
    }
}
