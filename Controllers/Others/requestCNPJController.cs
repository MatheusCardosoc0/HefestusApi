using HefestusApi.Models.Pessoal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HefestusApi.Controllers.Others
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class requestCNPJController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public requestCNPJController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("fetchData/{id}")]
        public async Task<IActionResult> FetchData(string id)
        {
            string cnpj = id;
            string apiUrl = $"https://api.cnpja.com/office/{cnpj}";

            string authToken = _configuration["MY_TOKEN_AUTHORIZATION"];

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", authToken);
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Company.EmpresaInfo empresaInfo = JsonConvert.DeserializeObject<Company.EmpresaInfo>(responseBody);

                    var resultado = new
                    {
                        nome = empresaInfo.alias ?? null,
                        razao = empresaInfo.company.name ?? null,
                        ibge = empresaInfo.address.municipality ?? null,
                        cep = empresaInfo.address.zip ?? null,
                        endereco = empresaInfo.address.details ?? null,
                        bairro = empresaInfo.address.district ?? null,
                        ddd = empresaInfo.phones[0].area ?? null,
                        telcom = empresaInfo.phones[0].number ?? null,
                        email = empresaInfo.emails[0].address ?? null
                    };

                    return Ok(resultado);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"{e.Message}");
                    return BadRequest($"Erro: {e.Message}");
                }
            }
        }
    }
}
