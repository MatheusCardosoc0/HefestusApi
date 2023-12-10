using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HefestusApi.Controllers.Others
{
    [Route("api/[controller]")]
    [ApiController]
    public class fetchCityDataIBGEController : ControllerBase
    {
        [HttpGet("fetchData/{id}")]
        public async Task<IActionResult> FetchData(string id)
        {
            string state = id;
            string apiUrl = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{state}/municipios";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest("Não foi possível obter dados da API do IBGE");
                    }

                    string responseBody = await response.Content.ReadAsStringAsync();
                    var cities = JsonConvert.DeserializeObject<List<CityData>>(responseBody);

                    return Ok(cities);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest($"Erro: {e.Message}");
                }
            }
        }
    }

    public class CityData
    {
        public string Id { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }
    }

}
