using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Data
{
    public class ForbesPerson
    {
        public Guid Id { get; set; }

        public string Uri { get; set; } = null!;
        public int Rank { get; set; }
        public string? ListUri { get; set; }
        public bool ImageExists { get; set; }
        public double FinalWorth { get; set; }
        public string PersonName { get; set; } = null!;
        public string? Source { get; set; }
        public string? Industries { get; set; }
        public string? CountryOfCitizenship { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? LastName { get; set; }
        public double EstWorthPrev { get; set; }
        public string? SquareImage { get; set; }
    }
}
