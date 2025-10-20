using InvoiceEF.Crud.CrossCutting;
using InvoiceEF.Crud.Infrastructure.Proxies.Contracts;
using InvoiceEF.Crud.Infrastructure.Proxies.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Proxies.Implementations
{
    public class ForbesProxy : IForbesProxy
    {
        private readonly HttpClient _httpClient;

        public ForbesProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ForbesPersonDto>> GetListAsync(CancellationToken cancellationToken)
        {
            using var response = await _httpClient.GetAsync("list", cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            // 🔹 Debug: mostrar el JSON recibido
            Console.WriteLine("JSON recibido de Forbes API:");
            Console.WriteLine(json);

            IEnumerable<ForbesPersonDto>? list = null;
            try
            {
                // 🔹 Debug: intentar deserializar
                list = json.Deserialize<IEnumerable<ForbesPersonDto>>();
                Console.WriteLine($"Se deserializaron {list?.Count() ?? 0} elementos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deserializando Forbes API:");
                Console.WriteLine(ex);
            }

            return list ?? new List<ForbesPersonDto>();
        }
    }
}
