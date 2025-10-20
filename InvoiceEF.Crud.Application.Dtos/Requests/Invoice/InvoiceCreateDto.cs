using InvoiceEF.Crud.Application.Dtos.Requests.InvoiceLine;

namespace InvoiceEF.Crud.Application.Dtos.Requests.Invoice
{
    public record InvoiceCreateDto(
        Guid clientId,
        Guid companyId,
        DateTime invoiceDate,
        decimal estimate,
        string signature,
        List<InvoiceLineCreateDto> lines
    );
}
