using InvoiceEF.Crud.Application.Dtos.Requests.Client;
using InvoiceEF.Crud.Application.Dtos.Responses.Client;
using InvoiceEF.Crud.Domain.Entities;


namespace InvoiceEF.Crud.Application.Mappers.Contracts
{
    public interface IClientApplicationMapper : IRequestMapper<ClientEntity, ClientCreateDto, ClientUpdateDto>, IResponseMapper<ClientEntity, ClientResponseDto>
    {
    }
}
