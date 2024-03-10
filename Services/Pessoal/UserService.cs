using AutoMapper;
using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Interfaces;
using HefestusApi.Repositories.Pessoal.Interfaces;
using HefestusApi.Services.functions;
using HefestusApi.Services.Pessoal.Interfaces;

namespace HefestusApi.Services.Pessoal
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var response = new ServiceResponse<IEnumerable<UserDto>>();
            try
            {
                var users = await _userRepository.GetAllUsersAsync();

                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

                response.Data = userDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as usuários: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<UserDto>> GetUserByIdAsync(int id)
        {
            var response = new ServiceResponse<UserDto>();
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Usuário não encontrada.";
                    return response;
                }

                var userDtos = _mapper.Map<UserDto>(user);

                response.Data = userDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a usuário: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchUserByNameAsync(string searchTerm, string detailLevel)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var users = await _userRepository.SearchUserByNameAsync(searchTerm.ToLower());

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = users.Select(c => new UserSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = _mapper.Map<IEnumerable<UserDto>>(users).Cast<object>().ToList();
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
                response.Message = "Ocorreu um erro ao processar a busca: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<User>> CreateUserAsync(UserRequestDataDto request)
        {
            var response = new ServiceResponse<User>();
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var existingPerson = await _userRepository.GetPersonAsync(request.PersonId);

                var exisitingUser = await _userRepository.GetUserByNameAsync(request.Name);

                if(exisitingUser != null)
                {
                    response.Success = false;
                    response.Message = $"Já existe o usuário {request.Name} com esse nome!";
                    return response;
                }

                if(existingPerson == null)
                {
                    response.Success = false;
                    response.Message = $"Pessoa com o id {request.PersonId} não encontrada!";
                    return response;
                }

                if (existingPerson.User != null)
                {
                    response.Success = false;
                    response.Message = $"Pessoa com o id {request.PersonId} já tem um usuário!";
                    return response;
                }

                var user = new User
                {
                    Name = request.Name,
                    Password = hashedPassword,
                    PersonId = request.PersonId,
                    Person = existingPerson,
                };

                await _userRepository.AddUserAsync(user);

                response.Data = user;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a usuário: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateUserAsync(int id, UserRequestDataDto request)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var userToUpdate = await _userRepository.GetUserByIdAsync(id);

                if (userToUpdate == null)
                {
                    response.Success = false;
                    response.Message = $"Usuário com o ID {id} não encontrado.";
                    return response;
                }

               
                if(userToUpdate.Name != request.Name)
                {
                    response.Success = false;
                    response.Message = $"O nome do usuário não pode ser alterado";
                    return response;
                }

                var existingPerson = await _userRepository.GetPersonAsync(request.PersonId);
                if (existingPerson == null)
                {
                    response.Success = false;
                    response.Message = $"Pessoa com o ID {request.PersonId} não encontrada.";
                    return response;
                }

                if (existingPerson.User != null && existingPerson.User.Id != id)
                {
                    response.Success = false;
                    response.Message = $"A pessoa com ID {request.PersonId} já está vinculada a outro usuário.";
                    return response;
                }

                userToUpdate.Name = request.Name;
                userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                userToUpdate.PersonId = request.PersonId;
                userToUpdate.Person = existingPerson;

                bool updateResult = await _userRepository.UpdateUserAsync(userToUpdate);
                if (!updateResult)
                {
                    throw new Exception("A atualização do usuário falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a usuário: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = $"Usuário com o ID {id} não existe.";
                    return response;
                }

                bool deleted = await _userRepository.DeleteUserAsync(user);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a usuário devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a usuário: " + ex.Message;
            }

            return response;
        }
    }
}
