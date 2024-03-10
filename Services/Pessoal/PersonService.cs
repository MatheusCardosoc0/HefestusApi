using AutoMapper;
using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Pessoal.Interfaces;
using HefestusApi.Services.functions;
using HefestusApi.Services.Pessoal.Interfaces;

namespace HefestusApi.Services.Pessoal
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService (IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        async public Task<ServiceResponse<Person>> CreatePersonAsync(PersonRequestDataDto request)
        {
            var response = new ServiceResponse<Person>();
            try
            {
                var person = new Person
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Age = request.Age,
                    CPF = request.CPF,
                    Address = request.Address,
                    BirthDate = request.BirthDate,
                    IBGE = request.IBGE,
                    Razao = request.Razao,
                    InscricaoEstadual = request.InscricaoEstadual,
                    CEP = request.CEP,
                    UrlImage = request.UrlImage,
                    IsBlocked = false,
                    MaritalStatus = request.MaritalStatus,
                    Habilities = request.Habilities,
                    Description = request.Description,
                    PersonGroup = new List<PersonGroup>(),
                    Gender = request.Gender,
                    ICMSContributor = request.ICMSContributor,
                    PersonType = request.PersonType,
                    CityId = request.City.Id,
                };

                foreach (var group in request.PersonGroup)
                {
                    var existingGroup = await _personRepository.FindPersonGroupByIdAsync(group.Id);

                    if (existingGroup != null)
                    {
                        person.PersonGroup.Add(existingGroup);
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"Grupo de pessoa com o id {group.Id} não existe";
                        return response;
                    }
                }

                await _personRepository.AddPersonAsync(person);

                response.Data = person;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a pessoa: {ex.Message}";
                return response;
            }

            return response;
        }

        async public Task<ServiceResponse<bool>> DeletePersonAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var person = await _personRepository.GetPersonByIdAsync(id);

                if (person == null)
                {
                    response.Success = false;
                    response.Message = $"Pessoa com o ID {id} não existe.";
                    return response;
                }

                bool deleted = await _personRepository.DeletePersonAsync(person);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a pessoa devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a pessoa: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<PersonDto>>> GetAllPersonsAsync()
        {

            var response = new ServiceResponse<IEnumerable<PersonDto>>();
            try
            {
                var persons = await _personRepository.GetAllPersonsAsync();

                var personDtos = _mapper.Map<IEnumerable<PersonDto>>(persons);

                response.Data = personDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as pessoas: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<Person>> GetPersonByIdAsync(int id)
        {
            var response = new ServiceResponse<Person>();
            try
            {
                var person = await _personRepository.GetPersonByIdAsync(id);
                if (person == null)
                {
                    response.Success = false;
                    response.Message = "Pessoa não encontrada.";
                    return response;
                }

                response.Data = person;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a pessoa: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<object>>> SearchPersonByNameAsync(string searchTerm, string detailLevel)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var cities = await _personRepository.SearchPersonByNameAsync(searchTerm.ToLower());

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = cities.Select(c => new PersonSimpleSearchDataDto
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

        async public Task<ServiceResponse<bool>> UpdatePersonAsync(int id, PersonRequestDataDto request)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var person = await _personRepository.GetPersonByIdAsync(id);
                if (person == null)
                {
                    response.Success = false;
                    response.Message = $"Pessoa com o ID {id} não foi encontrada.";
                    return response;
                }

                person.Name = request.Name;
                person.Email = request.Email;
                person.Phone = request.Phone;
                person.Age = request.Age;
                person.CPF = request.CPF;
                person.Address = request.Address;
                person.BirthDate = request.BirthDate;
                person.IBGE = request.IBGE;
                person.Razao = request.Razao;
                person.InscricaoEstadual = request.InscricaoEstadual;
                person.CEP = request.CEP;
                person.UrlImage = request.UrlImage;
                person.IsBlocked = request.IsBlocked;
                person.MaritalStatus = request.MaritalStatus;
                person.Habilities = request.Habilities;
                person.Description = request.Description;
                person.Gender = request.Gender;
                person.ICMSContributor = request.ICMSContributor;
                person.PersonType = request.PersonType;
                person.CityId = request.City.Id;

                person.PersonGroup.Clear();

                foreach (var group in request.PersonGroup)
                {
                    var existingGroup = await _personRepository.FindPersonGroupByIdAsync(group.Id);

                    if (existingGroup != null)
                    {
                        person.PersonGroup.Add(existingGroup);
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"Grupo de pessoa com o id {group.Id} não existe";
                        return response;
                    }
                }

                bool updateResult = await _personRepository.UpdatePersonAsync(person);
                if (!updateResult)
                {
                    throw new Exception("A atualização da pessoa falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a pessoa: {ex.Message}";
                return response;
            }

            return response;
        }
    }
}
