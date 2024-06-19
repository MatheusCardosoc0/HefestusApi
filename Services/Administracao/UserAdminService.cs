using HefestusApi.Models.Administracao;
using HefestusApi.Repositories.Administracao.Interfaces;
using HefestusApi.Services.Administracao.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Administracao
{
    public class UserAdminService : IUserAdminService
    {
        private readonly IUserAdminRepository _userAdminRepository;
        private readonly UserAdminTokenService _userAdminTokenService;

        public UserAdminService(IUserAdminRepository userAdminRepository, UserAdminTokenService userAdminTokenService)
        {
            _userAdminRepository = userAdminRepository;
            _userAdminTokenService = userAdminTokenService;
        }

        private string GenerateRandomPassword(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<ServiceResponse<Dictionary<string, string>>> AuthUserAdminAsync(string password)
        {
            var response = new ServiceResponse<Dictionary<string, string>>();
            try
            {
                var userAdmin = await _userAdminRepository.GetUserAdmin();

                if (userAdmin == null)
                {
                    response.Success = false;
                    response.Message = "Nenhum usuário admin encontrado";
                    return response;
                }

                if (userAdmin.LastPassword == password)
                {
                    response.Success = false;
                    response.Message = "Essa senha não é mais válida";
                    return response;
                }

                bool validPassword = BCrypt.Net.BCrypt.Verify(password, userAdmin.Password);

                if (!validPassword)
                {
                    response.Success = false;
                    response.Message = "Senha incorreta.";
                    return response;
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                userAdmin.LastPassword = password;
                userAdmin.Password = hashedPassword;

                var updateResult = await _userAdminRepository.UpdateUserAdminAsync(userAdmin);

                if (!updateResult)
                {
                    response.Success = false;
                    response.Message = "Erro ao atualizar a senha.";
                    return response;
                }

                var token = _userAdminTokenService.GenerateToken(userAdmin);

                response.Success = true;
                response.Data = new Dictionary<string, string> { { "token", token } };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<string>> CreateUserAdminAsync()
        {
            var response = new ServiceResponse<string>();
            try
            {
                var checkExistingUserAdmin = await _userAdminRepository.GetUserAdmin();

                if (checkExistingUserAdmin != null)
                {
                    response.Success = false;
                    response.Message = "Já existe um usuário admin";
                    return response;
                }

                var password = GenerateRandomPassword();

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var newUserAdmin = new UserAdmin
                {
                    Password = hashedPassword,
                    LastPassword = "123"
                };

                var result = await _userAdminRepository.AddUserAdminAsync(newUserAdmin);
                if (result)
                {
                    response.Success = true;
                    response.Data = password;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Erro ao criar usuário admin";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> NewPasswordUserAdminAsync()
        {
            var response = new ServiceResponse<string>();
            try
            {
                var userAdmin = await _userAdminRepository.GetUserAdmin();

                if (userAdmin == null)
                {
                    response.Success = false;
                    response.Message = "Nenhum usuário admin encontrado";
                    return response;
                }

                var password = GenerateRandomPassword();

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                userAdmin.LastPassword = userAdmin.Password;
                userAdmin.Password = hashedPassword;

                var result = await _userAdminRepository.UpdateUserAdminAsync(userAdmin);
                if (result)
                {
                    response.Success = true;
                    response.Data = password;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Erro ao atualizar a senha do usuário admin";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
