using cep.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace cep.Service
{
    public static class CepService 
    {
        private const string BaseUrl = "https://viacep.com.br/ws/";

        public static async Task<Endereco> ObterPorCep(string cep)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}{cep}/json").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    return JsonConvert.DeserializeObject<Endereco>(await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
            }

            return null;
        }

        public static async Task<List<Endereco>> ObterPorEnderecoAsync(string estado, string cidade, string rua)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}{estado}/{cidade}/{rua}/json").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<Endereco>>(await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }
    }
}
