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
                    var rawCities = JsonConvert.DeserializeObject<List<CityData>>(responseBody);

                    var cities = rawCities.Select(city => new
                    {
                        Id = city.Id,
                        IbgeNumber = city.Id,
                        Name = city.Name,
                        State = city.microrregiao.mesorregiao.UF.sigla
                    }).ToList();

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

    public class UF
    {
        public string sigla { get; set; }
    }

    public class Mesorregiao
    {
        public UF UF { get; set; }
    }

    public class Microrregiao
    {
        public Mesorregiao mesorregiao { get; set; }
    }

    public class CityData
    {
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }
        public Microrregiao microrregiao { get; set; }
    }

}
