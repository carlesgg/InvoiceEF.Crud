using System.Text.Json.Serialization;

namespace InvoiceEF.Crud.Infrastructure.Proxies.Dtos
{
    public class ForbesPersonDto
    {
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("listUri")]
        public string? ListUri { get; set; }

        [JsonPropertyName("imageExists")]
        public bool? ImageExists { get; set; }

        [JsonPropertyName("finalWorth")]
        public double? FinalWorth { get; set; }

        [JsonPropertyName("personName")]
        public string? PersonName { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("industries")]
        public List<string>? Industries { get; set; }

        [JsonPropertyName("countryOfCitizenship")]
        public string? CountryOfCitizenship { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("birthDate")]
        public long? BirthDate { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("estWorthPrev")]
        public double EstWorthPrev { get; set; }

        [JsonPropertyName("squareImage")]
        public string? SquareImage { get; set; }
    }
}
